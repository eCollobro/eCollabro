// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;
using System.Collections.Generic;

#endregion 
namespace eCollabro.Client.Models.Core
{
    /// <summary>
    /// UserNavigationModel
    /// </summary>
    public class UserNavigationModel
    {
        public int NavigationId { get; set; }
        public string NavigationCode { get; set; }
        public string NavigationText { get; set; }
        public string AdditionalHtml { get; set; }
        public int SiteId { get; set; }
        public Nullable<int> NavigationParentId { get; set; }
        public string NavigationParentCode { get; set; }
        public string NavigationParentText { get; set; }
        public int NavigationTypeId { get; set; }
        public string NavigationTypeCode { get; set; }
        public string NavigationType { get; set; }
        public Nullable<int> FeatureId { get; set; }
        public string FeatureCode { get; set; }
        public string FeatureName { get; set; }
        public string Link { get; set; }
        public Nullable<int> ContentPageId { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public bool IsAnomynousAccess { get; set; }
        public List<UserNavigationModel> ChildNavigations { get; set; }
    }
}
