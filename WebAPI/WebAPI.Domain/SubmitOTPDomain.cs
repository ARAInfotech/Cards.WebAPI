#region Namespace
using System;
#endregion

namespace WebAPI.Domain
{
    #region SubmitOTPDomain
    /// <summary>
    /// SubmitOTPDomain
    /// </summary>
    public class SubmitOTPDomain
    {
        #region UserID
        /// <summary>
        /// UserID
        /// </summary>
        public long UserID { get; set; }
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
