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
    /// FeatureDC
    /// </summary>
    [DataContract]
    public class FeatureDC
    {
        [DataMember]
        public int FeatureLanguageId { get; set; }

        [DataMember]
        public int FeatureId { get; set; }

        [DataMember]
        public string FeatureName { get; set; }

        [DataMember]
        public string FeatureDesc { get; set; }

        [DataMember]
        public int FeatureTypeId { get; set; }

        [DataMember]
        public string FeatureCode { get; set; }

        [DataMember]
        public string FeatureTypeCode { get; set; }

        [DataMember]
        public string FeatureType { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public string LanguageCode { get; set; }

        [DataMember]
        public string Language { get; set; }

        [DataMember]
        public Nullable<int> FeatureParentId { get; set; }

        [DataMember]
        public string ParentFeature { get; set; }

        [DataMember]
        public Nullable<int> DisplayOrder { get; set; }

        [DataMember]
        public Nullable<int> AppPageId { get; set; }

        [DataMember]
        public int SiteId { get; set; }

        [DataMember]
        public int ModuleId { get; set; }

        [DataMember]
        public bool IsAssigned { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public int CreatedById { get; set; }

        [DataMember]
        public DateTime CreatedOn { get; set; }

        [DataMember]
        public Nullable<int> ModifiedId { get; set; }

        [DataMember]
        public Nullable<DateTime> ModifiedOn { get; set; }

        [DataMember]
        public string ModifiedBy { get; set; }

        [DataMember]
        public string CreatedBy { get; set; }

        [DataMember]
        public bool IsAnomynousAccess { get; set; }

        [DataMember]
        public bool IsNavigationLink { get; set; }

        [DataMember]
        public Nullable<int> NavigationId { get; set; }

        [DataMember]
        public string Link { get; set; }

        [DataMember]
        public Nullable<int> ContentId { get; set; }

    }
}
