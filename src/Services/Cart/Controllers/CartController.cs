using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VerteCommerce.Services.Cart.Data;

namespace VerteCommerce.Services.Cart.Controllers
{
	using Core.Domain.Cart;
	using Microsoft.AspNetCore.Authorization;

	[Route("api/cart")]
	[Authorize]
	public class CartController : Controller
	{

		private readonly IRepository _repository;
		private readonly ILogger _logger;
		public CartController(IRepository repository,
								ILoggerFactory loggerFactory
								)
		{
			_repository = repository;
			_logger = loggerFactory.CreateLogger<CartController>();
		}

		[HttpGet]
		[Route("{id}")]
		public IActionResult GetCart(string id)
		{
			return Ok(_repository.GetCart(id));
		}

		[HttpPost]
		public IActionResult CreateCartItem([FromBody]Cart value)
		{
			var basket =  _repository.UpdateCart(value);

			return Ok(basket);
		}

		[HttpDelete]
		[Route("{id}")]
		public void Delete(string id)
		{
			_logger.LogInformation("Delete method in Cart controller reached");
			_repository.DeleteCart(id);


		}

	}
}
