using ECommerce.Application.Pipelines;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application;

public static class  DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection serviceCollection,IConfiguration configuration)
    {
        var assembly = ApplicationLayerAssembly.Assembly;
        var assemblyInfrastructure = AppDomain.CurrentDomain
            .GetAssemblies()
            .FirstOrDefault(a => a.GetName().Name == "ECommerce.Infrastructure");
        serviceCollection.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.RegisterServicesFromAssembly(assemblyInfrastructure!);
        });
       
        serviceCollection.AddValidatorsFromAssembly(assembly);
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlerBehavior<,>));
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(TransactionBehavior<,>));
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(UnitOfWorkBehavior<,>));
    }
}