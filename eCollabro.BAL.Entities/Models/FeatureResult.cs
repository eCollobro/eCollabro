using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCollabro.BAL.Entities.Models
{
    public partial class FeatureResult
    {
        public int ModuleId { get; set; }
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public string ModuleDescription { get; set; }
        public int FeatureId { get; set; }
        public string FeatureCode { get; set; }
        public string FeatureName { get; set; }
        public bool IsNavigationLink { get; set; }
        public string Link { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> CreatedById { get; set; }
        public Nullable<bool> IsAssigned { get; set; }
        public List<FeaturePermissionResult> RoleFeaturePermissions { get; set; }
        public List<SiteContentSettingResult> SiteContentSettings { get; set; }
    }
}
