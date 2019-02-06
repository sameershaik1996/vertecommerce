using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Products.Data;
namespace Products.Controllers
{
	[Route("api/catalog")]
	[Produces("application/json")]
    public class BrandController : Controller
    {

		private readonly VerteObjectContext _verteObjectContext;
		private readonly IOptionsSnapshot<ProductSettings> _productSettings;
		public BrandController(
									VerteObjectContext verteObjectContext,
									IOptionsSnapshot<ProductSettings> productSettings)
		{
			_verteObjectContext = verteObjectContext;
			_productSettings = productSettings;
		}

		[HttpGet]
		[Route("brands/")]
		public async Task<IActionResult> GetBrandById(int id)
		{
			var products = await _verteObjectContext.Brands.ToListAsync();

			return Json(products);
		}
	}
}