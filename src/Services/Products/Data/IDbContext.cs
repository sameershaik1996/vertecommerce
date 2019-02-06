using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Data
{
	public  interface IDbContext
	{
		DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

		int SaveChanges();
		void Dispose();
	}
}
