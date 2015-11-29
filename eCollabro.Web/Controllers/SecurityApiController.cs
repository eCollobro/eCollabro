// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.Utilities;
using eCollabro.Web.Base;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using eCollabro.Client.Models.Core;
using System.Net.Http;
using System.Net;
using eCollabro.Client.Interface;
using eCollabro.Common;
using eCollabro.Resources;

#endregion

namespace eCollabro.Web.Controllers
{
    /// <summary>
    /// SecurityApiController
    /// </summary>
    [Authorize, WebApiExceptionFiler]
    public class SecurityApiController : BaseApiController
    {
        #region Property

        /// <summary>
        /// SecurityClientProxy
        /// </summary>
        private ISecurityClient SecurityClientProcessor { get; set; }


        /// <summary>
        /// LookupClientProxy
        /// </summary>
        private ILookupClient LookupClientProcessor { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// SecurityClientProcessor
        /// </summary>
        public SecurityApiController()
        {
            this.SecurityClientProcessor = ApplicationContext.Getinstance().UnityContainer.Resolve<ISecurityClient>();
            this.LookupClientProcessor = ApplicationContext.Getinstance().UnityContainer.Resolve<ILookupClient>();

        }

        #endregion

        #region Site Configuration

        /// <summary>
        /// GetSiteConfiguration
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        [Route("SecurityApi/GetSiteConfiguration/{siteId}")]
        public HttpResponseMessage GetSiteConfiguration(int siteId)
        {
            SecurityClientProcessor.UserContext.SiteId = siteId;
            SiteConfigurationModel siteConfiguration = SecurityClientProcessor.GetSiteConfiguration();
            return Request.CreateResponse(HttpStatusCode.OK, siteConfiguration);
        }

        /// <summary>
        /// CheckSiteRegistrationAllowed
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("SecurityApi/GetSiteRegistrationAllowed/{siteId}")]
        public HttpResponseMessage GetSiteRegistrationAllowed(int siteId)
        {
            SecurityClientProcessor.UserContext.SiteId = siteId;
            bool result = SecurityClientProcessor.GetSiteConfiguration().AllowRegistration;
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// SaveSiteFeaturesSettings
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        [Route("SecurityApi/SaveSiteFeaturesSettings/{siteId}"), HttpPost]
        public HttpResponseMessage SaveSiteFeaturesSettings(SiteFeaturesModel siteFeatures, int siteId)
        {
            SecurityClientProcessor.UserContext.SiteId = siteId;
            SecurityClientProcessor.SaveSiteFeaturesSettings(siteFeatures);
            return Request.CreateResponse(HttpStatusCode.OK, CoreMessages.SavedSuccessfully);
        }

        /// <summary>
        /// SaveSiteConfiguration
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("SecurityApi/SaveSiteConfiguration/{siteId}"), HttpPost]
        public HttpResponseMessage SaveSiteConfiguration(SiteConfigurationModel siteConfigurationModel, int siteId)
        {
            SecurityClientProcessor.UserContext.SiteId = siteId;
            SecurityClientProcessor.SaveSiteConfiguration(siteConfigurationModel);
            return Request.CreateResponse(HttpStatusCode.OK, CoreMessages.SavedSuccessfully);
        }


        #endregion

        #region Role

        /// <summary>
        /// GetRoles
        /// </summary>
        /// <returns></returns>
        [Route("SecurityApi/GetRoles/{siteId}")]
        public HttpResponseMessage GetRoles(int siteId) // Role by Site Id
        {
            SecurityClientProcessor.UserContext.SiteId = siteId;
            SetPagingParameters(SecurityClientProcessor.RequestContext);
            List<RoleModel> roles = SecurityClientProcessor.GetRoles();
            return GetListResult<List<RoleModel>>(roles,SecurityClientProcessor.RequestContext,SecurityClientProcessor.ResponseContext);
        }

