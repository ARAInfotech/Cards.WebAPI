#region NameSpace
using CommunicationManager.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Utilities;
using WebAPI.BLL.Interface.Login;
using WebAPI.Common;
using WebAPI.Domain;
using WebAPI.Helpers;
using WebAPI.Interface;
using WebAPI.Model;
using WebApp.Domain;
#endregion

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        #region Variables
        #region _configurationManager
        /// <summary>
        /// _configurationManager
        /// </summary>
        private ConfigManager.Interfaces.IConfigurationManager _configurationManager;
        #endregion

        #region _loginBLL
        /// <summary>
        /// _loginBLL
        /// </summary>
        private ILoginBLL _login;
        #endregion

        #region EmailManager
        /// <summary>
        /// EmailManager
        /// </summary>
        IEmailManager _email;
        #endregion

        #endregion

        #region Constructor
        public LoginController(ConfigManager.Interfaces.IConfigurationManager configurationManager, ILoginBLL loginBLL, IEmailManager email)
        {
            _configurationManager = configurationManager;
            _login = loginBLL;
            _email = email;
        }
        #endregion

        #region Public Methods

        #region Login
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public APIReturnModel<UserModel> Login(LoginModel login)
        {
            APIReturnModel<UserModel> response = new APIReturnModel<UserModel>();

            if (login != null)
            {
                UserDomain dom = this._login.Login(login.Username, login.Password.EncryptPassword());

                if (dom != null)
                {
                    UserModel objModel = this.MapUserDomainToModel(dom);

                    response = ReturnData.SuccessResponse<UserModel>(objModel);
                }
                else
                {
                    response = ReturnData.ErrorResponse<UserModel>("Invalid username or password.");
                }
            }
            else
            {
                response = ReturnData.InvalidRequestResponse<UserModel>();
            }

            return response;
        }
        #endregion

        #region GenerateOTP
        /// <summary>
        /// GenerateOTP
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [HttpPost("GenerateOTP")]
        public APIReturnModel<string> GenerateOTP(SignInModel newUser)
        {
            APIReturnModel<string> response = new APIReturnModel<string>();

            bool status = false;

            if (newUser != null)
            {
                UserDomain dom = this.MapSignInModelToUserDomain(newUser);

                OTPDomain otp= this._login.TempUserOTPCreate(dom);

                if (otp != null)
                {
                    bool sendMailStatus = this.SendOTPEmail(dom, otp.OTP);

                    if (sendMailStatus)
                    {
                        response = ReturnData.SuccessResponse<string>(otp.TempUserID.Encrypt());
                    }
                    else
                    {
                        response = ReturnData.ErrorResponse<string>("Email sending failed.");
                    }
                }
                else
                {
                    response = ReturnData.ErrorResponse<string>("OTP creation failed.");
                }
            }
            else
            {
                response = ReturnData.InvalidRequestResponse<string>();
            }

            return response;
        }
        #endregion

        #region VerifyOTP
        /// <summary>
        /// VerifyOTP
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("VerifyOTP")]
        public APIReturnModel<SignInResponseModel> VerifyOTP(SubmitOTP model)
        {
            APIReturnModel<SignInResponseModel> response = new APIReturnModel<SignInResponseModel>();

            bool status = false;

            if (model != null)
            {
                SubmitOTPDomain dom = this.MapSubmitOTPModelToDomain(model);

                SignInResponseDomain otp = this._login.ValidateOTP(dom);

                if (otp != null)
                {
                    if (otp.IsSuccess)
                    {
                        bool sendMailStatus = this.SendRegistrationSuccessEmail(new UserDomain());

                        if (sendMailStatus)
                        {
                            SignInResponseModel otpModel = new SignInResponseModel()
                            {
                                Message = otp.Message,
                                IsSuccess = otp.IsSuccess
                            };

                            response = ReturnData.SuccessResponse<SignInResponseModel>(otpModel);
                        }
                        else
                        {
                            response = ReturnData.ErrorResponse<SignInResponseModel>("Email sending failed.");
                        }
                    }
                    else
                    {
                        response = ReturnData.ErrorResponse<SignInResponseModel>(otp.Message);
                    }
                }
                else
                {
                    response = ReturnData.ErrorResponse<SignInResponseModel>("OTP validation failed.");
                }
            }
            else
            {
                response = ReturnData.InvalidRequestResponse<SignInResponseModel>();
            }

            return response;
        }
        #endregion

        #region ChangePassword
        /// <summary>
        /// ChangePassword
        /// </summary>
        /// <param name="username"></param>
        /// <param name="newPassword"></param>
        /// <param name="oldPassword"></param>
        /// <returns></returns>
        [HttpPost("ChangePassword")]
        [Authorize]
        public APIReturnModel<string> ChangePassword(string username, string newPassword, string oldPassword)
        {
            APIReturnModel<string> response = new APIReturnModel<string>();

            if (!string.IsNullOrEmpty(username) & !string.IsNullOrEmpty(newPassword) & !string.IsNullOrEmpty(oldPassword))
            {
                UserDomain dom = this._login.Login(username, oldPassword.EncryptPassword());

                if (dom != null)
                {
                    bool status = this._login.ChangePassword(username, newPassword.EncryptPassword(), oldPassword.EncryptPassword());

                    if (status)
                    {
                        response = ReturnData.SuccessResponse<string>("Password updated successfully.");
                    }
                    else
                    {
                        response = ReturnData.ErrorResponse<string>("Password update failed.");
                    }

                }
                else
                {
                    response = ReturnData.ErrorResponse<string>("Old passwor doesn't math.");
                }
            }
            else
            {
                response = ReturnData.InvalidRequestResponse<string>();
            }

            return response;
        }
        #endregion

        #region ForgetPassword
        /// <summary>
        /// ForgetPassword
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPost("ForgetPassword")]
        public APIReturnModel<bool> ForgetPassword(string username)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region UsernameStatus
        /// <summary>
        /// UsernameStatus
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("UsernameStatus")]
        public APIReturnModel<bool> UsernameStatus(string username)
        {
            APIReturnModel<bool> response = new APIReturnModel<bool>();

            if (!string.IsNullOrEmpty(username))
            {
                bool userNameExists = this._login.UsernameExists(username);

                response = ReturnData.SuccessResponse<bool>(userNameExists);
            }
            else
            {
                response = ReturnData.InvalidRequestResponse<bool>();
            }

            return response;
        }
        #endregion

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
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configurationManager.GetJWTConfig("Key")));
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

        #region MapUserModelToDomain
        /// <summary>
        /// MapUserModelToDomain
        /// </summary>
        /// <param name="dom"></param>
        /// <returns></returns>
        private UserDomain MapUserModelToDomain(UserModel model)
        {
            UserDomain dom = new UserDomain();

            dom.Email = model.Email;
            dom.UserID = model.EncUserID.DecryptToLong();
            dom.UserName = model.UserName;
            dom.CreatedBy = model.EncCreatedBy.DecryptToLong();
            dom.CreatedDate = model.CreatedDate;
            dom.FirstName = model.FirstName;
            dom.LastName = model.LastName;
            dom.MobileNumber = model.MobileNumber;
            dom.ModifiedBy = model.EncModifiedBy.DecryptToLong();
            dom.ModifiedDate = model.ModifiedDate;

            return dom;
        }
        #endregion

        #region MapSignInModelToUserDomain
        /// <summary>
        /// MapSignInModelToUserDomain
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private UserDomain MapSignInModelToUserDomain(SignInModel model)
        {
            UserDomain dom = new UserDomain();

            dom.Email = model.Email;
            dom.UserName = model.UserName;
            //dom.CreatedBy = model.EncCreatedBy.DecryptToLong();
            //dom.CreatedDate = model.CreatedDate;
            dom.FirstName = model.FirstName;
            dom.LastName = model.LastName;
            dom.MobileNumber = model.MobileNumber;
            dom.Password = model.Password.DecryptInput().EncryptPassword();
            dom.UserTypeID = model.EncUserTypeID.DecryptToLong();

            return dom;
        }

        #endregion

        #region SendRegistrationSuccessEmail
        /// <summary>
        /// SendRegistrationSuccessEmail
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        private bool SendRegistrationSuccessEmail(UserDomain newUser)
        {
            return true;
        }
        #endregion

        #region SendOTPEmail
        /// <summary>
        /// SendOTPEmail
        /// </summary>
        /// <param name="user"></param>
        /// <param name="otp"></param>
        /// <returns></returns>
        private bool SendOTPEmail(UserDomain user, string otp)
        {
            EmailDomain dom = new EmailDomain();

            dom.Body = this.GetOTPMailBody(user, otp);
            dom.FromAddress = _configurationManager.GetEmailConfig("FromMailAddress");
            dom.Subject = "Verify your email";
            dom.ToAddress = user.Email;

            return _email.SendMail(dom);
        }

        #endregion

        #region GetOTPMailBody
        /// <summary>
        /// GetOTPMailBody
        /// </summary>
        /// <param name="user"></param>
        /// <param name="otp"></param>
        /// <returns></returns>
        private string GetOTPMailBody(UserDomain user, string otp)
        {
            string body = string.Empty;
            string path = AppDomain.CurrentDomain.BaseDirectory;

            if (path.Contains("\\bin\\"))
            {
                path = path.Split("\\bin\\")[0];
            }

            path += "\\Template\\OTP.html";

            try
            {
                string contents = System.IO.File.ReadAllText(path);

                body = contents.Replace("{{Login_URL}}", _configurationManager.GetConfigValue("LoginURL"))
                                   .Replace("{{Company_Name}}", _configurationManager.GetConfigValue("ApplicationName"))
                                   .Replace("{{Date}}", DateTime.Today.ToShortDateString())
                                   .Replace("{{Full_Name}}", user.FirstName + " " + user.LastName)
                                   .Replace("{{OTP}}", otp)
                                   .Replace("{{support_email}}", _configurationManager.GetConfigValue("ApplicationName"));
            }
            catch (Exception)
            {
                throw;
            }

            return body;
        }
        #endregion

        #region MapSubmitOTPModelToDomain
        /// <summary>
        /// MapSubmitOTPModelToDomain
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private SubmitOTPDomain MapSubmitOTPModelToDomain(SubmitOTP model)
        {
            return new SubmitOTPDomain()
            {
                OTP = model.OTP,
                UserID = Convert.ToInt32(model.UserID.Decrypt())
            };
        }
        #endregion
    }
    #endregion
}
