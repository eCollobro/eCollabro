namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserTask")]
    public partial class UserTask
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        [StringLength(100)]
        public string TaskTitle { get; set; }

        [StringLength(255)]
        public string TaskDescription { get; set; }

        public int SiteId { get; set; }

        public int? AssignedUserId { get; set; }

        public int? AssignedByUserId { get; set; }

        public DateTime? AssignedDate { get; set; }

        public DateTime? DueDate { get; set; }

        public double? CompletionPercentage { get; set; }

        [Required]
        [StringLength(50)]
        public string TaskStatus { get; set; }

        public DateTime? CompletionDate { get; set; }

        [StringLength(50)]
        public string TaskType { get; set; }

        public int ContextId { get; set; }

        public int ContexContentId { get; set; }

        public bool IsDeleted { get; set; }

        [StringLength(10)]
        public string ApprovalStatus { get; set; }

        public bool IsActive { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifiedById { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual lkpContext lkpContext { get; set; }

        public virtual Site Site { get; set; }

        public virtual UserMembership UserMembership { get; set; }

        public virtual UserMembership UserMembership1 { get; set; }
    }
}
