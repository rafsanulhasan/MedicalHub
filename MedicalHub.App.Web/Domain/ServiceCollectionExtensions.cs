using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalHub.App.Web.Domain
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddDomain(
			this IServiceCollection services
		)
		{
			services.AddScoped<ISeed, Seed>();
			return services;
		}
	}
}
