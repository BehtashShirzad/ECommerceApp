namespace ECommerce.Domain.Aggregates.Category;

public interface ICategoryRepository
{
    public Task AddCategoryAsync(Category category,CancellationToken cancellationToken);
    public void DeleteCategoryAsync(Category category);
    public Task<Category?> GetCategoryAsync(CategoryId id);
    
}