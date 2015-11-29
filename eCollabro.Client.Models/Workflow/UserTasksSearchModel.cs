// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.Common;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#endregion 
namespace eCollabro.Client.Models.Workflow
{
    /// <summary>
    /// UserTasksSearchModel
    /// </summary>
    public class UserTasksSearchModel
    {
        public ContextEnum Context { get; set; }

        [DisplayName("Assigned To")]
        public string AssignedTo { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date From")]
        public DateTime? FromDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date To")]
        public DateTime? ToDate { get; set; }

        [DisplayName("Active Tasks")]
        public bool ActiveTasks { get; set; }
    }
}
