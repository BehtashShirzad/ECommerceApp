using ECommerce.Application.Features.Product.Commands;
using ECommerce.Application.Features.Product.Commands.AddImage;
using ECommerce.Application.Features.Product.Commands.CreateProduct;
using ECommerce.Application.Features.Product.Commands.UpdateProduct;
using ECommerce.Application.Features.Product.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

public class ProductController(ISender sender) : BaseApiController
{
   readonly  ISender _sender=sender;
   [HttpPost]
    public async Task<ActionResult> CreateProduct([FromBody]CreateProductCommand command)
    {
        var result =await _sender.Send(command);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<ActionResult> GetProductsAsync(CancellationToken cancellationToken = default)
    {
        var products = await _sender.Send(new GetProductsQuery(),cancellationToken);
        return  Ok(products);
    }
    
    
    
    [HttpGet("{productId:guid}")]
    public async Task<ActionResult> GetProductAsync(Guid productId,CancellationToken cancellationToken = default)
    {
        var product = await _sender.Send(new GetProductQuery(new (productId)),cancellationToken);
        return  Ok(product);
    }
    
    [HttpPut]
    public async Task<ActionResult> UpdateProductAsync([FromBody]UpdateProductCommand request,CancellationToken cancellationToken = default)
    {
        await _sender.Send(request,cancellationToken);
        return  NoContent();
    }

    [HttpPost("add-image")]
    public async Task<ActionResult> AddProductImage( [FromForm] AddProductImage productImage,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(productImage, cancellationToken);
       return Ok(result);
        
    }
}