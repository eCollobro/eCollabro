#region References
using eCollabro.Client.ServiceProxy;
using eCollabro.Service.Core;
using eCollabro.Service.DataContracts;
using eCollabro.Service.DataContracts.Core;
using eCollabro.Service.DataContracts.RequestWrapper;
using eCollabro.Service.DataContracts.ResponseWrapper;
using System;
using System.Collections.Generic;
using eCollabro.Utilities;
using eCollabro.DataMapper;
using eCollabro.Client.Models.Core;
using eCollabro.Service.DataMembers.Core;
using eCollabro.Client.Interface;
using eCollabro.Common;
using eCollabro.Client.ServiceProxy.Interface;

#endregion

namespace eCollabro.Client
{

    /// <summary>
    /// SecurityClient
    /// </summary>
    public class SecurityClient : BaseClient, ISecurityClient
    {
        private ISecurityProxy  _securityProxy = null;

        public SecurityClient()
        {
            _securityProxy = new SecurityServiceProxy();
            _securityProxy.Initialize(SecurityClientTranslate.Convert(UserContext));
        }

        #region Site Configuration

        /// <summary>
        /// GetSiteConfiguration
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public SiteConfigurationModel GetSiteConfiguration()
        {
            SiteConfigurationModel siteConfigurationModel = new SiteConfigurationModel();
            ServiceResponse<SiteConfigurationDC> siteConfigurationResponse = _securityProxy.Execute(opt => opt.GetSiteConfiguration());
            if (siteConfigurationResponse.Status == ResponseStatus.Success)
            {
                siteConfigurationModel = Mapper.Map<SiteConfigurationDC, SiteConfigurationModel>(siteConfigurationResponse.Result);
            }
            else
            {
                HandleError(siteConfigurationResponse.Status, siteConfigurationResponse.ResponseMessage);
            }
            return siteConfigurationModel;

        }

        /// <summary>
        /// SaveSiteConfiguration
        /// </summary>
        /// <param name="siteConfigurationModel"></param>
        /// <returns></returns>
        public void SaveSiteConfiguration(SiteConfigurationModel siteConfigurationModel)
        {
            SiteConfigurationDC siteConfigurationRequest = Mapper.Map<SiteConfigurationModel, SiteConfigurationDC>(siteConfigurationModel);
            ServiceResponse saveSiteConfigurationResponse = _securityProxy.Execute(opt => opt.SaveSiteConfiguration(siteConfigurationRequest));
            if (saveSiteConfigurationResponse.Status != ResponseStatus.Success)
                HandleError(saveSiteConfigurationResponse.Status, saveSiteConfigurationResponse.ResponseMessage);
        }
        #endregion

        #region Account

        /// <summary>
        /// CreateAccount
        /// </summary>
        /// <param name="registerModel"></param>
        public void CreateAccount(RegisterModel registerModel)
        {
            RegisterDC registerDC = new RegisterDC();
            registerDC.UserName = registerModel.UserName;
            registerDC.Password = DataEncryption.Encrypt(registerModel.Password);
            registerDC.Email = registerModel.Email;
            ServiceResponse createAccountResponse = _securityProxy.Execute(opt => opt.CreateAccount(registerDC));
            if (createAccountResponse.Status != ResponseStatus.Success)
            {
                HandleError(createAccountResponse.Status, createAccountResponse.ResponseMessage);
            }
        }

        /// <summary>
        /// VerifyAccount
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public void VerifyAccount(string username, string verificationToken)
        {
            string password = string.Empty;
            UserTokenVerificationDC verificationRequest = new UserTokenVerificationDC();
            verificationRequest.UserName = username;
            verificationRequest.Token = verificationToken;
            ServiceResponse verifyAccountResponse = _securityProxy.Execute(opt => opt.VerifyAccount(verificationRequest));
            if (verifyAccountResponse.Status != ResponseStatus.Success)
            {
                HandleError(verifyAccountResponse.Status, verifyAccountResponse.ResponseMessage);
            }
        }

        /// <summary>
        /// AuthenticateUser
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void AuthenticateUser(string username, string password)
        {
            AuthenticateUserRequest verifyLoginRequest = new AuthenticateUserRequest();
            verifyLoginRequest.Username = username;
            verifyLoginRequest.Password = DataEncryption.Encrypt(password);
            verifyLoginRequest.UserContext = SecurityClientTranslate.Convert(UserContext);
            ServiceResponse verifyLoginResponse = _securityProxy.Execute(opt => opt.AuthenticateUser(verifyLoginRequest));
            if (verifyLoginResponse.Status != ResponseStatus.Success)
            {
                HandleError(verifyLoginResponse.Status, verifyLoginResponse.ResponseMessage);
            }
        }

