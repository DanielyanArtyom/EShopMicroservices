using Marten;

namespace Basket.API.Data;

public class BasketRepository(IDocumentSession session): IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken ct = default)
    {
        var basket = await session.LoadAsync<ShoppingCart>(userName, ct);

        if (basket == null)
            throw new BasketNotFoundException(userName);

        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken ct = default)
    {
        session.Store(cart);
        await session.SaveChangesAsync(ct);
        return cart;
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken ct = default)
    {
        session.Delete(userName);

        await session.SaveChangesAsync(ct);

        return true;
    }
}