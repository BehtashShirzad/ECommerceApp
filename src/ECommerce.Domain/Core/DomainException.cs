using ECommerce.Domain.Exceptions;

namespace ECommerce.Domain.Core;

public class DomainException : Exception
{
    public DomainError Error { get; }

    public DomainException(DomainError error)
        : base(error.ErrorDescription)
    {
        Error = error;
    }
}