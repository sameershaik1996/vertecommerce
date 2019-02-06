using Microsoft.EntityFrameworkCore;
using Products.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Products.Core.Domain;
namespace Products.Data
{
	public class VerteObjectContext : DbContext,IDbContext
	{
		
		public VerteObjectContext(DbContextOptions<VerteObjectContext> options) : base(options)
		{
		
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			new BrandMap(modelBuilder.Entity<Brand>());
			new CategoryMap(modelBuilder.Entity<Category>());
			new ProductMap(modelBuilder.Entity<Product>());
		}

		public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
		{
			return base.Set<TEntity>();
		}
		
		public DbSet<Product> Products { get; set; }
		public DbSet<Brand> Brands { get; set; }
		public DbSet<Category> Categorys { get; set; }


	}
}
