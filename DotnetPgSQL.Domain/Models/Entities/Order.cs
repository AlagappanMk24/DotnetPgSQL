namespace DotnetPgSQL.Domain.Models.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }

        // One-to-many relationship
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
