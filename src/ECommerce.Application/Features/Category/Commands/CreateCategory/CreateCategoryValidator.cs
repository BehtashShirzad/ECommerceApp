using FluentValidation;

namespace ECommerce.Application.Features.Category.Commands.CreateCategory;

public class CreateCategoryValidator:AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryValidator()
    {
        RuleFor(c => c.Name).NotEmpty().NotNull().WithMessage("Name is required");
        RuleFor(c => c.IsActive).NotEmpty().NotNull().WithMessage("IsActive is required");
    }
}