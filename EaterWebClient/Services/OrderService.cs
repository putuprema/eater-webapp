namespace EaterWebClient.Services
{
    public class OrderService : IOrderService
    {
        public IDictionary<string, OrderItem> Items { get; } = new Dictionary<string, OrderItem>();
        public Order? ActiveOrder { get; private set; }
        public Action? OnOrderItemsChanged { get; set; }

        private readonly AppViewModel _appViewModel;
        private readonly IEaterApiClient _eaterApi;

        public OrderService(AppViewModel appViewModel, IEaterApiClient eaterApi)
        {
            _appViewModel = appViewModel;
            _eaterApi = eaterApi;
        }

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

        public async Task<Order?> PlaceOrderAsync()
        {
            var table = _appViewModel.CurrentTable;

            if (table == null)
                return null;

            var orderRequest = new PlaceOrderRequest(table, Items.Values.ToList());
            ActiveOrder = await _eaterApi.PlaceOrderAsync(orderRequest);

            return ActiveOrder;
        }

        public void ClearCart()
        {
            Items.Clear();
        }

        public async Task<Order?> GetOrderAsync(string id)
        {
            return await _eaterApi.GetOrderAsync(id);
        }
    }
}
