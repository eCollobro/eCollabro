// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using eCollabro.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion 
namespace eCollabro.Client.Models.Core
{
    /// <summary>
    /// SiteRoleModel
    /// </summary>
    public class SiteRoleModel
    {
        public int SiteId { get; set; }

        [Display(Name = "Role", ResourceType = typeof(FieldNames))]
        [Required(ErrorMessage = "Role is required.")]
        public int RoleId { get; set; }
        public int CreatedBy {get; set; }
        public DateTime CreatedOn { get; set; }

        public IEnumerable<RoleModel> Roles { get; set; } 
    }
}
