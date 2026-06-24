using Microsoft.OpenApi;

namespace ECommerce.Api.ApiConfiguration;

public static class SwaggerConfiguration
{
    public static void AddSwagger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
        });
    }
}