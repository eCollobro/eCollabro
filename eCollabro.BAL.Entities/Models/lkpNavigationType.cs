namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("lkpNavigationType")]
    public partial class lkpNavigationType
    {
        public lkpNavigationType()
        {
            Navigations = new HashSet<Navigation>();
        }

        [Key]
        public int NavigationTypeId { get; set; }

        [Required]
        [StringLength(10)]
        public string NavigationTypeCode { get; set; }

        [StringLength(50)]
        public string NavigationType { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifiedById { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<Navigation> Navigations { get; set; }
    }
}
