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
    /// ContentPageController
    /// </summary>
    [Authorize]
    public class ContentPageController : BaseController
    {
        #region Property

        /// <summary>
        /// SecurityClientProxy
        /// </summary>
        public ISecurityClient SecurityClientProcessor { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// ContentPageController
        /// </summary>
        public ContentPageController()
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

        /// <summary>
        /// Index - ContentPage 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index(int Id)
        {
            ContentPageModel contentPageModel = new ContentPageModel();
            contentPageModel.ContentPageId = Id;
            return View(contentPageModel);
        }

        /// <summary>
        /// Categories 
        /// </summary>
        /// <returns></returns>
        public ActionResult Categories()
        {
            if (!SavePermissionsToViewBag(FeatureEnum.ContentPage))
                return Redirect("~/home/unauthorized");
            return View();
        }


        /// <summary>
        /// Manage ContentPageCategory - Add /Edit 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Category(int Id = 0)
        {
            if (!SavePermissionsToViewBag(FeatureEnum.ContentPage))
                return Redirect("~/home/unauthorized");

            ContentPageCategoryModel ContentPageCategoryModel = new ContentPageCategoryModel();
            ContentPageCategoryModel.ContentPageCategoryId = Id;
            if (Request.IsAjaxRequest())
                return PartialView(ContentPageCategoryModel);
            else
                return View(ContentPageCategoryModel);

        }

        /// <summary>
        /// ContentPages
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Pages(int Id)
        {
            if (!SavePermissionsToViewBag(FeatureEnum.ContentPage))
                return Redirect("~/home/unauthorized");

            ContentPageCategoryModel ContentPageCategoryModel = new ContentPageCategoryModel();
            ContentPageCategoryModel.ContentPageCategoryId = Id;
            if (Request.IsAjaxRequest())
                return PartialView(ContentPageCategoryModel);
            else
                return View(ContentPageCategoryModel);
        }

        /// <summary>
        /// Manage ContentPage
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Page(int Id = 0, int catId = 0)
        {
            if (!SavePermissionsToViewBag(FeatureEnum.ContentPage))
                return Redirect("~/home/unauthorized");

            ContentPageModel ContentPageModel = new ContentPageModel();
            ContentPageModel.ContentPageId = Id;
            ContentPageModel.ContentPageCategoryId = catId;
            if (Request.IsAjaxRequest())
                return PartialView(ContentPageModel);
            else
                return View(ContentPageModel);
        }

     }
}
