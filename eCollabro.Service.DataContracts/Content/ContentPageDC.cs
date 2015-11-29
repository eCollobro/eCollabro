// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using System;
using System.Runtime.Serialization;

#endregion
namespace eCollabro.Service.DataContracts
{
    /// <summary>
    /// ContentPageDC
    /// </summary>
    [DataContract]
    public  class ContentPageDC
    {
        [DataMember]
        public int ContentPageId { get; set; }

        [DataMember]
        public string ContentPageTitle { get; set; }

        [DataMember]
        public string ContentPageDescription { get; set; }

        [DataMember]
        public string ContentPageContent { get; set; }

        [DataMember]
        public int ContentPageCategoryId { get; set; }

        [DataMember]
        public string ApprovalStatus { get; set; }

        [DataMember]
        public Nullable<int> ApproveRejectById { get; set; }

        [DataMember]
        public Nullable<System.DateTime> ApproveRejectDate { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public bool IsAnomynousAccess { get; set; }


        [DataMember]
        public bool IsCommentsAllowed { get; set; }

        [DataMember]
        public bool IsRatingAllowed { get; set; }

        [DataMember]
        public bool IsVotingAllowed { get; set; }

        [DataMember]
        public bool IsLikeAllowed { get; set; }

        [DataMember]
        public bool SetToHomePage { get; set; }

        [DataMember]
        public bool AddToNavigation { get; set; }

        [DataMember]
        public string MenuTitle { get; set; }

        [DataMember]
        public int NavigationParentId { get; set; }

        [DataMember]
        public int CreatedById { get; set; }

        [DataMember]
        public System.DateTime CreatedOn { get; set; }

        [DataMember]
        public Nullable<int> ModifiedById { get; set; }

        [DataMember]
        public Nullable<System.DateTime> ModifiedOn { get; set; }

    }
}
