// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;
using System.Runtime.Serialization;

#endregion 
namespace eCollabro.Service.DataContracts.Core
{
    /// <summary>
    /// RoleDC
    /// </summary>
    [DataContract]
    public class RoleDC
    {
        [DataMember]
        public int RoleId { get; set; }

        [DataMember]
        public string RoleCode { get; set; }

        [DataMember]
        public int SiteId { get; set; }

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public string RoleDescription { get; set; }

        [DataMember]
        public bool IsSystem { get; set; }

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
