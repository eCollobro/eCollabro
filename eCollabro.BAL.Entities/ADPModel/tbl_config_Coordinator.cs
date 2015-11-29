namespace eCollabro.BAL.Entities.ADPModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("amt.tbl_config_Coordinator")]
    public partial class tbl_config_Coordinator
    {
        [Key]
        public int CoordinatorID { get; set; }

        [StringLength(300)]
        public string CoordinatorName { get; set; }

        [StringLength(500)]
        public string CoordinatorDesc { get; set; }

        public bool? IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(500)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(500)]
        public string ModifiedBy { get; set; }
    }
}
