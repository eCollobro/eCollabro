// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.Client.Interface;
using eCollabro.Client.Models.Content;
using eCollabro.Resources;
using eCollabro.Utilities;
using eCollabro.Web.Base;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

#endregion

namespace eCollabro.Web.Areas.Content.Controllers
{
    /// <summary>
    /// ContentPageApiController
    /// </summary>
    [Authorize,WebApiExceptionFiler]
    public class ContentPageApiController : BaseApiController
    {
        #region Property

        /// <summary>
        /// ContentClientProcessor
        /// </summary>
        private IContentClient ContentClientProcessor { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// ContentClientProcessor
        /// </summary>
        public ContentPageApiController()
        {
            this.ContentClientProcessor = ApplicationContext.Getinstance().UnityContainer.Resolve<IContentClient>();
        }

        #endregion

        #region ContentPage Api Methods

        /// <summary>
        /// GetContentPageCategories
        /// </summary>
        /// <returns></returns>
        [Route("ContentPageApi/GetCategories/{siteId}")]
        public HttpResponseMessage GetCategories(int siteId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            SetPagingParameters(ContentClientProcessor.RequestContext);
            List<ContentPageCategoryModel> categories= ContentClientProcessor.GetContentPageCategories();
            return GetListResult<List<ContentPageCategoryModel>>(categories,ContentClientProcessor.RequestContext,ContentClientProcessor.ResponseContext);
        }

        /// <summary>
        /// GetContentPageCategory
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [Route("ContentPageApi/GetCategory/{siteId}/{categoryId}")]
        public HttpResponseMessage GetCategory(int siteId, int categoryId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            ContentPageCategoryModel category=ContentClientProcessor.GetContentPageCategory(categoryId);
            return Request.CreateResponse(HttpStatusCode.OK, category);
        }

        /// <summary>
        /// DeleteContentPageCategory
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [Route("ContentPageApi/DeleteCategory/{siteId}/{categoryId}"), HttpGet]
        public HttpResponseMessage DeleteCategory(int siteId, int categoryId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            ContentClientProcessor.DeleteContentPageCategory(categoryId);
            return Request.CreateResponse(HttpStatusCode.OK,CoreMessages.DeletedSuccessfully);
        }


        /// <summary>
        /// SaveContentPageCategory - Post Back 
        /// </summary>
        /// <param name="contentPageCategoryModel"></param>
        /// <returns></returns>
        [Route("ContentPageApi/SaveCategory/{siteId}"), HttpPost]
        public HttpResponseMessage SaveCategory(ContentPageCategoryModel contentPageCategoryModel, int siteId)
        {
            ContentClientProcessor.UserContext.SiteId =siteId;
            ContentClientProcessor.SaveContentPageCategory(contentPageCategoryModel);
            return Request.CreateResponse(HttpStatusCode.OK, new { Message = CoreMessages.SavedSuccessfully, Id = contentPageCategoryModel.ContentPageCategoryId });
        }

        /// <summary>
        /// GetContentPages - Get by ContentPage Category Id
        /// </summary>
        /// <returns></returns>
        [Route("ContentPageApi/GetPages/{siteId}/{categoryId}")]
        public HttpResponseMessage GetPages(int siteId, int categoryId)
        {
             ContentClientProcessor.UserContext.SiteId =siteId;
             SetPagingParameters(ContentClientProcessor.RequestContext);
             ContentPageCategoryModel category= ContentClientProcessor.GetContentPages(categoryId);
             return GetListResult<ContentPageCategoryModel>(category, ContentClientProcessor.RequestContext, ContentClientProcessor.ResponseContext);
        }

        /// <summary>
        /// GetContentPage
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        [Route("ContentPageApi/GetPage/{siteId}/{pageId}"),AllowAnonymous]
        public  HttpResponseMessage GetPage(int siteId,int pageId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            ContentPageModel contentPage= ContentClientProcessor.GetContentPage(pageId);
            return Request.CreateResponse(HttpStatusCode.OK,contentPage);
        }

        /// <summary>
        /// DeleteContentPage
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        [Route("ContentPageApi/DeletePage/{siteId}/{pageId}"), HttpGet]
        public HttpResponseMessage DeletePage(int siteId, int pageId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            ContentClientProcessor.DeleteContentPage(pageId);
            return Request.CreateResponse(HttpStatusCode.OK,CoreMessages.DeletedSuccessfully);
        }


        /// <summary>
        /// SaveContentPage - Post Back 
        /// </summary>
        /// <param name="contentPageModel"></param>
        /// <returns></returns>
        [Route("ContentPageApi/SavePage/{siteId}"), HttpPost]
        public HttpResponseMessage SavePage(ContentPageModel contentPageModel, int siteId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            ContentClientProcessor.SaveContentPage(contentPageModel);
            return Request.CreateResponse(HttpStatusCode.OK,new {Message= CoreMessages.SavedSuccessfully, Id=contentPageModel.ContentPageId});
        }

        #endregion

    }
}