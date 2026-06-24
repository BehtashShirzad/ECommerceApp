using ECommerce.Api.ApiConfiguration;
using ECommerce.Application;
using ECommerce.Domain;
using ECommerce.Infrastructure;

namespace ECommerce.Api;

public static class DependencyInjection
{
    public static void AddECommerceServices(this IServiceCollection serviceCollection,IConfiguration configuration)
    {
        #region Api Services

        serviceCollection.AddSwagger();
        // serviceCollection.AddAuthentication();
        // serviceCollection.AddAuthorization();
        #endregion
        
        serviceCollection.AddInfrastructureServices(configuration);
        serviceCollection.AddApplicationServices(configuration);
        serviceCollection.AddDomainServices();
        
    }
}