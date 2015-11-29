namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Feature")]
    public partial class Feature
    {
        public Feature()
        {
            FeatureContentSettings = new HashSet<FeatureContentSetting>();
            FeaturePermissions = new HashSet<FeaturePermission>();
            Navigations = new HashSet<Navigation>();
            RoleFeatures = new HashSet<RoleFeature>();
            SiteContentSettings = new HashSet<SiteContentSetting>();
            SiteFeatures = new HashSet<SiteFeature>();
        }

        public int FeatureId { get; set; }

        public int ModuleId { get; set; }

        [Required]
        [StringLength(10)]
        public string FeatureCode { get; set; }

        [Required]
        [StringLength(50)]
        public string FeatureName { get; set; }

        public bool IsNavigationLink { get; set; }

        [StringLength(50)]
        public string Link { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedById { get; set; }

        public int? ModifiedById { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<FeatureContentSetting> FeatureContentSettings { get; set; }

        public virtual ICollection<FeaturePermission> FeaturePermissions { get; set; }

        public virtual Module Module { get; set; }

        public virtual ICollection<Navigation> Navigations { get; set; }

        public virtual ICollection<RoleFeature> RoleFeatures { get; set; }

        public virtual ICollection<SiteContentSetting> SiteContentSettings { get; set; }

        public virtual ICollection<SiteFeature> SiteFeatures { get; set; }
    }
}
