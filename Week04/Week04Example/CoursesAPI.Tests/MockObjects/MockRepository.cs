using System;
using System.Collections.Generic;
using System.Linq;
using CoursesAPI.Services.DataAccess;

namespace CoursesAPI.Tests.MockObjects
{
	public class MockRepository<T> : IRepository<T> where T : class
	{
		#region Member variables
		private List<T> _context;
		#endregion

		public MockRepository(List<T> ctx)
		{
			_context = ctx;
		}

		public List<T> SetData(List<T> d)
		{
			var oldData = _context;
			_context = d;
			return oldData;
		}

		public virtual void Add(T entity)
		{
			_context.Add(entity);
		}

		public virtual void Delete(T entity)
		{
			_context.Remove(entity);
		}

		public virtual void Update(T entity)
		{
			var entry = _context.Where(s => s == entity).SingleOrDefault();
			entry = entity;
		}

		public virtual T GetById(long id)
		{
			throw new NotImplementedException();
		}

		public virtual IQueryable<T> All(string includeProperties = "")
		{
			// Note: we don't explicitly implement the include properties.
			// Test data should explicitly set connected properties:
			// var testObject = new SomeObject {
			//     SomeOtherObject = new SomeOtherObject(etc)
			// }

			return _context.AsQueryable();
		}
	}
}