namespace FabioMuniz.CookieAuth.Blazor.Interfaces;

public interface ICookieAuthService
{
	Task SignInAsync(string username, string password);
}
