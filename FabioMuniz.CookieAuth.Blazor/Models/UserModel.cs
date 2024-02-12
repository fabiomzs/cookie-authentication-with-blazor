namespace FabioMuniz.CookieAuth.Blazor.Models;

public class UserModel
{
	public string? Name { get; set; }
	public string? Login { get; set; }
	public string? Password { get; set; }
    public string? Email { get; set; }
    public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();

    public UserModel(string name, string login, string email, IEnumerable<string> roles)
	{
		Name = name;
		Login = login;
		Email = email;
		Roles = roles;
	}
}
