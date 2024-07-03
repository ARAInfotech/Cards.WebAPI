using WebAPI.Domain;
namespace WebAPI.BLL.Interface.Category
{
    #region Category
    /// <summary>
    /// Category
    /// </summary>
    public interface ICategoryBll
    {
        #region AddCategory
        /// <summary>
        /// AddCategory
        /// </summary>
        /// <returns></returns>
        bool AddCategory(CategoryDomain Category);
        #endregion

        #region UpdateCategory
        /// <summary>
        /// UpdateCategory
        /// </summary>
        /// <returns></returns>
        bool UpdateCategory(CategoryDomain Category);
        #endregion

        #region DeleteCategory
        /// <summary>
        /// UpdateCategory
        /// </summary>
        /// <returns></returns>
        bool DeleteCategory(long CategoryId);
        #endregion

        #region GetCategoryDetailsById
        /// <summary>
        /// GetCategoryDetailsById
        /// </summary>
        /// <returns></returns>
        CategoryDomain GetCategoryDetailsById(long CategoryId);
        #endregion

        #region GetAllCategoryDetails
        /// <summary>
        /// GetAllCategoryDetails
        /// </summary>
        /// <returns></returns>
        List<CategoryDomain> GetAllCategoryDetails();
        #endregion
    }
    #endregion
}
