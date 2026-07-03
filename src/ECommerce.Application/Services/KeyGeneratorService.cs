using ECommerce.Application.Abstractions.Contracts.Services;

namespace ECommerce.Application.Services;

public class KeyGeneratorService : IKeyGeneratorService
{
    public string GenerateProductImageKey(string extension)
        => $"products/{Guid.NewGuid()}{extension}";

    public string GenerateCategoryImageKey(string extension)
        => $"categories/{Guid.NewGuid()}{extension}";

    public string GenerateUserAvatarKey(string extension)
        => $"avatars/{Guid.NewGuid()}{extension}";
}