using Microsoft.AspNetCore.Identity;

namespace EduHome.Core.Entities.Identity;

public class AppUser : IdentityUser
{
	public string? Fullname { get; set; }
}
