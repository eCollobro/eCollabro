// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;
using System.ComponentModel.DataAnnotations;
using eCollabro.Resources;

#endregion 

namespace eCollabro.Client.Models.Core
{
    /// <summary>
    /// SiteModel
    /// </summary>
    public class SiteModel
    {
        public int SiteId { get; set; }

        [Display(Name = "SiteCode", ResourceType = typeof(FieldNames))]
        public string SiteCode { get; set; }

        [Required,StringLength(50)]
        [Display(Name = "SiteName", ResourceType = typeof(FieldNames))]
        public string SiteName { get; set; }

        [StringLength(255)]
        [Display(Name = "SiteDesc", ResourceType = typeof(FieldNames))]
        public string SiteDesc { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(FieldNames))]
        public bool IsActive { get; set; }

        public int CreatedById { get; set; }

        [Display(Name = "CreatedOn", ResourceType = typeof(FieldNames))]
        public System.DateTime CreatedOn { get; set; }

        public Nullable<int> ModifiedById { get; set; }

        [Display(Name = "ModifiedOn", ResourceType = typeof(FieldNames))]
        public Nullable<System.DateTime> ModifiedOn { get; set; }

    }
}
