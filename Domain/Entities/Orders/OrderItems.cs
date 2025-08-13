namespace WebApi.Domain.Entities.Orders
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ShoeId { get; set; } 
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
