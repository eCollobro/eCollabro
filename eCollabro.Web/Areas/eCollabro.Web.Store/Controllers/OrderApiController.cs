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
    public class OrderApiController : BaseApiController
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
        public OrderApiController()
        {
                this.StoreClientProcessor = ApplicationContext.Getinstance().UnityContainer.Resolve<IStoreClient>();
        }

        #endregion

        #region Order Api Methods

 
        /// <summary>
        /// GetOrderCart
        /// </summary>
        /// <returns></returns>
        [Route("OrderApi/GetOrderCart/{siteId}")]
        public HttpResponseMessage GetOrderCart(int siteId)
        {
            StoreClientProcessor.UserContext.SiteId = siteId;
            OrderCartModel orderCart= StoreClientProcessor.GetOrderCart();
            return Request.CreateResponse(HttpStatusCode.OK,orderCart);
        }

        /// <summary>
        /// SaveItemToOrderCart
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [Route("OrderApi/SaveItemToOrderCart/{siteId}"),HttpPost]
        public HttpResponseMessage SaveItemToOrderCart(int siteId, OrderCartItemModel orderCartItem)
        {
            StoreClientProcessor.UserContext.SiteId = siteId;
            StoreClientProcessor.SaveItemToOrderCart(orderCartItem);
            return Request.CreateResponse(HttpStatusCode.OK,new { Message = CoreMessages.SavedSuccessfully, Id = orderCartItem.CartId });
        }


        #endregion

    }
}