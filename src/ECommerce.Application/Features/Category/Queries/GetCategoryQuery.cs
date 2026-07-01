using ECommerce.Application.Abstractions.Contracts;
using ECommerce.Application.Abstractions.Contracts.Query;
using ECommerce.Domain.Aggregates.Category;

namespace ECommerce.Application.Features.Category.Queries;

public record GetCategoryQuery(CategoryId CategoryId) : IQuery<GetCategoryQueryResponse>;
 