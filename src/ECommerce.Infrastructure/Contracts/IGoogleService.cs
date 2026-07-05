namespace ECommerce.Infrastructure.Contracts;

public interface IGoogleService
{
    public Task<GoogleLoginDto> Login(string idToken);
}