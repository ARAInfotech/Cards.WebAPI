using Microsoft.AspNetCore.Mvc;
using Utilities;
using WebAPI.BLL;
using WebAPI.BLL.Interface.Category;
using WebAPI.BLL.Interface.Category;
using WebAPI.Common;
using WebAPI.Domain;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        #region Variables
        #region _configurationManager
        /// <summary>
        /// _configurationManager
        /// </summary>
        private ConfigManager.Interfaces.IConfigurationManager _configurationManager;
        #endregion

        #region _CategoryBLL
        /// <summary>
        /// _CategoryBLL
        /// </summary>
        private ICategoryBll _categoryBLL;
        #endregion
        #endregion

        #region Constructor
        public CategoryController(ConfigManager.Interfaces.IConfigurationManager configurationManager, ICategoryBll categoryBLL)
        {
            _configurationManager = configurationManager;
            _categoryBLL = categoryBLL;
        }
        #endregion

        #region Public Methods

        #region AddCategory
        /// <summary>
        /// AddCategory
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPost("AddCategory")]
        public APIReturnModel<string> AddCategory(CategoryModel category)
        {
            APIReturnModel<string> response = new APIReturnModel<string>();
            bool status = false;

            if (category != null)
            {
                CategoryDomain categoryDomain = this.MapCategoryModelToCategoryDomain(category);
                status = this._categoryBLL.AddCategory(categoryDomain);

                if (status)
                {
                    response = ReturnData.SuccessResponse<string>("Category added successfully.");
                }
                else
                {
                    response = ReturnData.ErrorResponse<string>("Category added failed.");
                }
            }
            else
            {
                response = ReturnData.InvalidRequestResponse<string>();
            }

            return response;
        }
        #endregion

        //#region UpdateCategory
        ///// <summary>
        ///// UpdateCategory
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost("UpdateCategory")]
        //public APIReturnModel<string> UpdateCategory(CategoryModel Category)
        //{
        //    APIReturnModel<string> response = new APIReturnModel<string>();
        //    bool status = false;

        //    if (Category != null)
        //    {
        //        CategoryDomain CategoryDomain = this.MapCategoryModelToCategoryDomain(Category);
        //        status = this._CategoryBll.UpdateCategory(CategoryDomain);

        //        if (status)
        //        {
        //            response = ReturnData.SuccessResponse<string>("Category updated successfully.");
        //        }
        //        else
        //        {
        //            response = ReturnData.ErrorResponse<string>("Category updated failed.");
        //        }
        //    }
        //    else
        //    {
        //        response = ReturnData.InvalidRequestResponse<string>();
        //    }

        //    return response;
        //}
        //#endregion

        //#region DeletCategory
        ///// <summary>
        ///// DeleteCategory
        ///// </summary>
        ///// <returns></returns>
        //[HttpDelete("DeleteCategory")]
        //public APIReturnModel<string> DeleteCategory(string Id)
        //{
        //    APIReturnModel<string> response = new APIReturnModel<string>();
        //    bool status = false;

        //    if (!string.IsNullOrEmpty(Id) && Id.DecryptToLong() > 0)
        //    {
        //        status = this._CategoryBll.DeleteCategory(Id.DecryptToLong());

        //        if (status)
        //        {
        //            response = ReturnData.SuccessResponse<string>("Category Deleted successfully.");
        //        }
        //        else
        //        {
        //            response = ReturnData.ErrorResponse<string>("Category Delete failed.");
        //        }
        //    }
        //    else
        //    {
        //        response = ReturnData.InvalidRequestResponse<string>();
        //    }

        //    return response;
        //}
        //#endregion

        //#region GetCategoryDetailsById
        ///// <summary>
        ///// GetCategoryDetailsById
        ///// </summary>
        ///// <param name="username"></param>
        ///// <returns></returns>
        //[HttpGet("GetCategoryDetailsById")]
        //public APIReturnModel<CategoryModel> GetCategoryDetailsById(string Id)
        //{
        //    APIReturnModel<CategoryModel> response = new APIReturnModel<CategoryModel>();
        //    bool status = false;

        //    if (!string.IsNullOrEmpty(Id) && Id.DecryptToLong() > 0)
        //    {
        //        var Category = this._CategoryBll.GetCategoryDetailsById(Id.DecryptToLong());
        //        if (Category != null)
        //        {
        //            CategoryModel CategoryModel = this.MapCategoryDomainToCategoryModel(Category);
        //            response = ReturnData.SuccessResponse<CategoryModel>(CategoryModel);
        //        }
        //        else
        //        {
        //            response = ReturnData.ErrorResponse<CategoryModel>("Failed to get Category details by id.");
        //        }
        //    }
        //    else
        //    {
        //        response = ReturnData.InvalidRequestResponse<CategoryModel>();
        //    }

        //    return response;
        //}
        //#endregion

        //#region GetAllCategoryDetails
        ///// <summary>
        ///// GetAllCategoryDetails
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("GetAllCategoryDetails")]
        //public APIReturnModel<List<CategoryModel>> GetAllCategoryDetails()
        //{
        //    APIReturnModel<List<CategoryModel>> response = new APIReturnModel<List<CategoryModel>>();

        //    List<CategoryDomain> Category = this._CategoryBll.GetAllCategoryDetails();
        //    if (Category.Count > 0)
        //    {
        //        List<CategoryModel> CategoryModel = this.MapCategoryDomainListToCategoryModelList(Category);

        //        response = ReturnData.SuccessResponse<List<CategoryModel>>(CategoryModel);
        //    }
        //    else
        //    {
        //        response = ReturnData.ErrorResponse<List<CategoryModel>>("Failed to get Category details");
        //    }

        //    return response;
        //}
        //#endregion

        #endregion


        #region Private Methods

        #region MapCategoryModelToCategoryDomain
        /// <summary>
        /// MapCategoryModelToCategoryDomain
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private CategoryDomain MapCategoryModelToCategoryDomain(CategoryModel model)
        {
            CategoryDomain dom = new CategoryDomain
            {
                CategoryId = model.CategoryId.DecryptToLong(),
                CategoryName = model.CategoryName,
                Description = model.Description,
                CreatedBy = string.IsNullOrEmpty(model.CreatedBy) ? 0 : model.CreatedBy.DecryptToLong(),
                CreatedByName=model.CreatedByName,
                CreatedDate = model.CreatedDate,
                ModifiedBy = string.IsNullOrEmpty(model.ModifiedBy) ? 0 : model.ModifiedBy.DecryptToLong(),
                ModifiedByName = model.ModifiedByName,
                ModifiedDate = model.ModifiedDate
            };

            return dom;
        }
        #endregion

        #region MapCategoryDomainToCategoryModel
        /// <summary>
        /// MapCategoryDomainToCategoryModel
        /// </summary>
        /// <param name="dom"></param>
        /// <returns></returns>
        private CategoryModel MapCategoryDomainToCategoryModel(CategoryDomain dom)
        {
            CategoryModel model = new CategoryModel
            {
                CategoryId = dom.CategoryId.Encrypt(),
                CategoryName = dom.CategoryName,
                Description = dom.Description,
                CreatedBy = dom.CreatedBy.Encrypt(),
                CreatedByName = dom.CreatedByName,
                CreatedDate = dom.CreatedDate,
                ModifiedBy = dom.ModifiedBy.Encrypt(),
                ModifiedByName= dom.ModifiedByName,
                ModifiedDate = dom.ModifiedDate
            };

            return model;
        }
        #endregion

        #region MapCategoryDomainListToCategoryModelList
        /// <summary>
        /// MapCategoryDomainListToCategoryModelList
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        private List<CategoryModel> MapCategoryDomainListToCategoryModelList(List<CategoryDomain> Category)
        {
            List<CategoryModel> CategoryModels = new List<CategoryModel>();

            foreach (CategoryDomain dom in Category)
            {
                CategoryModels.Add(new CategoryModel
                {
                    CategoryId = dom.CategoryId.Encrypt(),
                    CategoryName = dom.CategoryName,
                    Description = dom.Description,
                    CreatedBy = dom.CreatedBy.Encrypt(),
                    CreatedDate = dom.CreatedDate,
                    ModifiedBy = dom.ModifiedBy.Encrypt(),
                    ModifiedDate = dom.ModifiedDate,
                    CreatedByName = dom.CreatedByName,
                    ModifiedByName = dom.ModifiedByName
                });
            }

            return CategoryModels;
        }
        #endregion

        #endregion
    }
}