        /// <summary>
        /// GetActiveRoles
        /// </summary>
        /// <returns></returns>
        [Route("SecurityApi/GetActiveRoles/{siteId}")]
        public HttpResponseMessage GetActiveRoles(int siteId) // Role by Site Id
        {
            SecurityClientProcessor.UserContext.SiteId = siteId;
            SetPagingParameters(SecurityClientProcessor.RequestContext);
            List<RoleModel> roles = SecurityClientProcessor.GetRoles().Where(qry => qry.IsActive.Equals(true)).ToList();
            return GetListResult<List<RoleModel>>(roles, SecurityClientProcessor.RequestContext, SecurityClientProcessor.ResponseContext);
        }

        /// <summary>
        /// GetRole
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("SecurityApi/GetRole/{siteId}/{roleId}")]
        public HttpResponseMessage GetRole(int siteId, int roleId)
        {
            SecurityClientProcessor.UserContext.SiteId = siteId;
            RoleModel role = SecurityClientProcessor.GetRole(roleId);
            return Request.CreateResponse(HttpStatusCode.OK, role);
        }

        /// <summary>
        /// DeleteRole
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("SecurityApi/DeleteRole/{siteId}/{roleId}"), HttpGet]
        public HttpResponseMessage DeleteRole(int siteId, int roleId)
        {
            SecurityClientProcessor.UserContext.SiteId = siteId;
            SecurityClientProcessor.DeleteRole(roleId);
            return Request.CreateResponse(HttpStatusCode.OK, CoreMessages.DeletedSuccessfully);
        }

        /// <summary>
        /// SaveRole
        /// </summary>
        /// <param name="roleModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SecurityApi/SaveRole/{siteId}")]
        public HttpResponseMessage SaveRole(RoleModel roleModel, int siteId)
        {
            SecurityClientProcessor.UserContext.SiteId = siteId;
            SecurityClientProcessor.SaveRole(roleModel);
            return Request.CreateResponse(HttpStatusCode.OK, new { Message = CoreMessages.SavedSuccessfully, Id = roleModel.RoleId });
        }

        #endregion

        #region Role Features

        /// <summary>
        /// GetRoleFeatures
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("SecurityApi/GetRoleFeatures/{siteId}/{roleId}")]
        public HttpResponseMessage GetRoleFeatures(int siteId, int roleId)
        {
            SecurityClientProcessor.UserContext.SiteId = siteId;
            RoleFeaturesModel roleFeaturesModel = SecurityClientProcessor.GetRoleFeatures(roleId);
            return Request.CreateResponse(HttpStatusCode.OK, roleFeaturesModel);
        }

        /// <summary>
        /// SaveRoleFeature
        /// </summary>
        /// <param name="RoleFeaturesModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SecurityApi/SaveRoleFeatures/{siteId}")]
        public HttpResponseMessage SaveRoleFeatures(RoleFeaturesModel roleFeatureModel, int siteId)
        {
            SecurityClientProcessor.UserContext.SiteId = siteId;
            SecurityClientProcessor.SaveRoleFeatures(roleFeatureModel);
            return Request.CreateResponse(HttpStatusCode.OK, CoreMessages.SavedSuccessfully);
        }

        #endregion

        #region Site

        /// <summary>
        /// GetSites
        /// </summary>
        /// <returns></returns>
        [Route("SecurityApi/GetSites")]
        public HttpResponseMessage GetSites()
        {
            SetPagingParameters(SecurityClientProcessor.RequestContext);
            List<SiteModel> sites = SecurityClientProcessor.GetSites();
            return GetListResult<List<SiteModel>>(sites,SecurityClientProcessor.RequestContext,SecurityClientProcessor.ResponseContext);
        }

        /// <summary>
        /// GetSite
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        [Route("SecurityApi/GetSite/{siteId}")]
        public HttpResponseMessage GetSite(int siteId)
        {
            SiteModel site = SecurityClientProcessor.GetSite(siteId);
            return Request.CreateResponse(HttpStatusCode.OK, site);
        }

        /// <summary>
        /// DeleteSite
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        [Route("SecurityApi/DeleteSite/{siteId}"), HttpGet]
        public HttpResponseMessage DeleteSite(int siteId)
        {
            SecurityClientProcessor.DeleteSite(siteId);
            return Request.CreateResponse(HttpStatusCode.OK, CoreMessages.DeletedSuccessfully);
        }

        /// <summary>
        /// Copy Existing Site
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("SecurityApi/CopySite/{siteId}"), HttpGet]
        public HttpResponseMessage CopySite(int siteId)
        {
            SecurityClientProcessor.CopySite(siteId);
            return Request.CreateResponse(HttpStatusCode.OK, CoreMessages.SiteCopiedSuccessfully);
        }

        /// <summary>
        /// SaveSite - Post Back 
        /// </summary>
        /// <param name="fm"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SecurityApi/SaveSite")]
        public HttpResponseMessage SaveSite(SiteModel siteModel)
        {
            SecurityClientProcessor.SaveSite(siteModel);
            return Request.CreateResponse(HttpStatusCode.OK, new { Message = CoreMessages.SavedSuccessfully, Id = siteModel.SiteId });
        }


        #endregion

        #region Site Features

        /// <summary>
        /// GetSiteFeatures
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        [Route("SecurityApi/GetSiteFeatures/{siteId}")]
        public HttpResponseMessage GetSiteFeatures(int siteId)
        {
            SiteFeaturesModel siteFeaturesModel = SecurityClientProcessor.GetSiteFeatures(siteId);
            return Request.CreateResponse(HttpStatusCode.OK, siteFeaturesModel);
        }

        /// <summary>
        /// GetSiteFeatures
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        [Route("SecurityApi/GetSiteFeaturesSettings/{siteId}")]
        public HttpResponseMessage GetSiteFeaturesSettings(int siteId)
        {
            SiteFeaturesModel siteFeatures = SecurityClientProcessor.GetSiteFeaturesSettings(siteId);
            return Request.CreateResponse(HttpStatusCode.OK, siteFeatures);
        }


        /// <summary>
        /// SaveSiteFeatures
        /// </summary>
        /// <param name="siteFeaturesModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SecurityApi/SaveSiteFeatures")]
        public HttpResponseMessage SaveSiteFeatures(SiteFeaturesModel siteFeatures)
        {
            string featureIds = string.Empty;
            foreach (ModuleModel module in siteFeatures.Features)
            {
                foreach (ModuleFeatureModel feature in module.ModuleFeatures)
                {
                    if (feature.IsSelected)
                    {
                        featureIds += feature.FeatureId + ",";
                    }
                }
            }
            if (featureIds.Length > 0)
                featureIds = featureIds.Substring(0, featureIds.Length - 1);
            SecurityClientProcessor.SaveSiteFeatures(siteFeatures.SiteId, featureIds, siteFeatures.CreateNavigations);
            return Request.CreateResponse(HttpStatusCode.OK, CoreMessages.SavedSuccessfully);
        }

        #endregion

        #region User

        /// <summary>
        /// GetUsers
        /// </summary>
        /// <returns></returns>
        [Route("SecurityApi/GetUsers/{siteId}")]
        public HttpResponseMessage GetUsers(int siteId)
        {
            SecurityClientProcessor.UserContext.SiteId = siteId;
            SetPagingParameters(SecurityClientProcessor.RequestContext);
            List<UserModel> users = SecurityClientProcessor.GetUsers();
            return GetListResult<List<UserModel>>(users,SecurityClientProcessor.RequestContext,SecurityClientProcessor.ResponseContext);
        }

        /// <summary>
        /// GetUser
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("SecurityApi/GetUser/{siteId}/{userId}")]
        public HttpResponseMessage GetUser(int siteId, int userId)
        {
            SecurityClientProcessor.UserContext.SiteId = siteId;
            UserModel user = SecurityClientProcessor.GetUser(userId);
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        /// <summary>
        /// DeleteUser
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("SecurityApi/DeleteUser/{siteId}/{userId}"), HttpGet]
        public HttpResponseMessage DeleteUser(int siteId, int userId)
        {
            SecurityClientProcessor.UserContext.SiteId = siteId;
            SecurityClientProcessor.DeleteUser(userId);
            return Request.CreateResponse(HttpStatusCode.OK, CoreMessages.DeletedSuccessfully);
        }

        /// <summary>
        /// SaveUser - Post Back 
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SecurityApi/SaveUser/{siteId}")]
        public HttpResponseMessage SaveUser(UserModel userModel, int siteId)
        {
            SecurityClientProcessor.UserContext.SiteId = siteId;
            SecurityClientProcessor.SaveUser(userModel);
            return Request.CreateResponse(HttpStatusCode.OK, new { Message = CoreMessages.SavedSuccessfully, Id = userModel.UserId });
        }

        /// <summary>
        /// ResetPassword
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [Route("SecurityApi/ResetPassword/{username}"), HttpGet, AllowAnonymous]
        public HttpResponseMessage ResetPassword(string username)
        {
            UserTokenVerificationModel passwordResetTokenModel = SecurityClientProcessor.GeneratePasswordResetToken(username);
            return Request.CreateResponse(HttpStatusCode.OK,CoreMessages.ResetPasswordEmailMessage);
        }

        /// <summary>
        /// ResetUserPassword
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [Route("SecurityApi/ResetUserPassword/{userId}"), HttpGet]
        public HttpResponseMessage ResetUserPassword(int userId)
        {
            SecurityClientProcessor.ResetPassword(userId);
            return Request.CreateResponse(HttpStatusCode.OK, CoreMessages.UserPasswordReset);
        }

        /// <summary>
        /// ChangePassword
        /// </summary>
        /// <param name="changePasswordModel"></param>
        /// <returns></returns>
        [Route("SecurityApi/ChangePassword"), HttpPost]
        public HttpResponseMessage ChangePassword(ChangePasswordModel changePasswordModel)
        {
            if (string.IsNullOrEmpty(changePasswordModel.UserName)) // user is chanming self password else called from manage user
                changePasswordModel.UserName = SecurityClientProcessor.UserContext.UserName;
            SecurityClientProcessor.ChangePassword(changePasswordModel);
            return Request.CreateResponse(HttpStatusCode.OK,CoreMessages.PasswordChanged);
        }

        /// <summary>
        /// ConfirmUser
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("SecurityApi/ConfirmUser/{userId}"), HttpGet]
        public HttpResponseMessage ConfirmUser(int userId)
        {
            SecurityClientProcessor.ConfirmUser(userId);
            return Request.CreateResponse(HttpStatusCode.OK, CoreMessages.AccountConfirmed);
        }

        /// <summary>
        /// ApproveUser
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("SecurityApi/ApproveUser/{userId}"), HttpGet]
        public HttpResponseMessage ApproveUser(int userId)
        {
            SecurityClientProcessor.ApproveUser(userId);
            return Request.CreateResponse(HttpStatusCode.OK, CoreMessages.AccountApproved);
        }

        /// <summary>
        /// UnlockUser
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("SecurityApi/UnlockUser/{userId}"), HttpGet]
        public HttpResponseMessage UnlockUser(int userId)
        {
            SecurityClientProcessor.UnlockUser(userId);
            return Request.CreateResponse(HttpStatusCode.OK, "User account has been unlocked.");
        }

        #endregion

        #region Navigation

        /// <summary>
        /// GetNavigations
        /// </summary>
        /// <returns></returns>
        [Route("SecurityApi/GetNavigations/{siteId}")]
        public HttpResponseMessage GetNavigations(int siteId) // Navigations by Site Id
        {
            SecurityClientProcessor.UserContext.SiteId = siteId;
            List<NavigationModel> navigations = SecurityClientProcessor.GetNavigations();
            return Request.CreateResponse(HttpStatusCode.OK, navigations);
        }

        /// <summary>
        /// GetNavigation
        /// </summary>
        /// <param name="navigationId"></param>
        /// <param name="siteId"></param>
        /// <returns></returns>
        [Route("SecurityApi/GetNavigation/{siteId}/{navigationId}")]
        public HttpResponseMessage GetNavigation(int siteId, int navigationId)
        {
            SecurityClientProcessor.UserContext.SiteId = siteId;
            NavigationModel navigation = SecurityClientProcessor.GetNavigation(navigationId);
            return Request.CreateResponse(HttpStatusCode.OK, navigation);
        }

        /// <summary>
        /// DeleteNavigation
        /// </summary>
        /// <param name="navigationId"></param>
        /// <returns></returns>
        [Route("SecurityApi/DeleteNavigation/{siteId}/{navigationId}"), HttpGet]
        public HttpResponseMessage DeleteNavigation(int siteId, int navigationId)
        {
            SecurityClientProcessor.UserContext.SiteId = siteId;
            SecurityClientProcessor.DeleteNavigation(navigationId);
            return Request.CreateResponse(HttpStatusCode.OK, CoreMessages.DeletedSuccessfully);
        }

        /// <summary>
        /// SaveNavigation - Post Back 
        /// </summary>
        /// <param name="fm"></param>
        /// <returns></returns>
        [Route("SecurityApi/SaveNavigation/{siteId}"), HttpPost]
        public HttpResponseMessage SaveNavigation(NavigationModel navigationModel, int siteId)
        {
            SecurityClientProcessor.UserContext.SiteId = siteId;
            SecurityClientProcessor.SaveNavigation(navigationModel);
            return Request.CreateResponse(HttpStatusCode.OK, new { Message = CoreMessages.SavedSuccessfully, Id = navigationModel.NavigationId });
        }

        /// <summary>
        /// GetUserNavigations
        /// </summary>
        /// <returns></returns>
        [Route("SecurityApi/GetUserNavigations/{siteId}"), AllowAnonymous]
        public HttpResponseMessage GetUserNavigations(int siteId)
        {
            SecurityClientProcessor.UserContext.SiteId = siteId;
            List<UserNavigationModel> userNavigations = SecurityClientProcessor.GetUserNavigations();
            return Request.CreateResponse(HttpStatusCode.OK, GetUserMenus(userNavigations));
        }

        /// <summary>
        /// GetUserMenus
        /// </summary>
        /// <param name="userNavigations"></param>
        /// <returns></returns>
        private List<UserNavigationModel> GetUserMenus(List<UserNavigationModel> userNavigations)
        {
            List<UserNavigationModel> mainMenus = new List<UserNavigationModel>();
            foreach (UserNavigationModel item in userNavigations.Where(m => m.NavigationParentId == null))
            {
                AddChildMenus(item, userNavigations, mainMenus);
            }
            return mainMenus;
        }

        /// <summary>
        /// AddChildMenus
        /// </summary>
        /// <param name="item"></param>
        /// <param name="items"></param>
        /// <param name="mainMenus"></param>
        private void AddChildMenus(UserNavigationModel item, List<UserNavigationModel> items, List<UserNavigationModel> mainMenus)
        {
            if (item.NavigationTypeId == (int)NavigationTypeEnum.Content)
            {
                item.Link = "/content/contentpage/index/" + item.ContentPageId;
            }
            mainMenus.Add(item);
            item.ChildNavigations = new List<UserNavigationModel>();
            List<UserNavigationModel> children = items.Where(m => m.NavigationParentId == item.NavigationId).ToList();
            if (children.Count >= 0)
            {
                foreach (UserNavigationModel child in children)
                {
                    AddChildMenus(child, children, item.ChildNavigations);
                }
            }
        }

        /// <summary>
        /// GetNavigationTypes
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("SecurityApi/GetNavigationTypes/")]
        public HttpResponseMessage GetNavigationTypes()
        {
            List<NavigationTypeModel> navigationTypes = LookupClientProcessor.GetNavigationTypes();
            return Request.CreateResponse(HttpStatusCode.OK, navigationTypes);
        }


        #endregion

    }
}