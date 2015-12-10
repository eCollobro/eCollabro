using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCollabro.BAL.Entities.TempModels
{
    public class TempBlog 
    {
        public int BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogDescription { get; set; }
        public string BlogContent { get; set; }
        public int BlogCategoryId { get; set; }
        public string ApprovalStatus { get; set; }
        public Nullable<int> ApproveRejectById { get; set; }
        public Nullable<System.DateTime> ApproveRejectDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsAnomynousAccess { get; set; }
        public bool IsCommentsAllowed { get; set; }
        public bool IsLikeAllowed  { get; set; }
        public bool IsRatingAllowed { get; set; }
        public bool IsVotingAllowed { get; set; }
        public int CreatedById { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<int> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}
