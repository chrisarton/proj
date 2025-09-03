using ExTrace.Shared.Services;
using System.Net.Http.Json;

namespace ExTrace.Web.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly ITokenStorage _tokenStorage;

        public AuthService(HttpClient http, ITokenStorage tokenStorage)
        {
            _http = http;
            _tokenStorage = tokenStorage;
        }

        public async Task<bool> RegisterAsync(string email, string password)
        {
            var response = await _http.PostAsJsonAsync("api/auth/register", new { email, password });
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", new { email, password });
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
            if (result is null || string.IsNullOrWhiteSpace(result.Token))
            {
                return false;
            }
            await _tokenStorage.SetTokenAsync(result.Token);
            return true;
        }

        public async Task LogoutAsync()
        {
            await _tokenStorage.ClearTokenAsync();
        }

        private class AuthResponse
        {
            public string Token { get; set; } = string.Empty;
        }
    }
}


