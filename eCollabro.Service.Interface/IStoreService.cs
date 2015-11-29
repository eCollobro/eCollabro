// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System.ServiceModel;
using eCollabro.Service.DataContracts;
using System.Collections.Generic;
using eCollabro.Service.DataContracts.Store;

#endregion

namespace eCollabro.Service.Interface
{
    /// <summary>
    /// IContentService
    /// </summary>
    [ServiceContract]
    public interface IStoreService
    {
        #region Product Categories

        /// <summary>
        /// GetProductCategories
        /// </summary>
        /// <param name="productCategoriesRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<List<ProductCategoryDC>> GetProductCategories();


        /// <summary>
        /// GetProductCategory
        /// </summary>
        /// <param name="productCategoryId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<ProductCategoryDC> GetProductCategory(int productCategoryId);

        /// <summary>
        /// SaveProductCategory
        /// </summary>
        /// <param name="productCategory"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<int> SaveProductCategory(ProductCategoryDC productCategory);

        /// <summary>
        /// DeleteProductCategory
        /// </summary>
        /// <param name="productCategoryId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse DeleteProductCategory(int productCategoryId);

        #endregion

        #region Product

        /// <summary>
        /// GetProducts
        /// </summary>
        /// <param name="productCategoryId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<ProductCategoryDC> GetProducts(int productCategoryId);

        /// <summary>
        /// GetRecentProducts
        /// </summary>
        /// <param name="productsRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<List<ProductDC>> GetRecentProducts();

        /// <summary>
        /// GetProduct
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<ProductDC> GetProduct(int productId);

        /// <summary>
        /// SaveProduct
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<int> SaveProduct(ProductDC product);

        /// <summary>
        /// DeleteProduct
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse DeleteProduct(int productId);

        #endregion

        #region Order

        /// <summary>
        /// SaveOrderCartItem
        /// </summary>
        /// <param name="orderCartItem"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<int> AddItemToOrderCart(OrderCartItemDC orderCartItem);

        /// <summary>
        /// GetOrderCart
        /// </summary>
        /// <param name="orderCartRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<OrderCartDC> GetOrderCart();

        #endregion 

    }
}