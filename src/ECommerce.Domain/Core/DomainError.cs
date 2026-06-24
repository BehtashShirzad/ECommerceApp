namespace ECommerce.Domain.Core;

public class DomainError:Enumeration
{
    public string ErrorDescription { get; init; }
    public DomainError( int errorCode,string name,string description) : base(errorCode, name)
    {
        ErrorDescription = description;
    }
    
}