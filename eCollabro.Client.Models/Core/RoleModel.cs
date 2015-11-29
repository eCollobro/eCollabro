// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.Resources;
using System.ComponentModel.DataAnnotations;

#endregion 
namespace eCollabro.Client.Models.Core
{
    /// <summary>
    /// RoleModel
    /// </summary>
    public class RoleModel
    {
        public int RoleId { get; set; }

        [Display(Name = "RoleCode", ResourceType = typeof(FieldNames))]
        public string RoleCode { get; set; }

        public int SiteId { get; set; }

        [Required, StringLength(50)]
        [Display(Name = "RoleName", ResourceType = typeof(FieldNames))]
        public string RoleName { get; set; }

        [StringLength(255)]
        [Display(Name = "RoleDescription", ResourceType = typeof(FieldNames))]
        public string RoleDescription { get; set; }

        public bool IsSystem { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(FieldNames))]
        public bool IsActive { get; set; }

        public int CreatedById { get; set; }

    }
}