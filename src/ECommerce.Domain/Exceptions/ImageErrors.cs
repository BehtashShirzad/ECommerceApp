using ECommerce.Domain.Core;

namespace ECommerce.Domain.Exceptions;

public class ImageErrors
{
    public static readonly DomainError InvalidImageId = new DomainError(-1,nameof(InvalidImageId),"Invalid Image Id");
    public static readonly DomainError InvalidImageExtemstion = new DomainError(-2,nameof(InvalidImageExtemstion),"Invalid Image Extension");
    public static readonly DomainError InvalidImageUrl = new DomainError(-3,nameof(InvalidImageUrl),"Invalid Image Url");
    public static readonly DomainError InvalidImageSize= new DomainError(-4,nameof(InvalidImageSize),"Invalid Image Size");

}