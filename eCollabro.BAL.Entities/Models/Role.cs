namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Role")]
    public partial class Role
    {
        public Role()
        {
            RoleFeatures = new HashSet<RoleFeature>();
            SiteConfigurations = new HashSet<SiteConfiguration>();
            UserRoles = new HashSet<UserRole>();
        }

        public int RoleId { get; set; }

        [Required]
        [StringLength(10)]
        public string RoleCode { get; set; }

        public int SiteId { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        [StringLength(255)]
        public string RoleDescription { get; set; }

        public bool IsSystem { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifiedById { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual Site Site { get; set; }

        public virtual ICollection<RoleFeature> RoleFeatures { get; set; }

        public virtual ICollection<SiteConfiguration> SiteConfigurations { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
