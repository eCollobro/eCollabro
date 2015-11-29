// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using eCollabro.Resources;

#endregion 
namespace eCollabro.Client.Models.Core
{
    /// <summary>
    /// NavigationModel
    /// </summary>
    public class NavigationModel
    {
        [Required]
        public Int32 NavigationId { get; set; }

        [Required,DisplayName("Navigation Code")]
        public String NavigationCode { get; set; }

        [Required, StringLength(50)]
        [Display(Name = "NavigationText", ResourceType = typeof(FieldNames))]
        public String NavigationText { get; set; }

        [DisplayName("Additional Html")]
        public string AdditionalHtml { get; set; }

        [Required]
        public Int32 SiteId { get; set; }

        [DisplayName("Parent Navigation")]
        public Nullable<Int32> NavigationParentId { get; set; }

        [Required,DisplayName("Navigation Type")]
        public Int32 NavigationTypeId { get; set; }

        public string NavigationTypeCode { get; set; }

        [Required,DisplayName("Feature Id")]
        public Nullable<Int32> FeatureId { get; set; }

        [Required, DisplayName("Content Page Id")]
        public Nullable<Int32> ContentPageId { get; set; }

        [DisplayName("Display Order")]
        public Nullable<Int32> DisplayOrder { get; set; }

        [DisplayName("Anomynous Access")]
        public Boolean IsAnomynousAccess { get; set; }

        public Boolean IsActive { get; set; }

        public Int32 CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public Nullable<Int32> ModifiedById { get; set; }

        public Nullable<DateTime> ModifiedOn { get; set; }

        public String NavigationParentText { get; set; }
     
        public String NavigationType { get; set; }

        public String SiteName { get; set; }

        public String CreatedByName { get; set; }

        public String ModifiedByName { get; set; }

        [StringLength(255),Required]
        public String Link { get; set; }

    }
}
