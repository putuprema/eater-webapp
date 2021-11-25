using System.Text.Json.Serialization;

namespace EaterWebClient.Models
{
    public class OrderItem
    {
        [JsonIgnore]
        public Product Product { get; set; }
        public string Id { get; set; }
        public int Quantity { get; set; }
        public string? Notes { get; set; }

        public OrderItem(Product product, int quantity, string? notes)
        {
            Product = product;
            Id = product.Id;
            Quantity = quantity;
            Notes = notes;
        }
    }
}
