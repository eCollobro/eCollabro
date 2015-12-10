namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RoleFeaturePermission")]
    public partial class RoleFeaturePermission
    {
        public int RoleFeaturePermissionId { get; set; }

        public int RoleFeatureId { get; set; }

        public int PermissionId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifiedById { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual RoleFeature RoleFeature { get; set; }
    }
}
