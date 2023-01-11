using AutoMapper;
using EduHome.Business.DTOs.Courses;
using EduHome.Core.Entities;

namespace EduHome.Business.Mappers;

public class CourseMapper : Profile
{
	public CourseMapper()
	{
		CreateMap<Course, CourseDto>().ReverseMap();
		CreateMap<CoursePostDto, Course>().ReverseMap();
	}
}
