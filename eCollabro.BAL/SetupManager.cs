// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using eCollabro.BAL.Entities.Models;
using eCollabro.Exceptions;
using System.IO;
using eCollabro.DataMapper;
using eCollabro.Common;

#endregion

namespace eCollabro.BAL
{
    /// <summary>
    /// SetupManager
    /// </summary>
    public class SetupManager : BaseManager
    {
        #region Data Members

        private SecurityManager _securityManager;

        #endregion

        #region Methods

        #region Check Permission
        /// <summary>
        /// CheckSiteCollectionAdminPermission
        /// </summary>
        /// <param name="userId"></param>
        private void CheckSiteCollectionAdminPermission(int userId)
        {
            if (_securityManager == null)
                _securityManager = new SecurityManager();
            bool isSiteCollectionAdmin = _securityManager.CheckSiteCollectionAdmin(userId);
            if (!isSiteCollectionAdmin)
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);
        }

        #endregion

        #region eCollabro Setup

        /// <summary>
        /// IsEcollabroSetupReady
        /// </summary>
        /// <returns></returns>
        public bool IsEcollabroSetupReady()
        {
            return eCollabroDbContext.Repository<UserMembership>().Query().Get().Count() > 0;
        }

        /// <summary>
        /// SetupEcollabro
        /// </summary>
        /// <param name="adminUser"></param>
        public void SetupEcollabro(UserMembership adminUser)
        {
            if (IsEcollabroSetupReady())
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.eCollabroAlreadySetup), CoreValidationMessagesConstants.eCollabroAlreadySetup);
            else
            {
                KeyValuePair<string, string> pass = DataEncryption.CreateHashedText(DataEncryption.Decrypt(adminUser.Password), true);

                //language
                eCollabroDbContext.Repository<lkpLanguage>().Insert(new lkpLanguage { Language = "English", LanguageCode = "en-US", CreatedById = 1, CreatedOn = DateTime.UtcNow, IsActive = true });

                //Code Formats
                eCollabroDbContext.Repository<CodeFormat>().Insert(new CodeFormat { EntityName = "Role", Suffix = "", Prefix = "RL", CurrentSeed = 1, Seprator = "", Codelength = 10 });
                eCollabroDbContext.Repository<CodeFormat>().Insert(new CodeFormat { EntityName = "Site", Suffix = "", Prefix = "ST", CurrentSeed = 1, Seprator = "", Codelength = 10 });
                eCollabroDbContext.Repository<CodeFormat>().Insert(new CodeFormat { EntityName = "Module", Suffix = "", Prefix = "MD", CurrentSeed = 2, Seprator = "", Codelength = 10 });
                eCollabroDbContext.Repository<CodeFormat>().Insert(new CodeFormat { EntityName = "Feature", Suffix = "", Prefix = "FT", CurrentSeed = 9, Seprator = "", Codelength = 10 });
                eCollabroDbContext.Repository<CodeFormat>().Insert(new CodeFormat { EntityName = "Navigation", Suffix = "", Prefix = "NAV", CurrentSeed = 13, Seprator = "", Codelength = 10 });

                //navigation type
                eCollabroDbContext.Repository<lkpNavigationType>().Insert(new lkpNavigationType { NavigationTypeCode = "NAVTYP0001", NavigationType = "None", CreatedById = 1, CreatedOn = DateTime.UtcNow, IsActive = true });
                eCollabroDbContext.Repository<lkpNavigationType>().Insert(new lkpNavigationType { NavigationTypeCode = "NAVTYP0002", NavigationType = "Content", CreatedById = 1, CreatedOn = DateTime.UtcNow, IsActive = true });
                eCollabroDbContext.Repository<lkpNavigationType>().Insert(new lkpNavigationType { NavigationTypeCode = "NAVTYP0003", NavigationType = "Feature", CreatedById = 1, CreatedOn = DateTime.UtcNow, IsActive = true });
                eCollabroDbContext.Repository<lkpNavigationType>().Insert(new lkpNavigationType { NavigationTypeCode = "NAVTYP0004", NavigationType = "Link", CreatedById = 1, CreatedOn = DateTime.UtcNow, IsActive = true });

                //Module - Setup
                Module moduleSetup = new Module { ModuleCode = "MD00000001", ModuleName = "Setup", ModuleDescription = "Site Settings/Setup Options", CreatedById = 1, CreatedOn = DateTime.UtcNow };
                moduleSetup.Features.Add(new Feature { FeatureCode = "FT00000001", FeatureName = "Manage Roles", IsNavigationLink = true, Link = "/security/roles", CreatedById = 1, CreatedOn = DateTime.UtcNow, IsActive = true });
                moduleSetup.Features.Add(new Feature { FeatureCode = "FT00000002", FeatureName = "Manage Users", IsNavigationLink = true, Link = "/security/users", CreatedById = 1, CreatedOn = DateTime.UtcNow, IsActive = true });
                moduleSetup.Features.Add(new Feature { FeatureCode = "FT00000003", FeatureName = "Manage Navigations", IsNavigationLink = true, Link = "/security/navigations", CreatedById = 1, CreatedOn = DateTime.UtcNow, IsActive = true });
                moduleSetup.Features.Add(new Feature { FeatureCode = "FT00000004", FeatureName = "Site Configuration", IsNavigationLink = true, Link = "/security/siteconfiguration", CreatedById = 1, CreatedOn = DateTime.UtcNow, IsActive = true });


                //Module Content
                Module moduleContent = new Module { ModuleCode = "MD00000002", ModuleName = "Content", ModuleDescription = "Content Module - Blog, Document Library, Image Gallery, Content Pages & Announcements", CreatedById = 1, CreatedOn = DateTime.UtcNow };
                moduleContent.Features.Add(new Feature { FeatureCode = "FT00000005", FeatureName = "Manage Content Pages", IsNavigationLink = true, Link = "/content/contentpage/categories", CreatedById = 1, CreatedOn = DateTime.UtcNow, IsActive = true });
                moduleContent.Features.Add(new Feature { FeatureCode = "FT00000006", FeatureName = "Manage Blogs", IsNavigationLink = true, Link = "/content/blog/blogcategories", CreatedById = 1, CreatedOn = DateTime.UtcNow, IsActive = true });
                moduleContent.Features.Add(new Feature { FeatureCode = "FT00000007", FeatureName = "Manage Document Libraries", IsNavigationLink = true, Link = "/content/documentlibrary/documentlibraries", CreatedById = 1, CreatedOn = DateTime.UtcNow, IsActive = true });
                moduleContent.Features.Add(new Feature { FeatureCode = "FT00000008", FeatureName = "Manage Image Galleries", IsNavigationLink = true, Link = "/content/imagegallery/imagegalleries", CreatedById = 1, CreatedOn = DateTime.UtcNow, IsActive = true });
                moduleContent.Features.Add(new Feature { FeatureCode = "FT00000009", FeatureName = "Manage Announcements", IsNavigationLink = true, Link = "/content/announcement/announcements", CreatedById = 1, CreatedOn = DateTime.UtcNow, IsActive = true });

                eCollabroDbContext.Repository<Module>().Insert(moduleSetup);
                eCollabroDbContext.Repository<Module>().Insert(moduleContent);

                //Context 

                eCollabroDbContext.Repository<lkpContext>().Insert(new lkpContext { Context = "User", IsActive = true, CreatedById = 1, CreatedOn = DateTime.UtcNow });
                eCollabroDbContext.Repository<lkpContext>().Insert(new lkpContext { Context = "Content Page", IsActive = true, CreatedById = 1, CreatedOn = DateTime.UtcNow });
                eCollabroDbContext.Repository<lkpContext>().Insert(new lkpContext { Context = "Blog", IsActive = true, CreatedById = 1, CreatedOn = DateTime.UtcNow });
                eCollabroDbContext.Repository<lkpContext>().Insert(new lkpContext { Context = "Document", IsActive = true, CreatedById = 1, CreatedOn = DateTime.UtcNow });
                eCollabroDbContext.Repository<lkpContext>().Insert(new lkpContext { Context = "Image", IsActive = true, CreatedById = 1, CreatedOn = DateTime.UtcNow });
                eCollabroDbContext.Repository<lkpContext>().Insert(new lkpContext { Context = "Announcement", IsActive = true, CreatedById = 1, CreatedOn = DateTime.UtcNow });

                //Content Permission

                eCollabroDbContext.Repository<ContentPermission>().Insert(new ContentPermission { ContentPermissionName = "View Content", ContentPermissionDescription = "View Content" });
                eCollabroDbContext.Repository<ContentPermission>().Insert(new ContentPermission { ContentPermissionName = "View Unapproved Content", ContentPermissionDescription = "View Unapproved Content" });
                eCollabroDbContext.Repository<ContentPermission>().Insert(new ContentPermission { ContentPermissionName = "View Inactive Content", ContentPermissionDescription = "View Inactive Content" });
                eCollabroDbContext.Repository<ContentPermission>().Insert(new ContentPermission { ContentPermissionName = "Add Content", ContentPermissionDescription = "Add Content" });
                eCollabroDbContext.Repository<ContentPermission>().Insert(new ContentPermission { ContentPermissionName = "Edit Content", ContentPermissionDescription = "Edit Content" });
                eCollabroDbContext.Repository<ContentPermission>().Insert(new ContentPermission { ContentPermissionName = "Delete Content", ContentPermissionDescription = "Delete Content" });
                eCollabroDbContext.Repository<ContentPermission>().Insert(new ContentPermission { ContentPermissionName = "Approve Content", ContentPermissionDescription = "Approve Content" });

                //Content Setting 
                eCollabroDbContext.Repository<ContentSetting>().Insert(new ContentSetting { ContentSettingName = "Approval Required", ContentSettingDescription = "Approval Required" });

                // Site
                Site site = new Site { SiteCode = "ST00000001", SiteName = "Primary", SiteDesc = "Primary Site", CreatedById = 1, CreatedOn = DateTime.UtcNow, IsActive = true };
                site.Roles.Add(new Role { RoleCode = "RL00000001", RoleName = "Site Admin", RoleDescription = "Site Admin role has access to all options available for Site.", CreatedById = 1, CreatedOn = DateTime.UtcNow, IsActive = true, IsSystem = true });

                eCollabroDbContext.Repository<Site>().Insert(site);

                // setup admin user
                adminUser.PasswordChangedDate = DateTime.UtcNow;
                adminUser.Password = pass.Key;
                adminUser.PasswordSalt = pass.Value;
                adminUser.CreatedById = 1;
                adminUser.CreatedOn = DateTime.UtcNow;
                adminUser.IsConfirmed = true;
                adminUser.IsActive = true;
                adminUser.IsApproved = true;
                adminUser.SiteCollectionAdmins.Add(new SiteCollectionAdmin { CreatedById = 1, CreatedOn = DateTime.UtcNow });
                eCollabroDbContext.Repository<UserMembership>().Insert(adminUser);

                eCollabroDbContext.Save();

                eCollabroDbContext.Repository<Navigation>().Insert(new Navigation { NavigationCode = "NAV0000001", NavigationText = "Home", AdditionalHtml = "<span class=\"glyphicon glyphicon-home\"></span>&nbsp;", SiteId = site.SiteId, NavigationTypeId = 4, Link = "/", CreatedById = 1, CreatedOn = DateTime.UtcNow, IsAnomynousAccess = true, DisplayOrder = 1, IsActive = true });
                eCollabroDbContext.Repository<Navigation>().Insert(new Navigation { NavigationCode = "NAV0000002", NavigationText = "Dashboard", AdditionalHtml = "<span class=\"glyphicon glyphicon-dashboard\"></span>&nbsp;", SiteId = site.SiteId, NavigationTypeId = 4, Link = "/home/dashboard", CreatedById = 1, CreatedOn = DateTime.UtcNow, IsAnomynousAccess = false, DisplayOrder = 2, IsActive = true });

                Navigation navigationSetup = new Navigation { NavigationCode = "NAV0000003", NavigationText = "Setup", SiteId = site.SiteId, NavigationTypeId = 1, CreatedById = 1, CreatedOn = DateTime.UtcNow, IsAnomynousAccess = false, DisplayOrder = 3, IsActive = true };
                Navigation navigationContent = new Navigation { NavigationCode = "NAV0000008", NavigationText = "Content", SiteId = site.SiteId, NavigationTypeId = 1, CreatedById = 1, CreatedOn = DateTime.UtcNow, IsAnomynousAccess = false, DisplayOrder = 8, IsActive = true };

                eCollabroDbContext.Repository<Navigation>().Insert(navigationSetup);
                eCollabroDbContext.Repository<Navigation>().Insert(navigationContent);
                eCollabroDbContext.Save();

                eCollabroDbContext.Repository<Navigation>().Insert(new Navigation { NavigationCode = "NAV0000004", NavigationText = "Roles", SiteId = site.SiteId, NavigationParentId = navigationSetup.NavigationId, NavigationTypeId = 3, FeatureId = 1, CreatedById = 1, CreatedOn = DateTime.UtcNow, IsAnomynousAccess = false, DisplayOrder = 4, IsActive = true });
                eCollabroDbContext.Repository<Navigation>().Insert(new Navigation { NavigationCode = "NAV0000005", NavigationText = "Users", SiteId = site.SiteId, NavigationParentId = navigationSetup.NavigationId, NavigationTypeId = 3, FeatureId = 2, CreatedById = 1, CreatedOn = DateTime.UtcNow, IsAnomynousAccess = false, DisplayOrder = 5, IsActive = true });
                eCollabroDbContext.Repository<Navigation>().Insert(new Navigation { NavigationCode = "NAV0000006", NavigationText = "Navigations", SiteId = site.SiteId, NavigationParentId = navigationSetup.NavigationId, NavigationTypeId = 3, FeatureId = 3, CreatedById = 1, CreatedOn = DateTime.UtcNow, IsAnomynousAccess = false, DisplayOrder = 6, IsActive = true });
                eCollabroDbContext.Repository<Navigation>().Insert(new Navigation { NavigationCode = "NAV0000007", NavigationText = "Site Settings", SiteId = site.SiteId, NavigationParentId = navigationSetup.NavigationId, NavigationTypeId = 3, FeatureId = 4, CreatedById = 1, CreatedOn = DateTime.UtcNow, IsAnomynousAccess = false, DisplayOrder = 7, IsActive = true });


                eCollabroDbContext.Repository<Navigation>().Insert(new Navigation { NavigationCode = "NAV0000009", NavigationText = "Pages", SiteId = site.SiteId, NavigationParentId = navigationContent.NavigationId, NavigationTypeId = 3, FeatureId = 5, CreatedById = 1, CreatedOn = DateTime.UtcNow, IsAnomynousAccess = false, DisplayOrder = 9, IsActive = true });
                eCollabroDbContext.Repository<Navigation>().Insert(new Navigation { NavigationCode = "NAV0000010", NavigationText = "Blogs", SiteId = site.SiteId, NavigationParentId = navigationContent.NavigationId, NavigationTypeId = 3, FeatureId = 6, CreatedById = 1, CreatedOn = DateTime.UtcNow, IsAnomynousAccess = false, DisplayOrder = 10, IsActive = true });
                eCollabroDbContext.Repository<Navigation>().Insert(new Navigation { NavigationCode = "NAV0000011", NavigationText = "Document Libraries", SiteId = site.SiteId, NavigationParentId = navigationContent.NavigationId, NavigationTypeId = 3, FeatureId = 7, CreatedById = 1, CreatedOn = DateTime.UtcNow, IsAnomynousAccess = false, DisplayOrder = 11, IsActive = true });
                eCollabroDbContext.Repository<Navigation>().Insert(new Navigation { NavigationCode = "NAV0000012", NavigationText = "Image Galleries", SiteId = site.SiteId, NavigationParentId = navigationContent.NavigationId, NavigationTypeId = 3, FeatureId = 8, CreatedById = 1, CreatedOn = DateTime.UtcNow, IsAnomynousAccess = false, DisplayOrder = 12, IsActive = true });
                eCollabroDbContext.Repository<Navigation>().Insert(new Navigation { NavigationCode = "NAV0000013", NavigationText = "Announcements", SiteId = site.SiteId, NavigationParentId = navigationContent.NavigationId, NavigationTypeId = 3, FeatureId = 9, CreatedById = 1, CreatedOn = DateTime.UtcNow, IsAnomynousAccess = false, DisplayOrder = 13, IsActive = true });


                eCollabroDbContext.Repository<UserRole>().Insert(new UserRole { RoleId = site.Roles.FirstOrDefault().RoleId, SiteId = site.SiteId, UserId = adminUser.UserId, CreatedById = 1, CreatedOn = DateTime.UtcNow, IsActive = true });

                //Feature Permission
                //Role 
                for (int ctr = 1; ctr <= 6; ctr++)
                {
                    if (ctr != 2)
                        eCollabroDbContext.Repository<FeaturePermission>().Insert(new FeaturePermission { FeatureId = 1, PermissionId = ctr });
                }
                //User
                for (int ctr = 1; ctr <= 7; ctr++)
                    eCollabroDbContext.Repository<FeaturePermission>().Insert(new FeaturePermission { FeatureId = 2, PermissionId = ctr });
                //Navigation
                for (int ctr = 1; ctr <= 6; ctr++)
                {
                    if (ctr != 2)
                        eCollabroDbContext.Repository<FeaturePermission>().Insert(new FeaturePermission { FeatureId = 3, PermissionId = ctr });
                }
                //Site Settings
                eCollabroDbContext.Repository<FeaturePermission>().Insert(new FeaturePermission { FeatureId = 4, PermissionId = 1 });
                eCollabroDbContext.Repository<FeaturePermission>().Insert(new FeaturePermission { FeatureId = 4, PermissionId = 5 });
                //Content Page
                for (int ctr = 1; ctr <= 7; ctr++)
                    eCollabroDbContext.Repository<FeaturePermission>().Insert(new FeaturePermission { FeatureId = 5, PermissionId = ctr });
                //Blogs
                for (int ctr = 1; ctr <= 7; ctr++)
                    eCollabroDbContext.Repository<FeaturePermission>().Insert(new FeaturePermission { FeatureId = 6, PermissionId = ctr });
                //Document Library
                for (int ctr = 1; ctr <= 7; ctr++)
                    eCollabroDbContext.Repository<FeaturePermission>().Insert(new FeaturePermission { FeatureId = 7, PermissionId = ctr });
                //Image Gallery
                for (int ctr = 1; ctr <= 7; ctr++)
                    eCollabroDbContext.Repository<FeaturePermission>().Insert(new FeaturePermission { FeatureId = 8, PermissionId = ctr });
                //Announcement
                for (int ctr = 1; ctr <= 7; ctr++)
                    eCollabroDbContext.Repository<FeaturePermission>().Insert(new FeaturePermission { FeatureId = 9, PermissionId = ctr });

                //Content Setting
                eCollabroDbContext.Repository<FeatureContentSetting>().Insert(new FeatureContentSetting { FeatureId = 2, ContentSettingId = 1 }); //User,ApprovalRequired

                eCollabroDbContext.Repository<FeatureContentSetting>().Insert(new FeatureContentSetting { FeatureId = 5, ContentSettingId = 1 }); //Content Page,ApprovalRequired
                eCollabroDbContext.Repository<FeatureContentSetting>().Insert(new FeatureContentSetting { FeatureId = 6, ContentSettingId = 1 }); //Blog,ApprovalRequired
                eCollabroDbContext.Repository<FeatureContentSetting>().Insert(new FeatureContentSetting { FeatureId = 7, ContentSettingId = 1 }); //Document Library,ApprovalRequired
                eCollabroDbContext.Repository<FeatureContentSetting>().Insert(new FeatureContentSetting { FeatureId = 8, ContentSettingId = 1 }); //Image Gallery,ApprovalRequired
                eCollabroDbContext.Repository<FeatureContentSetting>().Insert(new FeatureContentSetting { FeatureId = 9, ContentSettingId = 1 }); //Announcement,ApprovalRequired

                // add all features to primary site
                for (int ctr = 1; ctr <= 9; ctr++)
                    eCollabroDbContext.Repository<SiteFeature>().Insert(new SiteFeature { SiteId = site.SiteId, FeatureId = ctr, CreatedById = 1, CreatedOn = DateTime.UtcNow });

                eCollabroDbContext.Save();
                ExecuteScripts();
            }

        }

        /// <summary>
        /// ExecuteScripts
        /// </summary>
        public void ExecuteScripts()
        {
            string scriptDBSqlPath = AppDomain.CurrentDomain.BaseDirectory + "App_Data\\SetupScripts";
            DirectoryInfo di = new DirectoryInfo(scriptDBSqlPath);
            FileInfo[] rgFiles = di.GetFiles("*.sql");
            List<string> scripts = new List<string>();
            foreach (FileInfo fi in rgFiles)
            {

                FileInfo fileInfo = new FileInfo(fi.FullName);
                using (StreamReader reader = fileInfo.OpenText())
                {
                    string script = reader.ReadToEnd();
                    scripts.Add(script);
                    reader.Close();
                }
            }

            // Execute Stored Procs
            eCollabroDbContext.ExecuteScripts(scripts);
        }

        #endregion

        #region Email Configuration

        /// <summary>
        /// GetEmailConfiguration
        /// </summary>
        /// <returns></returns>
        public EmailConfiguration GetEmailConfiguration()
        {
            EmailConfiguration emailConfiguration = eCollabroDbContext.Repository<EmailConfiguration>().Query().Get().FirstOrDefault();
            return emailConfiguration;
        }


        /// <summary>
        /// SaveEmailConfiguration
        /// </summary>
        public void SaveEmailConfiguration(EmailConfiguration emailConfiguration)
        {
            CheckSiteCollectionAdminPermission(UserContextDetails.UserId);
            EmailConfiguration existingEmailConfiguration = GetEmailConfiguration();
            if (existingEmailConfiguration == null)
            {
                eCollabroDbContext.Repository<EmailConfiguration>().Insert(emailConfiguration);
            }
            else
            {
                var map = Mapper.Resolve<EmailConfiguration, EmailConfiguration>();
                map.Ignore("EmailConfigurationId");
                Mapper.Map<EmailConfiguration, EmailConfiguration>(emailConfiguration, existingEmailConfiguration);
            }
            eCollabroDbContext.Save();
        }

        #endregion

        #region Site Collection Admins

        ///<Summary>
        ///Get All USers
        ///</Summary>
        ///<returns></returns>
        public List<SiteCollectionAdmin> GetSiteCollectionAdmins()
        {
            List<SiteCollectionAdmin> users = eCollabroDbContext.Repository<SiteCollectionAdmin>().Query().Include(inc => inc.UserMembership).Get().ToList();
            return users;
        }

        /// <summary>
        /// SaveSiteCollectionAdmin
        /// </summary>
        /// <param name="username"></param>
        public void SaveSiteCollectionAdmin(string username)
        {
            CheckSiteCollectionAdminPermission(UserContextDetails.UserId);
            UserMembership user = _securityManager.FindUser(username);
            SiteCollectionAdmin siteCollectionAdmin = eCollabroDbContext.Repository<SiteCollectionAdmin>().Query().Filter(qry => qry.UserId.Equals(user.UserId)).Get().FirstOrDefault();
            if (siteCollectionAdmin != null)
            {
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UserAlreadyExist), CoreValidationMessagesConstants.UserAlreadyExist);
            }
            else
            {
                siteCollectionAdmin = new SiteCollectionAdmin();
                siteCollectionAdmin.CreatedById = UserContextDetails.UserId;
                siteCollectionAdmin.CreatedOn = DateTime.UtcNow;
                siteCollectionAdmin.UserId = user.UserId;
                eCollabroDbContext.Repository<SiteCollectionAdmin>().Insert(siteCollectionAdmin);
                eCollabroDbContext.Save();
            }
        }

        /// <summary>
        /// DeleteSiteCollectionAdmin
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public void DeleteSiteCollectionAdmin(int userId)
        {
            CheckSiteCollectionAdminPermission(UserContextDetails.UserId);
            List<SiteCollectionAdmin> siteCollectionAdmins = eCollabroDbContext.Repository<SiteCollectionAdmin>().Query().Get().ToList();
            if (siteCollectionAdmins.Count() == 1)
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.AtleastOneSiteCollectionAdminRequired), CoreValidationMessagesConstants.AtleastOneSiteCollectionAdminRequired);
            SiteCollectionAdmin siteCollectionAdmin = siteCollectionAdmins.Where(qry => qry.UserId.Equals(userId)).FirstOrDefault();
            if (siteCollectionAdmin == null)
            {
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.RecordNotFound), CoreValidationMessagesConstants.RecordNotFound);
            }
            else
            {
                siteCollectionAdmin.ModifiedById = UserContextDetails.UserId;
                siteCollectionAdmin.ModifiedOn = DateTime.UtcNow;
                siteCollectionAdmin.IsDeleted = true;
                eCollabroDbContext.Save();
            }
        }

        #endregion


        #endregion
    }
}
