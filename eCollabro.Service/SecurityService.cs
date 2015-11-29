// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.BAL;
using eCollabro.Service.Core;
using eCollabro.Service.DataContracts;
using eCollabro.Service.DataContracts.Core;
using eCollabro.Service.DataContracts.RequestWrapper;
using eCollabro.Service.DataContracts.ResponseWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using eCollabro.BAL.Entities.Models;
using eCollabro.DataMapper;
using eCollabro.Service.DataMembers.Core;
using eCollabro.Common;
using eCollabro.Service.ServiceContracts;
using System.ServiceModel;

#endregion

namespace eCollabro.Service
{
    /// <summary>
    /// SecurityService
    /// </summary>
    public class SecurityService : BaseService, ISecurityService
    {
        #region Data Members

        private SecurityManager _securityManager;

        #endregion

        #region Constructor

        public SecurityService()
        {
            _securityManager = new SecurityManager();
        }

        #endregion

        #region Role

        /// <summary>
        /// GetRoles
        /// </summary>
        /// <param name="rolesRequest"></param>
        /// <returns></returns>
        public ServiceResponse<List<RoleDC>> GetRoles()
        {
            ServiceResponse<List<RoleDC>> rolesResponse = new ServiceResponse<List<RoleDC>>();
            rolesResponse.Result = new List<RoleDC>();
            try
            {
                SetContext();
                _securityManager.GetRoles().ForEach
                    (
                       role => rolesResponse.Result.Add(Mapper.Map<Role, RoleDC>(role))
                    );

            }
            catch (Exception ex)
            {
                HandleError(ex, rolesResponse);
            }
            return rolesResponse;
        }

        /// <summary>
        /// GetRole
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public ServiceResponse<RoleDC> GetRole(int roleId)
        {
            ServiceResponse<RoleDC> roleResponse = new ServiceResponse<RoleDC>();
            try
            {
                SetContext();
                Role role = _securityManager.GetRole(roleId);
                roleResponse.Result = Mapper.Map<Role, RoleDC>(role);
            }
            catch (Exception ex)
            {
                HandleError(ex, roleResponse);
            }
            return roleResponse;
        }


        /// <summary>
        /// SaveRole
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public ServiceResponse<int> SaveRole(RoleDC role)
        {
            ServiceResponse<int> saveRoleResponse = new ServiceResponse<int>();
            try
            {
                SetContext();
                Role roleModel = Mapper.Map<RoleDC, Role>(role);
                _securityManager.SaveRole(roleModel);
                saveRoleResponse.Result = roleModel.RoleId;
            }
            catch (Exception ex)
            {
                HandleError(ex, saveRoleResponse);
            }
            return saveRoleResponse;
        }

        /// <summary>
        /// DeleteRole
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public ServiceResponse DeleteRole(int roleId)
        {
            ServiceResponse deleteRoleResponse = new ServiceResponse();
            try
            {
                SetContext();
                _securityManager.DeleteRole(roleId);
            }
            catch (Exception ex)
            {
                HandleError(ex, deleteRoleResponse);
            }
            return deleteRoleResponse;
        }


        #endregion

        #region Role Features


        /// <summary>
        /// GetRoleFeatures
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public ServiceResponse<RoleFeatures> GetRoleFeatures(int roleId)
        {
            ServiceResponse<RoleFeatures> roleFeaturesResponse = new ServiceResponse<RoleFeatures>();
            try
            {
                SetContext();
                Role role = _securityManager.GetRole(roleId);

                roleFeaturesResponse.Result=new RoleFeatures();
                roleFeaturesResponse.Result.RoleId = role.RoleId;
                roleFeaturesResponse.Result.RoleCode = role.RoleCode;
                roleFeaturesResponse.Result.RoleName = role.RoleName;
                List<FeatureResult> roleFeatures = _securityManager.GetRoleFeatures(roleId);
                Translate.Convert(roleFeatures, roleFeaturesResponse.Result.Features);

            }
            catch (Exception ex)
            {
                HandleError(ex, roleFeaturesResponse);
            }
            return roleFeaturesResponse;
        }

