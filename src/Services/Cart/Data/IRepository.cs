using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VerteCommerce.Services.Cart.Data
{
	public interface IRepository<TEntity> where TEntity : BaseEntity
	{

		TEntity GetById(Object id);

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




	}
}
