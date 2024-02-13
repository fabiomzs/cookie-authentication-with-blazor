using FabioMuniz.CookieAuth.Blazor.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace FabioMuniz.CookieAuth.Blazor.Services;

public class CookieAuthService : ICookieAuthService
{
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly ICookieAuthRepository _repository;

	public CookieAuthService(IHttpContextAccessor httpContextAccessor, ICookieAuthRepository repository)
	{
		_httpContextAccessor = httpContextAccessor;
		_repository = repository;
	}

	public async Task SignInAsync(string username, string password)
	{
		var claims = new List<Claim>();
		var user = await _repository.GetByLoginAsync(username, password);

		if (user != null)
		{
			claims.Add(new Claim(ClaimTypes.Name, user.Name!));
			claims.Add(new Claim(ClaimTypes.GivenName, user.Name!));
			claims.Add(new Claim(ClaimTypes.Email, user.Email!));

			foreach (var claim in user.Roles)
				claims.Add(new Claim(ClaimTypes.Role, claim));

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

			var authProps = new AuthenticationProperties()
			{
				IsPersistent = true,
				ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2),				
			};

			await _httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProps);
		}
	}

	public async Task SignOutAsync() =>	await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
	
}
