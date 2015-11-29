using eCollabro.Client.Interface;
using eCollabro.Client.Models.Store;
using eCollabro.Utilities;
using eCollabro.Web.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Practices.Unity;
using eCollabro.Resources;

namespace eCollabro.Web.Store.Controllers
{
    [Authorize,WebApiExceptionFiler]
    public class ProductApiController : BaseApiController
    {
    
        #region Property

        /// <summary>
        /// StoreClientProcessor
        /// </summary>
        private IStoreClient StoreClientProcessor { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// StoreClientProcessor
        /// </summary>
        public ProductApiController()
        {
                this.StoreClientProcessor = ApplicationContext.Getinstance().UnityContainer.Resolve<IStoreClient>();
        }

        #endregion

        #region Product Api Methods

 
        /// <summary>
        /// GetProductCategories
        /// </summary>
        /// <returns></returns>
        [Route("ProductApi/GetProductCategories/{siteId}"),AllowAnonymous]
        public HttpResponseMessage GetProductCategories(int siteId)
        {
            StoreClientProcessor.UserContext.SiteId = siteId;
            SetPagingParameters(StoreClientProcessor.RequestContext);
            List<ProductCategoryModel> productCategories= StoreClientProcessor.GetProductCategories();
            return GetListResult<List<ProductCategoryModel>>(productCategories,StoreClientProcessor.RequestContext,StoreClientProcessor.ResponseContext);
        }

        /// <summary>
        /// GetProductCategory
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [Route("ProductApi/GetProductCategory/{siteId}/{categoryId}")]
        public HttpResponseMessage GetProductCategory(int siteId, int categoryId)
        {
            StoreClientProcessor.UserContext.SiteId = siteId;
            ProductCategoryModel productCategory= StoreClientProcessor.GetProductCategory(categoryId);
            return Request.CreateResponse(HttpStatusCode.OK, productCategory);
        }

        /// <summary>
        /// DeleteProductCategory
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [Route("ProductApi/DeleteProductCategory/{siteId}/{categoryId}"), HttpGet]
        public HttpResponseMessage DeleteProductCategory(int siteId, int categoryId)
        {
            StoreClientProcessor.UserContext.SiteId = siteId;
            StoreClientProcessor.DeleteProductCategory(categoryId);
            return Request.CreateResponse(HttpStatusCode.OK,CoreMessages.DeletedSuccessfully);
        }


        /// <summary>
        /// SaveProductCategory - Post Back 
        /// </summary>
        /// <param name="fm"></param>
        /// <returns></returns>
        [Route("ProductApi/SaveProductCategory/{siteId}"),HttpPost]
        public HttpResponseMessage SaveProductCategory(ProductCategoryModel productCategoryModel, int siteId)
        {
            StoreClientProcessor.UserContext.SiteId = siteId;
            StoreClientProcessor.SaveProductCategory(productCategoryModel);
            return Request.CreateResponse(HttpStatusCode.OK, new { Message = CoreMessages.SavedSuccessfully, Id = productCategoryModel.ProductCategoryId });
        }
        /// <summary>

        /// GetRecentProducts
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("ProductApi/GetRecentProducts/{siteId}")]
        public HttpResponseMessage GetRecentProducts(int siteId)
        {
            StoreClientProcessor.UserContext.SiteId = siteId;
            StoreClientProcessor.RequestContext.PageSize = 20;
            List<ProductModel> products=StoreClientProcessor.GetRecentProducts();
            return GetListResult<List<ProductModel>>(products, StoreClientProcessor.RequestContext, StoreClientProcessor.ResponseContext);
        }

    
        /// <summary>
        /// GetProducts - Get by Product Category Id
        /// </summary>
        /// <returns></returns>
        [Route("ProductApi/GetProducts/{siteId}/{categoryId}"),AllowAnonymous]
        public  HttpResponseMessage GetProducts(int siteId,int categoryId)
        {
            StoreClientProcessor.UserContext.SiteId = siteId;
            SetPagingParameters(StoreClientProcessor.RequestContext);
            ProductCategoryModel productCategory=StoreClientProcessor.GetProducts(categoryId);
            return GetListResult<ProductCategoryModel>(productCategory,StoreClientProcessor.RequestContext, StoreClientProcessor.ResponseContext); 
        }

        /// <summary>
        /// GetProduct
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("ProductApi/GetProduct/{siteId}/{productId}"),AllowAnonymous]
        public HttpResponseMessage GetProduct(int siteId, int productId)
        {
            StoreClientProcessor.UserContext.SiteId = siteId;
            ProductModel product= StoreClientProcessor.GetProduct(productId);
            return Request.CreateResponse(HttpStatusCode.OK, product);
        }

      
        /// <summary>
        /// DeleteProduct
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("ProductApi/DeleteProduct/{siteId}/{productId}"),HttpGet]
        public HttpResponseMessage DeleteProduct(int siteId, int productId)
        {
            StoreClientProcessor.UserContext.SiteId = siteId;
            StoreClientProcessor.DeleteProduct(productId);
            return Request.CreateResponse(HttpStatusCode.OK,CoreMessages.DeletedSuccessfully);
        }


        /// <summary>
        /// SaveProduct - Post Back 
        /// </summary>
        /// <param name="fm"></param>
        /// <returns></returns>
        [Route("ProductApi/SaveProduct/{siteId}"), HttpPost]
        public HttpResponseMessage SaveProduct(ProductModel productModel, int siteId)
        {
            StoreClientProcessor.UserContext.SiteId = siteId;
            StoreClientProcessor.SaveProduct(productModel);
            return Request.CreateResponse(HttpStatusCode.OK, new { Message = CoreMessages.SavedSuccessfully, Id = productModel.ProductId });
        }

        #endregion

    }
}