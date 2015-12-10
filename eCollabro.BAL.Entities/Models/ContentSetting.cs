namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContentSetting")]
    public partial class ContentSetting
    {
        public ContentSetting()
        {
            FeatureContentSettings = new HashSet<FeatureContentSetting>();
            SiteContentSettings = new HashSet<SiteContentSetting>();
        }

        public int ContentSettingId { get; set; }

        [Required]
        [StringLength(50)]
        public string ContentSettingName { get; set; }

        [StringLength(255)]
        public string ContentSettingDescription { get; set; }

        public virtual ICollection<FeatureContentSetting> FeatureContentSettings { get; set; }

        public virtual ICollection<SiteContentSetting> SiteContentSettings { get; set; }
    }
}
