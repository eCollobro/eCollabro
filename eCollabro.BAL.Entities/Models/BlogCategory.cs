namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BlogCategory")]
    public partial class BlogCategory
    {
        public BlogCategory()
        {
            Blogs = new HashSet<Blog>();
        }

        public int BlogCategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string BlogCategoryName { get; set; }

        [StringLength(255)]
        public string BlogCategoryDescription { get; set; }

        public int SiteId { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public bool IsAnomynousAccess { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifiedById { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }

        public virtual Site Site { get; set; }
    }
}
