﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using VerteCommerce.Web.Store.Infrastructure;
using VerteCommerce.Web.Store.Services;

namespace VerteCommerce.Web.Store
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var identityUrl = Configuration.GetValue<string>("IdentityUrl");
			var callBackUrl = Configuration.GetValue<string>("CallBackUrl");
			services.AddMvc();
			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
			services.Configure<AppSettings>(Configuration);
			services.AddSingleton<IHttpClient, CustomHttpClient>();
			services.AddTransient<ICatalogService, CatalogService>();
			services.AddAuthentication(options =>
			{
				options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
				// options.DefaultAuthenticateScheme = "Cookies";
			})
		   .AddCookie()
		   .AddOpenIdConnect(options => {
			   options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

			   options.Authority = identityUrl.ToString();
			   options.SignedOutRedirectUri = callBackUrl.ToString();
			   options.ClientId = "mvc";
			   options.ClientSecret = "vertecommerce";
			   options.ResponseType = "code id_token";
			   options.SaveTokens = true;
			   options.GetClaimsFromUserInfoEndpoint = true;
			   options.RequireHttpsMetadata = false;
			   options.Scope.Add("openid");
			   options.Scope.Add("profile");
			   options.Scope.Add("offline_access");
			   options.TokenValidationParameters = new TokenValidationParameters()
			   {

				   NameClaimType = "name",
				   RoleClaimType = "role"
			   };



		   });
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Catalog}/{action=Index}/{id?}");
			});
		}
	}
}
