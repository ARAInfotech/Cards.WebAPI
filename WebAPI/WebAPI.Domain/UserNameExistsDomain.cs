#region NameSpace
using System;
#endregion

namespace WebAPI.Domain
{
    #region UserNameExistsDomain
    /// <summary>
    /// UserNameExistsDomain
    /// </summary>
    public class UserNameExistsDomain
    {
        #region UserName
        /// <summary>
        /// UserName
        /// </summary>
        public string? UserName { get; set; }
        #endregion

        #region UserNameExists
        /// <summary>
        /// UserNameExists
        /// </summary>
        public bool UserNameExists { get; set; }
        #endregion
    }
    #endregion
}
