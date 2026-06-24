namespace ECommerce.Shared;

public static class IdGenerator
{
    public static Guid New()
    {
        return Guid.CreateVersion7();
    }
}