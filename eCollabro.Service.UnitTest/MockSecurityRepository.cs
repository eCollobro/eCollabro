using eCollabro.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCollabro.BAL.Entities.Models;
using eCollabro.DAL.Interface;

namespace eCollabro.Service.UnitTest
{
    public class MockSecurityRepository:ISecurityRepository
    {

        public List<FeatureResult> GetSiteFeatures(int siteId)
        {
            throw new NotImplementedException();
        }

        public List<FeatureResult> GetSiteAssignedFeatures(int siteId)
        {
            throw new NotImplementedException();
        }

        public List<NavigationResult> GetNavigations(int siteId)
        {
            throw new NotImplementedException();
        }

        public NavigationResult GetNavigationDetails(int navigationId)
        {
            throw new NotImplementedException();
        }

        public List<NavigationResult> GetUserNavigations(int userId, int SiteId)
        {
            throw new NotImplementedException();
        }

        public List<SiteResult> GetUserSites(int userId, int languageId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<RoleFeature> GetRoleFeatures(int siteId, int featureId)
        {
            throw new NotImplementedException();
        }

        public List<SiteContentSettingResult> GetFeaturesContentSettings(int siteId)
        {
            throw new NotImplementedException();
        }


        public List<FeaturePermissionResult> GetUserPermissions(int siteId, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
