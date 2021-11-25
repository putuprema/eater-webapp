namespace EaterWebClient.Interfaces
{
    public interface IUserService
    {
        Account? CurrentUser { get; }

        Task<Account> AuthenticateAsync(AuthTokenRequest request);
        Task RegisterAsync(RegisterRequest request);
        Task<Account?> GetIdentityAsync();
    }
}
