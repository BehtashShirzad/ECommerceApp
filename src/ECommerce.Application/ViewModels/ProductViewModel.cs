namespace ECommerce.Application.ViewModels;

public class ProductViewModel
{
    public record ProductViewModelInput(Guid ProductId,int Quantity);
    public record ProductViewModelOutput(Guid ProductId,int Quantity,decimal Price,string ProductName);

    public record ProductImageViewModelOutput(Guid ImageId, string ImageAddress, int Sort, bool IsCoder);

}