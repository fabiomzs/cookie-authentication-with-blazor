using FabioMuniz.CookieAuth.Application.Models;

namespace FabioMuniz.CookieAuth.Application.Repositories;

public class AuthenticateRepository
{
	public Task<UserModel?> GetByLoginAsync(string login, string password)
	{
		UserModel? user = null;

		if (login == "admin" && password == "admin")
		{
			string[] roles = { "admin" };

			user = new("Administrador do Sistema", login, "admin@cookieauth.com", roles);
		}

		if (login == "user" && password == "user")
		{
			string[] roles = { "user" };

			user = new("Usuário do Sistema", login, "user@cookieauth.com", roles);
		}

		return Task.FromResult(user);
	}
}
