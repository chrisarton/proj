using Microsoft.JSInterop;

namespace ExTrace.Web.Client.Services
{
    public interface ITokenStorage
    {
        Task SetTokenAsync(string token);
        Task<string?> GetTokenAsync();
        Task ClearTokenAsync();
    }

    public class TokenStorage : ITokenStorage
    {
        private const string TokenKey = "authToken";
        private readonly IJSRuntime _js;

        public TokenStorage(IJSRuntime js)
        {
            _js = js;
        }

        public async Task SetTokenAsync(string token)
        {
            await _js.InvokeVoidAsync("localStorage.setItem", TokenKey, token);
        }

        public async Task<string?> GetTokenAsync()
        {
            return await _js.InvokeAsync<string?>("localStorage.getItem", TokenKey);
        }

        public async Task ClearTokenAsync()
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", TokenKey);
        }
    }
}


