using ECommerce.Application.Abstractions.Contracts.Query;
using ECommerce.Application.Abstractions.Contracts.Services.Identity;
using ECommerce.Application.Features.User.Query;
using ECommerce.Domain.Aggregates.Customer;

namespace ECommerce.Infrastructure.QueryHandler.User;

public class GetMeQueryHandler(ICustomerRepository customerRepository,IUserManagerService userManagerService):IQueryHandler<GetMeQuery,GetMeQueryResponse>
{
    public async Task<GetMeQueryResponse> Handle(GetMeQuery request, CancellationToken cancellationToken)
    {
        var custoemr =await customerRepository.GetAsync(new(request.UserId), cancellationToken);
        var identityUser = await userManagerService.GetUserById(custoemr!.IdentityUserId,cancellationToken);
        return new GetMeQueryResponse(custoemr.FirstName, custoemr.LastName, custoemr?.Email??string.Empty, custoemr?.PhoneNumber??string.Empty,identityUser.EmailConfirmed,identityUser.PhoneNumberConfirmed);
    }
}