        /// <summary>
        /// SaveRoleFeatures
        /// </summary>
        /// <param name="addroleFeaturesRequest"></param>
        /// <returns></returns>
        public ServiceResponse SaveRoleFeatures(AddRoleFeaturesRequest addroleFeaturesRequest)
        {
            ServiceResponse addRoleFeaturesResponse = new ServiceResponse();
            try
            {
                SetContext();
                List<FeatureResult> lstFeatures = new List<FeatureResult>();
                foreach (ModuleFeatureDC feature in addroleFeaturesRequest.Features)
                {
                    FeatureResult featureResult = new FeatureResult();
                    featureResult.FeatureId = feature.FeatureId;
                    featureResult.RoleFeaturePermissions = new List<FeaturePermissionResult>();
                    foreach (FeaturePermissionDC featurePermission in feature.RoleFeaturePermissions)
                    {
                        featureResult.RoleFeaturePermissions.Add(new FeaturePermissionResult { FeatureId = feature.FeatureId, ContentPermissionId = featurePermission.ContentPermissionId });
                    }
                    lstFeatures.Add(featureResult);
                }
                _securityManager.SaveRoleFeatures(addroleFeaturesRequest.RoleId, lstFeatures);
            }
            catch (Exception ex)
            {
                HandleError(ex, addRoleFeaturesResponse);
            }
            return addRoleFeaturesResponse;
        }



        #endregion

        #region Account

        /// <summary>
        /// CreateAccount
        /// </summary>
        /// <param name="registerAccount"></param>
        /// <returns></returns>
        public ServiceResponse CreateAccount(RegisterDC registerAccount)
        {
            ServiceResponse createAccountResponse = new ServiceResponse();
            try
            {
                SetContext();
                UserMembership userMembership = new UserMembership();
                userMembership.UserName = registerAccount.UserName;
                userMembership.Password = registerAccount.Password;
                userMembership.Email = registerAccount.Email;
                _securityManager.CreateAccount(userMembership);
            }
            catch (Exception ex)
            {
                HandleError(ex, createAccountResponse);
            }
            return createAccountResponse;

        }

        /// <summary>
        /// AuthenticateUser
        /// </summary>
        /// <param name="verifyLoginRequest"></param>
        /// <returns></returns>
        public ServiceResponse AuthenticateUser(AuthenticateUserRequest verifyLoginRequest)
        {
            ServiceResponse verifyLoginResponse = new ServiceResponse();
            try
            {
                SetContext();
                _securityManager.AuthenticateUser(verifyLoginRequest.Username, verifyLoginRequest.Password);
            }
            catch (Exception ex)
            {
                HandleError(ex, verifyLoginResponse);
            }
            return verifyLoginResponse;

        }

        /// <summary>
        /// ResetPassword
        /// </summary>
        /// <param name="resetPasswordRequest"></param>
        /// <returns></returns>
        public ServiceResponse<string> ResetPassword(UserTokenVerificationDC resetPasswordRequest)
        {
            ServiceResponse<string> resetPasswordResponse = new ServiceResponse<string>();
            try
            {
                SetContext();
                resetPasswordResponse.Result = _securityManager.ResetPassword(resetPasswordRequest.UserName, resetPasswordRequest.Token);
            }
            catch (Exception ex)
            {
                HandleError(ex, resetPasswordResponse);
            }
            return resetPasswordResponse;
        }

        /// <summary>
        /// VerifyAccount
        /// </summary>
        /// <param name="verifyAccountRequest"></param>
        /// <returns></returns>
        public ServiceResponse VerifyAccount(UserTokenVerificationDC verifyAccountRequest)
        {
            ServiceResponse verifyAccountResponse = new ServiceResponse();
            try
            {
                SetContext();
                _securityManager.VerifyAccount(verifyAccountRequest.UserName, verifyAccountRequest.Token);
            }
            catch (Exception ex)
            {
                HandleError(ex, verifyAccountResponse);
            }
            return verifyAccountResponse;
        }

