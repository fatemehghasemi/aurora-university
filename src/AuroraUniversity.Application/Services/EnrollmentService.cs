using AuroraUniversity.Application.Interfaces;
using AuroraUniversity.Domain.Entities;
using AuroraUniversity.Domain.Interfaces;

namespace AuroraUniversity.Application.Services;

public class SeminarAllocationResult
{
    public Guid ModuleId { get; set; }

    public List<Student> SuccessfullyEnrolledStudents { get; set; } = [];
    public Dictionary<Student, string> FailedEnrollmentStudents { get; set; } = [];

    public Dictionary<Session, List<Student>> GroupAssignments { get; set; } = [];
    public List<Student> UnplacedStudents { get; set; } = [];
}

public class EnrollmentService : IEnrollmentService
{
    private readonly IStudentRepository _studentRepo;
    private readonly ITermModuleRepository _moduleRepo;

    public EnrollmentService(IStudentRepository studentRepo, ITermModuleRepository moduleRepo)
    {
        _studentRepo = studentRepo;
        _moduleRepo = moduleRepo;
    }

    public async Task<SeminarAllocationResult> BatchEnrollAndAllocateAsync(
        Guid moduleId,
        IEnumerable<Guid> studentIds)
    {
        var module = await _moduleRepo.GetByIdAsync(moduleId)
            ?? throw new Exception($"Module with ID {moduleId} not found.");

        var students = await _studentRepo.GetForEnrollmentCheckAsync(studentIds);

        var result = new SeminarAllocationResult { ModuleId = moduleId };

        int remainingCapacity = module.Capacity - module.EnrolledStudents.Count;

        foreach (var student in students.OrderBy(s => s.LastName).ThenBy(s => s.FirstName))
        {
            string failureReason = string.Empty;

            if (student.EnrolledModules.Any(m => m.Id == module.Id))
                failureReason = $"Student is already enrolled in Module {module.Code}.";
            else if (_studentRepo.HasTimeConflict(student, module))
                failureReason = $"Student has a time conflict with sessions in Module {module.Code}.";
            else if (remainingCapacity <= 0)
                failureReason = $"Module {module.Code} is at full capacity ({module.Capacity}).";

            if (string.IsNullOrEmpty(failureReason))
            {
                module.EnrolledStudents.Add(student);
                result.SuccessfullyEnrolledStudents.Add(student);
                remainingCapacity--;
            }
            else
            {
                result.FailedEnrollmentStudents.Add(student, failureReason);
            }
        }

        foreach (var session in module.Sessions)
            result.GroupAssignments[session] = new List<Student>();

        foreach (var student in result.SuccessfullyEnrolledStudents)
        {
            bool placed = false;

            foreach (var session in module.Sessions.OrderBy(s => s.StartTime))
            {
                bool hasConflict = student.RegisteredSessions.Any(rs =>
                    rs.StartTime < session.EndTime &&
                    session.StartTime < rs.EndTime);

                bool hasCapacity = result.GroupAssignments[session].Count < module.Capacity;

                if (!hasConflict && hasCapacity)
                {
                    result.GroupAssignments[session].Add(student);
                    student.RegisteredSessions.Add(session);
                    placed = true;
                    break;
                }
            }

            if (!placed)
                result.UnplacedStudents.Add(student);
        }

        if (result.SuccessfullyEnrolledStudents.Any())
            await _moduleRepo.UpdateAsync(module);

        return result;
    }
}
