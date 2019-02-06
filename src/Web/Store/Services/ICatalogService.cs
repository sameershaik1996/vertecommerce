using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VerteCommerce.Web.Store.Models;
using VerteCommerce.Web.Store.Models.ApiResponse;
namespace VerteCommerce.Web.Store.Services
{
	public interface ICatalogService
	{
		ApiResponse<Product> GetProducts(int page, int take, int? brand, int? type);
		IEnumerable<SelectListItem> GetBrands();
		IEnumerable<SelectListItem> GetCategories();


	}
}
