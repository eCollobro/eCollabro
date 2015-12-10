using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCollabro.BAL.Entities.TempModels
{
    public class TempProduct
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductDescription { get; set; }
        public string ProductSpecifications { get; set; }
        public int ProductCategoryId { get; set; }
        public int ProductTypeId { get; set; }
        public double ProductPrice { get; set; }
        public int QuantityAvailable { get; set; }
        public string ApprovalStatus { get; set; }
        public Nullable<int> ApproveRejectById { get; set; }
        public Nullable<System.DateTime> ApproveRejectDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsAnomynousAccess { get; set; }
        public bool IsCommentsAllowed { get; set; }
        public bool IsRatingAllowed { get; set; }
        public bool IsVotingAllowed { get; set; }
        public bool IsLikeAllowed { get; set; }
        public int CreatedById { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<int> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}
