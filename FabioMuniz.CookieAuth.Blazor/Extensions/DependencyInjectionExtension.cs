using FabioMuniz.CookieAuth.Blazor.Interfaces;
using FabioMuniz.CookieAuth.Blazor.Repositories;

namespace FabioMuniz.CookieAuth.Blazor.Extensions;

public static class DependencyInjectionExtension
{
	public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
	{
		services.AddScoped<ICookieAuthRepository, CookieAuthRepository>();

		return services;
	}
}
