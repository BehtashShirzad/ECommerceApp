using ECommerce.Application.Abstractions.Contracts;
using ECommerce.Application.Abstractions.Contracts.Query;
using ECommerce.Application.Features.Category.Queries;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.QueryHandler.Category;

public class GetCategoryQueryHandler(ApplicationDbContext dbContext):IQueryHandler<GetCategoryQuery,GetCategoryQueryResponse>
{
    public async Task<GetCategoryQueryResponse> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await dbContext.Categories
            .AsNoTracking()
            .Where(c => c.Id == request.CategoryId && c.IsActive)
            .Select(c => new GetCategoryQueryResponse(c.Id.Value, c.Name, c.Description ?? string.Empty))
            .FirstOrDefaultAsync(cancellationToken);
            
        return category;
    }
}