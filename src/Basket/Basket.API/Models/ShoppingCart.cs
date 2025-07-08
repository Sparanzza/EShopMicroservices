namespace Basket.API.Models;

public class ShoppingCart
{
    public string UserName { get; set; } = null!;
    public List<ShoppingCartItem> Items { get; set; } = [];
    public decimal TotalPrice { get; set; } = 0;

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }

    public ShoppingCart()
    {
    }
}