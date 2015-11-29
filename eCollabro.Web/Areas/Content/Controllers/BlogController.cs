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

namespace eCollabro.Web.Areas.Content.Controllers
{
    /// <summary>
    /// BlogController
    /// </summary>
    [Authorize]
    public class BlogController : BaseController
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
        public BlogController()
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
        /// Index - Blog Home [Users Startup View - Visitors Role]
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// BlogCategories 
        /// </summary>
        /// <returns></returns>
        public ActionResult BlogCategories()
        {
            if (!SavePermissionsToViewBag(FeatureEnum.Blog))
                return Redirect("~/home/unauthorized");
           
            return View();
        }

        /// <summary>
        /// Manage BlogCategory - Add /Edit 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BlogCategory(int Id = 0)
        {
            if (!SavePermissionsToViewBag(FeatureEnum.Blog))
                return Redirect("~/home/unauthorized");

            BlogCategoryModel BlogCategoryModel = new BlogCategoryModel();
            BlogCategoryModel.BlogCategoryId = Id;
            if (Request.IsAjaxRequest())
                return PartialView(BlogCategoryModel);
            else
                return View(BlogCategoryModel);

        }

        /// <summary>
        /// Blogs
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Blogs(int Id)
        {
            if (!SavePermissionsToViewBag(FeatureEnum.Blog))
                return Redirect("~/home/unauthorized");

            SavePermissionsToViewBag(FeatureEnum.Blog);
            BlogCategoryModel BlogCategoryModel = new BlogCategoryModel();
            BlogCategoryModel.BlogCategoryId = Id;
            if (Request.IsAjaxRequest())
                return PartialView(BlogCategoryModel);
            else
                return View(BlogCategoryModel);
        }

        /// <summary>
        /// Manage Blog
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ManageBlog(int Id = 0, int catId = 0)
        {
            if (!SavePermissionsToViewBag(FeatureEnum.User))
                return Redirect("~/home/unauthorized");


            BlogModel BlogModel = new BlogModel();
            BlogModel.BlogId = Id;
            BlogModel.BlogCategoryId = catId;
            if (Request.IsAjaxRequest())
                return PartialView(BlogModel);
            else
                return View(BlogModel);
        }

        /// <summary>
        /// Blog - Users View - Visitors
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Blog(int Id = 0)
        {
            BlogModel blogModel = new BlogModel();
            blogModel.BlogId = Id;
            if (Request.IsAjaxRequest())
                return PartialView(blogModel);
            else
                return View(blogModel);
        }

        #endregion
    }
}
