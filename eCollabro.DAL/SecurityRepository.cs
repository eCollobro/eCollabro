// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System.Collections.Generic;
using System.Linq;
using eCollabro.BAL.Entities.Models;
using System.Data.SqlClient;
using System.Data.Entity;
using eCollabro.DAL.Interface;

#endregion

namespace eCollabro.DAL
{
    #region Extended Repository Extension for Security Repository 

    /// <summary>
    /// ExtendedRepository
    /// </summary>
    public partial class ExtendedRepository : IExtendedRepository
    {
        private ISecurityRepository _securityRepository;
        public ISecurityRepository SecurityRepository
        {
            get
            {
                if (_securityRepository == null)
                {
                    _securityRepository = new SecurityRepository(this._dbContext);
                }
                return _securityRepository;
            }
        }
    }

    #endregion 

    /// <summary>
    /// SecurityRepository
    /// </summary>
    public class SecurityRepository : ISecurityRepository
    {
        #region Data Members 

        private eCollabroDbModel _dbContext = null;

        #endregion 

        #region Constructor 

        /// <summary>
        /// SecurityRepository
        /// </summary>
        /// <param name="dbContext"></param>
        public SecurityRepository(DbContext dbContext)
        {
            _dbContext = dbContext as eCollabroDbModel;
        }

        #endregion 

        #region Methods

        #region Site 

        /// <summary>
        /// GetSiteFeatures
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public List<FeatureResult> GetSiteFeatures(int siteId)
        {
            SqlParameter param = new SqlParameter("SiteId", siteId);
            var featureResult = _dbContext.Database.SqlQuery<FeatureResult>("uspGetSiteFeatures @SiteId", param).ToList();
            return featureResult;
        }

        /// <summary>
        /// GetSiteFeatures
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public List<FeatureResult> GetSiteAssignedFeatures(int siteId)
        {
            SqlParameter param = new SqlParameter("SiteId", siteId);
            var featureResult = _dbContext.Database.SqlQuery<FeatureResult>("uspGetSiteAssignedFeatures @SiteId", param).ToList();
            return featureResult;
        }

        /// <summary>
        /// GetUserSites
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="languageId"></param>
        /// <returns></returns>
        public List<SiteResult> GetUserSites(int userId, int languageId)
        {
            SqlParameter[] param = new SqlParameter[]{ new SqlParameter("LanguageId", languageId),
             new SqlParameter("UserId", userId)};

            var sites = _dbContext.Database.SqlQuery<SiteResult>("uspGetUserSites @LanguageId, @UserId", param).ToList();
            return sites;
        }

        #endregion 

        #region Navigation

        /// <summary>
        /// GetNavigations
        /// </summary>
        /// <returns>List<NavigationResult></returns>
        public List<NavigationResult> GetNavigations(int siteId)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("SiteId", siteId) };
            var navigations = _dbContext.Database.SqlQuery<NavigationResult>("uspGetNavigations @SiteId ", param).ToList();
            return navigations;
        }

        /// <summary>
        /// GetNavigationDetails
        /// </summary>
        /// <returns>NavigationResult</returns>
        public NavigationResult GetNavigationDetails(int navigationId)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("SiteId", 1) };
            var navigations = _dbContext.Database.SqlQuery<NavigationResult>("uspGetNavigations @SiteId ", param).ToList();
            return navigations.Where(nv => nv.NavigationId.Equals(navigationId)).FirstOrDefault();
        }


        /// <summary>
        /// GetUserNavigations
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="SiteId"></param>
        /// <returns></returns>
        public List<NavigationResult> GetUserNavigations(int userId, int SiteId)
        {
            SqlParameter[] param = new SqlParameter[]{ new SqlParameter("UserId", userId),
            new SqlParameter("SiteId", SiteId)};
            var userNavigationResult = _dbContext.Database.SqlQuery<NavigationResult>("uspGetUserNavigations @UserId, @SiteId", param).ToList();
            return userNavigationResult;
        }

        #endregion

        #region Role 

        /// <summary>
        /// GetRoleFeatures
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="featureCode"></param>
        /// <returns></returns>
        public IQueryable<RoleFeature> GetRoleFeatures(int siteId, int featureId)
        {
            IQueryable<RoleFeature> roleFeatures = (from roleFeature in _dbContext.RoleFeatures
                                                    join
                                                        siteFeature in _dbContext.SiteFeatures on roleFeature.FeatureId equals siteFeature.FeatureId
                                                    where siteFeature.FeatureId.Equals(featureId) && siteFeature.SiteId.Equals(siteId)
                                                    select roleFeature).Include(qry => qry.RoleFeaturePermissions);
            return roleFeatures;

        }

        /// <summary>
        /// GetFeaturesContentSettings
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public List<SiteContentSettingResult> GetFeaturesContentSettings(int siteId)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("SiteId", siteId) };
            var siteContentSettingResults = _dbContext.Database.SqlQuery<SiteContentSettingResult>("uspGetSiteFeaturesSettings @SiteId", param).ToList();
            return siteContentSettingResults;
        }

        /// <summary>
        /// GetUserPermissions
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<FeaturePermissionResult> GetUserPermissions(int siteId, int userId)
        {
            var query=(from userRole in _dbContext.UserRoles join 
                      role in _dbContext.Roles on userRole.RoleId equals role.RoleId join
                      roleFeature in _dbContext.RoleFeatures on role.RoleId equals roleFeature.RoleId join
                      roleFeaturePermission in _dbContext.RoleFeaturePermissions on roleFeature.RoleFeatureId equals roleFeaturePermission.RoleFeatureId
                      where userRole.SiteId.Equals(siteId) && userRole.UserId.Equals(userId)
                      select new
                      {
                          roleFeature,
                          roleFeaturePermission
                      }); 
            List<FeaturePermissionResult> featurePermissions=new List<FeaturePermissionResult>();
            foreach(var item in query)
            {
                if(!featurePermissions.Any(qry=>qry.FeatureId.Equals(item.roleFeature.FeatureId) && qry.ContentPermissionId.Equals(item.roleFeaturePermission.PermissionId)))
                {
                    featurePermissions.Add(new FeaturePermissionResult { FeatureId = item.roleFeature.FeatureId, ContentPermissionId = item.roleFeaturePermission.PermissionId });
                }
            }
            return featurePermissions;
        }

        #endregion 

        #endregion
    }
}

