namespace EaterWebClient.Interfaces
{
    public interface IOrderService
    {
        IDictionary<string, OrderItem> Items { get; }
        Order? ActiveOrder { get; }
        Action? OnOrderItemsChanged { get; set; }

        void AddItemToCart(Product product, int quantity);
        void ClearCart();
        void RemoveItemFromCart(string productId);
        Task<Order?> PlaceOrderAsync();
        Task<Order?> GetOrderAsync(string id);
    }
}
