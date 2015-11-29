namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImageObject")]
    public partial class ImageObject
    {
        public ImageObject()
        {
            SiteImages = new HashSet<SiteImage>();
        }

        public int ImageObjectId { get; set; }

        [Required]
        public byte[] ImageObjectData { get; set; }

        [Required]
        public byte[] ImageThumbnailData { get; set; }

        public int ContextId { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifiedById { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual lkpContext lkpContext { get; set; }

        public virtual ICollection<SiteImage> SiteImages { get; set; }
    }
}
