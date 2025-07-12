namespace Ordering.Domain.Models;

public class Order : Aggregate<Guid>
{
    private readonly List<OrderItem> _orderItems;
    
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public Guid CustomerId { get; private set; } = Guid.Empty;
    
    public string OrderName { get; private set; } = string.Empty;
    
    public Address ShippingAddress { get; private set; } = new();
    
    public Address BillingAddress { get; private set; } = new();
    
    public Payment Payment { get; private set; } = new();

    public OrderStatus Status { get; private set; } = OrderStatus.Pending;

    public decimal TotalPrice
    {
        get => _orderItems.Sum(item => item.Price * item.Quantity);
        private set { }
    }
}