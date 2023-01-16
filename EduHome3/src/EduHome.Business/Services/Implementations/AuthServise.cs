using EduHome.Business.DTOs.Auth;
using EduHome.Business.Exceptions;
using EduHome.Business.Services.Interfaces;
using EduHome.Core.Entities.Identity;
using EduHome.Core.Utilities.Enums;
using Microsoft.AspNetCore.Identity;

namespace EduHome.Business.Services.Implementations;

public class AuthServise : IAuthServise
{
	private readonly UserManager<AppUser> _userManager;

	public AuthServise(UserManager<AppUser> userManager)
	{
		_userManager = userManager;
	}

	public async Task RegisterAsync(RegisterDto registerDto)
	{
		AppUser user = new()
		{
			Fullname = registerDto.Fullname,
			Email = registerDto.Email,
			UserName = registerDto.Username
		};
		var identityResult = await _userManager.CreateAsync(user);
		if (!identityResult.Succeeded)
		{
			string errors = String.Empty;
			int count = 0; 
			foreach (var error in identityResult.Errors)
			{
				errors += count != 0 ? $",{error.Description}" : $"{error.Description}";
				count++;
			}

			throw new UserCreatedFailException(errors);
		}


		var result = await _userManager.AddToRoleAsync(user, Roles.Member.ToString());
		if (!result.Succeeded)
		{
			string errors = String.Empty;
			int count = 0;
			foreach (var error in result.Errors)
			{
				errors += count != 0 ? $",{error.Description}" : $"{error.Description}";
				count++;
			}

			throw new RoleCreatedException(errors);
		}
	}
}
