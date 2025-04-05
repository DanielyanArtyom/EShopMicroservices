namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string? CardName { get; } = default!;
    public string CardNumber { get; } = default!;
    public string Expiration { get; } = default!;
    public string CVV { get; } = default!;
    public int PaymentMethod { get; } = default!;

    protected Payment() {}

    private Payment(string cardName, string cardNumber, string exipration, string cvv, int paymentMethod)
    {
        CardNumber = cardNumber;
        CardName = cardName;
        Expiration = exipration;
        CVV = cvv;
        PaymentMethod = paymentMethod;
    }
    
    public static Payment Of(string cardName, string cardNumber, string exipration, string cvv, int paymentMethod)
    {
        return new Payment(cardName, cardNumber, exipration, cvv, paymentMethod);
    }
}