namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RoleFeature")]
    public partial class RoleFeature
    {
        public RoleFeature()
        {
            RoleFeaturePermissions = new HashSet<RoleFeaturePermission>();
        }

        public int RoleFeatureId { get; set; }

        public int RoleId { get; set; }

        public int FeatureId { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual Feature Feature { get; set; }

        public virtual Role Role { get; set; }

        public virtual ICollection<RoleFeaturePermission> RoleFeaturePermissions { get; set; }
    }
}
