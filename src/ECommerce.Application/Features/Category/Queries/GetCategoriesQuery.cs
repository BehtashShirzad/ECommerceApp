using ECommerce.Application.Abstractions.Contracts;
using ECommerce.Application.Abstractions.Contracts.Query;
using ECommerce.Domain.Aggregates.Category;

namespace ECommerce.Application.Features.Category.Queries;

public record GetCategoriesQuery : IQuery<List<GetCategoryQueryResponse>>;
public record GetCategoryQueryResponse(Guid Id,string Name,string Description);


 