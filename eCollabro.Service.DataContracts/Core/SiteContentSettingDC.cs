// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

#endregion 
namespace eCollabro.Service.DataContracts.Core
{
    /// <summary>
    /// SiteContentSettingDC
    /// </summary>
    [DataContract]
    public class SiteContentSettingDC
    {
        [DataMember]
        public int FeatureId { get; set; }

        [DataMember]
        public int SiteId { get; set; }

        [DataMember]
        public int ContentSettingId { get; set; }

        [DataMember]
        public string ContentSettingName { get; set; }

        [DataMember]
        public string ContentSettingDescription { get; set; }

        [DataMember]
        public bool IsAssigned { get; set; }
    }
}
