using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VerteCommerce.Web.Store.Infrastructure;
using VerteCommerce.Web.Store.Models;
using RestSharp;
using VerteCommerce.Web.Store.Models.ApiResponse;

namespace VerteCommerce.Web.Store.Services
{
	public class CatalogService : ICatalogService
	{
		private readonly IOptionsSnapshot<AppSettings> _settings;
		private readonly IHttpClient _apiClient;
		private readonly ILogger<CatalogService> _logger;

		private readonly string _remoteServiceBaseUrl;
		public CatalogService(IOptionsSnapshot<AppSettings> settings, IHttpClient httpClient, ILogger<CatalogService> logger)
		{
			_settings = settings;
			_apiClient = httpClient;
			_logger = logger;

			_remoteServiceBaseUrl = $"{_settings.Value.CatalogUrl}/api/catalog/";
		}


		public IEnumerable<SelectListItem> GetBrands()
		{
			var getBrandsUri = ApiPaths.Catalog.GetAllBrands(_remoteServiceBaseUrl);

			var dataString =  _apiClient.GetResponse(getBrandsUri);

			var items = new List<SelectListItem>
			{
				new SelectListItem() { Value = null, Text = "All", Selected = true }
			};
			var brands = JArray.Parse(dataString.Content.ToString());

			foreach (var brand in brands.Children<JObject>())
			{
				items.Add(new SelectListItem()
				{
					Value = brand.Value<string>("id"),
					Text = brand.Value<string>("name")
				});
			}

			return items;
		}

		public ApiResponse<Product> GetProducts(int page, int take, int? brand, int? type)
		{
			var allcatalogItemsUri = ApiPaths.Catalog.GetAllCatalogItems(_remoteServiceBaseUrl+"products", page, take, brand, type);

			var dataString = _apiClient.GetResponse(allcatalogItemsUri);
			
			var catalog= JsonConvert.DeserializeObject<ApiResponse<Product>>(dataString.Content.ToString());
			

			return catalog;
		}

		public IEnumerable<SelectListItem> GetCategories()
		{
			var getTypesUri = ApiPaths.Catalog.GetAllTypes(_remoteServiceBaseUrl);

			var dataString =  _apiClient.GetResponse(getTypesUri);

			var items = new List<SelectListItem>
			{
				new SelectListItem() { Value = null, Text = "All", Selected = true }
			};
			var brands = JArray.Parse(dataString.Content.ToString());
			foreach (var brand in brands.Children<JObject>())
			{
				items.Add(new SelectListItem()
				{
					Value = brand.Value<string>("id"),
					Text = brand.Value<string>("name")
				});
			}
			return items;
		}
	}
}
