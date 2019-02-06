using Products.DataStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Products.Core.Domain;
using Products.Model.Catalog;
namespace Products.Services
{
	public interface ICatalogService
	{
		ApiList<Product> GetAllProducts(ProductParameterModel parameter);

		Product GetProductById(int id);

		void InsertProduct(Product product);

		void UpdateProduct(int id, Product product);

		void DeleteProduct(Product product);

		int GetCount(ProductParameterModel parameterModel);

	}
}
