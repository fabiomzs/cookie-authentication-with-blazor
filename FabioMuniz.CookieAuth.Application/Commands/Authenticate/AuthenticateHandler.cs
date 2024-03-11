using FabioMuniz.CookieAuth.Application.Repositories;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FabioMuniz.CookieAuth.Application.Commands.Authenticate;

public class AuthenticateHandler : IRequestHandler<AuthenticateRequest, AuthenticateResponse>
{
    private readonly AuthenticateRepository _authenticateRepository;

    public AuthenticateHandler(AuthenticateRepository authenticateRepository)
    {       
        _authenticateRepository = authenticateRepository;
    }
    public async Task<AuthenticateResponse> Handle(AuthenticateRequest request, CancellationToken cancellationToken)
	{		
		var handler = new JwtSecurityTokenHandler();
		var key = Encoding.ASCII.GetBytes("534E4E35-DCFC-417D-AB73-389226BB30EB");
		var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);		

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = await GenarateClaimsAsync(request.Login, request.Password),
			Expires = DateTime.UtcNow.AddHours(1),
			SigningCredentials = credentials,
		};
		
		var token = handler.CreateToken(tokenDescriptor);

		return new AuthenticateResponse(handler.WriteToken(token));
	}

	private async Task<ClaimsIdentity> GenarateClaimsAsync(string login, string password)
	{
		var claims = new List<Claim>();
		var claimsIdentity = new ClaimsIdentity();
		var user = await _authenticateRepository.GetByLoginAsync(login, password);

		if (user != null)
		{
			claims.Add(new Claim(ClaimTypes.Name, user.Name!));
			claims.Add(new Claim(ClaimTypes.GivenName, user.Name!));
			claims.Add(new Claim(ClaimTypes.Email, user.Email!));			

			foreach (var claim in user.Roles)
				claims.Add(new Claim(ClaimTypes.Role, claim));
			
			claimsIdentity.AddClaims(claims);
		}

		return claimsIdentity;
	}
}
