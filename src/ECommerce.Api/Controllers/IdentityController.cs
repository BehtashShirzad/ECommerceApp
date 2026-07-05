using ECommerce.Application.Features.Identity.GoogleLogin;
using ECommerce.Application.Features.Identity.Login;
using ECommerce.Application.Features.Identity.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

public class IdentityController(ISender sender):BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult> RegisterCustomer([FromBody] RegisterCustomerCommand dto,CancellationToken cancellationToken)
    {
        var result = await sender.Send(dto,cancellationToken);
        return Ok(result);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult>LoginUser([FromBody] LoginCommand command,CancellationToken cancellationToken)
    {
        var result = await sender.Send(command,cancellationToken);
        return Ok(result);
    }
    [HttpPost("signin-google")]
    public async Task<ActionResult>GoogleSignIn([FromBody] GoogleLoginCommand command,CancellationToken cancellationToken)
    {
        var result = await sender.Send(command,cancellationToken);
        return Ok(result);
    }
}