using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Utilities;
using WebAPI.BLL.Interface.Login;
using WebAPI.BLL.Interface.User;
using WebAPI.Common;
using WebAPI.Domain;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Variables
        #region _configurationManager
        /// <summary>
        /// _configurationManager
        /// </summary>
        private ConfigManager.Interfaces.IConfigurationManager _configurationManager;
        #endregion

        #region _user
        /// <summary>
        /// user
        /// </summary>
        private IUserBLL _user;
        #endregion
        #endregion

        #region Constructor
        public UserController(ConfigManager.Interfaces.IConfigurationManager configurationManager, IUserBLL user)
        {
            _configurationManager = configurationManager;
            _user = user;
        }
        #endregion

        #region Public Methods

        #region GetCustomerUserType
        /// <summary>
        /// GetCustomerUserType
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCustomerUserType")]
        public APIReturnModel<string> GetCustomerUserType()
        {
            APIReturnModel<string> response = new APIReturnModel<string>();

            string s = "efVEbe2XjAQ/qXzzsrFOKQ==".DecryptInput();

            List<UserTypeDomain> dom = this._user.GetCustomerUserType();

            if (dom != null)
            {
                List<UserTypeModel> model = this.MapUserTypeDomainToModel(dom);

                string CustomerUserTypeId = model.Find(f => f.UserTypeName == "Customer").EncUserTypeID;

                response = ReturnData.SuccessResponse<string>(CustomerUserTypeId);
            }
            else
            {
                response = ReturnData.InvalidRequestResponse<string>();
            }
            return response;
        }
        #endregion

        #endregion

        #region Private Methods

        #region MyRegion

        private List<UserTypeModel> MapUserTypeDomainToModel(List<UserTypeDomain> dom)
        {
            List<UserTypeModel> model = new List<UserTypeModel>();

            foreach (UserTypeDomain data in dom) {
                model.Add(new UserTypeModel()
                {
                    EncUserTypeID = data.UserTypeID.Encrypt(),
                    UserTypeName = data.UserTypeName
                });
            }

            return model;
        }
        #endregion

        #endregion
    }
}
