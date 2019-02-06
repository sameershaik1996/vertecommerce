using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Products.Model.Catalog
{
	public class ProductParameterModel : BaseParameterModel
	{

		[JsonProperty("categoryid")]
		public int? CategoryId { get; set; }

		[JsonProperty("brandid")]
		public int? BrandId { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }



	}
}
