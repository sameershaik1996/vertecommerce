using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Data
{
	public partial class EfRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
	{
		#region Fields

		private readonly IDbContext _context;

		private DbSet<TEntity> _entities;

		#endregion

		#region Ctor

		public EfRepository(IDbContext context)
		{
			this._context = context;
		}

		#endregion
		#region properties

		public virtual IQueryable<TEntity> Table => Entities;

		protected virtual DbSet<TEntity> Entities
		{
			get
			{
				if (_entities == null)
					_entities = _context.Set<TEntity>();

				return _entities;
			}
		}
		#endregion


		protected string GetFullErrorTextAndRollbackEntityChanges(DbUpdateException exception)
		{
			//rollback entity changes
			if (_context is DbContext dbContext)
			{
				var entries = dbContext.ChangeTracker.Entries()
					.Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

				entries.ForEach(entry => entry.State = EntityState.Unchanged);
			}

			_context.SaveChanges();
			return exception.ToString();
		}

		#region Methods



		/// <summary>
		/// Get entity by identifier
		/// </summary>
		/// <param name="id">Identifier</param>
		/// <returns>Entity</returns>
		public virtual TEntity GetById(object id)
		{
			return Entities.Find(id);
		}


		public virtual void Insert(TEntity entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			try
			{
				Entities.Add(entity);
				_context.SaveChanges();
			}
			catch (DbUpdateException exception)
			{
				//ensure that the detailed error text is saved in the Log
				throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
			}
		}

		public virtual void Update(TEntity entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			try
			{
				Entities.Update(entity);
				_context.SaveChanges();
			}
			catch (DbUpdateException exception)
			{
				//ensure that the detailed error text is saved in the Log
				throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
			}
		}

		/// <summary>
		/// Delete entity
		/// </summary>
		/// <param name="entity">Entity</param>
		public virtual void Delete(TEntity entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			try
			{
				Entities.Remove(entity);
				_context.SaveChanges();
			}
			catch (DbUpdateException exception)
			{
				//ensure that the detailed error text is saved in the Log
				throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
			}
		}







		#endregion

	}
}
