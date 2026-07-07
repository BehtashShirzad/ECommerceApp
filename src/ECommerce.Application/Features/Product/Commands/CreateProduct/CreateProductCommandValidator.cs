using FluentValidation;

namespace ECommerce.Application.Features.Product.Commands.CreateProduct;

public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Price).NotNull().GreaterThanOrEqualTo(0).WithMessage("Price must be greater than 0 or  equals to 0");
        RuleFor(x => x.CategoryId).NotNull().WithMessage("Category id is required");
    }
    
}