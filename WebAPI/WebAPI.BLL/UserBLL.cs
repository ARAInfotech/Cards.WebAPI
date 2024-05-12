#region NameSpace
using Dapper;
using System.Linq;
using WebAPI.BLL.Interface.Login;
using WebAPI.BLL.Interface.User;
using WebAPI.DAL;
using WebAPI.DAL.Interface;
using WebAPI.Domain;
using WebApp.Domain;
#endregion

namespace WebAPI.BLL
{
    public class UserBLL : IUserBLL
    {

        #region Variables
        /// <summary>
        /// _iDALRepository
        /// </summary>
        IDALRepository _iDALRepository;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="idALRepository"></param>
        /// <param name=""></param>
        public UserBLL(IDALRepository idALRepository)
        {
            _iDALRepository = idALRepository;
        }
        #endregion

        #region Public Methods

        #region GetCustomerUserType
        /// <summary>
        /// GetCustomerUserType
        /// </summary>
        /// <returns></returns>
        public List<UserTypeDomain> GetCustomerUserType()
        {
            DynamicParameters param = new DynamicParameters();

            List<UserTypeDomain> user = _iDALRepository.Select<UserTypeDomain>("[dbo].[UserTypeAllSelect]", param);

            return user;
        }
        #endregion
        #endregion
    }
}
