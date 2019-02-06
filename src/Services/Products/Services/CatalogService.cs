using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Products.Core.Domain;
using Products.Data;
using Products.DataStructure;
using Products.Model.Catalog;

namespace Products.Services
{
	public class CatalogService : ICatalogService
	{
		IRepository<Product> _productRepository;
		public CatalogService(IRepository<Product> repository)
		{
			_productRepository = repository;
		}


		public ApiList<Product> GetAllProducts(ProductParameterModel parameter)
		{
			var query = GetProductsQuery(parameter);



			var products = new ApiList<Product>(query, parameter.Page - 1, parameter.Limit);
			return products;
		}

		private IQueryable<Product> GetProductsQuery(ProductParameterModel parameter)
		{
			var query = _productRepository.Table;
			if (parameter.CategoryId != null)
				query = query.Where(p => p.CategoryId == parameter.CategoryId);
			if (parameter.BrandId != null)
				query = query.Where(p => p.BrandId == parameter.BrandId);
			if (parameter.Name != null)
				query = query.Where(p => p.Name.Contains(parameter.Name));
			return query;
		}

		public Product GetProductById(int id)
		{
			if (id <0)
				return null;
			var product = _productRepository.GetById(id);
			return product;

		}

		public void InsertProduct(Product product)
		{
			if (product != null)
				_productRepository.Insert(product);
		}

		public void UpdateProduct(int id, Product product)
		{
			if (product != null)
				_productRepository.Update(product);


		}

		public void DeleteProduct(Product product)
		{
			_productRepository.Delete(product);
		}

		

		public int GetCount(ProductParameterModel parameterModel)
		{
			var query = GetProductsQuery(parameterModel);
			return query.Count();
		}
	}
}
