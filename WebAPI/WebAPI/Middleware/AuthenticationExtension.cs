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
            var key = Encoding.ASCII.GetBytes(config.GetJWTConfig("Key"));


            services.AddAuthentication(cfg => {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8
                        .GetBytes(config.GetJWTConfig("Key"))
                    ),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
        #endregion
    }
    #endregion
}
