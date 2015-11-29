// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>

namespace eCollabro.Client.Models.Core
{
    /// <summary>
    /// SiteContentSettingModel
    /// </summary>
    public class SiteContentSettingModel
    {

        public int FeatureId { get; set; }


        public int SiteId { get; set; }


        public int ContentSettingId { get; set; }


        public string ContentSettingName { get; set; }


        public string ContentSettingDescription { get; set; }


        public bool IsAssigned { get; set; }
    }
}
