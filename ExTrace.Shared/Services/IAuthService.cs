namespace ExTrace.Shared.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(string email, string password);
        Task<bool> LoginAsync(string email, string password);
        Task LogoutAsync();
    }
}
