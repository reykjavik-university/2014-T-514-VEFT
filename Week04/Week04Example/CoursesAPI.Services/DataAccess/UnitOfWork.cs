using System;
using System.Collections.Generic;
using System.Linq;

namespace CoursesAPI.Services.DataAccess
{
	/// <summary>
	/// Class that implements Unit Of Work pattern
	/// Communicates with EF DataContext
	/// 
	/// Usage:
	/// 
	/// IUnitOfWork uow = new MockUnitOfWork<MockDataContext>();
	/// UnitOfWorkService service = new UnitOfWorkService(uow);
	/// var result = service.getAll();
	/// </summary>
	/// <typeparam name="TContext"></typeparam>
	public class UnitOfWork<TContext> : IUnitOfWork where TContext : IDbContext, new()
	{
		#region Member variables
		private readonly IDbContext               _ctx;
		private readonly Dictionary<Type, object> _repositories;
		private          bool                     _disposed;
		#endregion

		/// <summary>
		/// Constructor that initializes the context and Repository dictionary
		/// </summary>
		public UnitOfWork()
		{
			_ctx = new TContext();
			_repositories = new Dictionary<Type, object>();
			_disposed = false;
		}

		/// <summary>
		/// Retrieve a repository
		/// </summary>
		/// <typeparam name="TEntity">Model class, type of repository</typeparam>
		/// <returns>Repository for a specific Model class</returns>
		public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
		{
			// Checks if the Dictionary Key contains the Model class
			if (_repositories.Keys.Contains(typeof(TEntity)))
			{
				// Return the repository for that Model class
				return _repositories[typeof(TEntity)] as IRepository<TEntity>;
			}

			// If the repository for that Model class doesn't exist, create it
			var repository = new Repository<TEntity>(_ctx);

			// Add it to the dictionary
			_repositories.Add(typeof(TEntity), repository);

			return repository;
		}

		public void Save()
		{
			_ctx.SaveChanges();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Will handle disposing the context
		/// </summary>
		/// <param name="disposing"></param>
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (disposing)
				{
					_ctx.Dispose();
				}

				this._disposed = true;
			}
		}
	}
}
