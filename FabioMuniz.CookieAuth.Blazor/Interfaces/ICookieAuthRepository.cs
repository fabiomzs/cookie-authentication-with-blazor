using FabioMuniz.CookieAuth.Blazor.Models;

namespace FabioMuniz.CookieAuth.Blazor.Interfaces;

public interface ICookieAuthRepository
{
	Task<UserModel?> GetByLoginAsync(string login, string password);
}
