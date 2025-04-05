namespace Ordering.Domain.ValueObjects;

public record OrderName
{
    private const int DefaultLength = 5;
    
    public string Value { get; }
    
    private OrderName(string value) => Value = value;

    public static OrderName Of(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        if (value.Length < 5)
            throw new DomainException($"Order name could not be less than {DefaultLength}");

        return new OrderName(value);
    }
};