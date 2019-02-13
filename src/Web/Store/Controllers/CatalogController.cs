using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Models;
using VerteCommerce.Web.Store.Models;
using VerteCommerce.Web.Store.Services;
using VerteCommerce.Web.Store.ViewModels;
namespace Store.Controllers
{
	public class CatalogController : Controller
	{
		private ICatalogService _catalogService;

		public CatalogController(ICatalogService catalogService)
		{
			_catalogService = catalogService;
		}

		public IActionResult Index(int? BrandFilterApplied, int? TypesFilterApplied, int? page)
		{
			int itemsPage = 5;
			page = page ?? 0;
			int pageId = (int)page + 1;
			var catalog =  _catalogService.GetProducts(pageId, itemsPage, BrandFilterApplied, TypesFilterApplied);


			
				var vm = new CatalogIndexViewModel()
				{
					CatalogItems = catalog.Data,
					Brands = _catalogService.GetBrands(),
					Types = _catalogService.GetCategories(),
					BrandFilterApplied = BrandFilterApplied ?? 0,
					TypesFilterApplied = TypesFilterApplied ?? 0,
					PaginationInfo = new PaginationInfo()
					{
						ActualPage = page ?? 0,
						ItemsPerPage = itemsPage, //catalog.Data.Count,
						TotalItems = catalog.Count,
						TotalPages = (int)Math.Ceiling(((decimal)catalog.Count / itemsPage))
					}
				};
			
			vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
			vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

			return View(vm);
		}

		[Authorize]
		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
