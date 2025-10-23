using AuroraUniversity.Domain.Entities;
using global::AuroraUniversity.Infrastructure.SeedData;
using Microsoft.EntityFrameworkCore;

namespace AuroraUniversity.Infrastructure;

public class UniversityDbContext : DbContext
{
    public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Staff> Staffs { get; set; } = null!;
    public DbSet<Term> Terms { get; set; } = null!;
    public DbSet<TermModule> Modules { get; set; } = null!;
    public DbSet<Session> Sessions { get; set; } = null!;
    public DbSet<Assessment> Assessments { get; set; } = null!;
    public DbSet<Mark> Marks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        Seed.SeedData(modelBuilder);
    }
}