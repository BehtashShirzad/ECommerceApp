using ECommerce.Domain.Aggregates.Category;
using ECommerce.Domain.Aggregates.Product;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class CategoryRepository(ApplicationDbContext context):ICategoryRepository
{
    readonly ApplicationDbContext _context=context;
    public async Task AddCategoryAsync(Category category,CancellationToken cancellationToken = default)
    { 
        await   _context.Categories.AddAsync(category,cancellationToken);
       
    }

    public   void DeleteCategoryAsync(Category category)
    {
     _context.Categories.Remove( category);
      
    }

  
    public async Task<Category?> GetCategoryAsync(CategoryId id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public void UpdateCategory(Category category)
    {
          _context.Categories.Update(category);
    }
}