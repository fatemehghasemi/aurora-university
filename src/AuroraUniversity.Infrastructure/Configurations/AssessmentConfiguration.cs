using AuroraUniversity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuroraUniversity.Infrastructure.Configurations;

public class AssessmentConfiguration : IEntityTypeConfiguration<Assessment>
{
    public void Configure(EntityTypeBuilder<Assessment> builder)
    {
        builder.Property(a => a.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Date)
            .IsRequired();

        builder.Property(a => a.Weight)
            .IsRequired();

        builder.HasMany(a => a.Marks)
            .WithOne(m => m.Assessment)
            .HasForeignKey(m => m.AssessmentId);
    }
}
