using System.Text;
using ECommerce.Api.ApiConfiguration;
using ECommerce.Application;
using ECommerce.Domain;
using ECommerce.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Api;

public static class DependencyInjection
{
    public static void AddECommerceServices(this IServiceCollection serviceCollection,IConfiguration configuration)
    {
        #region Api Services

        serviceCollection.AddHttpContextAccessor();
        serviceCollection.AddSwagger();
       
        #endregion
        
        serviceCollection.AddInfrastructureServices(configuration);
        serviceCollection.AddApplicationServices(configuration);
        serviceCollection.AddDomainServices();
      

    }
}