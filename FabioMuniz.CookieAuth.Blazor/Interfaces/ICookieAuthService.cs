using FabioMuniz.CookieAuth.Blazor.Models;

namespace FabioMuniz.CookieAuth.Blazor.Interfaces;

public interface ICookieAuthService
{
	Task SignInAsync(SignInRequest signInRequest);
	Task SignOutAsync();
}
