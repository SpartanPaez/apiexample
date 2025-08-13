namespace WebApi.Domain.Entities.Orders;

public class Order
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
}