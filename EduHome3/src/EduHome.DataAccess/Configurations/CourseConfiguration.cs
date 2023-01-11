using EduHome.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduHome.DataAccess.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
	public void Configure(EntityTypeBuilder<Course> builder)
	{
		builder.Property(x => x.Name).IsRequired(true).HasMaxLength(200);
		builder.Property(x => x.Description).IsRequired(false).HasMaxLength(600);
		builder.Property(x => x.Image).IsRequired(false).HasMaxLength(500);
		builder.Property(x => x.IsDeleted).IsRequired(false);
	}
}
