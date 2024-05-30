#region NameSpace
using System;
#endregion

namespace WebAPI.Model
{
    #region UserTypeModel
    /// <summary>
    /// UserTypeModel
    /// </summary>
    public class UserTypeModel
    {
        #region EncUserTypeID
        /// <summary>
        /// EncryptedUserTypeID
        /// </summary>
        public string EncUserTypeID { get; set; }
        #endregion

        #region UserTypeName
        /// <summary>
        /// UserTypeName
        /// </summary>
        public string UserTypeName { get; set; }
        #endregion
    }
    #endregion
}
