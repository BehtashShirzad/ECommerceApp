using ECommerce.Application.Abstractions.Contracts;
using ECommerce.Application.Abstractions.Contracts.Command;
using ECommerce.Domain.Aggregates.Category;
using ECommerce.Domain.Aggregates.Product;

namespace ECommerce.Application.Features.Product.Commands.CreateProduct;

public record CreateProductCommand (Guid CategoryId,string Name,string? Description,decimal Price): ICommand<CreateProductCommandResponse>;

public record CreateProductCommandResponse(Guid ProductId);

public class CreateProductCommandHandler (IProductRepository productRepository ): ICommandHandler<CreateProductCommand, CreateProductCommandResponse>
{
    public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var slug = request.Name.Replace(" ", "-").ToLower();
        var product = Domain.Aggregates.Product.Product.Create(new CategoryId(request.CategoryId), request.Name,
            request.Price, request?.Description??string.Empty, slug
             );
        
        await productRepository.AddAsync(product,cancellationToken);
        return new  (product.Id.Value);
    }
}