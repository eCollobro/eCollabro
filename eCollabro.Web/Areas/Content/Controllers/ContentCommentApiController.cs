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

namespace eCollabro.Web.Areas.Content.Controllers
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
            ContentCommentDataModel contentCommentsData=ContentClientProcessor.GetContentComments((ContextEnum)contextId, contextContentId);
            return Request.CreateResponse(HttpStatusCode.OK, contentCommentsData);
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

        /// <summary>
        /// ChangeContentLikeDislike
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="contextId"></param>
        /// <param name="liked"></param>
        [Route("ContentCommentApi/ChangeContentLikeDislike/"), HttpGet]
        public HttpResponseMessage ChangeContentLikeDislike(int contentId, int contextId, bool liked)
        {
            ContentClientProcessor.ChangeContentLikeDislike(contentId,(ContextEnum) contextId, liked);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// ChangeContentVote
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="contextId"></param>
        /// <param name="vote"></param>
        [Route("ContentCommentApi/ChangeContentVote/"), HttpGet]
        public HttpResponseMessage ChangeContentVote(int contentId, ContextEnum contextId, bool vote)
        {
            ContentClientProcessor.ChangeContentVote(contentId, (ContextEnum)contextId, vote);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// ChangeContentRating
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="contextId"></param>
        /// <param name="rating"></param>
        [Route("ContentCommentApi/ChangeContentRating/"), HttpGet]
        public HttpResponseMessage ChangeContentRating(int contentId, ContextEnum contextId, int rating)
        {
            ContentClientProcessor.ChangeContentRating(contentId, (ContextEnum)contextId, rating);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion

    }
}