        /// <summary>
        /// ChangePassword
        /// </summary>
        /// <param name="changePasswordRequest"></param>
        /// <returns></returns>
        public ServiceResponse ChangePassword(ChangePasswordDC changePasswordRequest)
        {
            ServiceResponse changePasswordResponse = new ServiceResponse();
            try
            {
                SetContext();
                _securityManager.ChangePassword(changePasswordRequest.UserName, changePasswordRequest.OldPassword, changePasswordRequest.NewPassword);
            }
            catch (Exception ex)
            {
                HandleError(ex, changePasswordResponse);
            }
            return changePasswordResponse;
        }


        /// <summary>
        /// GeneratePasswordResetToken
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public ServiceResponse<UserTokenVerificationDC> GeneratePasswordResetToken(string username)
        {
            ServiceResponse<UserTokenVerificationDC> generatePasswordResetTokenResponse = new ServiceResponse<UserTokenVerificationDC>();
            try
            {
                SetContext();
                UserMembership userMembership = _securityManager.ResetPassword(username); //step 1
                generatePasswordResetTokenResponse.Result = new UserTokenVerificationDC();
                generatePasswordResetTokenResponse.Result.UserName = userMembership.UserName;
                generatePasswordResetTokenResponse.Result.Token = userMembership.PasswordVerificationToken;

            }
            catch (Exception ex)
            {
                HandleError(ex, generatePasswordResetTokenResponse);
            }
            return generatePasswordResetTokenResponse;
        }

        /// <summary>
        /// GetUserFeaturePermissions
        /// </summary>
        /// <param name="featureId"></param>
        /// <returns></returns>
        public ServiceResponse<List<UserFeaturePermissionDC>> GetUserFeaturePermissions(int featureId)
        {
            ServiceResponse<List<UserFeaturePermissionDC>> userFeaturePermissionsResponse = new ServiceResponse<List<UserFeaturePermissionDC>>();
            try
            {
                SetContext();
                List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(_securityManager.UserContextDetails.UserId, (FeatureEnum)featureId);

                userFeaturePermissionsResponse.Result = new List<UserFeaturePermissionDC>();
                foreach (PermissionEnum permission in userPermissions)
                {
                    userFeaturePermissionsResponse.Result.Add(new UserFeaturePermissionDC { PermissionId = Convert.ToInt32(permission), PermissionName = permission.ToString() });
                }
            }
            catch (Exception ex)
            {
                HandleError(ex, userFeaturePermissionsResponse);
            }
            return userFeaturePermissionsResponse;
        }


        #endregion

        #region Users

        /// <summary>
        /// GetUsers
        /// </summary>
        /// <param name="featureId"></param>
        /// <returns></returns>
        public ServiceResponse<List<UserDetailDC>> GetUsers()
        {
            ServiceResponse<List<UserDetailDC>> usersResponse = new ServiceResponse<List<UserDetailDC>>();
            try
            {
                SetContext();
                List<UserMembership> userResults = _securityManager.GetUsers();
                usersResponse.Result = new List<UserDetailDC>();
                foreach (UserMembership userMembership in userResults)
                {
                    UserDetailDC userDetailDC = Mapper.Map<UserMembership, UserDetailDC>(userMembership);
                    if (userDetailDC.UserRoles == null)
                        userDetailDC.UserRoles = new List<RoleDC>();
                    foreach (UserRole userRole in userMembership.UserRoles)
                    {
                        userDetailDC.UserRoles.Add(Mapper.Map<UserRole, RoleDC>(userRole));
                    }
                    usersResponse.Result.Add(userDetailDC);
                }
            }
            catch (Exception ex)
            {
                HandleError(ex, usersResponse);
            }
            return usersResponse;
        }

