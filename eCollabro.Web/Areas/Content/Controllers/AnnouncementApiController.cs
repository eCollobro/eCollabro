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
    /// AnnouncementApiController
    /// </summary>
    [Authorize,WebApiExceptionFiler]
    public class AnnouncementApiController : BaseApiController
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
        public AnnouncementApiController()
        {
                this.ContentClientProcessor = ApplicationContext.Getinstance().UnityContainer.Resolve<IContentClient>();
        }

        #endregion

        #region Announcement Api Methods

        /// <summary>
        /// GetRecentAnnouncements
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("AnnouncementApi/GetRecentAnnouncements/{siteId}")]
        public HttpResponseMessage GetRecentAnnouncements(int siteId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            List<AnnouncementModel> announcementModel=ContentClientProcessor.GetRecentAnnouncements();
            return GetListResult<List<AnnouncementModel>>(announcementModel,ContentClientProcessor.RequestContext,ContentClientProcessor.ResponseContext);
        }

    
        /// <summary>
        /// GetAnnouncements 
        /// </summary>
        /// <returns></returns>
        [Route("AnnouncementApi/GetAnnouncements/{siteId}")]
        public  HttpResponseMessage GetAnnouncements(int siteId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            SetPagingParameters(ContentClientProcessor.RequestContext);
            List<AnnouncementModel> announcements=ContentClientProcessor.GetAnnouncements();
            return GetListResult<List<AnnouncementModel>>(announcements,ContentClientProcessor.RequestContext,ContentClientProcessor.ResponseContext);
        }

 
        /// <summary>
        /// GetAnnouncement
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("AnnouncementApi/GetAnnouncement/{siteId}/{announcementId}")]
        public HttpResponseMessage GetAnnouncement(int siteId, int announcementId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            AnnouncementModel announcement= ContentClientProcessor.GetAnnouncement(announcementId);
            return Request.CreateResponse(HttpStatusCode.OK, announcement);
        }

      
        /// <summary>
        /// DeleteAnnouncement
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("AnnouncementApi/DeleteAnnouncement/{siteId}/{announcementId}"),HttpGet]
        public HttpResponseMessage DeleteAnnouncement(int siteId, int announcementId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            ContentClientProcessor.DeleteAnnouncement(announcementId);
            return Request.CreateResponse(HttpStatusCode.OK,CoreMessages.DeletedSuccessfully);
        }


        /// <summary>
        /// SaveAnnouncement - Post Back 
        /// </summary>
        /// <param name="fm"></param>
        /// <returns></returns>
        [Route("AnnouncementApi/SaveAnnouncement/{siteId}"), HttpPost]
        public HttpResponseMessage SaveAnnouncement(AnnouncementModel announcementModel, int siteId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            ContentClientProcessor.SaveAnnouncement(announcementModel);
            return Request.CreateResponse(HttpStatusCode.OK, new { Message = CoreMessages.SavedSuccessfully, Id = announcementModel.AnnouncementId });
        }

        #endregion

    }
}