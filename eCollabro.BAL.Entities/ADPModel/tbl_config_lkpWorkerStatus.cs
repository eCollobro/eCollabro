namespace eCollabro.BAL.Entities.ADPModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("amt.tbl_config_lkpWorkerStatus")]
    public partial class tbl_config_lkpWorkerStatus
    {
        public tbl_config_lkpWorkerStatus()
        {
            tbl_adp_WorkerFileProcess = new HashSet<tbl_adp_WorkerFileProcess>();
        }

        [Key]
        public int WorkerStatusID { get; set; }

        [StringLength(500)]
        public string WorkerStatusName { get; set; }

        [StringLength(500)]
        public string WorkerStatusDesc { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(500)]
        public string CreatedBy { get; set; }

        [StringLength(10)]
        public string ModifiedDate { get; set; }

        [StringLength(10)]
        public string ModifiedBy { get; set; }

        public virtual ICollection<tbl_adp_WorkerFileProcess> tbl_adp_WorkerFileProcess { get; set; }
    }
}
