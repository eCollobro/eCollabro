namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContentVote")]
    public partial class ContentVote
    {
        public int ContentVoteId { get; set; }

        public int ContextId { get; set; }

        public int ContextContentId { get; set; }

        public bool IsVoted { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifiedById { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual lkpContext lkpContext { get; set; }
    }
}
