using FabioMuniz.CookieAuth.Application.Commands.Authenticate;
using FabioMuniz.CookieAuth.Application.Repositories;
using FabioMuniz.CookieAuth.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<AuthenticateRepository>();
builder.Services.AddScoped<AuthenticateService>();

builder.Services.AddScoped<IRequestHandler<AuthenticateRequest, AuthenticateResponse>, AuthenticateHandler>();
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("api/v1/authenticate", async ([FromBody] AuthenticateRequest request, [FromServices] IMediator mediator) =>
{	
	var result = await mediator.Send(request);

	return Results.Ok(result);

})
.WithName("auth")
.WithOpenApi();

app.Run();