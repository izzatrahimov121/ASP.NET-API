using EduHome.Business.DTOs.Auth;
using FluentValidation;

namespace EduHome.Business.Validators.Auth;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
	public LoginDtoValidator()
	{
		RuleFor(u => u.UsernameOrEmail)
				.MinimumLength(3)
				.MaximumLength(256)
				.NotEmpty()
				.NotNull();
		RuleFor(u => u.Password)
				.NotNull()
				.NotEmpty()
				.MinimumLength(6)
				.MaximumLength(256);
	}
}
