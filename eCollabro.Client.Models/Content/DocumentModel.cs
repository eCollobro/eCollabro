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
    /// Document
    /// </summary>
    public class DocumentModel
    {

        
        public int DocumentId { get; set; }

        [Required,DisplayName("Title")]
        public string DocumentTitle { get; set; }

        [DisplayName("Description")]
        public string DocumentDescription { get; set; }

        [DisplayName("Document File Name")]
        public string DocumentFileName { get; set; }

        [DisplayName("Document Library")]
        public int DocumentLibraryId { get; set; }

        
        public byte[] DocumentFile { get; set; }

        [DisplayName("Approval Status")]
        public string ApprovalStatus { get; set; }

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

        public Nullable<int> ApproveRejectById { get; set; }


        public Nullable<System.DateTime> ApproveRejectDate { get; set; }

        
        public int CreatedById { get; set; }

        
        public System.DateTime CreatedOn { get; set; }

        
        public Nullable<int> ModifiedById { get; set; }

        
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}
