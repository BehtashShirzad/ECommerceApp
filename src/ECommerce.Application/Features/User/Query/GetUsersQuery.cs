using ECommerce.Application.Abstractions.Contracts.Query;

namespace ECommerce.Application.Features.User.Query;

public record GetUsersQuery:IQuery<ICollection<GetUserQueryResponse>>;
public record GetUserQueryResponse(Guid IdentityId,Guid CustomerId,string Username,string FirstName,string LastName, DateTime CreatedAt,string Email,string PhoneNumber,bool IsEmailConfirmed,bool IsPhoneNumberConfirmed);