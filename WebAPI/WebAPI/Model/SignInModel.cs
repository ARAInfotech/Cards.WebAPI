#region Namespace
using System;
#endregion

namespace WebAPI.Model
{
    #region SignInModel
    /// <summary>
    /// SignInModel
    /// </summary>
    public class SignInModel
    {
        #region UserName
        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { get; set; }
        #endregion

        #region Password
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        #endregion

        #region Email
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        #endregion

        #region FirstName
        /// <summary>
        /// FirstName
        /// </summary>
        public string FirstName { get; set; }
        #endregion

        #region LastName
        /// <summary>
        /// LastName
        /// </summary>
        public string LastName { get; set; }
        #endregion

        #region MobileNumber
        /// <summary>
        /// MobileNumber
        /// </summary>
        public string MobileNumber { get; set; }
        #endregion

        #region EncCreatedBy
        /// <summary>
        /// EncryptedCreatedBy
        /// </summary>
        public string EncCreatedBy { get; set; }
        #endregion

        #region CreatedDate
        /// <summary>
        /// CreatedDate
        /// </summary>
        public DateTime CreatedDate { get; set; }
        #endregion

        #region EncUserTypeID
        /// <summary>
        /// EncUserTypeID
        /// </summary>
        public string EncUserTypeID { get; set; }
        #endregion
    }
    #endregion
}
