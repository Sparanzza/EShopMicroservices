namespace Basket.API.Models;

public class ShoppingCart
{
    public string UserName { get; set; } = null!;
    public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    public decimal TotalPrice { get; set; }

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }

    public ShoppingCart()
    {
    }
}