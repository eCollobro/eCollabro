namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CodeFormat")]
    public partial class CodeFormat
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string EntityName { get; set; }

        [StringLength(20)]
        public string Suffix { get; set; }

        [Required]
        [StringLength(20)]
        public string Prefix { get; set; }

        public int CurrentSeed { get; set; }

        [StringLength(3)]
        public string Seprator { get; set; }

        public int? Codelength { get; set; }
    }
}
