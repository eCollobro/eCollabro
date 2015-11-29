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
    /// SiteDC
    /// </summary>
    [DataContract]
    public partial class SiteDC
    {
        [DataMember]
        public int SiteId { get; set; }

        [DataMember]
        public string SiteCode { get; set; }


        [DataMember]
        public string SiteName { get; set; }

        [DataMember]
        public string SiteDesc { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public int CreatedById { get; set; }

        [DataMember]
        public DateTime CreatedOn { get; set; }

        [DataMember]
        public Nullable<int> ModifiedById { get; set; }

        [DataMember]
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}
