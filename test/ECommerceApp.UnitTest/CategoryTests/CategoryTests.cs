using ECommerce.Domain.Aggregates.Category;
using ECommerce.Domain.Core;
using FluentAssertions;

namespace ECommerceApp.UnitTest.CategoryTests;

public class CategoryTests
{


    [Fact]
    public void Create_Should_Create_Category_When_Data_Is_Valid()
    {
        // Arrange
        var name = CategoryFaker.Name();
        var description = CategoryFaker.Description();
        var isActive = CategoryFaker.IsActive();

        // Act
        var category = Category.Create(name, description, isActive);

        // Assert
        category.Should().NotBeNull();
        category.Id.Should().NotBeNull();
        category.Id.Value.Should().NotBe(Guid.Empty);

        category.Name.Should().Be(name);
        category.Description.Should().Be(description);
        category.IsActive.Should().Be(isActive);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_Should_Throw_DomainException_When_Name_Is_Invalid(string? name)
    {
        // Arrange
        var description = CategoryFaker.Description();
        var isActive = CategoryFaker.IsActive();

        // Act
        var action = () => Category.Create(name!, description, isActive);

        // Assert
        action.Should().Throw<DomainException>();
    }

    [Fact]
    public void Create_Should_Allow_Null_Description()
    {
        // Arrange
        var name = CategoryFaker.Name();
        var isActive = CategoryFaker.IsActive();

        // Act
        var category = Category.Create(name, null, isActive);

        // Assert
        category.Description.Should().BeNull();
    }

    [Fact]
    public void Create_Should_Generate_Unique_Id()
    {
        // Arrange
        var name1 = CategoryFaker.Name();
        var name2 = CategoryFaker.Name();

        // Act
        var first = Category.Create(name1, CategoryFaker.Description(), true);
        var second = Category.Create(name2, CategoryFaker.Description(), true);

        // Assert
        first.Id.Value.Should().NotBe(second.Id.Value);
    }
}