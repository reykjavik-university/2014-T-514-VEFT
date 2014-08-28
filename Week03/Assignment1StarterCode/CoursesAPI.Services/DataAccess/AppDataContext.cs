using System.Data.Entity;
using CoursesAPI.Services.Models.Entities;

namespace CoursesAPI.Services.DataAccess
{
	public class AppDataContext : DbContext, IDbContext
	{
		public DbSet<Student> Students { get; set; }
		public DbSet<Course>  Courses  { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			// modelBuilder.Configurations.Add(new CourseInstanceMap());
		}

		public AppDataContext()
		{
			//SERIALIZE WILL FAIL WITH PROXIED ENTITIES
			Configuration.ProxyCreationEnabled = false;

			//ENABLING COULD CAUSE ENDLESS LOOPS AND PERFORMANCE PROBLEMS
			Configuration.LazyLoadingEnabled = false;
		}
	}
}