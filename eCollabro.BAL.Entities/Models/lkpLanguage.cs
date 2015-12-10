namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("lkpLanguage")]
    public partial class lkpLanguage
    {
        [Key]
        public int LanguageId { get; set; }

        [Required]
        [StringLength(6)]
        public string LanguageCode { get; set; }

        [Required]
        [StringLength(50)]
        public string Language { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifiedById { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
