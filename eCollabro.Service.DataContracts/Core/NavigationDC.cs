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
    /// NavigationDC
    /// </summary>
    [DataContract]
    public class NavigationDC
    {
        [DataMember]
        public int NavigationId { get; set; }
        [DataMember]
        public string NavigationCode { get; set; }
        [DataMember]
        public string NavigationText { get; set; }

        [DataMember]
        public string AdditionalHtml { get; set; }

        [DataMember]
        public int SiteId { get; set; }
        [DataMember]
        public Nullable<int> NavigationParentId { get; set; }
        [DataMember]
        public string Link { get; set; }
        [DataMember]
        public string NavigationParentCode { get; set; }
        [DataMember]
        public string NavigationParentText { get; set; }
        [DataMember]
        public int NavigationTypeId { get; set; }       
        [DataMember]
        public string NavigationTypeCode { get; set; }
        [DataMember]
        public string NavigationType { get; set; }
        [DataMember]
        public Nullable<int> FeatureId { get; set; }
        [DataMember]
        public string FeatureCode { get; set; }
        [DataMember]
        public string FeatureName { get; set; }
        [DataMember]
        public Nullable<int> ContentPageId { get; set; }
        [DataMember]
        public Nullable<int> DisplayOrder { get; set; }
        [DataMember]
        public bool IsAnomynousAccess { get; set; }
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
        [DataMember]
        public string SiteName { get; set; }
        [DataMember]
        public string ModifiedByName { get; set; }
        [DataMember]
        public string CreatedByName { get; set; }


    }
}