namespace Basket.API.Models;

public class ShoppingCartItem
{
    public Guid ProductId { get; set; }
    
    public int Quantity { get; set; }
    
    public decimal Price { get; set; }
    
    public string Color { get; set; } = default!;
    
    public string ProductName { get; set; } = default!;
    
}