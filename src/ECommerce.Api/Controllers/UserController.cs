using ECommerce.Application.Features.User.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;


public class UserController(ISender sender) : BaseApiController
{
    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult> GetUser()
    {
        
         var result = await sender.Send(new GetMeQuery(User.GetUserId()));
        return Ok(result);
    }

    [Authorize]
    [HttpGet("isAdmin")]
    public ActionResult IsAdmin()
    {
        var result = User.IsAdmin();
        return Ok(result);

    }
    
    // [HttpGet("me")]
    // public IActionResult GetUser()
    // {
    //     return Ok(new
    //     {
    //         AuthHeader = Request.Headers.Authorization.ToString(),
    //         IsAuthenticated = User.Identity?.IsAuthenticated
    //     });
    // }
}