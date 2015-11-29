namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SiteImage")]
    public partial class SiteImage
    {
        [Key]
        public int ImageId { get; set; }

        [Required]
        [StringLength(100)]
        public string ImageTitle { get; set; }

        [StringLength(255)]
        public string ImageDescription { get; set; }

        [Required]
        [StringLength(100)]
        public string ImageFileName { get; set; }

        public int? ImageObjectId { get; set; }

        public int ImageGalleryId { get; set; }

        [Required]
        [StringLength(20)]
        public string ApprovalStatus { get; set; }

        public int? ApproveRejectById { get; set; }

        public DateTime? ApproveRejectDate { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsAnomynousAccess { get; set; }

        public bool IsCommentsAllowed { get; set; }

        public bool IsRatingAllowed { get; set; }

        public bool IsVotingAllowed { get; set; }

        public bool IsLikeAllowed { get; set; }

        public bool IsActive { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifiedById { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ImageGallery ImageGallery { get; set; }

        public virtual ImageObject ImageObject { get; set; }
    }
}
