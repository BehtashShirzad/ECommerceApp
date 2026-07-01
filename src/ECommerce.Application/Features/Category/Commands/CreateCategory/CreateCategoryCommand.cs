using ECommerce.Application.Abstractions.Contracts;
using ECommerce.Application.Abstractions.Contracts.Command;
using ECommerce.Domain.Aggregates.Category;

namespace ECommerce.Application.Features.Category.Commands.CreateCategory;

public record CreateCategoryCommand(string Name, string? Description, bool IsActive) : ICommand<CategoryCreateResponse>;

public record CategoryCreateResponse(Guid Id);

public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository) : ICommandHandler<CreateCategoryCommand,CategoryCreateResponse>
{
    public async Task<CategoryCreateResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = Domain.Aggregates.Category.Category
            .Create(request.Name,request.Description,request.IsActive);
        await  categoryRepository.AddAsync(category, cancellationToken);
        return new (category.Id.Value);
    }
}