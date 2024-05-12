#region Namespace
using System;
#endregion

namespace WebAPI.Model
{
    #region SubmitOTP
    /// <summary>
    /// SubmitOTP
    /// </summary>
    public class SubmitOTP
    {
        #region UserID
        /// <summary>
        /// UserID
        /// </summary>
        public string UserID { get; set; }
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
