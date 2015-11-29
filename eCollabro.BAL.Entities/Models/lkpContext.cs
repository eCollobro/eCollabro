namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("lkpContext")]
    public partial class lkpContext
    {
        public lkpContext()
        {
            ApprovalQueues = new HashSet<ApprovalQueue>();
            ContentLikeDislikes = new HashSet<ContentLikeDislike>();
            ContentRatings = new HashSet<ContentRating>();
            ContentVotes = new HashSet<ContentVote>();
            FileObjects = new HashSet<FileObject>();
            ImageObjects = new HashSet<ImageObject>();
            UserTasks = new HashSet<UserTask>();
            WorkflowComments = new HashSet<WorkflowComment>();
        }

        [Key]
        public int ContextId { get; set; }

        [Required]
        [StringLength(50)]
        public string Context { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifiedById { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<ApprovalQueue> ApprovalQueues { get; set; }

        public virtual ICollection<ContentLikeDislike> ContentLikeDislikes { get; set; }

        public virtual ICollection<ContentRating> ContentRatings { get; set; }

        public virtual ICollection<ContentVote> ContentVotes { get; set; }

        public virtual ICollection<FileObject> FileObjects { get; set; }

        public virtual ICollection<ImageObject> ImageObjects { get; set; }

        public virtual ICollection<UserTask> UserTasks { get; set; }

        public virtual ICollection<WorkflowComment> WorkflowComments { get; set; }
    }
}
