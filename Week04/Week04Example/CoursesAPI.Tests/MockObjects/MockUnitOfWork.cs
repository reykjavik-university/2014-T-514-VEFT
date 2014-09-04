using System;
using System.Collections.Generic;
using System.Linq;
using CoursesAPI.Services.DataAccess;

namespace CoursesAPI.Tests.MockObjects
{
	public class MockUnitOfWork<T> : IUnitOfWork where T : class, new()
	{
		private readonly T _ctx;
		private readonly Dictionary<Type, object> _repositories;

		/// <summary>
		/// It would be nice if we could set up a spy for the Save()
		/// method and then count how many times it has been called.
		/// This IS probably possible, but we can also do this manually...
		/// </summary>
		private int _saveCallCount = 0;

		public MockUnitOfWork()
		{
			_ctx = new T();
			_repositories = new Dictionary<Type, object>();
		}

		public int GetSaveCallCount()
		{
			return _saveCallCount;
		}

		public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
		{
			if (_repositories.Keys.Contains(typeof(TEntity)))
			{
				return _repositories[typeof(TEntity)] as IRepository<TEntity>;
			}

			var entityName = typeof(TEntity).Name;
			var prop = _ctx.GetType().GetProperty(entityName);
			MockRepository<TEntity> repository;
			if (prop != null)
			{
				var entityValue = prop.GetValue(_ctx, null);
				repository = new MockRepository<TEntity>(entityValue as List<TEntity>);
			}
			else
			{
				repository = new MockRepository<TEntity>(new List<TEntity>());
			}
			_repositories.Add(typeof(TEntity), repository);
			return repository;
		}

		public List<TEntity> SetRepositoryData<TEntity>(List<TEntity> data) where TEntity : class
		{
			var repo = GetRepository<TEntity>();

			var mockRepo = repo as MockRepository<TEntity>;
			if (mockRepo != null)
			{
				return mockRepo.SetData(data);
			}
			return null;
		}

		public void Save()
		{
			_saveCallCount++;
		}

		public void Dispose()
		{
		}
	}
}
