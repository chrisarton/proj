using Microsoft.Maui.Storage;

namespace ExTrace.Services
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

        public async Task SetTokenAsync(string token)
        {
            await SecureStorage.Default.SetAsync(TokenKey, token);
        }

        public async Task<string?> GetTokenAsync()
        {
            try
            {
                return await SecureStorage.Default.GetAsync(TokenKey);
            }
            catch
            {
                return null;
            }
        }

        public Task ClearTokenAsync()
        {
            SecureStorage.Default.Remove(TokenKey);
            return Task.CompletedTask;
        }
    }
}


