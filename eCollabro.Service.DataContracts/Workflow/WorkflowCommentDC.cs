// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using System;
using System.Runtime.Serialization;

#endregion
namespace eCollabro.Service.DataContracts.Workflow
{
    /// <summary>
    /// UserTaskCommentDC
    /// </summary>
    [DataContract]
    public class WorkflowCommentDC
    {
        [DataMember]
        public int WorkflowCommentId { get; set; }

        [DataMember]
        public int ContextId { get; set; }

        [DataMember]
        public string Context { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public string CreatedBy { get; set; }

        [DataMember]
        public int CreatedById { get; set; }

        [DataMember]
        public DateTime CreatedOn { get; set; }

        [DataMember]
        public string TimeInterval { get; set; }
    
    }
}
