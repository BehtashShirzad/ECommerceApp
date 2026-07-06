using ECommerce.Application.Abstractions.Contracts.Services.Identity;
using ECommerce.Application.Abstractions.Contracts.Transaction;
using ECommerce.Domain.Aggregates.Customer;
using ECommerce.Shared;

namespace ECommerce.Application.Features.Identity.Register;

public record RegisterCustomerCommand(string FirstName,string LastName,string Username,string PhoneNumber,string Password,string? Email=null):ITransactionalCommand<TokenPair>;
 

public class RegisterCustomerCommandHandler(IUserManagerService userManagerService,ICustomerRepository customerRepository):ITransactionalCommandHandler<RegisterCustomerCommand,TokenPair>
{
    public async Task<TokenPair> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {

        if (await customerRepository.AnyAsync(_=>_.Email==request.Email || _.PhoneNumber == request.PhoneNumber,cancellationToken))
        {
            throw new ApplicationException("Email or Phone number is already in use");
        }
        var user = await userManagerService.CreateUser(request.Username,
            request.Password,
            request.PhoneNumber,
            AppRoles.User,
            request.Email);
        var customer = Domain.Aggregates.Customer.Customer.Create(request.FirstName,request.LastName,request.PhoneNumber,user.Id,request.Email);
        await customerRepository.AddAsync(customer,cancellationToken);
        var token =await userManagerService.LoginUser(request.Username,request.Password);
        
        return new  TokenPair( token.AccessToken,token.AccessTokenExpiresAt) ;
    }
}