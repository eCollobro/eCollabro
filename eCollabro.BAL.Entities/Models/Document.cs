namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Document")]
    public partial class Document
    {
        public int DocumentId { get; set; }

        [Required]
        [StringLength(100)]
        public string DocumentTitle { get; set; }

        [StringLength(255)]
        public string DocumentDescription { get; set; }

        [Required]
        [StringLength(100)]
        public string DocumentFileName { get; set; }

        public int? FileId { get; set; }

        public int DocumentLibraryId { get; set; }

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

        public virtual DocumentLibrary DocumentLibrary { get; set; }

        public virtual FileObject FileObject { get; set; }
    }
}
