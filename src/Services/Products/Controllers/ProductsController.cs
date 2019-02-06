using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Products.Data;
using Products.Model;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Products.Core.Domain;
using Products.DataStructure;
using Products.Constants;
using Products.Model.Catalog;
using Products.Services;
using Products.Model.ApiResponse;
namespace Products.Controllers
{
	[Route("api/catalog")]
	[Produces("application/json")]
	public class ProductsController: Controller
	{

		private readonly VerteObjectContext _verteObjectContext;
		private readonly IOptionsSnapshot<ProductSettings> _productSettings;
		private readonly ICatalogService _catalogService;
		public ProductsController(
									VerteObjectContext verteObjectContext,
									IOptionsSnapshot<ProductSettings> productSettings,									
									ICatalogService catalogService)				 						
		{
			_verteObjectContext = verteObjectContext;
			_productSettings = productSettings;
			_catalogService = catalogService;
		}


		[HttpGet]
		[Route("products")]
		public IActionResult GetAllProduct(ProductParameterModel parameter)
		{
			var products = _catalogService.GetAllProducts(parameter);
			products = ChangePictureUrl(products);
			var count = _catalogService.GetCount(parameter);
			var result = new ApiResponse<Product>(parameter.Page, parameter.Limit, count, products);
			return Json(result);
		}


		[HttpGet]
		[Route("products/{id}")]
		public IActionResult GetProductById(int id)
		{
			if (id < 0)
				return null;

			var product = _catalogService.GetProductById(id);
			if(product!=null)
			{
				product.PictureUrl = product.PictureUrl.Replace("http://externalcatalogbaseurltobereplaced",_productSettings.Value.ExternalProductServiceUrl);
			}
			return Json(product);

		}


		[HttpPost]
		[Route("products")]
		public IActionResult CreateProduct([FromBody] Product product)
		{

			_catalogService.InsertProduct(product);
			
			return Ok("Created");

		}

		[HttpPut]
		[Route("products/{id}")]
		public IActionResult UpdateProduct(int id,[FromBody] Product product)
		{
			var currentProduct = _catalogService.GetProductById(id);

			if (currentProduct == null)
				return NotFound();
			_catalogService.UpdateProduct(id,product);

			return Ok("Updated");

		}


		[HttpDelete]
		[Route("products/{id}")]
		public IActionResult DeleteProduct(int id)
		{
			var product = _catalogService.GetProductById(id);
			if (id <0)
				return NotFound();

			_catalogService.DeleteProduct(product);

			return Ok("Deleted");

		}

		[HttpGet]
		[Route("count")]
		public IActionResult GetCount(ProductParameterModel parameterModel)
		{
			var products = _catalogService.GetCount(parameterModel);
			return Json(products);
		}




		private ApiList<Product> ChangePictureUrl(ApiList<Product> products)
		{
			products.ForEach(x =>
				   x.PictureUrl = x.PictureUrl.Replace("http://externalcatalogbaseurltobereplaced", _productSettings.Value.ExternalProductServiceUrl)
			   );
			return products;
		}
	}
}
