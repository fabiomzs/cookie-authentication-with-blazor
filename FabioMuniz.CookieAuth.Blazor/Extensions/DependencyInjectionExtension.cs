using FabioMuniz.CookieAuth.Blazor.Interfaces;
using FabioMuniz.CookieAuth.Blazor.Services;

namespace FabioMuniz.CookieAuth.Blazor.Extensions;

public static class DependencyInjectionExtension
{
	public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
	{
		services.AddHttpClient(Configuration.AuthApi.HttpClientName, client => client.BaseAddress = new Uri(Configuration.AuthApi.UrlBase));

		services.AddScoped<ICookieAuthService, CookieAuthService>();

		return services;
	}
}
