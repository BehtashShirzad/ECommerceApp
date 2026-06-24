using Ardalis.GuardClauses;
using ECommerce.Domain.Core;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.GuardExtensions;

namespace ECommerce.Domain.Aggregates.Product.ValueObjects;

public  class ProductId:ValueObject
{
   public ProductId(Guid value)
   {
      Guard.Against.EmptyGuid(value,GeneralErrors.InvalidId);
      Value = value;
   }
   private Guid Value { get; }  
   protected override IEnumerable<object?> GetEqualityComponents()
   {
      yield return Value;
   }
}