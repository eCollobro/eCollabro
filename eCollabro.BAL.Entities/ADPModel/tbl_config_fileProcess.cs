namespace eCollabro.BAL.Entities.ADPModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("amt.tbl_config_fileProcess")]
    public partial class tbl_config_fileProcess
    {
        public tbl_config_fileProcess()
        {
            tbl_adp_WorkerFileProcess = new HashSet<tbl_adp_WorkerFileProcess>();
            tbl_config_fileProcessStatus = new HashSet<tbl_config_fileProcessStatus>();
        }

        [Key]
        public int FileProcessID { get; set; }

        public int BatchID { get; set; }

        [Required]
        [StringLength(1000)]
        public string FilePath { get; set; }

        [Required]
        [StringLength(500)]
        public string FileName { get; set; }

        public int FileType { get; set; }

        [StringLength(100)]
        public string PublisherLiteral { get; set; }

        [Required]
        [StringLength(500)]
        public string VendorCodeOrName { get; set; }

        public int? FilePriority { get; set; }

        public int? BatchProcessingOrder { get; set; }

        public DateTime? ReceivedDate { get; set; }

        [Required]
        [StringLength(300)]
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<tbl_adp_WorkerFileProcess> tbl_adp_WorkerFileProcess { get; set; }

        public virtual ICollection<tbl_config_fileProcessStatus> tbl_config_fileProcessStatus { get; set; }
    }
}
