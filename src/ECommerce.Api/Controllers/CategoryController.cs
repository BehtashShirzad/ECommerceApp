 
using ECommerce.Application;
using ECommerce.Application.Features.Category.Commands.CreateCategory;
using ECommerce.Application.Features.Category.Commands.UpdateCategory;
using ECommerce.Application.Features.Category.Queries;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class CategoryController(ISender sender) : Controller
{
    private readonly ISender _sender = sender;
    [HttpPost]
    public async Task<ActionResult> AddCategoryAsync([FromBody]CreateCategoryCommand request, CancellationToken cancellationToken = default )
    {
      
        var result = await _sender.Send(request, cancellationToken);
        return Ok( result);
    }

    [HttpGet]
    public async Task<ActionResult> GetCategoriesAsync(CancellationToken cancellationToken = default)
    {
        var categories = await _sender.Send(new GetCategoriesQuery(),cancellationToken);
        return  Ok(categories);
    }
    
    
    
    [HttpGet("/{categoryId:guid}")]
    public async Task<ActionResult> GetCategoryAsync(Guid categoryId,CancellationToken cancellationToken = default)
    {
        var category = await _sender.Send(new GetCategoryQuery(new (categoryId)),cancellationToken);
        return  Ok(category);
    }
    
    [HttpPut]
    public async Task<ActionResult> GetCategoryAsync([FromBody]UpdateCategoryCommand request,CancellationToken cancellationToken = default)
    {
        await _sender.Send(request,cancellationToken);
        return  NoContent();
    }
}