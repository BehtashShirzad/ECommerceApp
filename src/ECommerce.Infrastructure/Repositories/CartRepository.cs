using ECommerce.Application.Features.Cart.Mapper;
using ECommerce.Application.ViewModels;
using ECommerce.Domain.Aggregates.Cart;
using Mapster;
using Microsoft.Extensions.Caching.Hybrid;
using Newtonsoft.Json;

namespace ECommerce.Infrastructure.Repositories;

public class CartRepository(HybridCache hybridCache):ICartRepository
{
    private const string CartCacheKey = "Cart_{0}";

    private readonly HybridCacheEntryOptions  _options=new HybridCacheEntryOptions()
    {
        Expiration = TimeSpan.FromHours(3),
        LocalCacheExpiration = TimeSpan.FromMinutes(30)
    };
    public async ValueTask AddAsync(CartAggregate cart, CancellationToken cancellationToken = default)
    {
        var key = string.Format(CartCacheKey, cart.CustomerId);
         await  hybridCache.SetAsync(key,cart.ToCacheViewModel(),_options,cancellationToken:cancellationToken);
    }

    public void RemoveAsync(CartAggregate entity)
    {
         
    }

    public async Task<CartAggregate?> GetAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var key = string.Format(CartCacheKey, userId);
         
        var cart = await hybridCache.GetOrCreateAsync(
            key,
            _ => ValueTask.FromResult<CartViewModel.CartCacheModel?>(null),
            _options,
            cancellationToken: cancellationToken);
        if (cart != null)
        {
            var aggr = cart.ToAggregate();
            return aggr;
        }

        return null;
    }
    
    
}