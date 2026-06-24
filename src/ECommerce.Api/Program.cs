using ECommerce.Api;
using ECommerce.Api.ApiConfiguration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddECommerceServices(builder.Configuration);
var app = builder.Build();
app.MapControllers();

// app.UseAuthentication();
// app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerService();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

 
 
app.Run();
 