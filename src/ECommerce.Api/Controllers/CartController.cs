using ECommerce.Application.Features.Cart.AddCart;
using ECommerce.Application.Features.Cart.AddProductToCart;
using ECommerce.Application.Features.Cart.Checkout;
using ECommerce.Application.Features.Cart.GetCart;
using ECommerce.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

public class CartController(ISender sender,IHttpContextAccessor httpContextAccessor):BaseApiController
{
    readonly ISender _sender=sender;
    readonly  IHttpContextAccessor _httpContextAccessor=httpContextAccessor;
    [HttpPost]
    public async Task<ActionResult<Guid>> AddToCart([FromBody] AddCartCommand command)
    {
        command.UserId=_httpContextAccessor.GetUserId();
        var result = await _sender.Send(command);
        return Ok(result);
        
    }
    
    [HttpPost("add-product")]
    public async Task<ActionResult> AddProductToCart([FromBody] AddProductToCartCommand command)
    {
        command.UserId=_httpContextAccessor.GetUserId();
          await _sender.Send(command) ;
        return Ok();
        
    }
    
    [HttpPost("checkout")]
    public async Task<ActionResult<Guid>> AddProductToCart( )
    {
        var command =new CheckoutCartCommand
        {
            UserId = _httpContextAccessor.GetUserId()
        };
        var  id=await _sender.Send(command);
        return Ok(id);
        
    }

    [HttpGet]
    public async Task<ActionResult<CartViewModel.CartDto>> GetCart()
    {
        var cart = await _sender.Send(
            new GetCartQuery()
            {
                UserId = _httpContextAccessor.GetUserId()
            }
        );

        return Ok(cart);
    }
    
}