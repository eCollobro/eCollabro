// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.Common;
using eCollabro.Resources;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#endregion 
namespace eCollabro.Client.Models.ESB
{
    /// <summary>
    /// ESBAppModel
    /// </summary>
    public class ESBAppModel
    {
        public int AppId { get; set; }

        public string AppName { get; set; }

        public string AppAssembly { get; set; }

        public string AppDescription { get; set; }

        public bool IsExternal { get; set; }

        public string ScheduleInfo { get; set; }

        public string Configuration { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public int ESBAppInstanceId { get; set; }

        public int ServiceId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? StopTime { get; set; }

        public int ComponentStatusId { get; set; }

        public string ComponentStatusCode { get; set; }

        public string ComponentStatusName { get; set; }

        public DateTime? LastExecuted { get; set; }

        public DateTime? NextSchedule { get; set; }
    }
}
