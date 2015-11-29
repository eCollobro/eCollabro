// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using eCollabro.Client.Models.Content;
using eCollabro.Web.Base;
using System.Web.Mvc;

#endregion 
namespace eCollabro.Web.Areas.Content.Controllers
{
    [Authorize]
    public class ContentCommentController : BaseController
    {
        /// <summary>
        /// Index - ContentComment Listing
        /// </summary>
        /// <param name="contextId"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index(int contextContentId,int context)
        {
            ContentCommentModel contentComment = new ContentCommentModel();
            contentComment.ContextId = context;
            contentComment.ContextContentId = contextContentId;
            if (Request.IsAjaxRequest())
                return PartialView(contentComment);
            else
                return View(contentComment);
        }
    }
}