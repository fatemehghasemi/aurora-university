using AuroraUniversity.Application.DTOs;
using FluentValidation;

namespace AuroraUniversity.Application.Validators
{
    public class MarkDtoValidator : AbstractValidator<MarkModel>
    {
        public MarkDtoValidator()
        {
            RuleFor(x => x.StudentId)
                .NotEmpty().WithMessage("StudentId is required.");

            RuleFor(x => x.AssessmentId)
                .NotEmpty().WithMessage("AssessmentId is required.");

            RuleFor(x => x.Score)
                .InclusiveBetween(0, 100)
                .WithMessage("Score must be between 0 and 100.");

            RuleFor(x => x.RecordedByStaffId)
                .NotEmpty().WithMessage("RecordedByStaffId is required.");

            RuleFor(x => x.Comments)
                .MaximumLength(500)
                .WithMessage("Comments cannot exceed 500 characters.");
        }
    }
}
