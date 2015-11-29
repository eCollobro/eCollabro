// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;
using System.Runtime.Serialization;

#endregion
namespace eCollabro.Service.DataContracts.ADP
{

    [DataContract]
    public class ESBAppDC
    {
        [DataMember]
        public int AppId { get; set; }

        [DataMember]
        public string AppName { get; set; }

        [DataMember]
        public string AppAssembly { get; set; }

        [DataMember]
        public string AppDescription { get; set; }

        [DataMember]
        public bool IsExternal { get; set; }

        [DataMember]
        public string ScheduleInfo { get; set; }

        [DataMember]
        public string Configuration { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public DateTime CreateDate { get; set; }

        [DataMember]
        public int ESBAppInstanceId { get; set; }

        [DataMember]
        public int ServiceId { get; set; }

        [DataMember]
        public DateTime? StartTime { get; set; }

        [DataMember]
        public DateTime? StopTime { get; set; }

        [DataMember]
        public int ComponentStatusId { get; set; }

        [DataMember]
        public string ComponentStatusCode { get; set; }

        [DataMember]
        public string ComponentStatusName { get; set; }

        [DataMember]
        public DateTime? LastExecuted { get; set; }

        [DataMember]
        public DateTime? NextSchedule { get; set; }
    }
}
