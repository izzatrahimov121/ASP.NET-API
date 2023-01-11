using EduHome.Core.Entities;
using EduHome.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EduHome.DataAccess.Contexts
{
	public class AppDbContexts : DbContext
	{
		public AppDbContexts(DbContextOptions<AppDbContexts> options) : base(options)
		{
		}

		DbSet<Course> Courses { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseConfiguration).Assembly);
			base.OnModelCreating(modelBuilder);
		}
	}
}
