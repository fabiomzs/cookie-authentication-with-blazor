using MediatR;

namespace FabioMuniz.CookieAuth.Application.Commands.Authenticate;

public record AuthenticateRequest(string Login, string Password) : IRequest<AuthenticateResponse>;
