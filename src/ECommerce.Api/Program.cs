using ECommerce.Api;
using ECommerce.Api.ApiConfiguration;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.Options;
using Microsoft.Extensions.Options;

DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddECommerceServices(builder.Configuration);
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings= true;
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("https://behtashshirzad.ir")
            .AllowAnyHeader()
            .AllowAnyMethod();
            #if  DEBUG
                    policy.WithOrigins("http://localhost:5173") // آدرس فرانت‌اند شما
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
            #endif
    });
    
});
var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    await DatabaseSeeder.SeedAsync(scope.ServiceProvider);
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
// app.UseAuthentication();
// app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerService();
    app.MapOpenApi();
}


 
app.MapControllers();
app.Run();
 