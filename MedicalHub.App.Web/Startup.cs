
using MedicalHub.App.Web.Data;
using MedicalHub.App.Web.Domain;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MedicalHub.App.Web
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public IHostEnvironment HostEnvironment { get; }

		public Startup(
			IConfiguration configuration,
			IHostEnvironment hostEnvironment
		)
		{
			Configuration = configuration;
			HostEnvironment = hostEnvironment;
		}		

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRazorPages();
			services.AddServerSideBlazor();
			services.AddDataLayer(
				Configuration.GetConnectionString("DefaultConnection"),
				typeof(Startup).Assembly.FullName,
				HostEnvironment
			);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeed seed)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				seed.MigrateAsync().Wait();
				seed.SeedDataAsync().Wait();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapBlazorHub();
				endpoints.MapFallbackToPage("/_Host");
			});
		}
	}
}
