using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Data;

public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache): IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken ct = default)
    {
        var cacheBasket = await cache.GetStringAsync(userName, ct);

        if (!string.IsNullOrEmpty(cacheBasket))
           return JsonSerializer.Deserialize<ShoppingCart>(cacheBasket)!;
        
        var basket = await repository.GetBasket(userName, ct);

        await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), ct);

        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken ct = default)
    {
        var basket = await repository.StoreBasket(cart, ct);
        
        await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), ct);

        return basket;
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken ct = default)
    {
        await repository.DeleteBasket(userName, ct);
        
        await cache.RemoveAsync(userName, ct);

        return true;
    }
}