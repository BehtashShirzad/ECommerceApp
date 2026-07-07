using ECommerce.Application.Features.User.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

public class UserController(ISender sender) : BaseApiController
{
    [HttpGet("me")]
    public async Task<ActionResult> GetUser()
    {
        var result = await sender.Send(new GetMeQuery(User.GetUserId()));
        return Ok(result);
    }
}