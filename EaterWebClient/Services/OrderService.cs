using System.ComponentModel;

namespace EaterWebClient.Services
{
    public class OrderService : IOrderService
    {
        public IDictionary<string, OrderItem> Items { get; } = new Dictionary<string, OrderItem>();

        public event PropertyChangedEventHandler? PropertyChanged;

        public void AddItemToCart(string productId, int quantity)
        {
            if (!Items.TryGetValue(productId, out OrderItem? orderItem))
            {
                orderItem = new OrderItem(productId, quantity, null);
            }

            orderItem.Quantity = quantity;
            Items.TryAdd(productId, orderItem);

            NotifyItemsChanged();
        }

        public void RemoveItemFromCart(string productId)
        {
            Items.Remove(productId);
            NotifyItemsChanged();
        }

        public void NotifyItemsChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Items)));
        }
    }
}
