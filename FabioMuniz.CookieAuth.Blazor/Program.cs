using FabioMuniz.CookieAuth.Blazor;
using FabioMuniz.CookieAuth.Blazor.Components;
using FabioMuniz.CookieAuth.Blazor.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

Configuration.AuthApi.UrlBase = builder.Configuration.GetSection("AppSettings").GetValue<string>("AuthApiBaseURL") ?? string.Empty;
Configuration.AuthApi.Endpoint = builder.Configuration.GetSection("AppSettings").GetValue<string>("AuthApiEndpoint") ?? string.Empty;
Configuration.AuthApi.HttpClientName = builder.Configuration.GetSection("AppSettings").GetValue<string>("AuthApiHttpClientName") ?? string.Empty;

Configuration.Security.TokenExpiration = builder.Configuration.GetSection("AppSettings").GetValue<int>("TokenExpiration");

builder.Services.AddHttpContextAccessor();
builder.Services.AddCookieAuthentication();
builder.Services.AddDependencyInjection();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
