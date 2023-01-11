using EduHome.Business.DTOs.Courses;
using FluentValidation;

namespace EduHome.Business.Validators.Courses;

public class CoursePostDtoValidator : AbstractValidator<CoursePostDto>
{
	public CoursePostDtoValidator()
	{
		RuleFor(c => c.Name)
			.NotEmpty().WithMessage("Boş buraxmayın")
			.NotNull().WithMessage("Boş buraxmayın")
			.MaximumLength(150).WithMessage("Uzunluq 150 simvolu keçdi");
		RuleFor(c => c.Description)
			.NotEmpty().WithMessage("Boş buraxmayın")
			.NotNull().WithMessage("Boş buraxmayın")
			.MaximumLength(600).WithMessage("Uzunluq 600 simvolu keçdi");
		RuleFor(c => c.Image)
			.NotEmpty().WithMessage("Boş buraxmayın")
			.NotNull().WithMessage("Boş buraxmayın")
			.MaximumLength(500).WithMessage("Uzunluq 500 simvolu keçdi");
	}
}
