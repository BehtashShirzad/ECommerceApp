using ECommerce.Application.Abstractions.Contracts;
using ECommerce.Application.Abstractions.Contracts.Command;
using ECommerce.Application.Features.Category.Queries;
using ECommerce.Domain.Aggregates.Category;
using ECommerce.Domain.Aggregates.Product;
using MediatR;

namespace ECommerce.Application.Features.Product.Commands.UpdateProduct;

public record UpdateProductCommand(Guid ProductId,
    Guid? CategoryId, string? Name, string? Description, 
    decimal? Price,string? ImageUrl):ICommandVoid;
public class  UpdateCategoryCommandHandler(IProductRepository productRepository,ISender sender) : ICommandHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        CategoryId? categoryId;
    if (request.CategoryId.HasValue)
    {
        var category=await sender
            .Send(new GetCategoryQuery(new (request.CategoryId.Value)),
                cancellationToken);
        if (category is null)
            throw new ApplicationException("Category not found");
        categoryId = new CategoryId(request.CategoryId.Value);

    }
    else
    {
        categoryId = null;
    }
    
    var product =await productRepository.GetAsync(new (request.ProductId),cancellationToken);
    if (product == null)
        throw   new Exception("Product not found");
    
    string slug;
    if (string.IsNullOrEmpty(request.Name))
        slug=request!.Name!.Replace(" ", "-").ToLower();
    else
        slug = string.Empty;
     
    
    product.Update(categoryId, 
        request.Name,
        request.Price,request.Description,slug,request.ImageUrl);
         
        
}}