        /// <summary>
        /// ResetPassword
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="passwordResetToken"></param>
        /// <returns></returns>
        public string ResetPassword(string Username, string passwordResetToken)
        {
            string password = string.Empty;
            UserTokenVerificationDC resetPasswordRequest = new UserTokenVerificationDC();
            resetPasswordRequest.UserName = Username;
            resetPasswordRequest.Token = passwordResetToken;
            ServiceResponse<string> changePasswordResponse = _securityProxy.Execute(opt => opt.ResetPassword(resetPasswordRequest));
            if (changePasswordResponse.Status == ResponseStatus.Success)
            {
                password = DataEncryption.Decrypt(changePasswordResponse.Result);
            }
            else
            {
                HandleError(changePasswordResponse.Status, changePasswordResponse.ResponseMessage);
            }
            return password;

        }


        /// <summary>
        /// ChangePassword
        /// </summary>
        /// <param name="ChangePasswordModel"></param>
        public void ChangePassword(ChangePasswordModel changePasswordModel)
        {
            ChangePasswordDC changePasswordRequest = new ChangePasswordDC();
            changePasswordRequest.OldPassword = DataEncryption.Encrypt(changePasswordModel.OldPassword);
            changePasswordRequest.NewPassword = DataEncryption.Encrypt(changePasswordModel.NewPassword);
            changePasswordRequest.UserName = changePasswordModel.UserName;
            ServiceResponse changePasswordResponse = _securityProxy.Execute(opt => opt.ChangePassword(changePasswordRequest));
            if (changePasswordResponse.Status != ResponseStatus.Success)
            {
                HandleError(changePasswordResponse.Status, changePasswordResponse.ResponseMessage);
            }
        }

        /// <summary>
        /// GeneratePasswordResetToken
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public UserTokenVerificationModel GeneratePasswordResetToken(string username)
        {
            UserTokenVerificationModel passwordResetTokenModel = new UserTokenVerificationModel();
            ServiceResponse<UserTokenVerificationDC> generatePasswordResetTokenResponse = _securityProxy.Execute(opt => opt.GeneratePasswordResetToken(username));
            if (generatePasswordResetTokenResponse.Status == ResponseStatus.Success)
            {
                passwordResetTokenModel = Mapper.Map<UserTokenVerificationDC, UserTokenVerificationModel>(generatePasswordResetTokenResponse.Result);
            }
            else
            {
                HandleError(generatePasswordResetTokenResponse.Status, generatePasswordResetTokenResponse.ResponseMessage);
            }
            return passwordResetTokenModel;
        }

        /// <summary>
        /// GetUserFeaturePermissions for Site 
        /// </summary>
        /// <returns></returns>
        public List<UserFeaturePermissionModel> GetUserFeaturePermissions(int featureId)
        {
            List<UserFeaturePermissionModel> userFeaturePermissions = new List<UserFeaturePermissionModel>();
            using (SecurityServiceProxy _securityProxy = new SecurityServiceProxy())
            {
                ServiceResponse<List<UserFeaturePermissionDC>> UserFeaturePermissionsResponse = _securityProxy.Execute(opt => opt.GetUserFeaturePermissions(featureId));

                if (UserFeaturePermissionsResponse.Status == ResponseStatus.Success)
                {
                    foreach (UserFeaturePermissionDC UserFeaturePermission in UserFeaturePermissionsResponse.Result)
                    {
                        userFeaturePermissions.Add(Mapper.Map<UserFeaturePermissionDC, UserFeaturePermissionModel>(UserFeaturePermission));
                    }
                }
                else
                {
                    HandleError(UserFeaturePermissionsResponse.Status, UserFeaturePermissionsResponse.ResponseMessage);
                }
            }
            return userFeaturePermissions;
        }


        #endregion

        #region Site

