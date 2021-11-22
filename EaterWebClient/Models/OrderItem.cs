namespace EaterWebClient.Models
{
    public class OrderItem
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public string? Notes { get; set; }

        public OrderItem(string id, int quantity, string? notes)
        {
            Id = id;
            Quantity = quantity;
            Notes = notes;
        }
    }
}
