#region Namespace
using System;
#endregion

namespace WebAPI.Model
{
    #region LoginModel
    /// <summary>
    /// LoginModel
    /// </summary>
    public class LoginModel
    {
        #region Username
        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }
        #endregion

        #region Password
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        #endregion
    }
    #endregion
}
