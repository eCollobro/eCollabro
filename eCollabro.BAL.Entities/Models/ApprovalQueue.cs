namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ApprovalQueue")]
    public partial class ApprovalQueue
    {
        public int ApprovalQueueId { get; set; }

        public int ContextId { get; set; }

        public int ContextContentId { get; set; }

        public int ModifiedById { get; set; }

        public DateTime ModifiedOn { get; set; }

        [Column(TypeName = "xml")]
        [Required]
        public string ObjectData { get; set; }

        public bool IsDraft { get; set; }

        public virtual lkpContext lkpContext { get; set; }
    }
}
