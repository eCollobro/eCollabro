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

namespace eCollabro.Web.Content.Controllers
{
     
    /// <summary>
    /// BlogApiController
    /// </summary>
    [Authorize,WebApiExceptionFiler]
    public class BlogApiController : BaseApiController
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
        public BlogApiController()
        {
                this.ContentClientProcessor = ApplicationContext.Getinstance().UnityContainer.Resolve<IContentClient>();
        }

        #endregion

        #region Blog Api Methods

 
        /// <summary>
        /// GetBlogCategories
        /// </summary>
        /// <returns></returns>
        [Route("BlogApi/GetBlogCategories/{siteId}"),AllowAnonymous]
        public HttpResponseMessage GetBlogCategories(int siteId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            SetPagingParameters(ContentClientProcessor.RequestContext);
            List<BlogCategoryModel> blogCategories= ContentClientProcessor.GetBlogCategories();
            return GetListResult<List<BlogCategoryModel>>(blogCategories,ContentClientProcessor.RequestContext,ContentClientProcessor.ResponseContext);
        }

        /// <summary>
        /// GetBlogCategory
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [Route("BlogApi/GetBlogCategory/{siteId}/{categoryId}")]
        public HttpResponseMessage GetBlogCategory(int siteId, int categoryId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            BlogCategoryModel blogCategory= ContentClientProcessor.GetBlogCategory(categoryId);
            return Request.CreateResponse(HttpStatusCode.OK, blogCategory);
        }

        /// <summary>
        /// DeleteBlogCategory
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [Route("BlogApi/DeleteBlogCategory/{siteId}/{categoryId}"), HttpGet]
        public HttpResponseMessage DeleteBlogCategory(int siteId, int categoryId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            ContentClientProcessor.DeleteBlogCategory(categoryId);
            return Request.CreateResponse(HttpStatusCode.OK,CoreMessages.DeletedSuccessfully);
        }


        /// <summary>
        /// SaveBlogCategory - Post Back 
        /// </summary>
        /// <param name="fm"></param>
        /// <returns></returns>
        [Route("BlogApi/SaveBlogCategory/{siteId}"),HttpPost]
        public HttpResponseMessage SaveBlogCategory(BlogCategoryModel blogCategoryModel, int siteId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            ContentClientProcessor.SaveBlogCategory(blogCategoryModel);
            return Request.CreateResponse(HttpStatusCode.OK, new { Message = CoreMessages.SavedSuccessfully, Id = blogCategoryModel.BlogCategoryId });
        }
        /// <summary>

        /// GetRecentBlogs
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("BlogApi/GetRecentBlogs/{siteId}")]
        public HttpResponseMessage GetRecentBlogs(int siteId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            ContentClientProcessor.RequestContext.PageSize = 20;
            List<BlogModel> blogs=ContentClientProcessor.GetRecentBlogs();
            return GetListResult<List<BlogModel>>(blogs, ContentClientProcessor.RequestContext, ContentClientProcessor.ResponseContext);
        }

    
        /// <summary>
        /// GetBlogs - Get by Blog Category Id
        /// </summary>
        /// <returns></returns>
        [Route("BlogApi/GetBlogs/{siteId}/{categoryId}")]
        public  HttpResponseMessage GetBlogs(int siteId,int categoryId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            SetPagingParameters(ContentClientProcessor.RequestContext);
            BlogCategoryModel blogCategory=ContentClientProcessor.GetBlogs(categoryId);
            return GetListResult<BlogCategoryModel>(blogCategory,ContentClientProcessor.RequestContext, ContentClientProcessor.ResponseContext); 
        }

        /// <summary>
        /// GetBlog
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("BlogApi/GetBlog/{siteId}/{blogId}")]
        public HttpResponseMessage GetBlog(int siteId, int blogId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            BlogModel blog= ContentClientProcessor.GetBlog(blogId);
            return Request.CreateResponse(HttpStatusCode.OK, blog);
        }

      
        /// <summary>
        /// DeleteBlog
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("BlogApi/DeleteBlog/{siteId}/{blogId}"),HttpGet]
        public HttpResponseMessage DeleteBlog(int siteId, int blogId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            ContentClientProcessor.DeleteBlog(blogId);
            return Request.CreateResponse(HttpStatusCode.OK,CoreMessages.DeletedSuccessfully);
        }


        /// <summary>
        /// SaveBlog - Post Back 
        /// </summary>
        /// <param name="fm"></param>
        /// <returns></returns>
        [Route("BlogApi/SaveBlog/{siteId}"), HttpPost]
        public HttpResponseMessage SaveBlog(BlogModel blogModel, int siteId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            ContentClientProcessor.SaveBlog(blogModel);
            return Request.CreateResponse(HttpStatusCode.OK, new { Message = CoreMessages.SavedSuccessfully, Id = blogModel.BlogId });
        }

        #endregion

    }
}