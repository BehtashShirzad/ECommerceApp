namespace ECommerce.Application.ViewModels;

public class ProductViewModel
{
    public record ProductViewModelInput(Guid ProductId,int Quantity,decimal Price,string ProductName);
    public record ProductViewModelOutput(Guid ProductId,int Quantity,decimal Price,string ProductName);

    public record ProductImageViewModelOutput(Guid ImageId, string FileKey, int Sort, bool IsCoder);

}