        /// <summary>
        /// GetSites
        /// </summary>
        /// <param name="featureId"></param>
        /// <returns></returns>
        public List<SiteModel> GetSites()
        {
            List<SiteModel> Sites = new List<SiteModel>();
            ServiceResponse<List<SiteDC>> sitesResponse = _securityProxy.Execute(opt => sitesResponse = opt.GetSites());
            if (sitesResponse.Status == ResponseStatus.Success)
            {
                sitesResponse.Result.ForEach(
                  site => Sites.Add(Mapper.Map<SiteDC, SiteModel>(site))
                  );
            }
            else
            {
                HandleError(sitesResponse.Status, sitesResponse.ResponseMessage);
            }
            return Sites;
        }

        /// <summary>
        /// GetSite
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public SiteModel GetSite(int siteId)
        {
            SiteModel SiteModel = new SiteModel();
            ServiceResponse<SiteDC> siteResponse = _securityProxy.Execute(opt => opt.GetSite(siteId));
            if (siteResponse.Status == ResponseStatus.Success)
            {
                SiteModel = Mapper.Map<SiteDC, SiteModel>(siteResponse.Result);
            }
            else
            {
                HandleError(siteResponse.Status, siteResponse.ResponseMessage);
            }
            return SiteModel;

        }

        /// <summary>
        /// SaveSite
        /// </summary>
        /// <param name="siteModel"></param>
        /// <returns></returns>
        public void SaveSite(SiteModel siteModel)
        {
            SiteDC site = Mapper.Map<SiteModel, SiteDC>(siteModel);
            ServiceResponse<int> saveSiteResponse = _securityProxy.Execute(opt => opt.SaveSite(site));
            if (saveSiteResponse.Status != ResponseStatus.Success)
                HandleError(saveSiteResponse.Status, saveSiteResponse.ResponseMessage);
            else
                siteModel.SiteId = saveSiteResponse.Result;
        }

        /// <summary>
        /// DeleteSite
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public void DeleteSite(int siteId)
        {
            ServiceResponse deleteSiteResponse = _securityProxy.Execute(opt => opt.DeleteSite(siteId));
            if (deleteSiteResponse.Status != ResponseStatus.Success)
                HandleError(deleteSiteResponse.Status, deleteSiteResponse.ResponseMessage);
        }

        /// <summary>
        /// Copy Existing Site
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public void CopySite(int siteId)
        {
            ServiceResponse copySiteResponse = _securityProxy.Execute(opt => opt.CopySite(siteId));
            if (copySiteResponse.Status != ResponseStatus.Success)
                HandleError(copySiteResponse.Status, copySiteResponse.ResponseMessage);
        }

        #endregion

        #region Site Features

        /// <summary>
        /// GetSiteFeatures
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public SiteFeaturesModel GetSiteFeatures(int siteId)
        {
            SiteFeaturesModel siteFeatureModel = new SiteFeaturesModel();
            ServiceResponse<SiteFeatures> siteFeaturesResponse = _securityProxy.Execute(opt => opt.GetSiteFeatures(siteId));

            if (siteFeaturesResponse.Status == ResponseStatus.Success)
            {
                siteFeatureModel.SiteId = siteFeaturesResponse.Result.SiteId;
                siteFeatureModel.SiteCode = siteFeaturesResponse.Result.SiteCode;
                siteFeatureModel.SiteName = siteFeaturesResponse.Result.SiteName;
                foreach (var feature in siteFeaturesResponse.Result.Features)
                {
                    siteFeatureModel.Features.Add(SecurityClientTranslate.Convert(feature));
                }
            }
            else
            {
                HandleError(siteFeaturesResponse.Status, siteFeaturesResponse.ResponseMessage);
            }
            return siteFeatureModel;

        }

