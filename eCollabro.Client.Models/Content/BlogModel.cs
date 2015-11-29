// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.Common;
using eCollabro.Resources;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#endregion 
namespace eCollabro.Client.Models.Content
{
    /// <summary>
    /// BlogModel
    /// </summary>
    public class BlogModel
    {

        public int BlogId { get; set; }

        [Required,DisplayName("Blog Title")]
        public string BlogTitle { get; set; }

        [DisplayName("Description")]
        public string BlogDescription { get; set; }

        [Required,DisplayName("Blog Content")]
        public string BlogContent { get; set; }

        [DisplayName("Blog Category")]
        public int BlogCategoryId { get; set; }

        [DisplayName("Approval Status")]
        public string ApprovalStatus { get; set; }


        public Nullable<int> ApprovedById { get; set; }


        public Nullable<System.DateTime> ApprovedDate { get; set; }

        [Display(Name = FieldNameConstants.Common_IsActive, ResourceType = typeof(FieldNames))]
        public bool IsActive { get; set; }

        [Display(Name = FieldNameConstants.Common_IsAnomynousAccess, ResourceType = typeof(FieldNames))]
        public bool IsAnomynousAccess { get; set; }

        [Display(Name = FieldNameConstants.Common_IsCommentsAllowed, ResourceType = typeof(FieldNames))]
        public bool IsCommentsAllowed { get; set; }

        [Display(Name = FieldNameConstants.Common_IsRatingAllowed, ResourceType = typeof(FieldNames))]
        public bool IsRatingAllowed { get; set; }

        [Display(Name = FieldNameConstants.Common_IsVotingAllowed, ResourceType = typeof(FieldNames))]
        public bool IsVotingAllowed { get; set; }

        [Display(Name = FieldNameConstants.Common_IsLikeAllowed, ResourceType = typeof(FieldNames))]
        public bool IsLikeAllowed { get; set; }


        public int CreatedById { get; set; }


        public System.DateTime CreatedOn { get; set; }


        public Nullable<int> ModifiedById { get; set; }


        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}
