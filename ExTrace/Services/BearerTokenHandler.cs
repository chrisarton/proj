using System.Net.Http.Headers;

namespace ExTrace.Services
{
    public class BearerTokenHandler : DelegatingHandler
    {
        private readonly ITokenStorage _tokenStorage;

        public BearerTokenHandler(ITokenStorage tokenStorage)
        {
            _tokenStorage = tokenStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _tokenStorage.GetTokenAsync();
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}


