using ExTrace.Shared.Services;

namespace ExTrace.Web.Client.Services
{
    public class AccessTokenProviderAdapter : IAccessTokenProvider
    {
        private readonly ITokenStorage _tokenStorage;

        public AccessTokenProviderAdapter(ITokenStorage tokenStorage)
        {
            _tokenStorage = tokenStorage;
        }

        public async Task<string?> GetTokenAsync()
        {
            return await _tokenStorage.GetTokenAsync();
        }
    }
}


