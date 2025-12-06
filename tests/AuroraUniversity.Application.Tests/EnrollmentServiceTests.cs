using AuroraUniversity.Application.Services;
using AuroraUniversity.Domain.Entities;
using AuroraUniversity.Domain.Interfaces;
using Moq;

public class EnrollmentServiceTests
{
    [Fact]
    public async Task BatchEnrollAndAllocate_ShouldEnrollAndAllocateStudentsCorrectly()
    {
        // داده‌ها
        var moduleId = Guid.NewGuid();
        var module = new TermModule
        {
            Id = moduleId,
            Code = "CS101",
            Capacity = 2, // محدودیت ماژول
            Sessions = new List<Session>
            {
                new Session { Id = Guid.NewGuid(), StartTime = new DateTime(2025,10,23,9,0,0), EndTime = new DateTime(2025,10,23,10,0,0) },
                new Session { Id = Guid.NewGuid(), StartTime = new DateTime(2025,10,23,10,0,0), EndTime = new DateTime(2025,10,23,11,0,0) }
            },
            EnrolledStudents = new List<Student>()
        };

        var student1 = new Student { Id = Guid.NewGuid(), FirstName = "Alice", LastName = "Zed", EnrolledModules = new List<TermModule>(), RegisteredSessions = new List<Session>() };
        var student2 = new Student { Id = Guid.NewGuid(), FirstName = "Bob", LastName = "Young", EnrolledModules = new List<TermModule>(), RegisteredSessions = new List<Session>() };
        var student3 = new Student
        {
            Id = Guid.NewGuid(),
            FirstName = "Charlie",
            LastName = "Xen",
            EnrolledModules = new List<TermModule>(),
            RegisteredSessions = new List<Session>
            {
                // ایجاد تداخل زمانی با اولین جلسه
                new Session { StartTime = new DateTime(2025,10,23,9,30,0), EndTime = new DateTime(2025,10,23,10,30,0) }
            }
        };

        var students = new List<Student> { student1, student2, student3 };

        // Mock کردن repository ماژول
        var moduleRepoMock = new Mock<ITermModuleRepository>();
        moduleRepoMock.Setup(r => r.GetByIdAsync(moduleId)).ReturnsAsync(module);
        moduleRepoMock
            .Setup(r => r.UpdateAsync(It.IsAny<TermModule>()))
            .ReturnsAsync((TermModule m) => m);

        var studentRepoMock = new Mock<IStudentRepository>();
        studentRepoMock.Setup(r => r.GetForEnrollmentCheckAsync(It.IsAny<IEnumerable<Guid>>()))
            .ReturnsAsync(students);

        studentRepoMock.Setup(r => r.HasTimeConflict(student1, module)).Returns(false);
        studentRepoMock.Setup(r => r.HasTimeConflict(student2, module)).Returns(false);
        studentRepoMock.Setup(r => r.HasTimeConflict(student3, module)).Returns(true);

        var service = new EnrollmentService(studentRepoMock.Object, moduleRepoMock.Object);

        var result = await service.BatchEnrollAndAllocateAsync(moduleId, students.Select(s => s.Id).ToList());

        Assert.Contains(result.SuccessfullyEnrolledStudents, s => s.Id == student1.Id);
        Assert.Contains(result.SuccessfullyEnrolledStudents, s => s.Id == student2.Id);
        Assert.DoesNotContain(result.SuccessfullyEnrolledStudents, s => s.Id == student3.Id);
        Assert.True(result.FailedEnrollmentStudents.ContainsKey(student3));

        var allAssignedStudents = result.GroupAssignments.SelectMany(kv => kv.Value).ToList();
        Assert.Contains(allAssignedStudents, s => s.Id == student1.Id);
        Assert.Contains(allAssignedStudents, s => s.Id == student2.Id);
        Assert.DoesNotContain(allAssignedStudents, s => s.Id == student3.Id);

        //Assert.Contains(result.UnplacedStudents, s => s.Id == student3.Id);
    }
}
