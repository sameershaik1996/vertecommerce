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
using Products.Data;
using Microsoft.EntityFrameworkCore;

using Swashbuckle.AspNetCore.Swagger;
using Autofac;
using Products.Services;

namespace Products
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
			services.AddMvc();
			var builder = new ContainerBuilder();
			builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

			var databaseServer = Configuration["DatabaseServer"];
			var databaseName = Configuration["DatabaseName"];
			var databaseUser = Configuration["DatabaseUser"];
			var databasePassword = Configuration["DatabasePassword"];
			var connectionString = string.Format("Server={0};Database={1};User Id={2};Password={3};MultipleActiveResultSets=true", databaseServer, databaseName, databaseUser, databasePassword);
			//services.AddDbContext<VerteObjectContext>(dbContextOptions => dbContextOptions.UseSqlServer(Configuration["ConnectionString"]));
			services.AddDbContext<VerteObjectContext>(dbContextOptions => dbContextOptions.UseSqlServer(connectionString));
			services.AddScoped<IDbContext>(provider => provider.GetService<VerteObjectContext>());
			services.AddTransient<ICatalogService,CatalogService>();
			services.Configure<ProductSettings>(Configuration);
			services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "Catalog Api", Version = "v1" });
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
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Products API V1");
			});
		}
	}
}
