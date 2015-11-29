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
    /// UserTasksRequestDC
    /// </summary>
    [DataContract]
    public class UserTasksRequestDC
    {
        [DataMember]
        public int ContextId{get;set;}

        [DataMember]
        public string AssignedTo{get;set;}

        [DataMember]
        public DateTime? FromDate{get;set;}

        [DataMember]
        public DateTime? ToDate { get; set; }

        [DataMember]
        public bool ActiveTasks { get; set; }
    }
}