        /// <summary>
        /// GetSiteFeatures
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public SiteFeaturesModel GetSiteFeaturesSettings(int siteId)
        {
            SiteFeaturesModel siteFeatureModel = new SiteFeaturesModel();
            ServiceResponse<SiteFeatures> siteFeaturesResponse = _securityProxy.Execute(opt => opt.GetSiteFeaturesSettings(siteId));
            if (siteFeaturesResponse.Status == ResponseStatus.Success)
            {
                siteFeatureModel.SiteId = siteFeaturesResponse.Result.SiteId;
                siteFeatureModel.SiteCode = siteFeaturesResponse.Result.SiteCode;
                siteFeatureModel.SiteName = siteFeaturesResponse.Result.SiteName;
                foreach (ModuleDC module in siteFeaturesResponse.Result.Features)
                {
                    ModuleModel moduleModel = Mapper.Map<ModuleDC, ModuleModel>(module);
                    moduleModel.ModuleFeatures = new List<ModuleFeatureModel>();
                    foreach (ModuleFeatureDC feature in module.Features)
                    {
                        ModuleFeatureModel featureModel = Mapper.Map<ModuleFeatureDC, ModuleFeatureModel>(feature);
                        featureModel.SiteContentSettings = new List<SiteContentSettingModel>();
                        foreach (SiteContentSettingDC siteContentSetting in feature.SiteContentSettings)
                        {
                            featureModel.SiteContentSettings.Add(Mapper.Map<SiteContentSettingDC, SiteContentSettingModel>(siteContentSetting));
                        }
                        moduleModel.ModuleFeatures.Add(featureModel);

                    }
                    siteFeatureModel.Features.Add(moduleModel);
                }
            }
            else
            {
                HandleError(siteFeaturesResponse.Status, siteFeaturesResponse.ResponseMessage);
            }
            return siteFeatureModel;
        }

        /// <summary>
        /// SaveSiteFeaturesSettings
        /// </summary>
        /// <param name="siteFeaturesModel"></param>
        public void SaveSiteFeaturesSettings(SiteFeaturesModel siteFeaturesModel)
        {
            List<ModuleFeatureDC> serviceRequest = new List<ModuleFeatureDC>();
            foreach (ModuleModel module in siteFeaturesModel.Features)
            {
                foreach (ModuleFeatureModel feature in module.ModuleFeatures)
                {
                    if (feature.SiteContentSettings.Count > 0)
                    {
                        ModuleFeatureDC featureDC = new ModuleFeatureDC();
                        featureDC.FeatureId = feature.FeatureId;
                        featureDC.SiteContentSettings = new List<SiteContentSettingDC>();
                        foreach (SiteContentSettingModel contentSetting in feature.SiteContentSettings)
                        {
                            if (contentSetting.IsAssigned)
                            {
                                featureDC.SiteContentSettings.Add(new SiteContentSettingDC { SiteId = contentSetting.SiteId, FeatureId = contentSetting.FeatureId, ContentSettingId = contentSetting.ContentSettingId, IsAssigned = true });
                            }
                        }
                        serviceRequest.Add(featureDC);
                    }
                }
            }
            ServiceResponse saveSiteFeaturesSettingsResponse = _securityProxy.Execute(opt => opt.SaveSiteFeaturesSettings(serviceRequest));
            if (saveSiteFeaturesSettingsResponse.Status != ResponseStatus.Success)
                HandleError(saveSiteFeaturesSettingsResponse.Status, saveSiteFeaturesSettingsResponse.ResponseMessage);
        }

        /// <summary>
        /// GetFeatureSettings
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        public List<FeatureSettingModel> GetFeatureSettings(FeatureEnum feature)
        {
            List<FeatureSettingModel> featureSettings = new List<FeatureSettingModel>();
            ServiceResponse<List<FeatureSettingDC>> featureSettingsResponse = _securityProxy.Execute(opt => opt.GetFeatureSettings((int)feature));

            if (featureSettingsResponse.Status == ResponseStatus.Success)
            {
                foreach (FeatureSettingDC featureSetting in featureSettingsResponse.Result)
                {
                    featureSettings.Add(Mapper.Map<FeatureSettingDC, FeatureSettingModel>(featureSetting));
                }
            }
            else
            {
                HandleError(featureSettingsResponse.Status, featureSettingsResponse.ResponseMessage);
            }
            return featureSettings;
        }

        /// <summary>
        /// SaveSiteFeatures
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="features"></param>
        /// <param name="isCreateNavigationChecked"></param>
        public void SaveSiteFeatures(int siteId, string features, Boolean isCreateNavigationChecked)
        {
            SaveSiteFeaturesRequest addSiteFeaturesRequest = new SaveSiteFeaturesRequest();
            addSiteFeaturesRequest.UserContext = SecurityClientTranslate.Convert(UserContext);
            addSiteFeaturesRequest.Features = features;
            addSiteFeaturesRequest.SiteId = siteId;
            addSiteFeaturesRequest.IsCreateNavigationChecked = isCreateNavigationChecked;

            ServiceResponse addSiteFeaturesResponse = _securityProxy.Execute(opt => opt.SaveSiteFeatures(addSiteFeaturesRequest));

            if (addSiteFeaturesResponse.Status != ResponseStatus.Success)
                HandleError(addSiteFeaturesResponse.Status, addSiteFeaturesResponse.ResponseMessage);

        }


