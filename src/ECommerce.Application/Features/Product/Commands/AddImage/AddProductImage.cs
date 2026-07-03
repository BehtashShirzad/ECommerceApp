using ECommerce.Application.Abstractions.Contracts.Command;
using ECommerce.Application.Abstractions.Contracts.Services;
using ECommerce.Domain.Aggregates.Product;
using ECommerce.Domain.Aggregates.Product.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Features.Product.Commands.AddImage;

public class AddProductImage:ICommand<AddProductImageCommandResponse>
{
    public IFormFile Image { get; set; }
    public Guid ProductId { get; set; }
}

public record AddProductImageCommandResponse(string Address);
public class AddProductImageCommandHandler(IFileService fileService,IKeyGeneratorService generator,IProductRepository productRepository) : ICommandHandler<AddProductImage, AddProductImageCommandResponse>
{
    readonly IFileService _fileService=fileService;
    readonly IProductRepository _productRepository=productRepository;
    
    public async Task<AddProductImageCommandResponse> Handle(AddProductImage request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAsync(new ProductId(request.ProductId),cancellationToken);
        if (product == null)
            throw new Exception("Product not found");
        
        var  extension = Path.GetExtension(request.Image.FileName);
        var key = generator.GenerateProductImageKey(extension);
        var result =await _fileService.UploadAsync(key, request.Image.OpenReadStream(), request.Image.ContentType,
            cancellationToken);
        var image = ProductImage.Create(product.Id, key, extension, request.Image.Length);
        product.AddImage(image);
        return  new AddProductImageCommandResponse(result.fullAddress);
    }
    
    
}