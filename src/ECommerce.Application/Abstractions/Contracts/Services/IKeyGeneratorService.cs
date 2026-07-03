namespace ECommerce.Application.Abstractions.Contracts.Services;

public interface IKeyGeneratorService
{
    string GenerateProductImageKey(string extension);

    string GenerateCategoryImageKey(string extension);

    string GenerateUserAvatarKey(string extension);
}