namespace EaterWebClient.Interfaces
{
    public interface IProductService
    {
        Task<List<FeaturedProducts>> GetFeaturedProductsAsync();
    }
}
