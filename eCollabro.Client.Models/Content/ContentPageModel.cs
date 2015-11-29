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
    /// ContentPageModel
    /// </summary>
    public class ContentPageModel
    {

        public int ContentPageId { get; set; }

        [DisplayName("Page Title"),Required]
        public string ContentPageTitle { get; set; }

        [DisplayName("Description")]
        public string ContentPageDescription { get; set; }

        [DisplayName("Page Content"),Required]
        public string ContentPageContent { get; set; }

        [DisplayName("Page Category"),Required]
        public int ContentPageCategoryId { get; set; }

        [DisplayName("Approval Status")]
        public string ApprovalStatus { get; set; }


        public Nullable<int> ApproveRejectById { get; set; }


        public Nullable<System.DateTime> ApproveRejectDate { get; set; }

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

        [DisplayName("Set to Home Page")]
        public bool SetToHomePage { get; set; }

        [DisplayName("Add To Navigation Menu")]
        public bool AddToNavigation { get; set; }

        [DisplayName("Menu Title"),Required]
        public string MenuTitle { get; set; }

        [DisplayName("Parent Navigation")]
        public int NavigationParentId { get;set ;}

        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}
