#region Namespace
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using ConfigManager.Interfaces;
using Microsoft.Extensions.Options;
#endregion

namespace WebAPI.Middleware
{
    #region AuthenticationExtension
    /// <summary>
    /// AuthenticationExtension
    /// </summary>
    public static class AuthenticationExtension
    {

        #region AddTokenAuthentication
        public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, ConfigManager.Interfaces.IConfigurationManager config)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(config.GetJWTConfig("Key"));

                services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = config.GetJWTConfig("Issuer"),
                        ValidAudience = config.GetJWTConfig("Audience"),
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true
                    };

                    x.Events = new JwtBearerEvents()
                    {
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            context.Response.ContentType = "application/json; charset=utf-8";
                            var message = "Authentication forbidden.";
                            var result = JsonConvert.SerializeObject(new { message });
                            return context.Response.WriteAsync(result);
                        },
                        OnAuthenticationFailed = context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json; charset=utf-8";
                            var message = "An error occurred while processing your authentication.";
                            var result = JsonConvert.SerializeObject(new { message });
                            return context.Response.WriteAsync(result);
                        }
                    };
                });
            }
            catch (Exception ex)
            {
                throw;
            }

            return services;
        }
        #endregion
    }
    #endregion
}
