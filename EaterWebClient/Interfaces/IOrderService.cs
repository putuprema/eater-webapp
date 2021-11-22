using System.ComponentModel;

namespace EaterWebClient.Interfaces
{
    public interface IOrderService : INotifyPropertyChanged
    {
        IDictionary<string, OrderItem> Items { get; }

        void AddItemToCart(string productId, int quantity);
        void RemoveItemFromCart(string productId);
    }
}
