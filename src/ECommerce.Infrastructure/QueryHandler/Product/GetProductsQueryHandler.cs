using ECommerce.Application.Abstractions.Contracts;
using ECommerce.Application.Abstractions.Contracts.Query;
using ECommerce.Application.Features.Product.Queries;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.QueryHandler.Product;

public class GetProductsQueryHandler(ApplicationDbContext context):IQueryHandler<GetProductsQuery,List<GetProductQueryResponse>>
{
    private readonly ApplicationDbContext _context = context;
    public async Task<List<GetProductQueryResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _context.Products
            .AsNoTracking()
            .Select(c=>
                new GetProductQueryResponse(c.Id.Value,c.Name,c.Description??string.Empty,c.CategoryId.Value,c.Price,c.ImageUrl))
            .ToListAsync(cancellationToken);
        return products;
    }
}