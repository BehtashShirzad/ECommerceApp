 

using ECommerce.Application.Abstractions.Contracts.Command;
using ECommerce.Application.Abstractions.Contracts.Services.Identity;
using ECommerce.Application.Abstractions.Contracts.Transaction;
using ECommerce.Domain.Aggregates.Customer;
using ECommerce.Shared;

namespace ECommerce.Application.Features.Identity.GoogleLogin;

public record GoogleLoginCommand(string IdToken) :ITransactionalCommand<TokenPair>;
 public class GoogleLoginCommandHandler(IUserManagerService userManagerService,ICustomerRepository customerRepository):ITransactionalCommandHandler<GoogleLoginCommand,TokenPair>
 {
     public async Task<TokenPair> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
     {
         var token =await userManagerService.LoginUserByGoogle(request.IdToken,AppRoles.User);
         if (token.IsNewUser)
            await customerRepository.AddAsync(Customer.Create(token.FirstName, token.LastName, "",token.IdentityId), cancellationToken); 
         
         return token.TokenPair;
     }
 }