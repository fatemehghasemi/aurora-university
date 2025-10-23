using AuroraUniversity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuroraUniversity.Infrastructure.Configurations;

public class MarkConfiguration : IEntityTypeConfiguration<Mark>
{
    public void Configure(EntityTypeBuilder<Mark> builder)
    {
        builder.Property(m => m.Score)
            .IsRequired();

        builder.Property(m => m.DateRecorded)
            .IsRequired();

        builder.HasOne(m => m.Student)
            .WithMany(s => s.Marks)
            .HasForeignKey(m => m.StudentId);

        builder.HasOne(m => m.Assessment)
            .WithMany(a => a.Marks)
            .HasForeignKey(m => m.AssessmentId);
    }
}
