using ECommerce.Application.Abstractions.Contracts;
using ECommerce.Domain.Aggregates.Category;

namespace ECommerce.Application.Features.Category.Queries;

public record GetCategoriesQuery : IQuery<List<GetCategoryQueryResponse>>;
public record GetCategoryQueryResponse(CategoryId Id,string Name,string Description);


 