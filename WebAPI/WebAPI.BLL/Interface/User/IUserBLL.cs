using System;
using WebAPI.Domain;

#region NameSpace
using WebApp.Domain;
#endregion

namespace WebAPI.BLL.Interface.User
{
    #region IUserBLL
    /// <summary>
    /// IUserBLL
    /// </summary>
    public interface IUserBLL
    {
        #region GetCustomerUserType
        /// <summary>
        /// GetCustomerUserType
        /// </summary>
        /// <returns></returns>
        List<UserTypeDomain> GetCustomerUserType();
        #endregion
    }
    #endregion
}
