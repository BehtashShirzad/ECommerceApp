using Ardalis.GuardClauses;
using ECommerce.Application.Abstractions.Contracts;
using ECommerce.Application.Features.Category.Queries;
using ECommerce.Domain.Aggregates.Category;
using MediatR;

namespace ECommerce.Application.Features.Category.Commands.UpdateCategory;

public record UpdateCategoryCommand(Guid CategoryId,string? Name,string? Description,bool? IsActive):ICommandVoid;
public class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository):ICommandHandler<UpdateCategoryCommand>
{
    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {

        var category =await categoryRepository.GetCategoryAsync(new (request.CategoryId));
        if (category == null)
         throw   new Exception("Category not found");
        
        category.Update(request.Name, request.Description, request.IsActive);
         
        
    }
}