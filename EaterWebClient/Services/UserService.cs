namespace EaterWebClient.Services
{
    public class UserService : IUserService
    {
        private readonly IEaterApiClient _eaterApi;
        private readonly ILocalStorageService _localStorage;

        public UserService(IEaterApiClient eaterApi, ILocalStorageService localStorage)
        {
            _eaterApi = eaterApi;
            _localStorage = localStorage;
        }

        public Account? CurrentUser { get; private set; }

        public async Task<Account> AuthenticateAsync(AuthTokenRequest request)
        {
            var authResult = await _eaterApi.GetAuthTokenAsync(request);

            await _localStorage.SetItemAsync(LocalStorageKeys.AccessToken, authResult.AccessToken);
            await _localStorage.SetItemAsync(LocalStorageKeys.RefreshToken, authResult.RefreshToken);

            return (await GetIdentityAsync())!;
        }

        public async Task<Account?> GetIdentityAsync()
        {
            if (CurrentUser == null)
            {
                try
                {
                    CurrentUser = await _eaterApi.GetIdentityAsync();
                }
                catch (EaterApiException ex)
                {
                    if (ex.StatusCode != System.Net.HttpStatusCode.Unauthorized)
                        throw;

                    return null;
                }
            }
            return CurrentUser;
        }

        public async Task RegisterAsync(RegisterRequest request)
        {
            await _eaterApi.RegisterUserAsync(request);
        }
    }
}
