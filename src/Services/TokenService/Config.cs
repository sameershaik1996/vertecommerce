using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VerteCommerce.Services.TokenService
{
	public class Config
	{

		public static Dictionary<string, string> GetUrls(IConfiguration configuration)
		{
			Dictionary<string, string> urls = new Dictionary<string, string>();

			urls.Add("Mvc", configuration.GetValue<string>("mvcClient"));

			urls.Add("Cart", configuration.GetValue<string>("cartswagger"));

			return urls;

		}

		public static IEnumerable<ApiResource> GetApiResources()
		{
			return new List<ApiResource>
			{
				new ApiResource("basket","shopping cart api"),
			new ApiResource("orders", "order service api"), 
			};
		}

		public static IEnumerable<IdentityResource> GetIdentityResources()
		{
			return new List<IdentityResource>()
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
               // new IdentityResources.Email()
            };
		}

		public static IEnumerable<Client> GetClients(Dictionary<string, string> clientUrls)
		{

			return new List<Client>()
				{
					new Client
					{
						ClientId = "mvc",
						ClientSecrets = new [] { new Secret("vertecommerce".Sha256())},
						AllowedGrantTypes = GrantTypes.Hybrid,

						 RedirectUris = {$"{clientUrls["Mvc"]}/signin-oidc"},
						PostLogoutRedirectUris = {$"{clientUrls["Mvc"]}/signout-callback-oidc"},
						AllowAccessTokensViaBrowser = false,
						AllowOfflineAccess = true,
						RequireConsent = false,
						AlwaysIncludeUserClaimsInIdToken = true,
						AllowedScopes = new List<string>
						{

							IdentityServerConstants.StandardScopes.OpenId,
							IdentityServerConstants.StandardScopes.Profile,
							IdentityServerConstants.StandardScopes.OfflineAccess,
						  //  IdentityServerConstants.StandardScopes.Email,
							 "orders",
							"basket",

						}

				},
					new Client
					{
						ClientId = "cartSwagger",
						ClientSecrets = new [] { new Secret("vertecommerce_cart".Sha256())},
						AllowedGrantTypes = GrantTypes.Implicit,

						RedirectUris = {$"{clientUrls["Cart"]}/swagger/o2c.html" },
						PostLogoutRedirectUris = {$"{clientUrls["Cart"]}/swagger"},
						AllowAccessTokensViaBrowser = true,
						AllowOfflineAccess = true,
						
						AlwaysIncludeUserClaimsInIdToken = true,
						AllowedScopes = new List<string>
						{

							
                            
							"basket",

						}

					}
				};
		}





	}
}
