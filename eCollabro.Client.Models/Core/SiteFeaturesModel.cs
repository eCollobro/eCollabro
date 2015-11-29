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
    /// SiteFeaturesModel
    /// </summary>
    public class SiteFeaturesModel
    {
        
        public int SiteId { get; set; }
        [DisplayName("Site Code")]
        public string SiteCode { get; set; }
        [DisplayName("Site")]
        public string SiteName { get; set; }
        public List<ModuleModel> Features { get; set; }
        public bool CreateNavigations { get; set; }

        public SiteFeaturesModel()
        {
            Features = new List<ModuleModel>();
        }
    }
}
