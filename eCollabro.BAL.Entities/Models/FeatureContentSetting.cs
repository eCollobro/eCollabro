namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FeatureContentSetting")]
    public partial class FeatureContentSetting
    {
        public int FeatureContentSettingId { get; set; }

        public int FeatureId { get; set; }

        public int ContentSettingId { get; set; }

        public virtual ContentSetting ContentSetting { get; set; }

        public virtual Feature Feature { get; set; }
    }
}
