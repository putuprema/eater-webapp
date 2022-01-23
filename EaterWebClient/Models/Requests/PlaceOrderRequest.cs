namespace EaterWebClient.Models.Requests
{
    public record PlaceOrderRequest(Table Table, List<OrderItem> Items);
}
