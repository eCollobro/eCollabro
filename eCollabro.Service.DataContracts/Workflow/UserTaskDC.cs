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
    /// UserTaskDC
    /// </summary>
    [DataContract]
    public class UserTaskDC
    {
        [DataMember]
        public int TaskId { get; set; }

        [DataMember]
        public string TaskTitle { get; set; }

        [DataMember]
        public string TaskDescription { get; set; }

        [DataMember]
        public Nullable<int> AssignedUserId { get; set; }

        [DataMember]
        public Nullable<int> AssignedByUserId { get; set; }

        [DataMember]
        public string AssignedUserName { get; set; }

        [DataMember]
        public string AssignedByUserName { get; set; }

        [DataMember]
        public Nullable<System.DateTime> AssignedDate { get; set; }

        [DataMember]
        public Nullable<System.DateTime> DueDate { get; set; }

        [DataMember]
        public Nullable<double> CompletionPercentage { get; set; }

        [DataMember]
        public string TaskStatus { get; set; }

        [DataMember]
        public Nullable<System.DateTime> CompletionDate { get; set; }

        [DataMember]
        public string TaskType { get; set; }

        [DataMember]
        public string Context { get; set; }

        [DataMember]
        public int ContextId { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }

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
