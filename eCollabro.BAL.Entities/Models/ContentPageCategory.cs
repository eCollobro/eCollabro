namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContentPageCategory")]
    public partial class ContentPageCategory
    {
        public ContentPageCategory()
        {
            ContentPages = new HashSet<ContentPage>();
        }

        public int ContentPageCategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string ContentPageCategoryName { get; set; }

        [StringLength(255)]
        public string ContentPageCategoryDescription { get; set; }

        public int SiteId { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public bool IsAnomynousAccess { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifiedById { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<ContentPage> ContentPages { get; set; }

        public virtual Site Site { get; set; }
    }
}
