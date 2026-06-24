namespace ECommerce.Api.ApiConfiguration;

public static class SwaggerMiddleware 
{
   public static void UseSwaggerService(this IApplicationBuilder app)
   {
      app.UseSwagger();
      app.UseSwaggerUI();
   }
}