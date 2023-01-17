using EduHome.Business.DTOs.Auth;
using EduHome.Business.Exceptions;
using EduHome.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EduHome.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
	private readonly IAuthServise _authServise;

	public AccountsController(IAuthServise authServise)
	{
		_authServise = authServise;
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> Register(RegisterDto registerDto)
	{
		try
		{
			await _authServise.RegisterAsync(registerDto);
			return Ok("User Successfully created"); 
		}
		catch (UserCreatedFailException ex)
		{
			return BadRequest(ex.Message);
		}
		catch(RoleCreatedException)
		{
			return StatusCode((int)HttpStatusCode.InternalServerError);
		}
		
	}



	[HttpPost("[action]")]
	public async Task<IActionResult> Login(LoginDto loginDto)
	{
		try
		{
			var tokenResponce = await _authServise.LoginAsync(loginDto);
			return Ok(tokenResponce);
		}
		catch (AuthFailException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (Exception) 
		{
			return StatusCode((int)HttpStatusCode.InternalServerError);
		}
	}
}
