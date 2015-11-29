// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region Reference

using eCollabro.Client.Models.Content;
using eCollabro.Utilities;
using eCollabro.Web.Base;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using eCollabro.Client.Models.Core;
using eCollabro.Client.Interface;
using eCollabro.Common;

#endregion

namespace eCollabro.Web.Content.Controllers
{
    /// <summary>
    /// AnnouncementController
    /// </summary>
    [Authorize]
    public class AnnouncementController : BaseController
    {

        #region Property

        /// <summary>
        /// SecurityClientProxy
        /// </summary>
        public ISecurityClient SecurityClientProcessor { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// SecurityClientProcessor
        /// </summary>
        public AnnouncementController()
        {
            this.SecurityClientProcessor = ApplicationContext.Getinstance().UnityContainer.Resolve<ISecurityClient>();
        }

        #endregion

        #region Private Method

        /// <summary>
        /// SavePermissionsToViewBag
        /// </summary>
        /// <param name="userPermissions"></param>
        private bool SavePermissionsToViewBag(FeatureEnum feature)
        {
            try
            {
                List<UserFeaturePermissionModel> userPermissions = SecurityClientProcessor.GetUserFeaturePermissions(Convert.ToInt32(feature));
                List<PermissionEnum> permissions = new List<PermissionEnum>();

                foreach (UserFeaturePermissionModel userFeaturePermission in userPermissions)
                {
                    permissions.Add((PermissionEnum)userFeaturePermission.PermissionId);
                }
                ViewBag.UserPermissions = permissions;
                if (!(permissions.Contains(PermissionEnum.ViewContent) || permissions.Contains(PermissionEnum.ViewAnomynousContent)))
                    return false;
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return true;
        }


        #endregion 

        #region Actions & Methods

        /// <summary>
        /// Index - Announcement Home [Users Startup View - Visitors Role]
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Announcements
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Announcements()
        {
            if (!SavePermissionsToViewBag(FeatureEnum.Announcement))
                return Redirect("~/home/unauthorized");

            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        /// <summary>
        /// Manage Announcement
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ManageAnnouncement(int Id = 0)
        {
            if (!SavePermissionsToViewBag(FeatureEnum.User))
                return Redirect("~/home/unauthorized");

            AnnouncementModel AnnouncementModel = new AnnouncementModel();
            AnnouncementModel.AnnouncementId = Id;
            if (Request.IsAjaxRequest())
                return PartialView(AnnouncementModel);
            else
                return View(AnnouncementModel);
        }

        /// <summary>
        /// Announcement - Users View - Visitors
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet,AllowAnonymous]
        public ActionResult Announcement(int Id)
        {
            AnnouncementModel announcementModel = new AnnouncementModel();
            announcementModel.AnnouncementId = Id;
            if (Request.IsAjaxRequest())
                return PartialView(announcementModel);
            else
                return View(announcementModel);
        }

        #endregion
    }
}