        #endregion

        #region Navigation

        /// <summary>
        /// GetNavigations
        /// </summary>
        /// <returns></returns>
        public List<NavigationModel> GetNavigations()
        {
            List<NavigationModel> navigationModels = new List<NavigationModel>();
            ServiceResponse<List<NavigationDC>> navigationsResponse = _securityProxy.Execute(opt => opt.GetNavigations());
            if (navigationsResponse.Status == ResponseStatus.Success)
            {
                navigationsResponse.Result.ForEach(
                    navigation => navigationModels.Add(Mapper.Map<NavigationDC, NavigationModel>(navigation))
                    );
            }
            else
            {
                HandleError(navigationsResponse.Status, navigationsResponse.ResponseMessage);
            }
            return navigationModels;
        }

        /// <summary>
        /// GetNavigation
        /// </summary>
        /// <param name="featureId"></param>
        /// <returns></returns>
        public NavigationModel GetNavigation(int navigationId)
        {
            NavigationModel navigationModel = new NavigationModel();
            ServiceResponse<NavigationDC> navigationResponse = _securityProxy.Execute(opt => opt.GetNavigation(navigationId));

            if (navigationResponse.Status == ResponseStatus.Success)
            {
                navigationModel = Mapper.Map<NavigationDC, NavigationModel>(navigationResponse.Result);
            }
            else
            {
                HandleError(navigationResponse.Status, navigationResponse.ResponseMessage);
            }
            return navigationModel;
        }

        /// <summary>
        /// SaveNavigation
        /// </summary>
        /// <param name="featureId"></param>
        /// <returns></returns>
        public void SaveNavigation(NavigationModel navigationModel)
        {
            NavigationDC navigationDC = Mapper.Map<NavigationModel, NavigationDC>(navigationModel);
            ServiceResponse<int> addNavigationResponse = _securityProxy.Execute(opt => opt.SaveNavigation(navigationDC));
            if (addNavigationResponse.Status != ResponseStatus.Success)
                HandleError(addNavigationResponse.Status, addNavigationResponse.ResponseMessage);
            else
                navigationModel.NavigationId = addNavigationResponse.Result;
        }

        /// <summary>
        /// DeleteFeature
        /// </summary>
        /// <param name="featureId"></param>
        /// <returns></returns>
        public void DeleteNavigation(int navigationId)
        {
            ServiceResponse deleteNavigationResponse = _securityProxy.Execute(opt => opt.DeleteNavigation(navigationId));
            if (deleteNavigationResponse.Status != ResponseStatus.Success)
                HandleError(deleteNavigationResponse.Status, deleteNavigationResponse.ResponseMessage);
        }

        /// <summary>
        /// GetUserNavigations
        /// </summary>
        /// <returns></returns>
        public List<UserNavigationModel> GetUserNavigations()
        {
            List<UserNavigationModel> navigationFeatureModels = new List<UserNavigationModel>();
            ServiceResponse<List<NavigationDC>> navigationFeaturesResponse = _securityProxy.Execute(opt => opt.GetUserNavigations());
            if (navigationFeaturesResponse.Status == ResponseStatus.Success)
            {
                navigationFeaturesResponse.Result.ForEach(
                    navigationFeatureModel => navigationFeatureModels.Add(Mapper.Map<NavigationDC, UserNavigationModel>(navigationFeatureModel))
                    );
            }
            else
            {
                HandleError(navigationFeaturesResponse.Status, navigationFeaturesResponse.ResponseMessage);
            }

            return navigationFeatureModels;
        }

        #endregion

        #region Role

        /// <summary>
        /// GetRoles for Site 
        /// </summary>
        /// <returns></returns>
        public List<RoleModel> GetRoles(int siteId)
        {
            List<RoleModel> roles = new List<RoleModel>();
            ServiceResponse<List<RoleDC>> rolesResponse = _securityProxy.Execute(opt => opt.GetRoles());

            if (rolesResponse.Status == ResponseStatus.Success)
            {
                foreach (RoleDC role in rolesResponse.Result)
                {
                    roles.Add(Mapper.Map<RoleDC, RoleModel>(role));
                }
            }
            else
            {
                HandleError(rolesResponse.Status, rolesResponse.ResponseMessage);
            }
            return roles;
        }

