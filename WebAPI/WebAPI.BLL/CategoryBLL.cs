using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Interface.Category;
using WebAPI.DAL.Interface;
using WebAPI.Domain;

namespace WebAPI.BLL
{
    public class CategoryBLL : ICategoryBll
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
        public CategoryBLL(IDALRepository idALRepository)
        {
            _iDALRepository = idALRepository;
        }
        #endregion

        #region AddCategory
        /// <summary>
        /// AddCategory
        /// </summary>
        /// <returns></returns>
        public bool AddCategory(CategoryDomain Category)
        {
            DynamicParameters param = new DynamicParameters();
            //param.Add("@CategoryId", Category.CategoryId);
            param.Add("@CategoryName", Category.CategoryName);
            param.Add("@Description", Category.Description);
            param.Add("@CreatedBy", Category.CreatedBy);
            param.Add("@CreatedDate", Category.CreatedDate);

            return _iDALRepository.Add("[dbo].[CategoryCreate]", param);
        }

        public bool DeleteCategory(long CategoryId)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@CategoryId", CategoryId);

            return _iDALRepository.Add("[dbo].[CategoryDelete]", param);
        }

        public List<CategoryDomain> GetAllCategoryDetails()
        {
            return _iDALRepository.SelectAll<CategoryDomain>("[dbo].[CategoryAllDetailsSelect]");
        }

        public CategoryDomain GetCategoryDetailsById(long CategoryId)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@CategoryId", CategoryId);

            return _iDALRepository.Select<CategoryDomain>("[dbo].[CategoryDetailsByIdSelect]", param).FirstOrDefault();
        }

        public bool UpdateCategory(CategoryDomain Category)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@CategoryId", Category.CategoryId);
            param.Add("@CategoryName", Category.CategoryName);
            param.Add("@Description", Category.Description);
            param.Add("@CategoryId", Category.CategoryId);
            param.Add("@ModifiedBy", Category.ModifiedBy);
            param.Add("@ModifiedDate", Category.ModifiedDate);


            return _iDALRepository.Add("[dbo].[CategoryUpdate]", param);
        }
        #endregion
    }
}
