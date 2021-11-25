namespace EaterWebClient.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private readonly IProductService _productService;
        public List<FeaturedProducts> FeaturedProductsGroup { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public MenuViewModel(IProductService productService)
        {
            _productService = productService;
        }

        public async Task GetFeaturedProductsAsync()
        {
            Loading = true;
            try
            {
                FeaturedProductsGroup = await _productService.GetFeaturedProductsAsync();
            }
            catch (EaterApiException ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                Loading = false;
            }
        }
    }
}
