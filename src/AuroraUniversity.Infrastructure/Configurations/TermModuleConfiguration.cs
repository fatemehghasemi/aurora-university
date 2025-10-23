using AuroraUniversity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuroraUniversity.Infrastructure.Configurations;

public class TermModuleConfiguration : IEntityTypeConfiguration<TermModule>
{
    public void Configure(EntityTypeBuilder<TermModule> builder)
    {
        builder.Property(m => m.Code)
            .IsRequired()
            .HasMaxLength(6);

        builder.Property(m => m.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(m => m.Staff)
            .WithMany(s => s.ManagedModules)
            .HasForeignKey(m => m.StaffId);

        builder.HasMany(m => m.Sessions)
            .WithOne(sess => sess.Module)
            .HasForeignKey(sess => sess.ModuleId);

        builder.HasMany(m => m.Assessments)
            .WithOne(a => a.Module)
            .HasForeignKey(a => a.ModuleId);
    }
}
