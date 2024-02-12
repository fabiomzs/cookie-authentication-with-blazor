using Microsoft.AspNetCore.Authentication.Cookies;

namespace FabioMuniz.CookieAuth.Blazor.Extensions;

public static class AuthenticationExtension
{
	public static IServiceCollection AddCookieAuthentication(this IServiceCollection services)
	{
		services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie(options =>
			{
				options.LoginPath = "/signin";
				options.AccessDeniedPath = "/unauthorized";
				options.Cookie.Name = ".FabioMuniz.CookieAuth";
			});
		services.AddAuthorization();
		services.AddCascadingAuthenticationState();

		return services;
	}
}
