namespace FabioMuniz.CookieAuth.Blazor;

public static class Configuration
{
	public static AuthApiConfiguration AuthApi { get; set; } = new();
	public static SecurityConfiguration Security {  get; set; } = new();
	public class AuthApiConfiguration
	{
		public string UrlBase { get; set; } = string.Empty;
		public string Endpoint { get; set; } = string.Empty;
		public string HttpClientName { get; set;} = string.Empty;
    }

	public class SecurityConfiguration
	{		
		public int TokenExpiration { get; set; }
	}
}

