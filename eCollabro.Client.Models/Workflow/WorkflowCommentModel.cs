// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;

#endregion 
namespace eCollabro.Client.Models.Workflow
{
    /// <summary>
    /// WorkflowCommentModel
    /// </summary>
    public class WorkflowCommentModel
    {

       
        public int WorkflowCommentId { get; set; }

        
        public int ContextId { get; set; }

        
        public string Context { get; set; }

        
        public string Comment { get; set; }

        
        public string CreatedBy { get; set; }

        
        public int CreatedById { get; set; }

        
        public DateTime CreatedOn { get; set; }

        
        public string TimeInterval { get; set; }
    }
}