        /// <summary>
        /// GetUser
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ServiceResponse<UserDetailDC> GetUser(int userId)
        {
            ServiceResponse<UserDetailDC> userResponse = new ServiceResponse<UserDetailDC>();
            try
            {
                SetContext();
                UserMembership user = _securityManager.GetUser(userId);
                userResponse.Result = Mapper.Map<UserMembership, UserDetailDC>(user);
                userResponse.Result.UserRoles = new List<RoleDC>();
                foreach (UserRole userRole in user.UserRoles)
                {
                    userResponse.Result.UserRoles.Add(Mapper.Map<Role, RoleDC>(userRole.Role));
                }
            }
            catch (Exception ex)
            {
                HandleError(ex, userResponse);
            }
            return userResponse;
        }

        /// <summary>
        /// SaveUser
        /// </summary>
        /// <param name="userDetail"></param>
        /// <returns></returns>
        public ServiceResponse<int> SaveUser(UserDetailDC userDetail)
        {
            ServiceResponse<int> saveUserResponse = new ServiceResponse<int>();
            try
            {
                SetContext();
                UserMembership user = Mapper.Map<UserDetailDC, UserMembership>(userDetail);
                user.UserRoles = new List<UserRole>();
                foreach (RoleDC role in userDetail.UserRoles)
                {
                    user.UserRoles.Add(new UserRole { RoleId = role.RoleId });
                }
                _securityManager.SaveUser(user);
                saveUserResponse.Result = user.UserId;
            }
            catch (Exception ex)
            {
                HandleError(ex, saveUserResponse);
            }
            return saveUserResponse;
        }

        /// <summary>
        /// DeleteUser
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ServiceResponse DeleteUser(int userId)
        {
            ServiceResponse deleteUserResponse = new ServiceResponse();
            try
            {
                SetContext();
                _securityManager.DeleteUser(userId);
            }
            catch (Exception ex)
            {
                HandleError(ex, deleteUserResponse);
            }
            return deleteUserResponse;
        }

        /// <summary>
        /// DeleteUser
        /// </summary>
        /// <param name="deleteUserRequest"></param>
        /// <returns></returns>
        public ServiceResponse UnlockUser(int userId)
        {
            ServiceResponse unlockUserResponse = new ServiceResponse();
            try
            {
                SetContext();
                _securityManager.UnlockUser(userId);
            }
            catch (Exception ex)
            {
                HandleError(ex, unlockUserResponse);
            }
            return unlockUserResponse;
        }


        /// <summary>
        /// ResetUserPassword
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ServiceResponse ResetUserPassword(int userId)
        {
            ServiceResponse resetPasswordResponse = new ServiceResponse();
            try
            {
                SetContext();
                _securityManager.ResetPassword(userId);
            }
            catch (Exception ex)
            {
                HandleError(ex, resetPasswordResponse);
            }
            return resetPasswordResponse;
        }

        /// <summary>
        /// Approve User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ServiceResponse ApproveUser(int userId)
        {
            ServiceResponse approveUserResponse = new ServiceResponse();
            try
            {
                SetContext();
                _securityManager.ApproveUser(userId);
            }
            catch (Exception ex)
            {
                HandleError(ex, approveUserResponse);
            }
            return approveUserResponse;
        }

        /// <summary>
        /// Confirm User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ServiceResponse ConfirmUser(int userId)
        {
            ServiceResponse confirmUserResponse = new ServiceResponse();
            try
            {
                SetContext();
                _securityManager.ConfirmUser(userId);
            }
            catch (Exception ex)
            {
                HandleError(ex, confirmUserResponse);
            }
            return confirmUserResponse;
        }

        #endregion

        #region Site Configuration

        /// <summary>
        /// GetSiteConfiguration
        /// </summary>
        /// <returns>SiteConfigurationResponse</returns>
        public ServiceResponse<SiteConfigurationDC> GetSiteConfiguration()
        {
            ServiceResponse<SiteConfigurationDC> siteConfigurationResponse = new ServiceResponse<SiteConfigurationDC>();
            try
            {
                SetContext();
                siteConfigurationResponse.Result = Mapper.Map<SiteConfiguration, SiteConfigurationDC>(_securityManager.GetSiteConfiguration());
            }
            catch (Exception ex)
            {
                HandleError(ex, siteConfigurationResponse);
            }
            return siteConfigurationResponse;
        }

