using AuroraUniversity.Domain.Entities;
using AuroraUniversity.Domain.Interfaces;

namespace AuroraUniversity.Infrastructure.Repositories;

public class StaffRepository(UniversityDbContext context)
    : Repository<Staff>(context), IStaffRepository
{

}
