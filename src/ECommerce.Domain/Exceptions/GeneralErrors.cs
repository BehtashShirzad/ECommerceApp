using ECommerce.Domain.Core;

namespace ECommerce.Domain.Exceptions;

public class GeneralErrors
{
    public static readonly DomainError InvalidId = new DomainError(-1,nameof(InvalidId),"Invalid id");
}