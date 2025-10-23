using AuroraUniversity.Application.Services;

namespace AuroraUniversity.Application.Interfaces;

public interface IEnrollmentService
{
    Task<SeminarAllocationResult> BatchEnrollAndAllocateAsync(
            Guid moduleId,
            IEnumerable<Guid> studentIds);
}
