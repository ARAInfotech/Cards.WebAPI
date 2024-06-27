using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Utilities;
using WebAPI.BLL.Interface.Product;
using WebAPI.Common;
using WebAPI.Domain;
using WebAPI.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        #region Variables
        #region _configurationManager
        /// <summary>
        /// _configurationManager
        /// </summary>
        private ConfigManager.Interfaces.IConfigurationManager _configurationManager;
        #endregion

        #region _productBll
        /// <summary>
        /// _productBll
        /// </summary>
        private IProductBLL _productBll;
        #endregion
        #endregion

        #region Constructor
        public ProductController(ConfigManager.Interfaces.IConfigurationManager configurationManager, IProductBLL productBLL)
        {
            _configurationManager = configurationManager;
            _productBll = productBLL;
        }
        #endregion

        #region Public Methods

        #region AddProduct
        /// <summary>
        /// AddProduct
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPost("AddProduct")]
        public APIReturnModel<string> AddProduct(ProductModel product)
        {
            APIReturnModel<string> response = new APIReturnModel<string>();
            bool status = false;

            if (product != null)
            {
                ProductDomain productDomain = this.MapProductModelToProductDomain(product);
                status = this._productBll.AddProduct(productDomain);

                if (status)
                {
                    response = ReturnData.SuccessResponse<string>("Product added successfully.");
                }
                else
                {
                    response = ReturnData.ErrorResponse<string>("Product added failed.");
                }
            }
            else
            {
                response = ReturnData.InvalidRequestResponse<string>();
            }

            return response;
        }
        #endregion

        #region UpdateProduct
        /// <summary>
        /// UpdateProduct
        /// </summary>
        /// <returns></returns>
        [HttpPost("UpdateProduct")]
        public APIReturnModel<string> UpdateProduct(ProductModel product)
        {
            APIReturnModel<string> response = new APIReturnModel<string>();
            bool status = false;

            if (product != null)
            {
                ProductDomain productDomain = this.MapProductModelToProductDomain(product);
                status = this._productBll.UpdateProduct(productDomain);

                if (status)
                {
                    response = ReturnData.SuccessResponse<string>("Product updated successfully.");
                }
                else
                {
                    response = ReturnData.ErrorResponse<string>("Product updated failed.");
                }
            }
            else
            {
                response = ReturnData.InvalidRequestResponse<string>();
            }

            return response;
        }
        #endregion

        #region DeletProduct
        /// <summary>
        /// DeleteProduct
        /// </summary>
        /// <returns></returns>
        [HttpDelete("DeleteProduct")]
        public APIReturnModel<string> DeleteProduct(string Id)
        {
            APIReturnModel<string> response = new APIReturnModel<string>();
            bool status = false;

            if (!string.IsNullOrEmpty(Id) && Id.DecryptToLong() > 0)
            {
                status = this._productBll.DeleteProduct(Id.DecryptToLong());

                if (status)
                {
                    response = ReturnData.SuccessResponse<string>("Product Deleted successfully.");
                }
                else
                {
                    response = ReturnData.ErrorResponse<string>("Product Delete failed.");
                }
            }
            else
            {
                response = ReturnData.InvalidRequestResponse<string>();
            }

            return response;
        }
        #endregion

        #region GetProductDetailsById
        /// <summary>
        /// GetProductDetailsById
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("GetProductDetailsById")]
        public APIReturnModel<ProductModel> GetProductDetailsById(string Id)
        {
            APIReturnModel<ProductModel> response = new APIReturnModel<ProductModel>();
            bool status = false;

            if (!string.IsNullOrEmpty(Id) && Id.DecryptToLong() > 0)
            {
                var product = this._productBll.GetProductDetailsById(Id.DecryptToLong());
                if (product != null)
                {
                    ProductModel productModel = this.MapProductDomainToProductModel(product);
                    response = ReturnData.SuccessResponse<ProductModel>(productModel);
                }
                else
                {
                    response = ReturnData.ErrorResponse<ProductModel>("Failed to get product details by id.");
                }
            }
            else
            {
                response = ReturnData.InvalidRequestResponse<ProductModel>();
            }

            return response;
        }
        #endregion

        #region GetAllProductDetails
        /// <summary>
        /// GetAllProductDetails
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllProductDetails")]
        public APIReturnModel<List<ProductModel>> GetAllProductDetails()
        {
            APIReturnModel<List<ProductModel>> response = new APIReturnModel<List<ProductModel>>();

            List<ProductDomain> product = this._productBll.GetAllProductDetails();
            if (product.Count > 0)
            {
                List<ProductModel> productModel = this.MapProductDomainListToProductModelList(product);

                response = ReturnData.SuccessResponse<List<ProductModel>>(productModel);
            }
            else
            {
                response = ReturnData.ErrorResponse<List<ProductModel>>("Failed to get product details");
            }

            return response;
        }
        #endregion
        #endregion

        #region Private Methods

        #region MapProductModelToProductDomain
        /// <summary>
        /// MapProductModelToProductDomain
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private ProductDomain MapProductModelToProductDomain(ProductModel model)
        {
            ProductDomain dom = new ProductDomain
            {
                ProductId = model.ProductId.DecryptToLong(),
                Name = model.Name,
                CreatedBy = string.IsNullOrEmpty(model.CreatedBy) ? 0 : model.CreatedBy.DecryptToLong(),
                CreatedDate = model.CreatedDate,
                Description = model.Description,
                Url = model.Url,
                CategoryId = model.CategoryId,
                Rate = model.Rate,
                ModifiedBy = string.IsNullOrEmpty(model.ModifiedBy) ? 0 : model.ModifiedBy.DecryptToLong(),
                ModifiedDate = model.ModifiedDate
            };

            return dom;
        }
        #endregion

        #region MapProductDomainToProductModel
        /// <summary>
        /// MapProductDomainToProductModel
        /// </summary>
        /// <param name="dom"></param>
        /// <returns></returns>
        private ProductModel MapProductDomainToProductModel(ProductDomain dom)
        {
            ProductModel model = new ProductModel
            {
                ProductId = dom.ProductId.Encrypt(),
                Name = dom.Name,
                CreatedBy = dom.CreatedBy.Encrypt(),
                CreatedDate = dom.CreatedDate,
                Description = dom.Description,
                Url = dom.Url,
                CategoryId = dom.CategoryId,
                Rate = dom.Rate,
                ModifiedBy = dom.ModifiedBy.Encrypt(),
                ModifiedDate = dom.ModifiedDate
            };

            return model;
        }
        #endregion

        #region MapProductDomainListToProductModelList
        /// <summary>
        /// MapProductDomainListToProductModelList
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        private List<ProductModel> MapProductDomainListToProductModelList(List<ProductDomain> product)
        {
            List<ProductModel> productModels = new List<ProductModel>();

            foreach (ProductDomain dom in product)
            {
                productModels.Add(new ProductModel
                {
                    ProductId = dom.ProductId.Encrypt(),
                    Name = dom.Name,
                    CreatedBy = dom.CreatedBy.Encrypt(),
                    CreatedDate = dom.CreatedDate,
                    Description = dom.Description,
                    Url = dom.Url,
                    CategoryId = dom.CategoryId,
                    CategoryName = dom.CategoryName,
                    Rate = dom.Rate,
                    ModifiedBy = dom.ModifiedBy.Encrypt(),
                    ModifiedDate = dom.ModifiedDate,
                    CreatedByName = dom.CreatedByName,
                    ModifiedByName = dom.ModifiedByName
                });
            }

            return productModels;
        }
        #endregion

        #endregion
    }
}
