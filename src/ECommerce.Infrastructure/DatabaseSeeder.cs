using System.Security.Cryptography;
using ECommerce.Application.Abstractions.Contracts;
using ECommerce.Application.Abstractions.Contracts.Services.Identity;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Aggregates.Customer;
using ECommerce.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ECommerce.Infrastructure;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider services,IConfiguration configuration)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var userManger = services.GetRequiredService<UserManager<AppUser>>();
        var customerRepository = services.GetRequiredService<ICustomerRepository>();
        var unitOfWork = services.GetRequiredService<IUnitOfWork>();
       
        
        string[] roles =
        {
            AppRoles.Admin,
            AppRoles.User
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>
                {
                    Name = role
                });
            }
        }

        string adminUseername =  configuration.GetSection("AdminConfigs:username")!.Value!;
       
        var admin =await userManger.FindByNameAsync(adminUseername);
        if (admin == null)
        {
            string email =  configuration.GetSection("AdminConfigs:email")!.Value!;
            string phoneNumber =  configuration.GetSection("AdminConfigs:phoneNumber")!.Value!;
            string firstName =  configuration.GetSection("AdminConfigs:firstName")!.Value!;
            string lastName =  configuration.GetSection("AdminConfigs:lastName")!.Value!;
            var appuser = new AppUser()
            {
                UserName =adminUseername,
                Email =  email,
                EmailConfirmed = true,
                PhoneNumber = phoneNumber,
                PhoneNumberConfirmed = true
                
                
            };

            //passwordFrom env
            var passwd = configuration.GetSection("AdminConfigs:password")!.Value!;
            await userManger.CreateAsync(appuser, passwd);
            await userManger.AddToRoleAsync(appuser, AppRoles.Admin);

            var customer = Customer.Create(firstName, lastName, phoneNumber, appuser.Id, email);
            await customerRepository.AddAsync(customer);
            await unitOfWork.SaveChangesAsync();
        }
    }
     
}