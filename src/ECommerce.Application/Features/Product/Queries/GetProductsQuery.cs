using ECommerce.Application.Abstractions.Contracts;
using ECommerce.Application.Abstractions.Contracts.Query;

namespace ECommerce.Application.Features.Product.Queries;

public record GetProductsQuery : IQuery<List<GetProductQueryResponse>>;
public record GetProductQueryResponse(Guid Id,string Name,string Description,Guid CategoryId,decimal Price,string? ImageUrl);


 