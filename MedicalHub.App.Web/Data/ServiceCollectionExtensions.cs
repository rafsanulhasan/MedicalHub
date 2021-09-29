using MedicalHub.App.Web.Data.Entities;
using MedicalHub.App.Web.Data.Repositories;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;

namespace MedicalHub.App.Web.Data
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddDataLayer(
			this IServiceCollection services,
			string connectionString,
			string migrationAssembly,
			IHostEnvironment hostEnvironment
		)
		{
			services.AddDbContext<MedicalHubContext>(dbContextOpt =>
			{
				dbContextOpt
					.UseSqlServer(
						connectionString,
						sqlDbContextOpt =>
						{
							sqlDbContextOpt.MigrationsAssembly(migrationAssembly);
							sqlDbContextOpt.EnableRetryOnFailure(3);
						}
					);
				var isDevelopment = hostEnvironment.IsDevelopment();
				dbContextOpt.EnableDetailedErrors(isDevelopment);
				dbContextOpt.EnableSensitiveDataLogging(isDevelopment);
			});

			services
				.AddIdentity<User, Role>()
				.AddEntityFrameworkStores<MedicalHubContext>()
				.AddDefaultUI();

			services.AddScoped<IRepository<Appointment, Guid>, AppointmentRepository>();

			return services;
		}
	}
}
