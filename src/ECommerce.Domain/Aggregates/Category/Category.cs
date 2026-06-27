using Ardalis.GuardClauses;
using ECommerce.Domain.Core;
using ECommerce.Domain.GuardExtensions;
using ECommerce.Shared;

namespace ECommerce.Domain.Aggregates.Category;

public class Category:Entity<CategoryId>
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }

    private Category(string name, string? description, bool isActive)
    {
        Name = name;
        Description = description;
        IsActive = isActive;
    }
    public static Category Create(string name, string? description,bool isActive)
    {
        Guard.Against.NullOrEmpty(name,CategoryErrors.InvalidCategoryName);
    return new Category(name, description, isActive)
        {

            Id = new CategoryId(IdGenerator.New())

        };
    }

    public void Update(string? name,string? description,bool? isActive)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            Guard.Against.NullOrEmpty(
                name,
                CategoryErrors.InvalidCategoryName);
            Name = name;
        }
        if (!string.IsNullOrWhiteSpace(description))
            Description = description;
        if (isActive.HasValue)
            IsActive = isActive.Value;

    }
}