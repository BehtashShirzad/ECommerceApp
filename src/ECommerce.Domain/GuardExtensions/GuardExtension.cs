using Ardalis.GuardClauses;
using ECommerce.Domain.Core;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Domain.GuardExtensions;

public  static class GuardExtension 
{

    public static void InvalidInput<T>(
        this IGuardClause guardClause,
        T input,
        Predicate<T> predicate,
        DomainError error)
    {
        if (!predicate(input))
            throw new DomainException(error);
    }
    
    public static void InvalidLength(
        this IGuardClause guardClause,
        string input,
        int minLength,
        int maxLength,
        DomainError error)
    {
        if (input.Length < minLength ||
            input.Length > maxLength)
        {
            throw new DomainException(error);
        }
    }
    
    public static void NullOrEmpty(
        this IGuardClause guardClause,
        string?  input,
        DomainError error)
    {
        if (string.IsNullOrWhiteSpace(input)|| string.IsNullOrWhiteSpace(input))
            throw new DomainException(error);
    }

    public static void Null<T>(
        this IGuardClause guardClause,
        T input,
        DomainError error
    )
    {
        if (input is null)
        {
            throw new DomainException(error);
        }
    }
    
    public static void EmptyGuid(
        this IGuardClause guardClause,
        Guid input,
        DomainError error)
    {
        if (input == Guid.Empty)
            throw new DomainException(error);
    }
}