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
    /// ContentCommentDC
    /// </summary>
    [DataContract]
    public class ContentCommentDC
    {
            [DataMember]
            public int ContentCommentId { get; set; }

            [DataMember]
            public int ContextId { get; set; }

            [DataMember]
            public int ContextContentId { get; set; }

            [DataMember]
            public string Comment { get; set; }

            [DataMember]    
            public Nullable<int> ParentContentCommentId { get; set; }

            [DataMember]
            public string ApprovalStatus { get; set; }

            [DataMember]
            public Nullable<int> ApproveRejectById { get; set; }

            [DataMember]
            public Nullable<System.DateTime> ApproveRejectDate { get; set; }

            [DataMember]
            public bool IsAnonymous { get; set; }

            [DataMember]
            public string AnonymousUserEmail { get; set; }

            [DataMember]
            public string AnonymousUserName { get; set; }

            [DataMember]
            public bool IsDeleted { get; set; }

            [DataMember]
            public int CreatedById { get; set; }

            [DataMember]
            public System.DateTime CreatedOn { get; set; }

            [DataMember]
            public Nullable<int> ModifiedById { get; set; }

            [DataMember]
            public Nullable<System.DateTime> ModifiedOn { get; set; }

            [DataMember]
            public string TimeInterval { get; set; }

            [DataMember]
            public string CreatedBy { get; set; }
    }
}
