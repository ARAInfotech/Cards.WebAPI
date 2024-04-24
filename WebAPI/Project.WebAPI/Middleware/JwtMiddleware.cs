using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Custom_Jwt_Token_Example.Helper
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ConfigManager.Interfaces.IConfigurationManager config;

        public JwtMiddleware(RequestDelegate _next, ConfigManager.Interfaces.IConfigurationManager _config)
        {
            this._next = _next;
            this.config = _config;

        }
        public async Task Invoke(HttpContext context)
        {

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
                //Validate Token
                attachUserToContext(context, token);
            _next(context);
        }

        private void attachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.GetJWTConfig("Key")));
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = config.GetJWTConfig("Issuer"),
                    ValidAudience = config.GetJWTConfig("Audience")
                }, out SecurityToken validateToken);

                var jwtToken = (JwtSecurityToken)validateToken;
                var userId = int.Parse(jwtToken.Claims.FirstOrDefault(_ => _.Type == "Id").Value);
            }
            catch (Exception ex)
            {


            }
        }
    }
}