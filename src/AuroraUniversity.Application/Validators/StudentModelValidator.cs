using AuroraUniversity.Application.DTOs;
using FluentValidation;

namespace AuroraUniversity.Application.Validators
{
    public class StudentModelValidator : AbstractValidator<StudentModel>
    {
        public StudentModelValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(80).WithMessage("Last name cannot exceed 50 characters.");

        }
    }
}
