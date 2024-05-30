using Dapper;
using WebAPI.BLL.Interface.Product;
using WebAPI.DAL.Interface;
using WebAPI.Domain;

namespace WebAPI.BLL
{
    #region Product
    /// <summary>
    /// LoginBLL
    /// </summary>
    public class ProductBLL : IProductBLL
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
        public ProductBLL(IDALRepository idALRepository)
        {
            _iDALRepository = idALRepository;
        }
        #endregion

        #region AddProduct
        /// <summary>
        /// AddProduct
        /// </summary>
        /// <returns></returns>
        public bool AddProduct(ProductDomain product)
        {
            DynamicParameters param = new DynamicParameters();
            //param.Add("@ProductId", product.ProductId);
            param.Add("@Name", product.Name);
            param.Add("@Description", product.Description);
            param.Add("@Url", product.Url);
            param.Add("@CategoryId", product.CategoryId);
            param.Add("@Rate", product.Rate);
            param.Add("@CreatedBy", product.CreatedBy);
            param.Add("@CreatedDate", product.CreatedDate);


            return _iDALRepository.Add("[dbo].[ProductCreate]", param);
        }

        public bool DeleteProduct(long productId)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@ProductId",productId);

            return _iDALRepository.Add("[dbo].[ProductDelete]", param);
        }

        public List<ProductDomain> GetAllProductDeteail()
        {
            return _iDALRepository.SelectAll<ProductDomain>("[dbo].[ProductAllDeteailsSelect]");
        }

        public ProductDomain GetProductDeteailsById(long productId)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@ProductId", productId);

            return _iDALRepository.Select<ProductDomain>("[dbo].[ProductDeteailsByIdSelect]", param).FirstOrDefault();
        }

        public bool UpdateProduct(ProductDomain product)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@ProductId", product.ProductId);
            param.Add("@Name", product.Name);
            param.Add("@Description", product.Description);
            param.Add("@Url", product.Url);
            param.Add("@CategoryId", product.CategoryId);
            param.Add("@Rate", product.Rate);
            param.Add("@ModifiedBy", product.ModifiedBy);
            param.Add("@ModifiedDate", product.ModifiedDate);


            return _iDALRepository.Add("[dbo].[ProductUpdate]", param);
        }
        #endregion

    }
    #endregion
}
