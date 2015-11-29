namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImageGallery")]
    public partial class ImageGallery
    {
        public ImageGallery()
        {
            SiteImages = new HashSet<SiteImage>();
        }

        public int ImageGalleryId { get; set; }

        [Required]
        [StringLength(100)]
        public string ImageGalleryName { get; set; }

        [StringLength(255)]
        public string ImageGalleryDescription { get; set; }

        public int SiteId { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public bool IsAnomynousAccess { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifiedById { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual Site Site { get; set; }

        public virtual ICollection<SiteImage> SiteImages { get; set; }
    }
}
