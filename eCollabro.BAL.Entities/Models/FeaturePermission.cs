namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FeaturePermission")]
    public partial class FeaturePermission
    {
        public int FeaturePermissionId { get; set; }

        public int FeatureId { get; set; }

        public int PermissionId { get; set; }

        public virtual ContentPermission ContentPermission { get; set; }

        public virtual Feature Feature { get; set; }
    }
}
