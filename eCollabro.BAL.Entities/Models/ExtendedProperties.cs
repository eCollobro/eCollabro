using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCollabro.BAL.Entities.Models
{
    public partial class BlogCategory
    {
        [NotMapped]
        public int NumberOfBlogs { get; set; }
    }

    public partial class DocumentLibrary
    {
        [NotMapped]
        public int NumberOfDocuments { get; set; }
    }

    public partial class ContentPageCategory
    {
        [NotMapped]
        public int NumberOfContentPages { get; set; }
    }

    public partial class ImageGallery
    {
        [NotMapped]
        public int NumberOfImages { get; set; }
    }
    public partial class ContentComment
    {
        [NotMapped]
        public string TimeInterval { get; set; }

        [NotMapped]
        public string CreatedBy {get;set;}
    }

    public partial class WorkflowComment
    {
        [NotMapped]
        public string TimeInterval { get; set; }

        [NotMapped]
        public string CreatedBy { get; set; }
    }
}
