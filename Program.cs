using Scalar.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ParrotShopBackend.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ParrotShopBackend.Application.Services;
using ParrotShopBackend.Infrastructure.Repos;
using FluentValidation;
using FluentValidation.AspNetCore;
using ParrotShopBackend.Application.Exceptions;
using Hangfire;
using Hangfire.PostgreSql;
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

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddDbContext<ShopContext>(option => { option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")); });
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IRevokedJWTRepository, RevokedJWTRepository>();
builder.Services.AddTransient<GlobalExceptionHandling>();

builder.Services.AddHangfire(conf =>
{
    conf.UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UsePostgreSqlStorage(builder.Configuration.GetConnectionString("DefaultConnection")!);

});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
        conf =>
        {
            conf.RequireHttpsMetadata = false;
            conf.Audience = builder.Configuration["API:Audience"];
            conf.SaveToken = true;
            conf.TokenValidationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                builder.Configuration["SecSettings:SecretKey"]!
            )),
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["API:Issuer"],
                ValidateAudience = true,
                ValidAudience = builder.Configuration["API:Audience"],
                ClockSkew = TimeSpan.FromMinutes(2)
            };
            conf.Events = new JwtBearerEvents
            {
                OnMessageReceived = async context =>
                {
                    context.Token = context.Request.Cookies["AccessToken"];
                },
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine($"Auth Failed: {context.Exception.Message}");
                    return Task.CompletedTask;
                },
                OnTokenValidated = async context =>
                {
                    var db = context.HttpContext.RequestServices.GetRequiredService<ShopContext>();
                    var TokenToCheck = context
                                            .HttpContext
                                            .Request
                                            .Cookies["AccessToken"]
                                            ?? context
                                            .HttpContext
                                            .Request
                                            .Headers
                                            .Authorization
                                            .ToString()
                                            .Replace("Bearer ", "");
                    var isRevoked = await db.revokedJWTs.AnyAsync(x => x.Token == TokenToCheck);
                    if (isRevoked) context.Fail("Token has been revoked.");
                }
            };
        }
    );


builder.Services.AddHangfireServer();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandling>();

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


using (var scope = app.Services.CreateScope())
{
    var _recurringJob = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
    _recurringJob.AddOrUpdate<IAuthService>(
        "jwt-clean",
        service => service.ClearExpiredTokensAsync(),
        Cron.Daily
    );
}



app.Run();
