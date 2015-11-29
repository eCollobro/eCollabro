// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;
using System.Runtime.Serialization;

#endregion
namespace eCollabro.Service.DataContracts.Content
{
    /// <summary>
    /// BlogDC
    /// </summary>
    [DataContract]
    public class BlogDC
    {
        [DataMember]
        public int BlogId { get; set; }

        [DataMember]
        public string BlogTitle { get; set; }

        [DataMember]
        public string BlogDescription { get; set; }

        [DataMember]
        public string BlogContent { get; set; }

        [DataMember]
        public int BlogCategoryId { get; set; }

        [DataMember]
        public string ApprovalStatus { get; set; }

        [DataMember]
        public Nullable<int> ApprovedById { get; set; }

        [DataMember]
        public Nullable<System.DateTime> ApprovedDate { get; set; }

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
        public int CreatedById { get; set; }

        [DataMember]
        public System.DateTime CreatedOn { get; set; }

        [DataMember]
        public Nullable<int> ModifiedById { get; set; }

        [DataMember]
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}
