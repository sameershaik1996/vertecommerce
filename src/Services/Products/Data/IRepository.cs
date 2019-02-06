using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Data
{
	public interface IRepository<TEntity> where TEntity : BaseEntity
	{

		#region Methods

		/// <summary>
		/// Get entity by identifier
		/// </summary>
		/// <param name="id">Identifier</param>
		/// <returns>Entity</returns>
		TEntity GetById(object id);

		/// <summary>
		/// Insert entity
		/// </summary>
		/// <param name="entity">Entity</param>
		void Insert(TEntity entity);

	
		/// <summary>
		/// Update entity
		/// </summary>
		/// <param name="entity">Entity</param>
		void Update(TEntity entity);

		
		/// <summary>
		/// Delete entity
		/// </summary>
		/// <param name="entity">Entity</param>
		void Delete(TEntity entity);

		
		#endregion

		#region Properties

		/// <summary>
		/// Gets a table
		/// </summary>
		IQueryable<TEntity> Table { get; }

		#endregion

	}
}
