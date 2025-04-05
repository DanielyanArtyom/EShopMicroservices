namespace Ordering.Domain.Models;

public class Product: Entity<ProductId>
{
    public string Name { get; private set; } = default!;
    public decimal Price { get; private set; }
    
    public static Product Create(ProductId id, string name, decimal price)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        var product = new Product
        {
            Name = name,
            Price = price,
            Id = id,
        };

        return product;
    }
}