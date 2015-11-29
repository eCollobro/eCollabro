// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;
using System.Collections.Generic;
using System.Linq;
using eCollabro.BAL.Entities.Models;

#endregion 

namespace eCollabro.DAL.Interface
{
    /// <summary>
    /// ISecurityRepository
    /// </summary>
    public interface ISecurityRepository
    {
        /// <summary>
        /// GetSiteFeatures
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        List<FeatureResult> GetSiteFeatures(int siteId);
 

        /// <summary>
        /// GetSiteFeatures
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        List<FeatureResult> GetSiteAssignedFeatures(int siteId);


        /// <summary>
        /// GetNavigations
        /// </summary>
        /// <returns>List<NavigationResult></returns>
        List<NavigationResult> GetNavigations(int siteId);
 

        /// <summary>
        /// GetNavigationDetails
        /// </summary>
        /// <returns>NavigationResult</returns>
        NavigationResult GetNavigationDetails(int navigationId);
 

        /// <summary>
        /// GetUserNavigations
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="SiteId"></param>
        /// <returns></returns>
        List<NavigationResult> GetUserNavigations(int userId, int SiteId);
  
        /// <summary>
        /// GetUserSites
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="languageId"></param>
        /// <returns></returns>
        List<SiteResult> GetUserSites(int userId, int languageId);

        /// <summary>
        /// GetRoleFeatures
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="featureCode"></param>
        /// <returns></returns>
        IQueryable<RoleFeature> GetRoleFeatures(int siteId, int featureId);

        /// <summary>
        /// GetFeaturesContentSettings
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        List<SiteContentSettingResult> GetFeaturesContentSettings(int siteId);

        /// <summary>
        /// GetUserPermissions
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<FeaturePermissionResult> GetUserPermissions(int siteId, int userId);
    }

}
