namespace EaterWebClient.Interfaces
{
    public interface IOrderService
    {
        IDictionary<string, OrderItem> Items { get; }
        Action? OnOrderItemsChanged { get; set; }

        void AddItemToCart(Product product, int quantity);
        void RemoveItemFromCart(string productId);
    }
}
