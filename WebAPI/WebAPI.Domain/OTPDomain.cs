#region NameSpace
using System;
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
    }
    #endregion
}
