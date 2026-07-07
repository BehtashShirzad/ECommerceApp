using ECommerce.Application.Features.User.Query;
using ECommerce.Shared;
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
    
    
    [Authorize(Roles = AppRoles.Admin)]
    [HttpGet("all")]
    public async Task<ActionResult> GetUsers()
    {
        var result =await sender.Send(new GetUsersQuery());
        return Ok(result);

    }
}