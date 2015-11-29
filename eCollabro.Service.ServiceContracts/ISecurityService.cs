// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.Service.DataContracts.RequestWrapper;
using eCollabro.Service.DataContracts.ResponseWrapper;
using System.ServiceModel;
using eCollabro.Service.DataContracts;
using System.Collections.Generic;
using eCollabro.Service.DataContracts.Core;
using eCollabro.Service.Core;

#endregion
namespace eCollabro.Service.ServiceContracts
{
    /// <summary>
    /// ISecurityService
    /// </summary>
    [ServiceContract]
    public interface ISecurityService
    {
        #region Site Configuration

        /// <summary>
        /// GetSiteConfiguration
        /// </summary>
        /// <returns>SiteConfigurationResponse</returns>
        [OperationContract]
        ServiceResponse<SiteConfigurationDC> GetSiteConfiguration();


        /// <summary>
        /// SaveSiteConfiguration
        /// </summary>
        /// <returns>FeaturesResponse</returns>
        [OperationContract]
        ServiceResponse SaveSiteConfiguration(SiteConfigurationDC siteConfiguration);


        #endregion

        #region Account

        /// <summary>
        /// CreateAccount
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse CreateAccount(RegisterDC register);

          /// <summary>
        /// VerifyAccount
        /// </summary>
        /// <param name="userTokenVerification"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse VerifyAccount(UserTokenVerificationDC userTokenVerification);

        /// <summary>
        /// AuthenticateUser
        /// </summary>
        /// <param name="authenticateUserRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse AuthenticateUser(AuthenticateUserRequest authenticateUserRequest);

        /// <summary>
        /// ResetPassword
        /// </summary>
        /// <param name="userTokenVerification"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<string> ResetPassword(UserTokenVerificationDC userTokenVerification);


        /// <summary>
        /// ChangePassword
        /// </summary>
        /// <param name="changePassword"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse ChangePassword(ChangePasswordDC changePassword);


        /// <summary>
        /// GeneratePasswordResetToken
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<UserTokenVerificationDC> GeneratePasswordResetToken(string userName);

        /// <summary>
        /// GetUserFeaturePermissions
        /// </summary>
        /// <param name="userFeaturePermissionId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<List<UserFeaturePermissionDC>> GetUserFeaturePermissions(int userFeaturePermissionId);

        #endregion

        #region Sites

        /// <summary>
        /// GetSites
        /// </summary>
        /// <param name="sitesRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<List<SiteDC>> GetSites();
        /// <summary>
        /// GetSite
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<SiteDC> GetSite(int siteId);

        /// <summary>
        /// SaveSite
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<int> SaveSite(SiteDC site);

        /// <summary>
        /// DeleteSite
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse DeleteSite(int siteId);


        /// <summary>
        /// CopySite
        /// </summary>
        /// <param name="saveSiteRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse CopySite(int sourceSiteId);

        #endregion

        #region Site Features


        /// <summary>
        /// SaveSiteFeatures
        /// </summary>
        /// <param name="saveSiteFeatureRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse SaveSiteFeatures(SaveSiteFeaturesRequest saveSiteFeatureRequest);


        /// <summary>
        /// GetSiteFeatures
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<SiteFeatures> GetSiteFeatures(int siteId);


        /// <summary>
        /// GetFeatureSettings
        /// </summary>
        /// <param name="featureSettingRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<List<FeatureSettingDC>> GetFeatureSettings(int featureId);

        /// <summary>
        /// GetSiteFeatures
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<SiteFeatures> GetSiteFeaturesSettings(int siteId);

        /// <summary>
        /// SaveSiteFeaturesSettings
        /// </summary>
        /// <param name="siteFeaturesSettingsRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse SaveSiteFeaturesSettings(List<ModuleFeatureDC> siteFeaturesSettingsRequest);
        #endregion

        #region Navigations

        [OperationContract]
        ServiceResponse<List<NavigationDC>> GetNavigations();

        [OperationContract]
        ServiceResponse<NavigationDC> GetNavigation(int navigationId);

        [OperationContract]
        ServiceResponse<int> SaveNavigation(NavigationDC navigation);

        [OperationContract]
        ServiceResponse DeleteNavigation(int navigationId);

        /// <summary>

        #endregion



        #region Roles

        /// <summary>
        /// GetRoles
        /// </summary>
        /// <param name="rolesRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<List<RoleDC>> GetRoles();

        /// <summary>
        /// GetRole
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<RoleDC> GetRole(int roleId);

        /// <summary>
        /// SaveRole
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<int> SaveRole(RoleDC role);


        /// <summary>
        /// DeleteRole
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse DeleteRole(int roleId);

        #endregion

        #region Role Feature
        [OperationContract]
        ServiceResponse<RoleFeatures> GetRoleFeatures(int roleId);

        [OperationContract]
        ServiceResponse SaveRoleFeatures(AddRoleFeaturesRequest addroleFeaturesRequest);
        #endregion

        #region Modules

        /// <summary>
        /// GetModules
        /// </summary>
        /// <param name="modulesRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<List<ModuleDC>> GetModules();

        [OperationContract]
        ServiceResponse<ModuleDC> GetModule(int moduleId);

        [OperationContract]
        ServiceResponse<List<ModuleFeatureDC>> GetFeatures(int featureId);

        [OperationContract]
        ServiceResponse SaveModule(ModuleDC module);

        [OperationContract]
        ServiceResponse DeleteModule(int moduleId);

        #endregion

        #region Navigation
        [OperationContract]
        ServiceResponse<List<NavigationDC>> GetUserNavigations();
        #endregion

        #region Manage Users
        [OperationContract]
        ServiceResponse<List<UserDetailDC>> GetUsers();

        [OperationContract]
        ServiceResponse<UserDetailDC> GetUser(int userId);

        [OperationContract]
        ServiceResponse<int> SaveUser(UserDetailDC userDetails);

        [OperationContract]
        ServiceResponse DeleteUser(int userId);

        [OperationContract]
        ServiceResponse UnlockUser(int userId);

        /// <summary>
        /// Confirm User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse ConfirmUser(int userId);

         /// <summary>
        /// ResetUserPassword
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse ResetUserPassword(int userId);

        /// <summary>
        /// Approve User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse ApproveUser(int userId);

        #endregion

        #region Features

        [OperationContract]
        ServiceResponse SaveFeature(FeatureDC feature);

        [OperationContract]
        ServiceResponse DeleteFeature(int featureId);

        #endregion

    }

}
