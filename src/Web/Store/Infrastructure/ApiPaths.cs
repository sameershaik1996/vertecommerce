using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VerteCommerce.Web.Store.Infrastructure
{
    public class ApiPaths
    {

        public static class Catalog
        {
            public static string GetAllCatalogItems(string baseUri, int page, int take, int? brand, int? type)
            {
				var filters = "";
				if (brand >= 0)
				{
					filters = $"&brandid={brand}";
				}
				if (type >= 0)
				{
					var br= $"&categoryid={type}";
					 filters=filters+br;
				}
                return $"{baseUri}?page={page}&limit={take}{filters}";
            }

            public static string GetCatalogItem(string baseUri, int id)
            {

                return $"{baseUri}/products/{id}";
            }
            public static string GetAllBrands(string baseUri)
            {
                return $"{baseUri}brands";
            }

            public static string GetAllTypes(string baseUri)
            {
                return $"{baseUri}categories";
            }
        }


    }
}
