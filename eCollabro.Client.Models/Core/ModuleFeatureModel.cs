// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.Service.DataContracts.Core;
using System;
using System.Collections.Generic;

#endregion 

namespace eCollabro.Client.Models.Core
{
    public class ModuleFeatureModel
    {
        public int FeatureId { get; set; }
        public int ModuleId { get; set; }
        public string FeatureName { get; set; }
        public bool IsNavigationLink { get; set; }
        public string Link { get; set; }
        public bool IsSiteAssignable { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
        public Nullable<int> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public bool IsSelected { get; set; }
        public List<FeaturePermissionModel> RoleFeaturePermissions { get; set; }
        public List<SiteContentSettingModel> SiteContentSettings { get; set; }
    }
}
