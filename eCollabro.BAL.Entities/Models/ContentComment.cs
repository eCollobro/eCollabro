namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContentComment")]
    public partial class ContentComment
    {
        public int ContentCommentId { get; set; }

        public int ContextId { get; set; }

        public int ContextContentId { get; set; }

        [Required]
        public string Comment { get; set; }

        public int? ParentContentCommentId { get; set; }

        [Required]
        [StringLength(20)]
        public string ApprovalStatus { get; set; }

        public int? ApproveRejectById { get; set; }

        public DateTime? ApproveRejectDate { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifiedById { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
