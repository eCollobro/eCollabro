namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContentPermission")]
    public partial class ContentPermission
    {
        public ContentPermission()
        {
            FeaturePermissions = new HashSet<FeaturePermission>();
        }

        public int ContentPermissionId { get; set; }

        [Required]
        [StringLength(50)]
        public string ContentPermissionName { get; set; }

        [StringLength(255)]
        public string ContentPermissionDescription { get; set; }

        public virtual ICollection<FeaturePermission> FeaturePermissions { get; set; }
    }
}
