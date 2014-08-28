using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace CoursesAPI.Services.DataAccess
{
	/// <summary>
	/// Interface for Entity Framework DataContext
	/// </summary>
	public interface IDbContext
	{
		DbSet<T> Set<T>() where T : class;
		DbEntityEntry<T> Entry<T>(T entity) where T : class;
		int SaveChanges();
		void Dispose();
	}
}
