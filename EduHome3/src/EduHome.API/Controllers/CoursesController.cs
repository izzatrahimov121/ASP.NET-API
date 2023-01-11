using EduHome.Business.DTOs.Courses;
using EduHome.Business.Exceptions;
using EduHome.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EduHome.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CoursesController : ControllerBase
	{
		private readonly ICourseServise _courseServise;

		public CoursesController(ICourseServise courseServise)
		{
			_courseServise = courseServise;
		}

		[HttpGet("")]
		public async Task<IActionResult> Get()
		{
			try
			{
				var courses = await _courseServise.FindAllAsync();
				return Ok(courses);
			}
			catch (NotFaundException ex)
			{
				return NotFound(ex.Message); 
			}
		}

		[HttpPost("")]
		public async Task<IActionResult> Post(CoursePostDto course)
		{
			try
			{
				await _courseServise.CreateAsync(course);
				return StatusCode((int)HttpStatusCode.Created);
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}

		}
	}
}
