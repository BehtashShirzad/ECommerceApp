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
        serviceCollection.AddMediatR(
            c=>
                c.RegisterServicesFromAssembly(assembly));
        serviceCollection.AddValidatorsFromAssembly(assembly);
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlerBehavior<,>));

    }
}