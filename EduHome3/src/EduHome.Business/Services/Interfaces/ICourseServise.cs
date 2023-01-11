using EduHome.Business.DTOs.Courses;
using EduHome.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.Services.Interfaces
{
	public interface ICourseServise
	{
		Task<List<CourseDto>> FindAllAsync();
		Task<List<Course>> FindByCondition(Expression<Func<Course, bool>> expression, bool IsTracking = true);
		Task<Course?> FindById(int id);
		Task CreateAsync(CoursePostDto course);
		void Update(Course entity);
		void Delete(Course entity);
	}
}
