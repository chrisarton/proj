using ExTrace.Shared.Services;
using ExTrace.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the ExTrace.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<ITokenStorage, TokenStorage>();
builder.Services.AddScoped<BearerTokenHandler>();
builder.Services.AddScoped<IAccessTokenProvider, AccessTokenProviderAdapter>();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();

builder.Services.AddHttpClient<IAuthService, AuthService>(client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
}).AddHttpMessageHandler<BearerTokenHandler>();

await builder.Build().RunAsync();
