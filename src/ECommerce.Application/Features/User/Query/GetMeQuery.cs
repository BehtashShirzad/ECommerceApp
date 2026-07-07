using ECommerce.Application.Abstractions.Contracts.Query;

namespace ECommerce.Application.Features.User.Query;

public record GetMeQuery(Guid UserId):IQuery<GetMeQueryResponse>;
public record GetMeQueryResponse(string FirstName, string LastName,string UserName, string Email,string PhoneNumber,bool IsEmailConfirmed,bool IsPhoneNumberConfirmed,IReadOnlyCollection<string> Roles);