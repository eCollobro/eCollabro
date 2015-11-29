namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SiteFeature")]
    public partial class SiteFeature
    {
        public int SiteFeatureId { get; set; }

        public int SiteId { get; set; }

        public int FeatureId { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual Feature Feature { get; set; }

        public virtual Site Site { get; set; }
    }
}
