using AuroraUniversity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuroraUniversity.Infrastructure.Configurations;

public class StaffConfiguration : IEntityTypeConfiguration<Staff>
{
    public void Configure(EntityTypeBuilder<Staff> builder)
    {
        builder.Property(s => s.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(s => s.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(s => s.ManagedModules)
            .WithOne(m => m.Staff)
            .HasForeignKey(m => m.StaffId);

        builder.HasMany(s => s.TaughtSessions)
            .WithOne(sess => sess.Staff)
            .HasForeignKey(sess => sess.StaffId);
    }
}
