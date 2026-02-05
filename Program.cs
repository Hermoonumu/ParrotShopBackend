using Scalar.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ParrotShopBackend.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
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


builder.Services.AddDbContext<ShopContext>(option => { option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")); });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
        conf =>
        {
            conf.RequireHttpsMetadata = false;
            conf.Audience = builder.Configuration["API:Audience"];
            conf.Authority = builder.Configuration["API:Authority"];
            conf.SaveToken = true;
            conf.TokenValidationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                builder.Configuration["SecSettings:SecretKey"]!
            )),
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["API:Authority"],
                ValidateAudience = true,
                ValidAudience = builder.Configuration["API:Audience"],
                ClockSkew = TimeSpan.FromMinutes(2)
            };
            conf.Events = new JwtBearerEvents
            {
                OnTokenValidated = async context =>
                {
                    var db = context.HttpContext.RequestServices.GetRequiredService<ShopContext>();
                    if (context.SecurityToken is JwtSecurityToken accessToken)
                    {
                        var tokenString = accessToken.RawData;
                        var isRevoked = await db.revokedJWTs.AnyAsync(x => x.Token == tokenString);
                        if (isRevoked) context.Fail("Token has been revoked.");
                    }
                }
            };
        }
    );



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