        /// <summary>
        /// GetRoles - for CurrentContext
        /// </summary>
        /// <returns></returns>
        public List<RoleModel> GetRoles()
        {
            return GetRoles(UserContext.SiteId);
        }

        /// <summary>
        /// GetRole
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public RoleModel GetRole(int roleId)
        {
            RoleModel roleModel = null;
            ServiceResponse<RoleDC> roleResponse = _securityProxy.Execute(opt => opt.GetRole(roleId));
            if (roleResponse.Status == ResponseStatus.Success)
            {
                roleModel = Mapper.Map<RoleDC, RoleModel>(roleResponse.Result);
            }
            else
            {
                HandleError(roleResponse.Status, roleResponse.ResponseMessage);
            }
            return roleModel;
        }

        /// <summary>
        /// AddRole
        /// </summary>
        /// <param name="roleModel"></param>
        public void SaveRole(RoleModel roleModel)
        {
            RoleDC roleRequest = Mapper.Map<RoleModel, RoleDC>(roleModel);
            ServiceResponse<int> saveRoleResponse = _securityProxy.Execute(opt => opt.SaveRole(roleRequest));

            if (saveRoleResponse.Status != ResponseStatus.Success)
                HandleError(saveRoleResponse.Status, saveRoleResponse.ResponseMessage);
            else
                roleModel.RoleId = saveRoleResponse.Result;
        }

        /// <summary>
        /// DeleteRole
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public void DeleteRole(int roleId)
        {
            ServiceResponse deleteRoleResponse = _securityProxy.Execute(opt => opt.DeleteRole(roleId));
            if (deleteRoleResponse.Status != ResponseStatus.Success)
                HandleError(deleteRoleResponse.Status, deleteRoleResponse.ResponseMessage);
        }

        #endregion

        #region Role Feature
        /// <summary>
        /// GetRoleFeatures
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>RoleFeaturesModel</returns>
        public RoleFeaturesModel GetRoleFeatures(int roleId)
        {
            RoleFeaturesModel roleFeatures = new RoleFeaturesModel();
            ServiceResponse<RoleFeatures> roleFeaturesResponse = _securityProxy.Execute(opt => opt.GetRoleFeatures(roleId));
            if (roleFeaturesResponse.Status == ResponseStatus.Success)
            {
                roleFeatures.RoleId = roleFeaturesResponse.Result.RoleId;
                roleFeatures.RoleCode = roleFeaturesResponse.Result.RoleCode;
                roleFeatures.RoleName = roleFeaturesResponse.Result.RoleName;
                roleFeatures.Features = new List<ModuleModel>();
                foreach (ModuleDC moduleDC in roleFeaturesResponse.Result.Features)
                {
                    ModuleModel module = Mapper.Map<ModuleDC, ModuleModel>(moduleDC);
                    roleFeatures.Features.Add(module);
                    module.ModuleFeatures = new List<ModuleFeatureModel>();
                    foreach (ModuleFeatureDC feature in moduleDC.Features)
                    {
                        ModuleFeatureModel featureModel = Mapper.Map<ModuleFeatureDC, ModuleFeatureModel>(feature);
                        module.ModuleFeatures.Add(featureModel);
                        featureModel.RoleFeaturePermissions = new List<FeaturePermissionModel>();
                        foreach (FeaturePermissionDC featurePermission in feature.RoleFeaturePermissions)
                        {
                            featureModel.RoleFeaturePermissions.Add(Mapper.Map<FeaturePermissionDC, FeaturePermissionModel>(featurePermission));
                        }
                    }
                }
            }
            else
            {
                HandleError(roleFeaturesResponse.Status, roleFeaturesResponse.ResponseMessage);
            }
            return roleFeatures;
        }

