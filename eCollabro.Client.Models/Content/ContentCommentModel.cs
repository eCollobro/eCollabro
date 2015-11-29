// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;
using System.Collections.Generic;

#endregion 
namespace eCollabro.Client.Models.Content
{
    /// <summary>
    /// ContentCommentModel
    /// </summary>
    public class ContentCommentModel
    {

        public int ContentCommentId { get; set; }


        public int ContextId { get; set; }


        public int ContextContentId { get; set; }


        public string Comment { get; set; }


        public Nullable<int> ParentContentCommentId { get; set; }


        public string ApprovalStatus { get; set; }


        public Nullable<int> ApproveRejectById { get; set; }


        public Nullable<System.DateTime> ApproveRejectDate { get; set; }


        public bool IsAnonymous { get; set; }


        public string AnonymousUserEmail { get; set; }


        public string AnonymousUserName { get; set; }


        public bool IsDeleted { get; set; }


        public int CreatedById { get; set; }


        public System.DateTime CreatedOn { get; set; }


        public Nullable<int> ModifiedById { get; set; }


        public Nullable<System.DateTime> ModifiedOn { get; set; }

        public List<ContentCommentModel> ContentComments { get; set; }

        public string TimeInterval { get; set; }

        public string CreatedBy { get; set; }
    }
}
