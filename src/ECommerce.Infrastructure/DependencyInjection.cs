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
using ECommerce.Infrastructure.Contracts;
using ECommerce.Infrastructure.Options;
using ECommerce.Infrastructure.Persistence;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Infrastructure.Services;
using ECommerce.Infrastructure.Services.Identity;
using ECommerce.Infrastructure.Services.Notification;
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

        serviceCollection.AddHttpClient();
        AddDbContexts(serviceCollection,configuration);
        AddRepositories(serviceCollection);
        AddObjectStorageServices(serviceCollection,configuration); 
        AddUserManagementServices(serviceCollection,configuration);
        AddNotificationServices(serviceCollection,configuration);
        AddOptions(serviceCollection,configuration);
        serviceCollection.AddScoped<ICurrentUser, CurrentUser>();
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        serviceCollection.AddScoped<ITransactionManager, TransactionManager>();
        serviceCollection.AddHybridCache();
       
        
    }

    private static void AddOptions(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<MinioOptions>(configuration.GetSection("Minio"));
        serviceCollection.Configure<SmsProviderOptions>(configuration.GetSection("SmsProvider"));
        serviceCollection.Configure<JwtOptions>(configuration.GetSection("Jwt"));
        serviceCollection.Configure<EmailProviderOptions>(configuration.GetSection("EmailProvider"));
        serviceCollection.Configure<GoogleOptions>(configuration.GetSection("Google"));
    }

    private static void AddNotificationServices(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        
        serviceCollection.AddScoped<IEmailService,EmailService>();
        serviceCollection.AddScoped<ISmsService,SmsService>();
        serviceCollection.AddScoped<ISmsServiceProvider, SmsServiceProvider>();
        serviceCollection.AddScoped<IEmailClient, EmailClient>();
    }

    private static void AddObjectStorageServices(IServiceCollection serviceCollection, IConfiguration configuration)
    {
      
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

    static void AddUserManagementServices(IServiceCollection serviceCollection, IConfiguration configuration)
    {
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
        
        serviceCollection.AddScoped<IUserManagerService, UserManagerService>();
        serviceCollection.AddScoped<IJwtService, JwtService>();
        serviceCollection.AddScoped<IIdentityService, IdentityService>();
        serviceCollection.AddScoped<ITokenService, TokenService>();
        serviceCollection.AddScoped<IPasswordService, PasswordService>();
        serviceCollection.AddScoped<IRoleService, RoleService>();
        serviceCollection.AddScoped<IGoogleService, GoogleService>();
    }
    static  void AddRepositories(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();
        serviceCollection.AddScoped<IProductRepository, ProductRepository>();
        serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
        serviceCollection.AddScoped<IOrderRepository, OrderRepository>();
        serviceCollection.AddSingleton<ICartRepository, CartRepository>();
    }

    static void AddDbContexts(IServiceCollection serviceCollection,IConfiguration configuration)
    {
          
        serviceCollection.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ApplicationConnection")) );
       

    }
}