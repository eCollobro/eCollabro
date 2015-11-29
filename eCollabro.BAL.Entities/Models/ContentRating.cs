namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContentRating")]
    public partial class ContentRating
    {
        public int ContentRatingId { get; set; }

        public int ContextId { get; set; }

        public int ContextContentId { get; set; }

        public double Rating { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifiedById { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual lkpContext lkpContext { get; set; }
    }
}
