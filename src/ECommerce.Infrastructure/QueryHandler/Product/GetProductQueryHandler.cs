using ECommerce.Application.Abstractions.Contracts;
using ECommerce.Application.Abstractions.Contracts.Query;
using ECommerce.Application.Features.Product.Queries;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.QueryHandler.Product;

public class GetProductQueryHandler(ApplicationDbContext context):IQueryHandler<GetProductQuery,GetProductQueryResponse>
{
    readonly ApplicationDbContext _context=context;
    public async Task<GetProductQueryResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .AsNoTracking()
            .Where(c => c.Id == request.ProductId ).Select(c=>
                new GetProductQueryResponse(c.Id.Value,c.Name,c.Description??string.Empty,c.CategoryId.Value,c.Price,c.ImageUrl))
            .FirstOrDefaultAsync(cancellationToken);
        return  product;
    }
}