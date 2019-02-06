using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Products.Constants;
namespace Products.Model
{
	public class BaseParameterModel
	{
		public BaseParameterModel()
		{
			Ids = null;
			Limit = Configuration.DefaultLimit;
			Page = Configuration.DefaultPageValue;
			
		}

		/// <summary>
		/// A comma-separated list of order ids
		/// </summary>
		[JsonProperty("ids")]
		public List<int> Ids { get; set; }

		/// <summary>
		/// Amount of results (default: 50) (maximum: 250)
		/// </summary>
		[JsonProperty("limit")]
		public int Limit { get; set; }

		/// <summary>
		/// Page to show (default: 1)
		/// </summary>
		[JsonProperty("page")]
		public int Page { get; set; }

		/// <summary>
		/// Restrict results to after the specified ID
		/// </summary>
		[JsonProperty("since_id")]
		public int SinceId { get; set; }
	}
}
