// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using System;
using System.Runtime.Serialization;


#endregion
namespace eCollabro.Service.DataContracts
{
    /// <summary>
    /// SiteRoleDC
    /// </summary>
    [DataContract]
    public partial class SiteRoleDC
    {
        [DataMember]
        public int SiteId { get; set; }

        [DataMember]
        public int RoleId { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public DateTime CreatedOn { get; set; }
    }
}
