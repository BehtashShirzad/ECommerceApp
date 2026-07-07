using ECommerce.Application.Abstractions.Contracts.Query;
using ECommerce.Application.Features.User.Query;
using ECommerce.Domain.Aggregates.Customer;

namespace ECommerce.Infrastructure.QueryHandler.User;

public class GetUsersQueryHandler(ICustomerRepository customerRepository):IQueryHandler<GetUsersQuery,ICollection<GetUserQueryResponse>>
{
    public async Task<ICollection<GetUserQueryResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var customers =await customerRepository.GetAllAsync(cancellationToken);
        
        return customers.Select(_ => new GetUserQueryResponse(_.IdentityUser.Id,_.Id.Value,_.IdentityUser!.UserName!, _.FirstName, _.LastName,
            _.CreatedAt, _.IdentityUser?.Email??string.Empty, _.IdentityUser?.PhoneNumber??string.Empty, _.IdentityUser!.EmailConfirmed,
            _.IdentityUser.PhoneNumberConfirmed)
        ).ToList();
    }
}