        /// <summary>
        /// SaveSiteConfiguration
        /// </summary>
        /// <returns>FeaturesResponse</returns>
        public ServiceResponse SaveSiteConfiguration(SiteConfigurationDC siteConfiguration)
        {
            ServiceResponse saveSiteConfigurationResponse = new ServiceResponse();
            try
            {
                SetContext();
                _securityManager.SaveSiteConfiguration(Mapper.Map<SiteConfigurationDC, SiteConfiguration>(siteConfiguration));
            }
            catch (Exception ex)
            {
                HandleError(ex, saveSiteConfigurationResponse);
            }
            return saveSiteConfigurationResponse;
        }

        /// <summary>
        /// GetSiteFeatures
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public ServiceResponse<SiteFeatures> GetSiteFeaturesSettings(int siteId)
        {
            ServiceResponse<SiteFeatures> siteFeaturesResponse = new ServiceResponse<SiteFeatures>();
            try
            {
                SetContext();
                List<FeatureResult> siteFeatures = _securityManager.GetSiteFeaturesSettings(siteId);
                siteFeaturesResponse.Result = new SiteFeatures();
                Translate.Convert(siteFeatures, siteFeaturesResponse.Result.Features);
                Site site = _securityManager.GetSite(siteId);
                siteFeaturesResponse.Result.SiteId = site.SiteId;
                siteFeaturesResponse.Result.SiteName = site.SiteName;
                siteFeaturesResponse.Result.SiteCode = site.SiteCode;
            }
            catch (Exception ex)
            {
                HandleError(ex, siteFeaturesResponse);
            }
            return siteFeaturesResponse;
        }

        /// <summary>
        /// SaveSiteFeaturesSettings
        /// </summary>
        /// <param name="siteFeaturesSettingsRequest"></param>
        /// <returns></returns>
        public ServiceResponse SaveSiteFeaturesSettings(List<ModuleFeatureDC> siteFeaturesSettingsRequest)
        {
            ServiceResponse addSiteFeaturesSettingsResponse = new ServiceResponse();
            try
            {
                SetContext();
                List<FeatureResult> lstFeatures = new List<FeatureResult>();
                foreach (ModuleFeatureDC feature in siteFeaturesSettingsRequest)
                {
                    FeatureResult featureResult = new FeatureResult();
                    featureResult.FeatureId = feature.FeatureId;
                    featureResult.SiteContentSettings = new List<SiteContentSettingResult>();
                    foreach (SiteContentSettingDC siteContentSetting in feature.SiteContentSettings)
                    {
                        featureResult.SiteContentSettings.Add(new SiteContentSettingResult { FeatureId = feature.FeatureId, ContentSettingId = siteContentSetting.ContentSettingId, IsAssigned = siteContentSetting.IsAssigned });
                    }
                    lstFeatures.Add(featureResult);
                }
                UserContextDC userContextDC = OperationContext.Current.IncomingMessageHeaders.GetHeader<UserContextDC>("ActiveUser", "s");
                _securityManager.SaveSiteFeaturesSettings(userContextDC.SiteId, lstFeatures);
            }
            catch (Exception ex)
            {
                HandleError(ex, addSiteFeaturesSettingsResponse);
            }
            return addSiteFeaturesSettingsResponse;
        }

        #endregion

        #region Site

        /// <summary>
        /// GetSites
        /// </summary>
        /// <returns></returns>
        public ServiceResponse<List<SiteDC>> GetSites()
        {
            ServiceResponse<List<SiteDC>> sitesResponse = new ServiceResponse<List<SiteDC>>();
            try
            {
                SetContext();
                sitesResponse.Result = new List<SiteDC>();
                _securityManager.GetSites().ForEach(
                        siteResult => sitesResponse.Result.Add(Mapper.Map<Site, SiteDC>(siteResult))
                    );
            }
            catch (Exception ex)
            {
                HandleError(ex, sitesResponse);
            }
            return sitesResponse;
        }

