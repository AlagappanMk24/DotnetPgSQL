using System.Text.Json.Serialization;

namespace DotnetPgSQL.Domain.Models.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }

        // Foreign key for Product
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        // Foreign key for Order
        public int OrderId { get; set; }

        [JsonIgnore]
        public Order Order { get; set; }
    }
}
