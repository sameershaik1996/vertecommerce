using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Products.Data;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace Products.Controllers
{
	[Route("api/catalog")]
	public class CategoryController : Controller
	{
		private readonly VerteObjectContext _verteObjectContext;
		private readonly IOptionsSnapshot<ProductSettings> _productSettings;
		public CategoryController(
									VerteObjectContext verteObjectContext,
									IOptionsSnapshot<ProductSettings> productSettings)
		{
			_verteObjectContext = verteObjectContext;
			_productSettings = productSettings;
		}

		[HttpGet]
		[Route("categories/")]
		public async Task<IActionResult> GetCatergoryById(int id)
		{
			var products = await _verteObjectContext.Categorys.ToListAsync();

			return Json(products);
		}
	}
}
