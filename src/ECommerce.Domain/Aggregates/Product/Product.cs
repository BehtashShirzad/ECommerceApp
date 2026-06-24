using Ardalis.GuardClauses;
using ECommerce.Domain.Aggregates.Product.ValueObjects;
using ECommerce.Domain.Core;
using ECommerce.Domain.GuardExtensions;
using ECommerce.Shared;

namespace ECommerce.Domain.Aggregates.Product;

public class Product:AggregateRoot<ProductId>
{
    public decimal Price { get;private  set; }//Todo: Better To change VlaueObject price
    public string Name { get; private set; } 
    public string? Description { get;private set; }
    public string Slug { get;private set; }
    public string? ImageUrl { get;private set; } //Todo: Better To change VlaueObject Image
    const long MinimumPrice = 1;
    const long MaximumPrice = long.MaxValue;
    private Product( string name, decimal price,string description, string slug,string? imageUrl)
    {
        Price = price;
        Name = name;
        Description = description;
        Slug = slug;
        ImageUrl = imageUrl;
    }
    public static Product Create(string name , decimal price,string description,string slug ,string? imageUrl)
    {
        Guard.Against.NullOrEmpty(name,ProductErrors.InvalidProductName);
         
        Guard.Against.InvalidNumerRange(price,MinimumPrice,MaximumPrice,ProductErrors.InvalidProductPrice);
        var product = new Product(name, price,description, slug, imageUrl){Id =new ProductId(IdGenerator.New())};
        
        return  product;
    }
}