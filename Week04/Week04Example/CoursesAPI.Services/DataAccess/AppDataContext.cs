using System.Data.Entity;
using CoursesAPI.Services.Models.Entities;

namespace CoursesAPI.Services.DataAccess
{
	public class AppDataContext : DbContext, IDbContext
	{
		public DbSet<Person>              Persons              { get; set; }
		public DbSet<Semester>            Semesters            { get; set; }
		public DbSet<CourseTemplate>      CourseTemplates      { get; set; }
		public DbSet<CourseInstance>      CourseInstances      { get; set; }
		public DbSet<TeacherRegistration> TeacherRegistrations { get; set; }

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