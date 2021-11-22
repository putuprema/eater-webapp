namespace EaterWebClient.ApiClients
{
    public interface IEaterApiClient
    {
        [Get("/v1/table/{id}")]
        Task<Table> GetTableByIdAsync(string id);

        [Get("/v1/products/featured")]
        Task<List<FeaturedProducts>> GetFeaturedProductsAsync();
    }
}
