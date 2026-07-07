 
using ECommerce.Application;
using ECommerce.Application.Features.Category.Commands.CreateCategory;
using ECommerce.Application.Features.Category.Commands.UpdateCategory;
using ECommerce.Application.Features.Category.Queries;
using ECommerce.Shared;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

public class CategoryController(ISender sender) : BaseApiController
{
    private readonly ISender _sender = sender;
    [Authorize(Roles = AppRoles.Admin)]
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
    
    
    
    [HttpGet("{categoryId:guid}")]
    public async Task<ActionResult> GetCategoryAsync(Guid categoryId,CancellationToken cancellationToken = default)
    {
        var category = await _sender.Send(new GetCategoryQuery(new (categoryId)),cancellationToken);
        return  Ok(category);
    }
    [Authorize(Roles = AppRoles.Admin)]
    [HttpPut]
    public async Task<ActionResult> GetCategoryAsync([FromBody]UpdateCategoryCommand request,CancellationToken cancellationToken = default)
    {
        await _sender.Send(request,cancellationToken);
        return  NoContent();
    }
}