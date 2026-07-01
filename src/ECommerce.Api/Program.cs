using ECommerce.Api;
using ECommerce.Api.ApiConfiguration;
using ECommerce.Infrastructure;

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
var app = builder.Build();
using(var scope = app.Services.CreateScope())
{
    await DatabaseSeeder.SeedAsync(scope.ServiceProvider);
}
app.MapControllers();
app.UseExceptionHandler();
// app.UseAuthentication();
// app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerService();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

 
 
app.Run();
 