namespace EaterWebClient.Services
{
    public class OrderService : IOrderService
    {
        public IDictionary<string, OrderItem> Items { get; } = new Dictionary<string, OrderItem>();
        public Action? OnOrderItemsChanged { get; set; }

        public void AddItemToCart(Product product, int quantity)
        {
            if (!Items.TryGetValue(product.Id, out OrderItem? orderItem))
            {
                orderItem = new OrderItem(product, quantity, null);
            }

            orderItem.Quantity = quantity;
            Items.TryAdd(product.Id, orderItem);
            NotifyItemsChanged();
        }

        public void RemoveItemFromCart(string productId)
        {
            Items.Remove(productId);
            NotifyItemsChanged();
        }

        public void NotifyItemsChanged()
        {
            OnOrderItemsChanged?.Invoke();
        }
    }
}
