using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCollabro.BAL.Entities.Models
{
    public partial class NavigationResult
    {
        public int NavigationId { get; set; }
        public string NavigationCode { get; set; }
        public string NavigationText { get; set; }
        public string AdditionalHtml { get; set; }
        public int SiteId { get; set; }
        public Nullable<int> NavigationParentId { get; set; }
        public string Link { get; set; }
        public int NavigationTypeId { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public bool IsAnomynousAccess { get; set; }
        public bool IsActive { get; set; }
        public int CreatedById { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedById { get; set; }
        public Nullable<int> ContentPageId { get; set; }
        public Nullable<int> FeatureId { get; set; }
        public string NavigationTypeCode { get; set; }
        public string NavigationType { get; set; }
        public string SiteName { get; set; }
        public string FeatureCode { get; set; }
        public string FeatureName { get; set; }
        public string NavigationParentCode { get; set; }
        public string NavigationParentText { get; set; }
        public string ModifiedByName { get; set; }
        public string CreatedByName { get; set; }
    }
}
