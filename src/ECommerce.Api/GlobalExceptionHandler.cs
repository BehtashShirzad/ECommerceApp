 
using System.Net;
using System.Text;
using ECommerce.Domain.Core;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;

namespace ECommerce.Api;

public class GlobalExceptionHandler:IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        
        var result = GenerateErrorResponseJson(exception);
        
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)result.statusCode;
       await httpContext.Response.WriteAsJsonAsync(result.response,cancellationToken);

        return true;
    }



    (object response,HttpStatusCode statusCode) GenerateErrorResponseJson(Exception exception)
    {
        switch (exception)
        {
            case ValidationException validationException:
                return (GetValidationError(validationException),HttpStatusCode.BadRequest);
            case DomainException domainException:
                return (GetDomainError(domainException), HttpStatusCode.BadRequest);
                
            
         default:   
         return (new {Error=exception.Message},HttpStatusCode.InternalServerError);
        }
        
    }

    private object GetDomainError(DomainException domainException)
    {
        return new
        {
            ErrorType = "BusinessException",
            domainException.Message,
            
        };
    }

    static object GetValidationError(ValidationException validationException)
    {
        return new
        {
            ErrorType = "ValidationException",
            Errors = validationException.Errors.Select(_ => new { _.PropertyName, _.ErrorMessage })
        };
    }
}