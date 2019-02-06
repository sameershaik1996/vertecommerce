using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VerteCommerce.Web.Store.Models
{
	public class Product
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public string PictureUrl { get; set; }
		public int BrandId { get; set; }
		//public string Brand { get; set; }
		public int CategoryId { get; set; }
		//public string Category { get; set; }
	}
}
