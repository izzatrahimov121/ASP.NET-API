using EduHome.Core.Entities.Identity;
using EduHome.Core.Utilities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EduHome.DataAccess.Contexts;

public class AppDbContextInitializer
{
	private readonly UserManager<AppUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly IConfiguration _configuration;
	private readonly AppDbContexts _contexts;

	public AppDbContextInitializer(UserManager<AppUser> userManager
		 , RoleManager<IdentityRole> roleManager
		 , IConfiguration configuration
		 , AppDbContexts contexts)
	{
		_userManager = userManager;
		_roleManager = roleManager;
		_configuration = configuration;
		_contexts = contexts;
	}


	public async Task InitializeAsync()
	{
		await _contexts.Database.MigrateAsync();
	}



	public async Task RoleSeedAsync()
	{
		foreach (var role in Enum.GetValues(typeof(Roles)))
		{
			if (!await _roleManager.RoleExistsAsync(role.ToString()))
			{
				await _roleManager.CreateAsync(new() { Name = role.ToString() });
			}
		}
	}


	public async Task UserSeedAsync()
	{
		AppUser admin = new AppUser
		{
			UserName = _configuration["AdminSettings:UserName"],
			Email = _configuration["AdminSettings:Email"]
		};

		await _userManager.CreateAsync(admin, _configuration["AdminSettings:Password"]);
		await _userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
		
	}
}
