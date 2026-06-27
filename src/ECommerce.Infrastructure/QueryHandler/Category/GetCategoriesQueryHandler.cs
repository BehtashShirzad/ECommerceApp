using ECommerce.Application.Abstractions.Contracts;
using ECommerce.Application.Features.Category.Queries;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.QueryHandler.Category;

public class GetCategoriesQueryHandler(ApplicationDbContext dbContext): IQueryHandler<GetCategoriesQuery, List<GetCategoryQueryResponse>>
{
    public async Task<List<GetCategoryQueryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await dbContext.Categories
            .AsNoTracking()
            .Where(_=>_.IsActive)
            .Select(c=> new GetCategoryQueryResponse(c.Id,c.Name,c.Description??string.Empty))
            .ToListAsync(cancellationToken);
        return categories;
    }
}