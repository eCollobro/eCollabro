namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FileObject")]
    public partial class FileObject
    {
        public FileObject()
        {
            Documents = new HashSet<Document>();
        }

        public int FileObjectId { get; set; }

        [Required]
        public byte[] FileObjectData { get; set; }

        public int ContextId { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifiedById { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<Document> Documents { get; set; }

        public virtual lkpContext lkpContext { get; set; }
    }
}
