using ExTrace.Services;
using ExTrace.Shared.Services;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Authorization;

namespace ExTrace
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add device-specific services used by the ExTrace.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();

            builder.Services.AddMauiBlazorWebView();

            // Auth + HttpClient
            builder.Services.AddAuthorizationCore();
            builder.Services.AddSingleton<ITokenStorage, TokenStorage>();
            builder.Services.AddTransient<BearerTokenHandler>();
            builder.Services.AddScoped<IAccessTokenProvider, AccessTokenProviderAdapter>();
            builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
            
            builder.Services.AddHttpClient<IAuthService, AuthService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7181/");
            });

            builder.Services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7181/");
            }).AddHttpMessageHandler<BearerTokenHandler>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
