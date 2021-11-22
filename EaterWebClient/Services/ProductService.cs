namespace EaterWebClient.Services
{
    public class ProductService : IProductService
    {
        private readonly IEaterApiClient _eaterApi;

        public ProductService(IEaterApiClient eaterApi)
        {
            _eaterApi = eaterApi;
        }

        public Task<List<FeaturedProducts>> GetFeaturedProductsAsync()
        {
            return _eaterApi.GetFeaturedProductsAsync();
        }
    }
}
