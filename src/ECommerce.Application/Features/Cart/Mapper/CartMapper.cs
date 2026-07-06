using ECommerce.Application.ViewModels;
using ECommerce.Domain.Aggregates.Cart;
using Mapster;

namespace ECommerce.Application.Features.Cart.Mapper;

public static class CartMapper
{
   
    public static CartAggregate ToAggregate(this CartViewModel.CartCacheModel model)
    {
        var localConfig = new TypeAdapterConfig();
        
        localConfig.NewConfig<CartViewModel.CartCacheModel, CartAggregate>()

            .ConstructUsing(() => (CartAggregate)Activator.CreateInstance(typeof(CartAggregate), true))

            .Ignore(dest => dest.Items)

            .Map(dest => dest.Id, src => src.Id) 
            .Map(dest => dest.CustomerId, src => src.CustomerId)
            .Map(dest => dest.IsCheckedOut, src => src.IsCheckedOut)

            .AfterMapping((src, dest) =>
            {
                foreach (var item in src.Items)
                {
                    dest.AddItem(
                        item.ProductId,
                        item.ProductName,
                        item.Price,
                        item.Quantity);
                }
            });

        return model.Adapt<CartAggregate>(localConfig);
    }
    public static CartViewModel.CartCacheModel ToCacheViewModel(this CartAggregate cart)
    {

        return new CartViewModel.CartCacheModel
        {
            Id = cart.Id,
            CustomerId = cart.CustomerId,
            IsCheckedOut = cart.IsCheckedOut,
            Items = cart.Items.Select(_ => new CartViewModel.CartItemCacheModel()
            {
                Price = _.Price,
                Quantity = _.Quantity,
                ProductId = _.ProductId,
                ProductName = _.ProductName
            }).ToList()
        };
    }
}