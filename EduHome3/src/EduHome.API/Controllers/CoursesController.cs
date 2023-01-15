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
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			try
			{
				var course = _courseServise.FindByIdAsync(id);
				return Ok(course);
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (FormatException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}


		[HttpGet("searchByName/{Name}")]
		public async Task<IActionResult> GetByName(string name)
		{
			try
			{
				var result = await _courseServise.FindByConditionAsync(n => n.Name !=null ? n.Name.Contains(name) : true);
				return Ok(result);
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}

		#region Update
		[HttpPut("id")]
		public async Task<IActionResult> Put(int id,CourseUpdateDto course)
		{
			try
			{
				await _courseServise.UpdateAsync(id,course);
				return NoContent();
			}
			catch (BadRequestException ex)
			{
				return BadRequest(ex.Message);
			}
			catch(NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch(Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}
		#endregion

		#region Created

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
		#endregion

		#region Delete
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				await _courseServise.Delete(id);
				return Ok("Deleted");
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (FormatException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}
		#endregion



	}
}
