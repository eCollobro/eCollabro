using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCollabro.BAL.Entities.TempModels
{
    public class TempContentPage 
    {
        public int ContentPageId { get; set; }
        public string ContentPageTitle { get; set; }
        public string ContentPageDescription { get; set; }
        public string ContentPageContent { get; set; }
        public int ContentPageCategoryId { get; set; }
        public string ApprovalStatus { get; set; }
        public Nullable<int> ApproveRejectById { get; set; }
        public Nullable<System.DateTime> ApproveRejectDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsAnomynousAccess { get; set; }
        public bool IsCommentsAllowed { get; set; }
        public bool IsLikeAllowed { get; set; }
        public bool IsRatingAllowed { get; set; }
        public bool IsVotingAllowed { get; set; }
        public int CreatedById { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<int> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }

    }
}
