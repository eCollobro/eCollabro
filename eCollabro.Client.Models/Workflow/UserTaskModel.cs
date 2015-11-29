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
    /// UserTaskModel
    /// </summary>
    public class UserTaskModel
    {
        public int TaskId { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public Nullable<int> AssignedUserId { get; set; }
        public Nullable<int> AssignedByUserId { get; set; }
        public string AssignedUserName { get; set; }
        public string AssignedByUserName { get; set; }
        
        public Nullable<System.DateTime> AssignedDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<double> CompletionPercentage { get; set; }
        public string TaskStatus { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public string TaskType { get; set; }
        public string Context { get; set; }
        public int ContextId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public int CreatedById { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<int> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}
