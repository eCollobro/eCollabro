// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.Service.DataMembers.Core;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion

namespace eCollabro.Service.DataContracts.Core
{
    
    /// <summary>
    /// ModuleFeatureDC
    /// </summary>
    [DataContract]
    public class ModuleFeatureDC
    {
        [DataMember]
        public int FeatureId { get; set; }
        [DataMember]
        public string FeatureCode { get; set; }
        [DataMember]
        public int ModuleId { get; set; }
        [DataMember]
        public string FeatureName { get; set; }
        [DataMember]
        public bool IsNavigationLink { get; set; }
        [DataMember]
        public string Link { get; set; }
        [DataMember]
        public System.DateTime CreatedOn { get; set; }
        [DataMember]
        public int CreatedById { get; set; }
        [DataMember]
        public Nullable<int> ModifiedById { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        [DataMember]
        public bool IsSelected { get;set; }

        [DataMember]
        public List<FeaturePermissionDC> RoleFeaturePermissions { get; set; }

        [DataMember]
        public List<SiteContentSettingDC> SiteContentSettings { get; set; }
    }
}
