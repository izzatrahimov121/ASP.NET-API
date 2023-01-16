using AutoMapper;
using EduHome.Business.DTOs.Courses;
using EduHome.Business.Exceptions;
using EduHome.Business.Services.Interfaces;
using EduHome.Core.Entities;
using EduHome.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EduHome.Business.Services.Implementations;

public class CourseServise : ICourseServise
{



	private readonly ICourseRepository _courseRepository;
	private readonly IMapper _mapper;

	public CourseServise(ICourseRepository courseRepository, IMapper mapper)
	{
		_courseRepository = courseRepository;
		_mapper = mapper;
	}





	public async Task<List<CourseDto>> FindAllAsync()
	{
		var courses = await _courseRepository.FindAll().ToListAsync();
		var resultCourses = _mapper.Map<List<CourseDto>>(courses);
		return resultCourses;
	}





	public async Task<List<CourseDto>> FindByConditionAsync(Expression<Func<Course, bool>> expression)
	{
		var courses = await _courseRepository.FindByCondition(expression).ToListAsync();
		var resultCourses = _mapper.Map<List<CourseDto>>(courses);
		return resultCourses;
	}






	public async Task<CourseDto?> FindByIdAsync(int id)
	{
		var course = await _courseRepository.FindByIdAsync(id);
		if (course == null)
		{
			throw new NotFoundException("not found");
		}

		var courseDTO = _mapper.Map<CourseDto>(course);
		return courseDTO;
	}






	public async Task CreateAsync(CoursePostDto course)
	{
		if (course is null) throw new ArgumentNullException();
		var resultCourse = _mapper.Map<Course>(course);
		await _courseRepository.CreateAsync(resultCourse);
		await _courseRepository.SaveAsync();
	}






	public async Task Delete(int id)
	{
		var baseCourse = await _courseRepository.FindByIdAsync(id);

		if (baseCourse == null)
		{
			throw new NotFoundException("Not Found.");
		}

		_courseRepository.Delete(baseCourse);
		await _courseRepository.SaveAsync();
	}





	public async Task UpdateAsync(int id,CourseUpdateDto course)
	{
		if (id != course.Id)
		{
			throw new BadRequestException("Enter valid ID.");
		}
		var baseCourse = _courseRepository.FindByCondition(c => c.Id == id);

		if (baseCourse == null)
		{
			throw new NotFoundException("Not Found.");
		}

		var updateCourse = _mapper.Map<Course>(course);
		_courseRepository.Update(updateCourse);
		await _courseRepository.SaveAsync();
	}




}
