using ConfigManager;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using WebAPI.BLL;
using WebAPI.BLL.DI;
using WebAPI.BLL.Interface;
using WebAPI.BLL.Interface.Login;
using WebAPI.Helpers;
using WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRepository();
builder.Services.AddControllers(options => options.Filters.Add(new HttpResponseExceptionFilter()));


builder.Services.AddSingleton<ConfigManager.Interfaces.IConfigurationManager, WebConfigManager>();
builder.Services.AddTokenAuthentication(new WebConfigManager(builder.Configuration));

builder.Services.AddScoped<IWeatherForecast, WeatherForecastBLL>();
builder.Services.AddScoped<ILoginBLL, LoginBLL>();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
                });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
