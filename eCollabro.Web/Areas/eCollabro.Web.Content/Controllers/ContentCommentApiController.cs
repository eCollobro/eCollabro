// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.Client.Interface;
using eCollabro.Client.Models.Content;
using eCollabro.Common;
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
    /// ContentApiController
    /// </summary>
    [Authorize,WebApiExceptionFiler]
    public class ContentCommentApiController : BaseApiController
    {
        #region Property

        /// <summary>
        /// ContentClientProcessor
        /// </summary>
        public IContentClient ContentClientProcessor { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// ContentCommentApiController
        /// </summary>
        public ContentCommentApiController()
        {
             this.ContentClientProcessor = ApplicationContext.Getinstance().UnityContainer.Resolve<IContentClient>();
        }

        #endregion

        #region Content Comment Api Methods

        /// <summary>
        /// GetContentComments
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="context"></param>
        /// <param name="contextId"></param>
        /// <returns></returns>
        [Route("ContentCommentApi/GetContentComments/{siteId}/{contextId}/{contextContentId}"),AllowAnonymous]
        public HttpResponseMessage  GetContentComments(int siteId,int contextId,int contextContentId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            List<ContentCommentModel> contentComments=ContentClientProcessor.GetContentComments((ContextEnum)contextId, contextContentId);
            return Request.CreateResponse(HttpStatusCode.OK, contentComments);
        }

        /// <summary>
        /// SaveContentComment
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="contentComment"></param>
        /// <returns></returns>
        [Route("ContentCommentApi/SaveContentComment/{siteId}"), HttpPost]
        public HttpResponseMessage SaveContentComment(int siteId, ContentCommentModel contentComment)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            ContentClientProcessor.SaveContentComment(contentComment);
            return Request.CreateResponse(HttpStatusCode.OK, new { Message = CoreMessages.SavedSuccessfully, Id = contentComment.ContentCommentId });
        }

        /// <summary>
        /// DeleteContentComment
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="contentComment"></param>
        /// <returns></returns>
        [Route("ContentCommentApi/DeleteContentComment/{siteId}/{contentCommentId}"), HttpGet]
        public HttpResponseMessage DeleteContentComment(int siteId, int contentCommentId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            ContentClientProcessor.DeleteContentComment(contentCommentId);
            return Request.CreateResponse(HttpStatusCode.OK, CoreMessages.DeletedSuccessfully);
        }
        #endregion

    }
}