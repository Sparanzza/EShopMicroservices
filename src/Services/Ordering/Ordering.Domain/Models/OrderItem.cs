using Ordering.Domain.Abstractions;

namespace Ordering.Domain.Models;

public class OrderItem : Entity<Guid>
{
    internal OrderItem(Guid orderId, Guid productId, decimal totalPrice)
    {
        OrderId = orderId;
        ProductId = productId;
        Price = totalPrice;
        ProductId = productId;
    }

    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}