using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Microsoft.AspNetCore.Http;

using VerteCommerce.Services.Cart.Core.Domain.Cart;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using VerteCommerce.Services.Cart.Infrastructure.Filters;
using VerteCommerce.Services.Cart;
using VerteCommerce.Services.Cart.Data;

namespace VertCommerce.Services.Cart
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
			services.AddMvc(options=>
			{
				options.Filters.Add(typeof(HttpGlobalExceptionFilter));
			}).AddControllersAsServices();

			services.Configure<CartSetting>(Configuration);

			ConfigureAuthService(services);

			services.AddSingleton<ConnectionMultiplexer>(sp =>
			{
				var settings = sp.GetRequiredService<IOptions<CartSetting>>().Value;
				var configuration = ConfigurationOptions.Parse(settings.RedisUrl, true);

				configuration.ResolveDns = true;
				configuration.AbortOnConnectFail = false;

				return ConnectionMultiplexer.Connect(configuration);
			});

			services.AddTransient<IRepository, RedisCartRepository>();

			services.AddSwaggerGen(c =>
			{
				c.DescribeAllEnumsAsStrings();
				c.SwaggerDoc("v1", new Info { Title = "cart Api", Version = "v1" });
				c.AddSecurityDefinition("oauth2", new OAuth2Scheme
				{
					Type = "oauth2",
					Flow = "implicit",
					AuthorizationUrl = $"{Configuration.GetValue<string>("IdentityUrl")}/connect/authorize",
					TokenUrl = $"{Configuration.GetValue<string>("IdentityUrl")}/connect/token",
					Scopes = new Dictionary<string, string>()
					{
						{ "basket", "Basket Api" }
					}
				});

				c.OperationFilter<AuthorizeCheckOperationFilter>();

			});


		}


		private void ConfigureAuthService(IServiceCollection services)
		{
			// prevent from mapping "sub" claim to nameidentifier.
			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

			var identityUrl = Configuration.GetValue<string>("IdentityUrl");

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

			}).AddJwtBearer(options =>
			{
				options.Authority = identityUrl;
				options.RequireHttpsMetadata = false;
				options.Audience = "basket";

			});





		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMvc();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cart API V1");
				c.ConfigureOAuth2("basketswaggerui", "", "", "Basket Swagger UI");
			});

		}
	}
}
