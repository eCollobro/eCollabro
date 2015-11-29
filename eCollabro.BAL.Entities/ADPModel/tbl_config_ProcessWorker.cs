namespace eCollabro.BAL.Entities.ADPModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("amt.tbl_config_ProcessWorker")]
    public partial class tbl_config_ProcessWorker
    {
        public tbl_config_ProcessWorker()
        {
            tbl_adp_WorkerFileProcess = new HashSet<tbl_adp_WorkerFileProcess>();
        }

        [Key]
        public int ProcessWorkerID { get; set; }

        [Required]
        [StringLength(500)]
        public string PrcoessWorkerName { get; set; }

        [Required]
        [StringLength(1000)]
        public string ProcessWorkerDesc { get; set; }

        [StringLength(1000)]
        public string ProcessWorkerConnectionString { get; set; }

        public bool? IsEnabled { get; set; }

        public int? ProcessWorkerPriorityOrder { get; set; }

        public int? ProcessCores { get; set; }

        public virtual ICollection<tbl_adp_WorkerFileProcess> tbl_adp_WorkerFileProcess { get; set; }
    }
}
