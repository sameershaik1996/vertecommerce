using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Products.Core.Domain;
namespace Products.Data
{
	public class CatalogSeed
	{
		public static async Task SeedAsync(VerteObjectContext context)
		{

			context.Database.Migrate();
			if (!context.Categorys.Any())
			{
				context.Categorys.AddRange(GetPreconfiguredCategory());
				await context.SaveChangesAsync();
			}

			if (!context.Brands.Any())
			{
				context.Brands.AddRange(GetPreconfiguredBrand());
				await context.SaveChangesAsync();
			}

			if (!context.Products.Any())
			{
				context.Products.AddRange(GetPreconfiguredProduct());
				await context.SaveChangesAsync();
			}
			
		}

		static IEnumerable<Brand> GetPreconfiguredBrand()
		{
			return new List<Brand>()
			{
				new Brand() { Name = "Apple"},
				new Brand() { Name = "Oneplus" },
				new Brand() { Name = "Samsung" }

			};
		}

		static IEnumerable<Category> GetPreconfiguredCategory()
		{
			return new List<Category>()
			{
				new Category() { Name = "Phones"},
				new Category() { Name = "SmartWatches" },
				new Category() { Name = "Accessories" },

			};
		}
		static IEnumerable<Product> GetPreconfiguredProduct()
		{
			return new List<Product>()
			{
				new Product() { CategoryId=1,BrandId=2, Description = "OnePlus 6T speed matters", Name = "OnePlus 6T", Price = 199.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/1" },
				new Product() { CategoryId=3,BrandId=2, Description = "OnePlus Official Charger", Name = "OnePlus Dash Charger", Price= 88.50M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/2" },
				new Product() { CategoryId=1,BrandId=1, Description = "Sell Your Kidney", Name = "Iphone XS", Price = 999, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/3" },
				new Product() { CategoryId=1,BrandId=1, Description = "Sell Both Of you Kidneys", Name = "Iphone XS MAX", Price = 12, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/4" },
				new Product() { CategoryId=1,BrandId=3, Description = "Display matters", Name = "Samsung S10", Price = 188.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/5" },
				new Product() { CategoryId=3,BrandId=3, Description = "power it up", Name = "Samsung PowerBank", Price = 112, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/6" },
				new Product() { CategoryId=2,BrandId=3, Description = "smartwatch for android", Name = "Samsung Gear", Price = 212, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/7"  },
				new Product() { CategoryId=2,BrandId=1, Description = "Sell Liver", Name = "IWatch", Price = 600, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/8"  },


			};
		}


	}
}
