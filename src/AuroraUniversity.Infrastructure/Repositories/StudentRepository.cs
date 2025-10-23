using AuroraUniversity.Domain.Entities;
using AuroraUniversity.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuroraUniversity.Infrastructure.Repositories;

public class StudentRepository(UniversityDbContext context)
    : Repository<Student>(context), IStudentRepository
{
    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        return await DbContext.Students
            .Include(s => s.EnrolledModules)
            .Include(s => s.Marks)
                .ThenInclude(m => m.Assessment)
            .Include(s => s.RegisteredSessions)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Student>> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        return await DbContext.Students
            .Include(s => s.EnrolledModules)
            .Include(s => s.Marks)
                .ThenInclude(m => m.Assessment)
            .Include(s => s.RegisteredSessions)
            .AsNoTracking()
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }

    public async Task<Student?> GetByIdAsync(Guid id)
    {
        return await DbContext.Students
            .Include(s => s.EnrolledModules)
            .Include(s => s.EnrolledModules)
            .Include(s => s.Marks)
                .ThenInclude(m => m.Assessment)
                .ThenInclude(m => m.Module)
            .Include(s => s.RegisteredSessions)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Student>> GetForEnrollmentCheckAsync(IEnumerable<Guid> studentIds)
    {
        return await DbContext.Students
          .Include(s => s.EnrolledModules)
              .ThenInclude(e => e.Sessions)
          .Include(s => s.RegisteredSessions)
          .Where(s => studentIds.Contains(s.Id))
          .ToListAsync();
    }
    public bool HasTimeConflict(Student student, TermModule module)
    {
        // بررسی تداخل با جلسات ماژول
        foreach (var moduleSession in module.Sessions)
        {
            foreach (var studentSession in student.RegisteredSessions)
            {
                if (moduleSession.StartTime < studentSession.EndTime &&
                    studentSession.StartTime < moduleSession.EndTime)
                {
                    return true;
                }
            }
        }

        // بررسی تداخل با جلسات ماژول‌هایی که دانشجو قبلاً ثبت‌نام کرده
        foreach (var enrolledModule in student.EnrolledModules)
        {
            foreach (var moduleSession in module.Sessions)
            {
                foreach (var enrolledSession in enrolledModule.Sessions)
                {
                    if (moduleSession.StartTime < enrolledSession.EndTime &&
                        enrolledSession.StartTime < moduleSession.EndTime)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

}
