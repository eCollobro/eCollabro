namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContentPage")]
    public partial class ContentPage
    {
        public int ContentPageId { get; set; }

        [Required]
        [StringLength(100)]
        public string ContentPageTitle { get; set; }

        [StringLength(255)]
        public string ContentPageDescription { get; set; }

        public string ContentPageContent { get; set; }

        public int ContentPageCategoryId { get; set; }

        [Required]
        [StringLength(20)]
        public string ApprovalStatus { get; set; }

        public int? ApproveRejectById { get; set; }

        public DateTime? ApproveRejectDate { get; set; }

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

        public virtual ContentPageCategory ContentPageCategory { get; set; }
    }
}
