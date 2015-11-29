namespace eCollabro.BAL.Entities.ADPModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using eCollabro.BAL.Entities.CustomModels;

    public partial class ADPModel : DbContext
    {
        public ADPModel()
            : base("name=ADPModel")
        {
        }

        public virtual DbSet<tbl_adp_WorkerFileProcess> tbl_adp_WorkerFileProcess { get; set; }
        public virtual DbSet<tbl_config_Coordinator> tbl_config_Coordinator { get; set; }
        public virtual DbSet<tbl_config_fileProcess> tbl_config_fileProcess { get; set; }
        public virtual DbSet<tbl_config_fileProcessStatus> tbl_config_fileProcessStatus { get; set; }
        public virtual DbSet<tbl_config_lkpProcessStatus> tbl_config_lkpProcessStatus { get; set; }
        public virtual DbSet<tbl_config_lkpWorkerStatus> tbl_config_lkpWorkerStatus { get; set; }
        public virtual DbSet<tbl_config_ProcessWorker> tbl_config_ProcessWorker { get; set; }
        public virtual DbSet<QueuesStatus> QueuesStatus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_config_fileProcess>()
                .HasMany(e => e.tbl_config_fileProcessStatus)
                .WithRequired(e => e.tbl_config_fileProcess)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_config_lkpProcessStatus>()
                .Property(e => e.ProcessStatusName)
                .IsFixedLength();

            modelBuilder.Entity<tbl_config_lkpProcessStatus>()
                .HasMany(e => e.tbl_config_fileProcessStatus)
                .WithRequired(e => e.tbl_config_lkpProcessStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_config_lkpWorkerStatus>()
                .Property(e => e.ModifiedDate)
                .IsFixedLength();

            modelBuilder.Entity<tbl_config_lkpWorkerStatus>()
                .Property(e => e.ModifiedBy)
                .IsFixedLength();

            modelBuilder.Entity<tbl_config_lkpWorkerStatus>()
                .HasMany(e => e.tbl_adp_WorkerFileProcess)
                .WithRequired(e => e.tbl_config_lkpWorkerStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_config_ProcessWorker>()
                .HasMany(e => e.tbl_adp_WorkerFileProcess)
                .WithRequired(e => e.tbl_config_ProcessWorker)
                .WillCascadeOnDelete(false);
        }
    }
}
