namespace eCollabro.BAL.Entities.ADPModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("amt.tbl_config_lkpProcessStatus")]
    public partial class tbl_config_lkpProcessStatus
    {
        public tbl_config_lkpProcessStatus()
        {
            tbl_config_fileProcessStatus = new HashSet<tbl_config_fileProcessStatus>();
        }

        [Key]
        public int ProcessStatusID { get; set; }

        [StringLength(300)]
        public string ProcessStatusName { get; set; }

        [StringLength(500)]
        public string ProcessStatusDesc { get; set; }

        public bool? IsEnabled { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(300)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(300)]
        public string ModifiedBy { get; set; }

        public virtual ICollection<tbl_config_fileProcessStatus> tbl_config_fileProcessStatus { get; set; }
    }
}
