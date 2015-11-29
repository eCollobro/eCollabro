namespace eCollabro.BAL.Entities.ADPModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("amt.tbl_config_fileProcessStatus")]
    public partial class tbl_config_fileProcessStatus
    {
        [Key]
        public int FileProcessStatusID { get; set; }

        public int FileProcessID { get; set; }

        public int ProcessStatusID { get; set; }

        [StringLength(1000)]
        public string Note { get; set; }

        [StringLength(1000)]
        public string OutPutPath { get; set; }

        [Required]
        [StringLength(300)]
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual tbl_config_fileProcess tbl_config_fileProcess { get; set; }

        public virtual tbl_config_lkpProcessStatus tbl_config_lkpProcessStatus { get; set; }
    }
}
