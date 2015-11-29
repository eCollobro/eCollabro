namespace eCollabro.BAL.Entities.ADPModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("amt.tbl_adp_WorkerFileProcess")]
    public partial class tbl_adp_WorkerFileProcess
    {
        [Key]
        public int WorkerFileProcessID { get; set; }

        public int ProcessWorkerID { get; set; }

        public int WorkerStatusID { get; set; }

        public int? FileProcessID { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsError { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(300)]
        public string CreatedBy { get; set; }

        public virtual tbl_config_fileProcess tbl_config_fileProcess { get; set; }

        public virtual tbl_config_lkpWorkerStatus tbl_config_lkpWorkerStatus { get; set; }

        public virtual tbl_config_ProcessWorker tbl_config_ProcessWorker { get; set; }
    }
}
