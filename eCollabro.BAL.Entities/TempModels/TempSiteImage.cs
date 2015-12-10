using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCollabro.BAL.Entities.TempModels
{
    public class TempSiteImage 
    {

        public int ImageId { get; set; }
        public string ImageTitle { get; set; }
        public string ImageDescription { get; set; }
        public string ImageFileName { get; set; }
        public byte[] ImageFile { get; set; }
        public byte[] ImageThumbnail { get; set; }
        public int ImageGalleryId { get; set; }
        public string ApprovalStatus { get; set; }
        public Nullable<int> ApproveRejectById { get; set; }
        public Nullable<System.DateTime> ApproveRejectDate { get; set; }
        public bool IsAnomynousAccess { get; set; }
        public bool IsCommentsAllowed { get; set; }
        public bool IsLikeAllowed { get; set; }
        public bool IsRatingAllowed { get; set; }
        public bool IsVotingAllowed { get; set; }
        public bool IsActive { get; set; }
        public int CreatedById { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<int> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}
