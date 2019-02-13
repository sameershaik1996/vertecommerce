using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TokenService.Data;
using TokenService.Models;
using TokenService.Services;
using VerteCommerce.Services.TokenService;

namespace TokenService
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

			var databaseServer = Configuration["DatabaseServer"];
			var databaseName = Configuration["DatabaseName"];
			var databaseUser = Configuration["DatabaseUser"];
			var databasePassword = Configuration["DatabasePassword"];
			var connectionString = string.Format("Server={0};Database={1};User Id={2};Password={3};MultipleActiveResultSets=true", databaseServer, databaseName, databaseUser, databasePassword);
			
			services.AddDbContext<ApplicationDbContext>(dbContextOptions => dbContextOptions.UseSqlServer(connectionString));

			//services.AddDbContext<ApplicationDbContext>(options =>
				//options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			// Add application services.
			services.AddTransient<IEmailSender, EmailSender>();

			services.AddIdentityServer()
			   .AddDeveloperSigningCredential()
			   .AddInMemoryPersistedGrants()
			   .AddInMemoryIdentityResources(Config.GetIdentityResources())
			   .AddInMemoryApiResources(Config.GetApiResources())
			   .AddInMemoryClients(Config.GetClients(Config.GetUrls(Configuration)))
			   .AddAspNetIdentity<ApplicationUser>();

			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			app.UseIdentityServer();

			 

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
