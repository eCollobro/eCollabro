namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DocumentLibrary")]
    public partial class DocumentLibrary
    {
        public DocumentLibrary()
        {
            Documents = new HashSet<Document>();
        }

        public int DocumentLibraryId { get; set; }

        [Required]
        [StringLength(100)]
        public string DocumentLibraryName { get; set; }

        [StringLength(255)]
        public string DocumentLibraryDescription { get; set; }

        public int SiteId { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public bool IsAnomynousAccess { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifiedById { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<Document> Documents { get; set; }

        public virtual Site Site { get; set; }
    }
}