        /// <summary>
        /// GetSite
        /// </summary>
        /// <returns></returns>
        public ServiceResponse<SiteDC> GetSite(int siteId)
        {
            ServiceResponse<SiteDC> serviceResponse = new ServiceResponse<SiteDC>();
            try
            {
                SetContext();
                serviceResponse.Result = Mapper.Map<Site, SiteDC>(_securityManager.GetSite(siteId));
            }
            catch (Exception ex)
            {
                HandleError(ex, serviceResponse);
            }
            return serviceResponse;
        }

        /// <summary>
        /// SaveSite
        /// </summary>
        /// <returns></returns>
        public ServiceResponse<int> SaveSite(SiteDC site)
        {
            ServiceResponse<int> saveSiteResponse = new ServiceResponse<int>();
            try
            {
                SetContext();
                Site siteModel = Mapper.Map<SiteDC, Site>(site);
                _securityManager.SaveSite(siteModel);
                saveSiteResponse.Result = site.SiteId;
            }
            catch (Exception ex)
            {
                HandleError(ex, saveSiteResponse);
            }
            return saveSiteResponse;
        }


        /// <summary>
        /// DeleteSite
        /// </summary>
        /// <returns></returns>
        public ServiceResponse DeleteSite(int siteId)
        {
            ServiceResponse deleteSiteResponse = new ServiceResponse();
            try
            {
                SetContext();
                _securityManager.DeleteSite(siteId);
            }
            catch (Exception ex)
            {
                HandleError(ex, deleteSiteResponse);
            }
            return deleteSiteResponse;
        }

        /// <summary>
        /// Copy Existing Site
        /// </summary>
        /// <returns></returns>
        public ServiceResponse CopySite(int sourceSiteId)
        {
            ServiceResponse copySiteResponse = new ServiceResponse();
            try
            {
                SetContext();
                _securityManager.CopySite(sourceSiteId);
            }
            catch (Exception ex)
            {
                HandleError(ex, copySiteResponse);
            }
            return copySiteResponse;
        }

        #endregion

        #region Site Features

        /// <summary>
        /// GetSiteFeatures
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public ServiceResponse<SiteFeatures> GetSiteFeatures(int siteId)
        {
            ServiceResponse<SiteFeatures> siteFeaturesResponse = new ServiceResponse<SiteFeatures>();
            try
            {
                SetContext();
                List<FeatureResult> siteFeatures = _securityManager.GetSiteFeatures(siteId);
                siteFeaturesResponse.Result = new SiteFeatures();
                siteFeaturesResponse.Result.Features = new List<ModuleDC>();
                Translate.Convert(siteFeatures, siteFeaturesResponse.Result.Features);
                Site site = _securityManager.GetSite(siteId);
                siteFeaturesResponse.Result.SiteId = site.SiteId;
                siteFeaturesResponse.Result.SiteName = site.SiteName;
                siteFeaturesResponse.Result.SiteCode = site.SiteCode;
            }
            catch (Exception ex)
            {
                HandleError(ex, siteFeaturesResponse);
            }
            return siteFeaturesResponse;
        }

        /// <summary>
        /// GetFeatureSettings
        /// </summary>
        /// <param name="featureSettingRequest"></param>
        /// <returns></returns>
        public ServiceResponse<List<FeatureSettingDC>> GetFeatureSettings(int featureId)
        {
            ServiceResponse<List<FeatureSettingDC>> serviceResponse = new ServiceResponse<List<FeatureSettingDC>>();
            try
            {
                SetContext();
                List<SiteContentSettingResult> siteFeatureSettings = _securityManager.GetSiteFeatureSettings((FeatureEnum)featureId);
                serviceResponse.Result = new List<FeatureSettingDC>();
                foreach (SiteContentSettingResult featureSettingResult in siteFeatureSettings)
                {
                    serviceResponse.Result.Add(Mapper.Map<SiteContentSettingResult, FeatureSettingDC>(featureSettingResult));
                }
            }
            catch (Exception ex)
            {
                HandleError(ex, serviceResponse);
            }
            return serviceResponse;
        }