        public void SaveRoleFeatures(RoleFeaturesModel roleFeatures)
        {
            AddRoleFeaturesRequest addRoleFeaturesRequest = new AddRoleFeaturesRequest();
            addRoleFeaturesRequest.RoleId = roleFeatures.RoleId;
            addRoleFeaturesRequest.Features = new List<ModuleFeatureDC>();
            foreach (ModuleModel module in roleFeatures.Features)
            {
                foreach (ModuleFeatureModel feature in module.ModuleFeatures)
                {
                    if (feature.IsSelected)
                    {
                        ModuleFeatureDC featureDC = new ModuleFeatureDC();
                        featureDC.FeatureId = feature.FeatureId;
                        featureDC.RoleFeaturePermissions = new List<FeaturePermissionDC>();
                        foreach (FeaturePermissionModel permission in feature.RoleFeaturePermissions)
                        {
                            if (permission.IsAssigned)
                            {
                                featureDC.RoleFeaturePermissions.Add(new FeaturePermissionDC { FeatureId = feature.FeatureId, ContentPermissionId = permission.ContentPermissionId });
                            }
                        }
                        addRoleFeaturesRequest.Features.Add(featureDC);
                    }
                }
            }

            ServiceResponse addRoleFeaturesResponse = _securityProxy.Execute(opt =>  opt.SaveRoleFeatures(addRoleFeaturesRequest));
            if (addRoleFeaturesResponse.Status != ResponseStatus.Success)
                HandleError(addRoleFeaturesResponse.Status, addRoleFeaturesResponse.ResponseMessage);

        }

        #endregion

        #region Modules

        /// <summary>
        /// GetModules
        /// </summary>
        public List<ModuleModel> GetModules()
        {
            List<ModuleModel> modules = null;
            ServiceResponse<List<ModuleDC>> modulesResponse = _securityProxy.Execute(opt => opt.GetModules());

            if (modulesResponse.Status == ResponseStatus.Success)
            {
                modules = new List<ModuleModel>();

                modulesResponse.Result.ForEach(
                    module => modules.Add(SecurityClientTranslate.Convert(module))
                    );
            }
            else
            {
                HandleError(modulesResponse.Status, modulesResponse.ResponseMessage);
            }
            return modules;
        }

        public ModuleModel GetModule(int moduleId)
        {
            ModuleModel module = null;
            ServiceResponse<ModuleDC> moduleResponse = _securityProxy.Execute(opt => opt.GetModule(moduleId));

            if (moduleResponse.Status == ResponseStatus.Success)
            {
                module = SecurityClientTranslate.Convert(moduleResponse.Result);
            }
            else
            {
                HandleError(moduleResponse.Status, moduleResponse.ResponseMessage);
            }
            return module;
        }


        public List<ModuleFeatureModel> GetFeatures(int moduleId)
        {
            List<ModuleFeatureModel> features = null;
            ServiceResponse<List<ModuleFeatureDC>> featuresResponse = _securityProxy.Execute(opt => opt.GetFeatures(moduleId));

            if (featuresResponse.Status == ResponseStatus.Success)
            {
                features = new List<ModuleFeatureModel>();
                featuresResponse.Result.ForEach(
                    f => features.Add(SecurityClientTranslate.Convert(f))
                    );
            }
            else
            {
                HandleError(featuresResponse.Status, featuresResponse.ResponseMessage);
            }
            return features; ;
        }

        /// <summary>
        /// AddRole
        /// </summary>
        /// <param name="roleModel"></param>
        public void SaveModule(ModuleModel moduleModel)
        {
            ModuleDC moduleRequest = SecurityClientTranslate.Convert(moduleModel);
            ServiceResponse moduleResponse = _securityProxy.Execute(opt => opt.SaveModule(moduleRequest));

            if (moduleResponse.Status != ResponseStatus.Success)
                HandleError(moduleResponse.Status, moduleResponse.ResponseMessage);
        }


        /// <summary>
        /// Delete Module
        /// </summary>
        /// <param name="roleModel"></param>
        public void DeleteModule(int moduleId)
        {
            ServiceResponse moduleResponse = _securityProxy.Execute(opt => opt.DeleteModule(moduleId));

            if (moduleResponse.Status != ResponseStatus.Success)
                HandleError(moduleResponse.Status, moduleResponse.ResponseMessage);
        }


        #endregion

        #region Manage Users

        /// <summary>
        /// GetUsers
        /// </summary>
        /// <returns></returns>
        public List<UserModel> GetUsers()
        {
            List<UserModel> users = new List<UserModel>();
            ServiceResponse<List<UserDetailDC>> usersServiceResponse = _securityProxy.Execute(opt => opt.GetUsers());

            if (usersServiceResponse.Status == ResponseStatus.Success)
            {
                foreach (UserDetailDC user in usersServiceResponse.Result)
                {
                    UserModel userModel = Mapper.Map<UserDetailDC, UserModel>(user);
                    if (userModel.UserRoles == null)
                        userModel.UserRoles = new List<RoleModel>();
                    foreach (RoleDC role in user.UserRoles)
                    {
                        userModel.UserRoles.Add(Mapper.Map<RoleDC, RoleModel>(role));
                    }
                    users.Add(userModel);
                }
            }
            else
            {
                HandleError(usersServiceResponse.Status, usersServiceResponse.ResponseMessage);
            }
            return users;
        }

