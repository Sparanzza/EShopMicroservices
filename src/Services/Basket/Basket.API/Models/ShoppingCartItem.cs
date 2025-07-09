namespace Basket.API.Models;

public class ShoppingCartItem
{
    public int Quantity { get; set; }
    public string Color { get; set; } = null!;
    public decimal Price { get; set; } = 0;
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
}