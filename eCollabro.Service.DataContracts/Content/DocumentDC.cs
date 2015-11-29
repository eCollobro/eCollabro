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
    /// DocumentDC
    /// </summary>
    [DataContract]
    public class DocumentDC
    {
        [DataMember]
        public int DocumentId { get; set; }

        [DataMember]
        public string DocumentTitle { get; set; }

        [DataMember]
        public string DocumentDescription { get; set; }

        [DataMember]
        public string DocumentFileName { get; set; }

        [DataMember]
        public int DocumentLibraryId { get; set; }

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
        public byte[] DocumentFile { get; set; }

        [DataMember]
        public string ApprovalStatus { get; set; }

        [DataMember]
        public Nullable<int> ApproveRejectById { get; set; }

        [DataMember]
        public Nullable<System.DateTime> ApproveRejectDate { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

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
