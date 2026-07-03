using ECommerce.Application.Abstractions.Contracts;
using ECommerce.Application.Abstractions.Contracts.Services;
using ECommerce.Application.Abstractions.Contracts.Services.Identity;
using ECommerce.Application.Abstractions.Contracts.Services.Security;
using ECommerce.Application.Abstractions.Contracts.Transaction;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Aggregates.Cart;
using ECommerce.Domain.Aggregates.Category;
using ECommerce.Domain.Aggregates.Customer;
using ECommerce.Domain.Aggregates.Order;
using ECommerce.Domain.Aggregates.Product;
using ECommerce.Infrastructure.Options;
using ECommerce.Infrastructure.Persistence;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Infrastructure.Services;
using ECommerce.Infrastructure.Services.Identity;
using ECommerce.Infrastructure.Services.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Minio;

namespace ECommerce.Infrastructure;

public static class  DependencyInjection
{
    public static void AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {

        AddDbContexts(serviceCollection,configuration);
        serviceCollection
            .AddIdentity<AppUser,IdentityRole<Guid>>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Lockout.AllowedForNewUsers = true;
                
            })
            .AddRoles<IdentityRole<Guid>>() 
            .AddSignInManager()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        
            
        serviceCollection.AddScoped<ICurrentUser, CurrentUser>();
        serviceCollection.Configure<JwtOptions>(configuration.GetSection("Jwt"));
        AddRepositories(serviceCollection);
        serviceCollection.AddScoped<IUserManagerService, UserManagerService>();
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        serviceCollection.AddScoped<ITransactionManager, TransactionManager>();
        serviceCollection.AddScoped<IIdentityService, IdentityService>();
        serviceCollection.AddScoped<ITokenService, TokenService>();
        serviceCollection.AddScoped<IPasswordService, PasswordService>();
        serviceCollection.AddScoped<IRoleService, RoleService>();
        serviceCollection.AddScoped<IJwtService, JwtService>();
        serviceCollection.AddScoped<IOrderRepository, OrderRepository>();
        serviceCollection.AddSingleton<ICartRepository, CartRepository>();
        serviceCollection.AddHybridCache();
        serviceCollection.Configure<MinioOptions>(configuration.GetSection("Minio"));
        serviceCollection.AddScoped<IFileService, FileService>();
        serviceCollection.AddSingleton<IMinioClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<MinioOptions>>().Value;

            return new MinioClient()
                .WithEndpoint(options.Endpoint)
                .WithCredentials(options.AccessKey, options.SecretKey)
                .WithSSL(options.UseSSL)
                .Build();
        });
    }

    static  void AddRepositories(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();
        serviceCollection.AddScoped<IProductRepository, ProductRepository>();
        serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
    }

    static void AddDbContexts(IServiceCollection serviceCollection,IConfiguration configuration)
    {
          
        serviceCollection.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ApplicationConnection")) );
       

    }
}