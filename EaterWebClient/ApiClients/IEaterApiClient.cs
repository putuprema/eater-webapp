namespace EaterWebClient.ApiClients
{
    public interface IEaterApiClient
    {
        [Get("/v1/table/{id}")]
        Task<Table> GetTableByIdAsync(string id);

        [Get("/v1/products/featured")]
        Task<List<FeaturedProducts>> GetFeaturedProductsAsync();

        [Post("/v1/auth/token")]
        Task<AuthResult> GetAuthTokenAsync(AuthTokenRequest request);

        [Post("/v1/customer")]
        Task RegisterUserAsync(RegisterRequest request);

        [Get("/v1/me")]
        Task<Account> GetIdentityAsync();

        [Post("/v1/order")]
        Task<Order> PlaceOrderAsync(PlaceOrderRequest request);

        [Get("/v1/order/{id}")]
        Task<Order> GetOrderAsync(string id);
    }
}
