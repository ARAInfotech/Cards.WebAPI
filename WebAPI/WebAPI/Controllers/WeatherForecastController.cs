using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Model;
using WebApp.Domain;
using Utilities;
using WebAPI.BLL.Interface;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        #region _weatherForecast
        /// <summary>
        /// _weatherForecast
        /// </summary>
        private readonly IWeatherForecast _weatherForecast;
        #endregion

        #region _configurationManager
        /// <summary>
        /// _configurationManager
        /// </summary>
        public ConfigManager.Interfaces.IConfigurationManager _configurationManager;
        #endregion

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ConfigManager.Interfaces.IConfigurationManager configurationManager)
        {
            _logger = logger;
            _configurationManager = configurationManager;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        [Authorize]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TempC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        #region Login
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public IActionResult Login(UserModel login)
        {
            IActionResult response = Unauthorized();

            UserModel user = this.AuthenticateUser(login);

            if (user != null)
            {
                user.Token = this.GenerateJWTWebToken(user);
                response = this.Ok(new { UserDetails = user });
            }

            return response;
        }
        #endregion

        #region Private Methods

        #region GenerateJWTWebToken
        /// <summary>
        /// GenerateJWTWebToken
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GenerateJWTWebToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF7.GetBytes(this._configurationManager.GetJWTConfig("Key")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim("UserID", user.EncUserID)
            };

            var token = new JwtSecurityToken(this._configurationManager.GetJWTConfig("Issuer"), this._configurationManager.GetJWTConfig("Issuer"),
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion

        #region AuthenticateUser
        /// <summary>
        /// AuthenticateUser
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        private UserModel AuthenticateUser(UserModel login)
        {
            UserModel user = null;

            if (login.UserName == "string" && login.Password.EncryptPassword() == "string".EncryptPassword())
            {
                user = new UserModel
                {
                    UserName = login.UserName,
                    Email = "Ajith.test@test.test",
                    EncUserID = "1001".Encrypt(),
                    Password = login.Password.EncryptPassword()
                };
            }

            return user;
        }
        #endregion

        #region MapUserDomainToModel
        /// <summary>
        /// MapUserDomainToModel
        /// </summary>
        /// <param name="dom"></param>
        /// <returns></returns>
        private UserModel MapUserDomainToModel(UserDomain dom)
        {
            UserModel model = new UserModel();

            model.Email = dom.Email;
            model.EncUserID = dom.UserID.Encrypt();
            model.UserName = dom.UserName;
            model.EncCreatedBy = dom.CreatedBy.Encrypt();
            model.CreatedDate = dom.CreatedDate;
            model.FirstName = dom.FirstName;
            model.LastName = dom.LastName;
            model.MobileNumber = dom.MobileNumber;
            model.EncModifiedBy = dom.ModifiedBy.Encrypt();
            model.ModifiedDate = dom.ModifiedDate;
            model.Token = this.GenerateJWTWebToken(model);

            return model;
        }
        #endregion

        #endregion
    }
}
