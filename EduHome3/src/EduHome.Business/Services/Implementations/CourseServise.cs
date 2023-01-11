using AutoMapper;
using EduHome.Business.DTOs.Courses;
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

	public Task<List<Course>> FindByCondition(Expression<Func<Course, bool>> expression, bool IsTracking = true)
	{
		throw new NotImplementedException();
	}

	public Task<Course?> FindById(int id)
	{
		throw new NotImplementedException();
	}

	public async Task CreateAsync(CoursePostDto course)
	{
		if (course is null) throw new ArgumentNullException();
		var resultCourse = _mapper.Map<Course>(course);
		await _courseRepository.CreateAsync(resultCourse);
		await _courseRepository.SaveAsync();
	}

	public void Delete(Course entity)
	{
		throw new NotImplementedException();
	}



	public void Update(Course entity)
	{
		throw new NotImplementedException();
	}
}
