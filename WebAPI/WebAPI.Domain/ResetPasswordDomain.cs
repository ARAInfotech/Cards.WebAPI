#region NameSpace
using System;
#endregion

namespace WebAPI.Domain
{
    #region ResetPasswordDomain
    /// <summary>
    /// ResetPasswordDomain
    /// </summary>
    public class ResetPasswordDomain
    {
        #region UserID
        /// <summary>
        /// UserID
        /// </summary>
        public long UserID { get; set; }
        #endregion

        #region Email
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        #endregion

        #region Password
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        #endregion

        #region RequestGeneratedTime
        /// <summary>
        /// RequestGeneratedTime
        /// </summary>
        public DateTime RequestGeneratedTime { get; set; }
        #endregion
    }
    #endregion
}
