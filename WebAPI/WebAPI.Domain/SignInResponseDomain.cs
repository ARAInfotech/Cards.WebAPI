#region NameSpace
using System;
#endregion

namespace WebAPI.Domain
{
    #region SignInResponseDomain
    /// <summary>
    /// SignInResponseDomain
    /// </summary>
    public class SignInResponseDomain
    {
        #region IsSuccess
        /// <summary>
        /// IsSuccess
        /// </summary>
        public bool IsSuccess { get; set; }
        #endregion

        #region Message
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }
        #endregion
    }
    #endregion
}