        /// <summary>
        /// SaveSiteFeatures
        /// </summary>
        /// <param name="saveSiteFeatureRequest"></param>
        /// <returns></returns>
        public ServiceResponse SaveSiteFeatures(SaveSiteFeaturesRequest saveSiteFeatureRequest)
        {

            ServiceResponse addSiteFeaturesResponse = new ServiceResponse();
            try
            {
                SetContext();
                List<int> lstFeatures = null;

                if (saveSiteFeatureRequest.Features.Length > 0)
                {
                    lstFeatures = new List<int>(saveSiteFeatureRequest.Features.Split(',').Select(int.Parse));
                }
                else
                {
                    lstFeatures = new List<int>();
                }
                _securityManager.SaveSiteFeatures(lstFeatures, saveSiteFeatureRequest.SiteId, saveSiteFeatureRequest.IsCreateNavigationChecked);
            }
            catch (Exception ex)
            {
                HandleError(ex, addSiteFeaturesResponse);
            }

            return addSiteFeaturesResponse;

        }

        #endregion

        #region Navigation

        /// <summary>
        /// GetNavigations
        /// </summary>
        /// <returns>navigationsRequest</returns>
        public ServiceResponse<List<NavigationDC>> GetNavigations()
        {
            ServiceResponse<List<NavigationDC>> navigationsResponse = new ServiceResponse<List<NavigationDC>>();
            navigationsResponse.Result = new List<NavigationDC>();
            try
            {
                SetContext();
                _securityManager.GetNavigations().ForEach
                (
                   navigation => navigationsResponse.Result.Add(Mapper.Map<NavigationResult, NavigationDC>(navigation))
                );
            }
            catch (Exception ex)
            {
                HandleError(ex, navigationsResponse);
            }
            return navigationsResponse;
        }

        /// <summary>
        /// GetNavigation
        /// </summary>
        /// <returns>NavigationResponse</returns>
        public ServiceResponse<NavigationDC> GetNavigation(int navigationId)
        {
            ServiceResponse<NavigationDC> navigationResponse = new ServiceResponse<NavigationDC>();
            try
            {
                SetContext();
                navigationResponse.Result = Mapper.Map<NavigationResult, NavigationDC>(_securityManager.GetNavigation(navigationId));
            }
            catch (Exception ex)
            {
                HandleError(ex, navigationResponse);
            }
            return navigationResponse;
        }

        /// <summary>
        /// AddNavigation
        /// </summary>
        /// <returns>FeaturesResponse</returns>
        public ServiceResponse<int> SaveNavigation(NavigationDC navigation)
        {
            ServiceResponse<int> addNavigationResponse = new ServiceResponse<int>();
            try
            {
                SetContext();
                Navigation navigationModel = Mapper.Map<NavigationDC, Navigation>(navigation);
                _securityManager.SaveNavigation(navigationModel);
                addNavigationResponse.Result = navigation.NavigationId;
            }
            catch (Exception ex)
            {
                HandleError(ex, addNavigationResponse);
            }
            return addNavigationResponse;
        }

        /// <summary>
        /// DeleteNavigation
        /// </summary>
        /// <returns>FeaturesResponse</returns>
        public ServiceResponse DeleteNavigation(int navigationId)
        {
            ServiceResponse deleteNavigationResponse = new ServiceResponse();
            try
            {
                SetContext();
                _securityManager.DeleteNavigation(navigationId);
            }
            catch (Exception ex)
            {
                HandleError(ex, deleteNavigationResponse);
            }
            return deleteNavigationResponse;
        }

        /// <summary>
        /// GetUserNavigations
        /// </summary>
        /// <param name="userNavigationsRequest"></param>
        /// <returns></returns>
        public ServiceResponse<List<NavigationDC>> GetUserNavigations()
        {
            ServiceResponse<List<NavigationDC>> userNavigationsResponse = new ServiceResponse<List<NavigationDC>>();
            userNavigationsResponse.Result = new List<NavigationDC>();
            try
            {
                SetContext();
                List<NavigationResult> userNavigations = _securityManager.GetUserNavigations();
                userNavigations.ForEach
                    (
                       navigation => userNavigationsResponse.Result.Add(Mapper.Map<NavigationResult, NavigationDC>(navigation))
                    );
            }
            catch (Exception ex)
            {
                HandleError(ex, userNavigationsResponse);
            }
            return userNavigationsResponse;
        }

