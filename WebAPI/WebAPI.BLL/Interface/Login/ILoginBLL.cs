#region NameSpace
using WebApp.Domain;
#endregion

namespace WebAPI.BLL.Interface.Login
{
    #region ILoginBLL
    /// <summary>
    /// ILoginBLL
    /// </summary>
    public interface ILoginBLL
    {
        #region Login
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="encryptedPassword"></param>
        /// <returns></returns>
        UserDomain Login(string username, string encryptedPassword);
        #endregion

        #region SignIn
        /// <summary>
        /// SignIn
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        bool SignIn(UserDomain newUser);
        #endregion

        #region ResetPassword
        /// <summary>
        /// ResetPassword
        /// </summary>
        /// <param name="username"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool ResetPassword(string username, string newPassword);
        #endregion

        #region ChangePassword
        /// <summary>
        /// ChangePassword
        /// </summary>
        /// <param name="username"></param>
        /// <param name="newPassword"></param>
        /// <param name="oldPassword"></param>
        /// <returns></returns>
        bool ChangePassword(string username, string newPassword, string oldPassword);
        #endregion

        #region UsernameExists
        /// <summary>
        /// UsernameExists
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        bool UsernameExists(string username);
        #endregion
    }
    #endregion
}
