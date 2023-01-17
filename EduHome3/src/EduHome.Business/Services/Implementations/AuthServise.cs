using EduHome.Business.DTOs.Auth;
using EduHome.Business.Exceptions;
using EduHome.Business.Services.Interfaces;
using EduHome.Core.Entities.Identity;
using EduHome.Core.Utilities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EduHome.Business.Services.Implementations;

public class AuthServise : IAuthServise
{
	private readonly UserManager<AppUser> _userManager;
	private readonly IConfiguration _configuration;

	public AuthServise(UserManager<AppUser> userManager, IConfiguration configuration)
	{
		_userManager = userManager;
		_configuration = configuration;
	}

	public async Task<TokenRespoceDto> LoginAsync(LoginDto loginDto)
	{
		var user = await _userManager.FindByNameAsync(loginDto.UsernameOrEmail);
		if (user == null)
		{
			user = await _userManager.FindByEmailAsync(loginDto.UsernameOrEmail);
			if (user == null) { throw new AuthFailException("User/Email or Password incorrect!"); }
		}
		var check = await _userManager.CheckPasswordAsync(user, loginDto.Password);
		if (!check) { throw new AuthFailException("User/Email or Password incorrect!"); }

		//Create Jwt

		List<Claim> claims = new()
		{
			new Claim(ClaimTypes.Name,user.UserName),
			new Claim(ClaimTypes.NameIdentifier,user.Id),
			new Claim(ClaimTypes.Email,user.Email)
		};
		//claims add user roles
		var roles = await _userManager.GetRolesAsync(user);
		foreach (var role in roles)
		{
			claims.Add(new Claim(ClaimTypes.Role, role));
		}

		SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));
		SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

		JwtSecurityToken jwtSecurityToken = new(
			issuer: _configuration["Jwt:Issuer"],
			audience: _configuration["Jwt:Audience"],
			claims: claims,
			notBefore: DateTime.UtcNow,
			expires: DateTime.UtcNow.AddSeconds(60),
			signingCredentials: signingCredentials
			) ;

		JwtSecurityTokenHandler tokenHandler = new();
		string token = tokenHandler.WriteToken(jwtSecurityToken);

		return new TokenRespoceDto()
		{
			Token = token,
			Expires = jwtSecurityToken.ValidTo,
			Username = user.UserName
		};
	}





	public async Task RegisterAsync(RegisterDto registerDto)
	{
		AppUser user = new()
		{
			Fullname = registerDto.Fullname,
			Email = registerDto.Email,
			UserName = registerDto.Username
		};
		var identityResult = await _userManager.CreateAsync(user, registerDto.Password);
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
