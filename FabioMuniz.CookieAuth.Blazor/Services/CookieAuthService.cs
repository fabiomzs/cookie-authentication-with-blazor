using FabioMuniz.CookieAuth.Blazor.Interfaces;
using FabioMuniz.CookieAuth.Blazor.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace FabioMuniz.CookieAuth.Blazor.Services;

public class CookieAuthService : ICookieAuthService
{
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IHttpClientFactory _httpClientFactory;

	public CookieAuthService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
	{
		_httpContextAccessor = httpContextAccessor;
		_httpClientFactory = httpClientFactory;
	}

	public async Task SignInAsync(SignInRequest signInRequest)
	{
		var claims = new List<Claim>();
		
		using HttpClient httpClient = _httpClientFactory.CreateClient(Configuration.AuthApi.HttpClientName);

		var content = new StringContent(JsonSerializer.Serialize(signInRequest), Encoding.UTF8, "application/json");

		var response = await httpClient.PostAsync(Configuration.AuthApi.Endpoint, content);

		var signInResponse = JsonSerializer.Deserialize<SignInResponse>(
			await response.Content.ReadAsStringAsync(), 
			new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true 
				
			});

		var jwt = GetSecurityToken(signInResponse.Jwt);

		claims.Add(new Claim("jwt", signInResponse.Jwt));
		claims.AddRange(jwt.Claims);

		var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
		var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

		var authProps = new AuthenticationProperties()
		{
			IsPersistent = true,
			ExpiresUtc = DateTimeOffset.UtcNow.AddHours(Configuration.Security.TokenExpiration),
		};

		await _httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProps);
		
	}

	public async Task SignOutAsync() =>	await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

	private JwtSecurityToken GetSecurityToken(string jwt)
	{
		return new JwtSecurityTokenHandler().ReadToken(jwt) as JwtSecurityToken;
	}
	
}
