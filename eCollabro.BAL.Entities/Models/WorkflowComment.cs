namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WorkflowComment")]
    public partial class WorkflowComment
    {
        public int WorkflowCommentId { get; set; }

        public int ContextId { get; set; }

        public int ContexContentId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Comment { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual lkpContext lkpContext { get; set; }
    }
}
