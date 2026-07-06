using System.Text.Json.Serialization;
using ECommerce.Application.Abstractions.Contracts.Command;
using ECommerce.Application.ViewModels;
using ECommerce.Domain.Aggregates.Cart;
using ECommerce.Domain.Aggregates.Product;
using ECommerce.Domain.Aggregates.Product.ValueObjects;
 

namespace ECommerce.Application.Features.Cart.AddProductToCart;

public record AddProductToCartCommand : ICommand<Guid>
{
    [JsonIgnore]
    public Guid UserId{get;set;}
   
    public ProductViewModel.ProductViewModelInput ProductDto{get;set;}=null!;
}
public class AddProductToCartCommandHandler(ICartRepository repository,IProductRepository productRepository) : ICommandHandler<AddProductToCartCommand,Guid>
{
    public async Task<Guid> Handle(AddProductToCartCommand request, CancellationToken cancellationToken)
    {
           
              
        var cart = await repository.GetAsync(request.UserId,cancellationToken);
        if(cart is null)
              cart = CartAggregate.Create(request.UserId);
        var product =await productRepository.GetAsync(new ProductId(request.ProductDto.ProductId),cancellationToken);
        if(product is null)
            throw new Exception("Product Not Found");
        cart.AddItem(request.ProductDto.ProductId ,product.Name, product.Price,
            request.ProductDto.Quantity);


        await repository.AddAsync(cart,cancellationToken);
        return cart.Id;


    }
}