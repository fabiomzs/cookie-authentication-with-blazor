using Microsoft.AspNetCore.Authentication.Cookies;

namespace FabioMuniz.CookieAuth.Blazor.Extensions;

public static class AuthenticationExtension
{
	public static IServiceCollection AddCookieAuthentication(this IServiceCollection services)
	{
		services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie(options =>
			{
				options.Cookie.Name = ".FabioMuniz.CookieAuth";
				options.LoginPath = "/signin";
				options.AccessDeniedPath = "/unauthorized";
				options.ExpireTimeSpan = TimeSpan.FromHours(2);
				options.Cookie.SameSite = SameSiteMode.Strict;
			});
		services.AddAuthorization();
		services.AddCascadingAuthenticationState();

		return services;
	}
}
