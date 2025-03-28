namespace Basket.API.Data;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasket(string userName, CancellationToken ct = default);
    Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken ct = default);
    Task<bool> DeleteBasket(string userName, CancellationToken ct = default);
}