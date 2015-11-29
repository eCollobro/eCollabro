#region References
using System;
using System.Collections.Generic;
using eCollabro.Client.Models.Core;
using eCollabro.Common;
#endregion 

namespace eCollabro.Client.Interface
{
    public interface ISecurityClient:IBaseClient
    {
        #region Site Confuguration

        /// <summary>
        /// GetSiteConfiguration
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        SiteConfigurationModel GetSiteConfiguration();

         /// <summary>
        /// SaveSiteConfiguration
        /// </summary>
        /// <param name="siteConfigurationModel"></param>
        /// <returns></returns>
        void SaveSiteConfiguration(SiteConfigurationModel siteConfigurationModel);

        #endregion

        #region Account

        /// <summary>
        /// CreateAccount
        /// </summary>
        /// <param name="registerModel"></param>
        void CreateAccount(RegisterModel registerModel);

          /// <summary>
        /// VerifyAccount
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        void VerifyAccount(string username, string verificationToken);

        /// <summary>
        /// AuthenticateUser
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        void AuthenticateUser(string Username, string Password);

        /// <summary>
        /// GeneratePasswordResetToken
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        UserTokenVerificationModel GeneratePasswordResetToken(string Username);

        /// <summary>
        /// ChangePassword
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="passwordResetToken"></param>
        /// <returns></returns>
        string ResetPassword(string Username, string passwordResetToken);


        /// <summary>
        /// ChangePassword
        /// </summary>
        /// <param name="changePasswordModel"></param>
        void ChangePassword(ChangePasswordModel changePasswordModel);

        #endregion

        #region Site

        List<SiteModel> GetSites();

        SiteModel GetSite(int siteId);

        void SaveSite(SiteModel siteModel);

        void DeleteSite(int siteId);

        void CopySite(int siteId);

        #endregion

        #region Site Features

        SiteFeaturesModel GetSiteFeatures(int siteId);

         /// <summary>
        /// GetSiteFeatures
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        SiteFeaturesModel GetSiteFeaturesSettings(int siteId);

        /// <summary>
        /// GetFeatureSettings
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        List<FeatureSettingModel> GetFeatureSettings(FeatureEnum feature);

         /// <summary>
        /// SaveSiteFeaturesSettings
        /// </summary>
        /// <param name="siteFeaturesModel"></param>
        void SaveSiteFeaturesSettings(SiteFeaturesModel siteFeaturesModel);

        void SaveSiteFeatures(int siteId, string features, Boolean IsCreateNavigationChecked);

        /// <summary>
        /// GetUserFeaturePermissions for Site 
        /// </summary>
        /// <returns></returns>
        List<UserFeaturePermissionModel> GetUserFeaturePermissions(int featureId);

        #endregion

        #region Navigations

        List<NavigationModel> GetNavigations();
        NavigationModel GetNavigation(int navigationId);
        void SaveNavigation(NavigationModel navigationModel);
        void DeleteNavigation(int navigationId);

        #endregion

        #region Role

        List<RoleModel> GetRoles(int siteId);
        List<RoleModel> GetRoles();
        void DeleteRole(int roleId);
        RoleModel GetRole(int roleId);
        void SaveRole(RoleModel roleModel);
        
        #endregion

        #region Role Features        
        
        RoleFeaturesModel GetRoleFeatures(int roleId);
        void SaveRoleFeatures(RoleFeaturesModel roleFeatures);

        #endregion

        #region Navigation
        List<UserNavigationModel> GetUserNavigations();
        #endregion

        #region Module
        List<ModuleModel> GetModules();
        ModuleModel GetModule(int moduleId);
        List<ModuleFeatureModel> GetFeatures(int moduleId);
        void SaveModule(ModuleModel moduleModel);
        void DeleteModule(int moduleId);

        #endregion

        #region Manage User
        List<UserModel> GetUsers();
        UserModel GetUser(int userRoleId);
        void SaveUser(UserModel userDetailModel);
        /// <summary>
        /// DeleteUser
        /// </summary>
        /// <param name="userId"></param>
        void DeleteUser(int userId);


        /// <summary>
        /// ResetPassword
        /// </summary>
        /// <param name="userId"></param>
        void ResetPassword(int userId);
        

        /// <summary>
        /// UnlockUser
        /// </summary>
        /// <param name="userId"></param>
        void UnlockUser(int userId);

        /// <summary>
        /// ConfirmUser
        /// </summary>
        /// <param name="userId"></param>
        void ConfirmUser(int userId);


        /// <summary>
        /// ApproveUser
        /// </summary>
        /// <param name="userId"></param>
        void ApproveUser(int userId);
        #endregion

      }
}

