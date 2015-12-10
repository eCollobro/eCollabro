namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Navigation")]
    public partial class Navigation
    {
        public Navigation()
        {
            Navigation1 = new HashSet<Navigation>();
        }

        public int NavigationId { get; set; }

        [Required]
        [StringLength(10)]
        public string NavigationCode { get; set; }

        [Required]
        [StringLength(50)]
        public string NavigationText { get; set; }

        [StringLength(255)]
        public string AdditionalHtml { get; set; }

        public int SiteId { get; set; }

        public int? NavigationParentId { get; set; }

        public int NavigationTypeId { get; set; }

        public int? FeatureId { get; set; }

        public int? ContentPageId { get; set; }

        [StringLength(255)]
        public string Link { get; set; }

        public int? DisplayOrder { get; set; }

        public bool IsAnomynousAccess { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifiedById { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual Feature Feature { get; set; }

        public virtual lkpNavigationType lkpNavigationType { get; set; }

        public virtual ICollection<Navigation> Navigation1 { get; set; }

        public virtual Navigation Navigation2 { get; set; }

        public virtual Site Site { get; set; }
    }
}
