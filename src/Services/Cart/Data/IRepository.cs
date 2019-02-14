using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VerteCommerce.Services.Cart.Data
{
	using Core.Domain.Cart;
	public interface IRepository 
	{

		Cart GetCart(string cartId);
		IEnumerable<string> GetUsers();
		Cart UpdateCart(Cart basket);
		bool DeleteCart(string id);



	}
}
