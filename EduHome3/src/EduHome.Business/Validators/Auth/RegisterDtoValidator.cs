using EduHome.Business.DTOs.Auth;
using FluentValidation;

namespace EduHome.Business.Validators.Auth;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
	public RegisterDtoValidator()
	{
		RuleFor(u => u.Fullname)
			.MinimumLength(5)
			.MaximumLength(256);
		RuleFor(u => u.Username)
			.MinimumLength(3)
			.MaximumLength(256)
			.NotEmpty()
			.NotNull();
		RuleFor(u => u.Email)
			.NotEmpty()
			.NotNull()
			.MaximumLength(256)
			.EmailAddress();
		RuleFor(u => u.Password)
			.NotNull()
			.NotEmpty()
			.MinimumLength(6)
			.MaximumLength(256);
	}
}