        /// <summary>
        /// GetUser
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserModel GetUser(int userId)
        {
            UserModel user = new UserModel();
            ServiceResponse<UserDetailDC> userResponse = _securityProxy.Execute(opt => opt.GetUser(userId));
            if (userResponse.Status == ResponseStatus.Success)
            {
                user = Mapper.Map<UserDetailDC, UserModel>(userResponse.Result);
                user.UserRoles = new List<RoleModel>();
                foreach (RoleDC role in userResponse.Result.UserRoles)
                {
                    user.UserRoles.Add(Mapper.Map<RoleDC, RoleModel>(role));
                }
            }
            else
            {
                HandleError(userResponse.Status, userResponse.ResponseMessage);
            }
            return user;
        }

        /// <summary>
        /// SaveUser
        /// </summary>
        /// <param name="userDetailModel"></param>
        public void SaveUser(UserModel userDetailModel)
        {
            UserDetailDC saveUserRequest = Mapper.Map<UserModel, UserDetailDC>(userDetailModel);
            saveUserRequest.UserRoles = new List<RoleDC>();
            foreach (RoleModel role in userDetailModel.UserRoles)
            {
                saveUserRequest.UserRoles.Add(new RoleDC { RoleId = role.RoleId });
            }
            ServiceResponse<int> saveUserResponse = null;
            _securityProxy.Execute(opt => saveUserResponse = opt.SaveUser(saveUserRequest));
            if (saveUserResponse.Status != ResponseStatus.Success)
                HandleError(saveUserResponse.Status, saveUserResponse.ResponseMessage);
            else
                userDetailModel.UserId = saveUserResponse.Result;

        }

        /// <summary>
        /// DeleteUser
        /// </summary>
        /// <param name="userId"></param>
        public void DeleteUser(int userId)
        {
            ServiceResponse deleteUserResponse = _securityProxy.Execute(opt => opt.DeleteUser(userId));
            if (!deleteUserResponse.Status.Equals(ResponseStatus.Success))
                HandleError(deleteUserResponse.Status, deleteUserResponse.ResponseMessage);
        }

        /// <summary>
        /// ResetPassword
        /// </summary>
        /// <param name="userId"></param>
        public void ResetPassword(int userId)
        {
            ServiceResponse resetPasswordResponse = _securityProxy.Execute(opt => opt.ResetUserPassword(userId));
            if (!resetPasswordResponse.Status.Equals(ResponseStatus.Success))
                HandleError(resetPasswordResponse.Status, resetPasswordResponse.ResponseMessage);
        }


        /// <summary>
        /// UnlockUser
        /// </summary>
        /// <param name="userId"></param>
        public void UnlockUser(int userId)
        {
            ServiceResponse unlockUserResponse = _securityProxy.Execute(opt => opt.UnlockUser(userId));
            if (!unlockUserResponse.Status.Equals(ResponseStatus.Success))
                HandleError(unlockUserResponse.Status, unlockUserResponse.ResponseMessage);
        }

        /// <summary>
        /// ConfirmUser
        /// </summary>
        /// <param name="userId"></param>
        public void ConfirmUser(int userId)
        {
            ServiceResponse confirmUserResponse = _securityProxy.Execute(opt => opt.ConfirmUser(userId));
            if (!confirmUserResponse.Status.Equals(ResponseStatus.Success))
                HandleError(confirmUserResponse.Status, confirmUserResponse.ResponseMessage);
        }


        /// <summary>
        /// ApproveUser
        /// </summary>
        /// <param name="userId"></param>
        public void ApproveUser(int userId)
        {
            ServiceResponse approveUserResponse = _securityProxy.Execute(opt => opt.ApproveUser(userId));
            if (!approveUserResponse.Status.Equals(ResponseStatus.Success))
                HandleError(approveUserResponse.Status, approveUserResponse.ResponseMessage);
        }

        #endregion


        #region Features




        #endregion
    }
}