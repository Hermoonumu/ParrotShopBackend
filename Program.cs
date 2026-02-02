using Scalar.AspNetCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // This makes your Enum show up as strings in the JSON response
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseHttpsRedirection();
app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (ctx, next) =>
{
    app.Logger.LogInformation($"The request was initiated on {DateTime.Now.ToString()}");
    app.Logger.LogInformation($"The request had this auth header: {ctx.Request.Headers.Authorization}");
    await next.Invoke(ctx);
});

app.MapControllers();






app.Run();