        #endregion

        #region Modules

        /// <summary>
        /// ModulesResponse
        /// </summary>
        /// <param name="moduleRequest"></param>
        public ServiceResponse<List<ModuleDC>> GetModules()
        {
            ServiceResponse<List<ModuleDC>> modulesResponse = new ServiceResponse<List<ModuleDC>>();
            try
            {
                SetContext();
                modulesResponse.Result = new List<ModuleDC>();
                _securityManager.GetModules().ForEach
                    (
                       module => modulesResponse.Result.Add(Translate.Convert(module))
                    );
            }
            catch (Exception ex)
            {
                HandleError(ex, modulesResponse);
            }
            return modulesResponse;

        }

        /// <summary>
        /// ModuleResponse
        /// </summary>
        /// <param name="moduleId"></param>
        public ServiceResponse<ModuleDC> GetModule(int moduleId)
        {
            ServiceResponse<ModuleDC> moduleResponse = new ServiceResponse<ModuleDC>();
            try
            {
                SetContext();
                moduleResponse.Result = Translate.Convert(_securityManager.GetModule(moduleId));
            }
            catch (Exception ex)
            {
                HandleError(ex, moduleResponse);
            }
            return moduleResponse;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public ServiceResponse<List<ModuleFeatureDC>> GetFeatures(int moduleId)
        {
            ServiceResponse<List<ModuleFeatureDC>> featureResponse = new ServiceResponse<List<ModuleFeatureDC>>();
            try
            {
                SetContext();
                List<Feature> features = _securityManager.GetFeatures(moduleId);
                featureResponse.Result = new List<ModuleFeatureDC>();
                features.ForEach(f =>
                    {
                        featureResponse.Result.Add(Translate.Convert(f));
                    });
            }
            catch (Exception ex)
            {
                HandleError(ex, featureResponse);
            }
            return featureResponse;
        }

        public ServiceResponse SaveModule(ModuleDC module)
        {
            ServiceResponse moduleResponse = new ServiceResponse();
            try
            {
                SetContext();
                _securityManager.SaveModule(Mapper.Map<ModuleDC, Module>(module));
            }
            catch (Exception ex)
            {
                HandleError(ex, moduleResponse);
            }
            return moduleResponse;
        }

        public ServiceResponse DeleteModule(int moduleId)
        {
            ServiceResponse moduleResponse = new ServiceResponse();
            try
            {
                SetContext();
                _securityManager.DeleteModule(moduleId);
            }
            catch (Exception ex)
            {
                HandleError(ex, moduleResponse);
            }
            return moduleResponse;
        }


        /// <summary>
        /// SaveSite
        /// </summary>
        /// <returns></returns>
        public ServiceResponse SaveFeature(FeatureDC feature)
        {
            ServiceResponse addFeatureResponse = new ServiceResponse();
            try
            {
                SetContext();
                Feature featureModel = Mapper.Map<FeatureDC, Feature>(feature);
                _securityManager.SaveFeature(featureModel);
            }
            catch (Exception ex)
            {
                HandleError(ex, addFeatureResponse);
            }
            return addFeatureResponse;
        }


        /// <summary>
        /// DeleteSite
        /// </summary>
        /// <returns></returns>
        public ServiceResponse DeleteFeature(int featureId)
        {
            ServiceResponse deleteFeatureResponse = new ServiceResponse();
            try
            {
                SetContext();
                _securityManager.DeleteFeature(featureId);
            }
            catch (Exception ex)
            {
                HandleError(ex, deleteFeatureResponse);
            }
            return deleteFeatureResponse;
        }

        #endregion

    }

}
