using ECommerce.Application.Abstractions.Contracts.Query;
using ECommerce.Application.Abstractions.Contracts.Services.Identity;
using ECommerce.Application.Features.User.Query;
using ECommerce.Domain.Aggregates.Customer;

namespace ECommerce.Infrastructure.QueryHandler.User;

public class GetMeQueryHandler(ICustomerRepository customerRepository,IUserManagerService userManagerService):IQueryHandler<GetMeQuery,GetMeQueryResponse>
{
    public async Task<GetMeQueryResponse> Handle(GetMeQuery request, CancellationToken cancellationToken)
    {
        var custoemr =await customerRepository.FindAsync( _=>_.IdentityUserId==request.UserId , cancellationToken,_=>_.IdentityUser);
        if (custoemr == null)
        {
            throw new ApplicationException("Customer Not Found");
        }
       var roles = await userManagerService.GetUserRoles(custoemr!.IdentityUser,cancellationToken);
        return new GetMeQueryResponse(custoemr!.FirstName,
            custoemr.LastName,
            custoemr!.IdentityUser.UserName!,
            custoemr?.Email??string.Empty,
            custoemr?.PhoneNumber??string.Empty,
            custoemr!.IdentityUser.EmailConfirmed,custoemr.IdentityUser.PhoneNumberConfirmed,roles
            );
    }
}