using System.Text.Json.Serialization;

namespace EaterWebClient.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentStatus
    {
        UNPAID,
        PAID,
        REJECTED,
        EXPIRED
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderStatus
    {
        VALIDATING,
        PENDING_PAYMENT,
        QUEUED,
        PREPARING,
        READY,
        SERVED,
        CANCELED
    }

    public class OrderItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
        public string? Notes { get; set; }

        public OrderItem(Product product, int quantity, string? notes)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Quantity = quantity;
            Notes = notes;
            ImageUrl = product.ImageUrl;
        }

        public OrderItem()
        {
        }
    }

    public record Payment
    (
        PaymentStatus Status,
        string Token,
        string RedirectUrl,
        DateTime CreatedOn,
        DateTime UpdatedOn
    );

    public record Order
    (
        bool IsNew,
        string Id,
        int Amount,
        List<OrderItem> Items,
        OrderStatus Status,
        Payment Payment,
        string CancellationReason,
        DateTime CreatedOn,
        DateTime UpdatedOn,
        DateTime? ServedOn
    );
}
