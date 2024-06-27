#region NameSpace
using System;
using System.Security.Principal;
#endregion

namespace WebAPI.Domain
{
    #region OTPDomain
    /// <summary>
    /// OTPDomain
    /// </summary>
    public class OTPDomain
    {
        #region TempUserID
        /// <summary>
        /// TempUserID
        /// </summary>
        public int TempUserID { get; set; }
        #endregion

        #region OTP
        /// <summary>
        /// OTP
        /// </summary>
        public string OTP { get; set; }
        #endregion

        #region FullName
        /// <summary>
        /// FullName
        /// </summary>
        public string FullName { get; set; }
        #endregion

        #region ID
        /// <summary>
        /// ID
        /// </summary>
        public long ID { get; set; }
        #endregion

        #region Email
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        #endregion
    }
    #endregion
}
