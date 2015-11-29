// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System.Collections.Generic;
using System.ComponentModel;

#endregion

namespace eCollabro.Client.Models.Core
{
    /// <summary>
    /// RoleFeaturesModel
    /// </summary>
    public class RoleFeaturesModel
    {
        public int SiteId { get; set; }
        public int RoleId { get; set; }

        [DisplayName("Role Code")]
        public string RoleCode { get; set; }

        [DisplayName("Role Name")]
        public string RoleName { get; set; }
        public List<ModuleModel> Features { get; set; }

        public RoleFeaturesModel()
        {
            Features = new List<ModuleModel>();
        }
    }
}
