namespace ECommerce.Application.ViewModels;

public class CartViewModel
{
    public record CartDto(List<ProductViewModel.ProductViewModelOutput>Products,decimal TotalPrice);
    public sealed class CartCacheModel
    {
        public Guid CustomerId { get; set; }
        public bool IsCheckedOut { get; set; }
        public List<CartItemCacheModel> Items { get; set; } = [];
    }
    public sealed class CartItemCacheModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}