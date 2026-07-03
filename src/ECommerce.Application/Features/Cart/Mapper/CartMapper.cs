using ECommerce.Application.ViewModels;
using ECommerce.Domain.Aggregates.Cart;

namespace ECommerce.Application.Features.Cart.Mapper;

public static class CartMapper
{
    public static CartAggregate ToAggregate(this CartViewModel.CartCacheModel model)
    {
        var cart = CartAggregate.Create(model.CustomerId);

        foreach (var item in model.Items)
        {
            cart.AddItem(
                item.ProductId,
                item.ProductName,
                item.Price,
                item.Quantity);
        }

        if (model.IsCheckedOut)
            cart.Checkout();

        return cart;
    }

    public static CartViewModel.CartCacheModel ToCacheViewModel(this CartAggregate cart)
    {

        return new CartViewModel.CartCacheModel
        {
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