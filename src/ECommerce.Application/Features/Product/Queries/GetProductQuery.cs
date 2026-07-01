using ECommerce.Application.Abstractions.Contracts;
using ECommerce.Application.Abstractions.Contracts.Query;
using ECommerce.Domain.Aggregates.Category;
using ECommerce.Domain.Aggregates.Product.ValueObjects;

namespace ECommerce.Application.Features.Product.Queries;

public record GetProductQuery(ProductId ProductId) : IQuery<GetProductQueryResponse>;
 