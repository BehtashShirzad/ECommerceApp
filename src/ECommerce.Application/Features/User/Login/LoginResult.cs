 

using ECommerce.Application.Abstractions.Contracts.Command;
using ECommerce.Application.Abstractions.Contracts.Services.Identity;

namespace ECommerce.Application.Features.User.Login;

public   record LoginCommand(
    string UserName,string Password):ICommand<TokenPair>;

public class LoginCommandHandler(IUserManagerService userManagerService):ICommandHandler<LoginCommand,TokenPair>
{
    public async Task<TokenPair> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var token = await userManagerService.LoginUser(request.UserName, request.Password);
        
        return  token;
    }
}
    