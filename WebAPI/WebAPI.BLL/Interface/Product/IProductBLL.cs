using WebAPI.Domain;

namespace WebAPI.BLL.Interface.Product
{
    #region Product
    /// <summary>
    /// Product
    /// </summary>
    /// <returns></returns>
    public interface IProductBLL
    {
        #region AddProduct
        /// <summary>
        /// AddProduct
        /// </summary>
        /// <returns></returns>
        bool AddProduct(ProductDomain product);
        #endregion

        #region UpdateProduct
        /// <summary>
        /// UpdateProduct
        /// </summary>
        /// <returns></returns>
        bool UpdateProduct(ProductDomain product);
        #endregion

        #region DeleteProduct
        /// <summary>
        /// UpdateProduct
        /// </summary>
        /// <returns></returns>
        bool DeleteProduct(long productId);
        #endregion

        #region GetProductDetailsById
        /// <summary>
        /// GetProductDetailsById
        /// </summary>
        /// <returns></returns>
        ProductDomain GetProductDetailsById(long productId);
        #endregion

        #region GetAllProductDetails
        /// <summary>
        /// GetAllProductDetails
        /// </summary>
        /// <returns></returns>
        List<ProductDomain> GetAllProductDetails();
        #endregion
    }
    #endregion
}
