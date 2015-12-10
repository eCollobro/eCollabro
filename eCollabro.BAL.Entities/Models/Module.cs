namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Module")]
    public partial class Module
    {
        public Module()
        {
            Features = new HashSet<Feature>();
        }

        public int ModuleId { get; set; }

        [Required]
        [StringLength(10)]
        public string ModuleCode { get; set; }

        [Required]
        [StringLength(50)]
        public string ModuleName { get; set; }

        [StringLength(255)]
        public string ModuleDescription { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedById { get; set; }

        public int? ModifiedById { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Feature> Features { get; set; }
    }
}
