namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Announcement")]
    public partial class Announcement
    {
        public int AnnouncementId { get; set; }

        [Required]
        [StringLength(100)]
        public string AnnouncementTitle { get; set; }

        [Required]
        public string AnnouncementDescription { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public int SiteId { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public bool IsAnomynousAccess { get; set; }

        public bool IsCommentsAllowed { get; set; }

        public bool IsRatingAllowed { get; set; }

        public bool IsVotingAllowed { get; set; }

        public bool IsLikeAllowed { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifiedById { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual Site Site { get; set; }
    }
}
