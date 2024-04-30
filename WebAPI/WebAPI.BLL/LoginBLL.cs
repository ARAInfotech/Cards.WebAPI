﻿#region NameSpace
using Dapper;
using System.Linq;
using WebAPI.BLL.Interface.Login;
using WebAPI.DAL;
using WebAPI.DAL.Interface;
using WebAPI.Domain;
using WebApp.Domain;
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
            
            UserNameExistsDomain user = _iDALRepository.Select<UserNameExistsDomain>("[dbo].[UserByUsernameSelect]", param).FirstOrDefault();

            return user.UserNameExists;
        }
        #endregion

        #endregion
    }
    #endregion
}
