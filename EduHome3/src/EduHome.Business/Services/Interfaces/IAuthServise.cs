using EduHome.Business.DTOs.Auth;

namespace EduHome.Business.Services.Interfaces;

public interface IAuthServise
{
	Task RegisterAsync(RegisterDto registerDto);
}
