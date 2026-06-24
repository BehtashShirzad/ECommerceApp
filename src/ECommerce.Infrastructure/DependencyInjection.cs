using ECommerce.Domain.Aggregates;
 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure;

public static class  DependencyInjection
{
    public static void AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {

        serviceCollection.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ApplicationConnection")) );
        serviceCollection.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ApplicationConnection")) );
        
        serviceCollection
            .AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<AppIdentityDbContext>();
        
            
        
        
    }
}