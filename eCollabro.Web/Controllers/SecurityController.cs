// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.Client;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using eCollabro.Web.Base;
using Microsoft.Practices.Unity;
using eCollabro.Utilities;
using eCollabro.Client.Models.Core;
using eCollabro.Client.Interface;
using eCollabro.Common;

#endregion

namespace eCollabro.Web.Controllers
{
    /// <summary>
    /// SecurityController
    /// </summary>
    [Authorize]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class SecurityController : BaseController
    {

        #region Property

        /// <summary>
        /// SecurityClientProxy
        /// </summary>
        private ISecurityClient SecurityClientProcessor { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// SecurityClientProcessor
        /// </summary>
        public SecurityController()
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
                if (!permissions.Contains(PermissionEnum.ViewContent))
                    return false;
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return true;
        }

        #endregion

        #region Site Configuration

        /// <summary>
        /// SiteConfiguration
        /// </summary>
        /// <returns></returns>
        public ActionResult SiteConfiguration()
        {
            if (!SavePermissionsToViewBag(FeatureEnum.SiteConfiguration))
                return Redirect("~/home/unauthorized");
            return View();
        }

        #endregion

        #region Roles

        /// <summary>
        /// Roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Roles()
        {
            if (!SavePermissionsToViewBag(FeatureEnum.Role))
                return Redirect("~/home/unauthorized");
            return View();
        }

        /// <summary>
        ///  Role -Add/Edit
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Role(int Id = 0)
        {
            if (!SavePermissionsToViewBag(FeatureEnum.Role))
                return Redirect("~/home/unauthorized");

            RoleModel roleModel = new RoleModel();
            roleModel.RoleId = Id;
            if (Request.IsAjaxRequest())
                return PartialView(roleModel);
            else
                return View(roleModel);
        }

        #endregion

        #region Role Feature

        /// <summary>
        /// RoleFeatures
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult RoleFeatures(int Id)
        {
            if (!SavePermissionsToViewBag(FeatureEnum.Role))
                return Redirect("~/home/unauthorized");

            RoleFeaturesModel roleFeatureModel = new RoleFeaturesModel();
            roleFeatureModel.RoleId = Id;
            if (Request.IsAjaxRequest())
                return PartialView(roleFeatureModel);
            else
                return View(roleFeatureModel);
        }

        #endregion

        #region  Users

        /// <summary>
        /// Users
        /// </summary>
        /// <returns></returns>
        public ActionResult Users()
        {
            if (!SavePermissionsToViewBag(FeatureEnum.User))
                return Redirect("~/home/unauthorized");
            return View();
        }


        /// <summary>
        ///  ManageUser -Add/Edit
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult ManageUser(int Id = 0)
        {
            if (!SavePermissionsToViewBag(FeatureEnum.User))
                return Redirect("~/home/unauthorized");
            UserModel userDetailsModel = new UserModel();
            userDetailsModel.UserId = Id;

            if (Request.IsAjaxRequest())
                return PartialView(userDetailsModel);
            else
                return View(userDetailsModel);
        }


        #endregion

        #region Common Methods

        #endregion

        #region Sites

        /// <summary>
        /// Sites - List Sites
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Sites()
        {
            return View();
        }

        /// <summary>
        /// Site - Switch Site
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SwitchSite(int Id)
        {
            Session["SiteId"] = Id;
            return Json(new ControllerMessage{ Message="Site changed successfully.", MessageType=ControllerMessageType.Info},JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Sites - Add /Edit 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Site(int Id = 0)
        {
            SiteModel siteModel = new SiteModel();
            siteModel.SiteId = Id;
            if (Request.IsAjaxRequest())
                return PartialView(siteModel);
            else
                return View(siteModel);

        }

        #endregion

        #region site Features

        /// <summary>
        /// SiteFeatures
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SiteFeatures(int Id = 0)
        {
            SiteFeaturesModel siteFeatureModel = new SiteFeaturesModel();
            siteFeatureModel.SiteId = Id;
            if (Request.IsAjaxRequest())
                return PartialView(siteFeatureModel);
            else
                return View(siteFeatureModel);

        }

        #endregion

        #region Navigations

        /// <summary>
        /// Navigations - List Navigations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult Navigations()
        {
            if (!SavePermissionsToViewBag(FeatureEnum.Navigation))
                return Redirect("~/home/unauthorized");
            return View();
        }

        /// <summary>
        /// Navigation
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Navigation(int Id = 0)
        {
            if (!SavePermissionsToViewBag(FeatureEnum.Navigation))
                return Redirect("~/home/unauthorized");
            NavigationModel navigationModel = new NavigationModel();
            navigationModel.NavigationId = Id;
            if (Request.IsAjaxRequest())
                return PartialView(navigationModel);
            else
                return View(navigationModel);

        }

        #endregion


        #region Features


        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ModuleAssignment()
        {
            return View();
        }
 
        /// <summary>
        /// Languages
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private List<LanguageModel> GetLanguages()
        {
            LookupClient lookUpClient = new LookupClient();
            List<LanguageModel> lstLanguage = lookUpClient.GetLanguages();
            return lstLanguage;
        }

        #endregion
    }
}
