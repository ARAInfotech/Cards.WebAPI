#region NameSpace
using Dapper;
using System.Linq;
using WebAPI.BLL.Interface.Login;
using WebAPI.DAL;
using WebAPI.DAL.Interface;
using WebAPI.Domain;
using WebApp.Domain;
using static System.Net.WebRequestMethods;
#endregion

namespace WebAPI.BLL
{
    #region LoginBLL
    /// <summary>
    /// LoginBLL
    /// </summary>
    public class LoginBLL : ILoginBLL
    {
        #region Variables
        /// <summary>
        /// _iDALRepository
        /// </summary>
        IDALRepository _iDALRepository;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="idALRepository"></param>
        /// <param name=""></param>
        public LoginBLL(IDALRepository idALRepository)
        {
            _iDALRepository = idALRepository;
        }
        #endregion

        #region Public Methods

        #region Login
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="encryptedPassword"></param>
        /// <returns></returns>
        public UserDomain Login(string username, string encryptedPassword)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@Username", username);
            param.Add("@Password", encryptedPassword);

            UserDomain user = _iDALRepository.Select<UserDomain>("[dbo].[UserByCredentialsSelect]", param).FirstOrDefault();

            return user;
        }
        #endregion

        #region ResetPassword
        /// <summary>
        /// ResetPassword
        /// </summary>
        /// <param name="username"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool ResetPassword(string username, string newPassword)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region SignIn
        /// <summary>
        /// SignIn
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public bool SignIn(UserDomain newUser)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@Username", newUser.UserName);
            param.Add("@Password", newUser.Password);
            param.Add("@CreatedBy", newUser.CreatedBy);
            param.Add("@CreatedDate", newUser.CreatedDate);
            param.Add("@Email", newUser.Email);
            param.Add("@FirstName", newUser.FirstName);
            param.Add("@LastName", newUser.LastName);
            param.Add("@MobileNumber", newUser.MobileNumber);
            param.Add("@UserTypeID", newUser.UserTypeID);

            return _iDALRepository.Add("[dbo].[UserSignInCreate]", param);
        }
        #endregion

        #region TempUserOTPCreate
        /// <summary>
        /// TempUserOTPCreate
        /// </summary>
        /// <param name="tempUser"></param>
        /// <returns></returns>
        public OTPDomain TempUserOTPCreate(UserDomain tempUser)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@Username", tempUser.UserName);
            param.Add("@Password", tempUser.Password);
            param.Add("@Email", tempUser.Email);
            param.Add("@FirstName", tempUser.FirstName);
            param.Add("@LastName", tempUser.LastName);
            param.Add("@MobileNumber", tempUser.MobileNumber);
            param.Add("@UserTypeID", tempUser.UserTypeID);

            return _iDALRepository.Select<OTPDomain>("[dbo].[TempUserOTPCreate]", param).FirstOrDefault();
        }
        #endregion

        #region ChangePassword
        /// <summary>
        /// ChangePassword
        /// </summary>
        /// <param name="username"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool ChangePassword(string username, string newPassword, string oldPassword)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@Username", username);
            param.Add("@NewPassword", newPassword);
            param.Add("@OldPassword", oldPassword);

            return _iDALRepository.Add("[dbo].[UserPasswordUpdate]", param);
        }
        #endregion

        #region UsernameExists
        /// <summary>
        /// UsernameExists
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool UsernameExists(string username)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@Username", username); 
            
            UserNameExistsDomain user = _iDALRepository.Select<UserNameExistsDomain>("[dbo].[UsernameExistsSelect]", param).FirstOrDefault();

            return user.UserNameExists;
        }
        #endregion

        #region ValidateSignInOTP
        /// <summary>
        /// ValidateSignInOTP
        /// </summary>
        /// <param name="otp"></param>
        /// <returns></returns>
        public SignInResponseDomain ValidateSignInOTP(SubmitOTPDomain otp)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@OTP", otp.OTP);
            param.Add("@UserID", otp.UserID);

            return _iDALRepository.Select<SignInResponseDomain>("[dbo].[OTPByUserIDSelect]", param).FirstOrDefault();
        }
        #endregion

        #region GenerateForgotPasswordOTP
        /// <summary>
        /// GenerateForgotPasswordOTP
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public OTPDomain GenerateForgotPasswordOTP(string email)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@email", email);

            return _iDALRepository.Select<OTPDomain>("[dbo].[OTPByEmailSelect]", param).FirstOrDefault();
        }
        #endregion

        #region ValidateForgotPasswordOTP
        /// <summary>
        /// ValidateForgotPasswordOTP
        /// </summary>
        /// <param name="otp"></param>
        /// <returns></returns>
        public SignInResponseDomain ValidateForgotPasswordOTP(SubmitOTPDomain otp)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@OTP", otp.OTP);
            param.Add("@ID", otp.ID);

            return _iDALRepository.Select<SignInResponseDomain>("[dbo].[OTPByIDSelect]", param).FirstOrDefault();
        }
        #endregion

        #region GetUserDetailsByOTPID
        /// <summary>
        /// GetUserDetailsByOTPID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserDomain GetUserDetailsByOTPID(long id)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@OTPID", id);

            return _iDALRepository.Select<UserDomain>("[dbo].[UserByOTPIDSelect]", param).FirstOrDefault();
        }
        #endregion

        #region UserPasswordReset
        /// <summary>
        /// UserPasswordReset
        /// </summary>
        /// <param name="dom"></param>
        /// <returns></returns>
        public ResetPasswordResponseDomain UserPasswordReset(ResetPasswordDomain dom)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@UserID", dom.UserID);
            param.Add("@RequestGeneratedTime", dom.RequestGeneratedTime);
            param.Add("@Email", dom.Email);
            param.Add("@Password", dom.Password);
            param.Add("@CurrentTime", DateTime.Now);

            return _iDALRepository.Select<ResetPasswordResponseDomain>("[dbo].[UserByUserIDPasswordUpdate]", param).FirstOrDefault();
        }
        #endregion

        #region GetUserDetailsByUserID
        /// <summary>
        /// GetUserDetailsByUserID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public UserDomain GetUserDetailsByUserID(long userID)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@UserID", userID);

            return _iDALRepository.Select<UserDomain>("[dbo].[UserByUserIDSelect]", param).FirstOrDefault();
        }
        #endregion

        #endregion
    }
    #endregion
}
