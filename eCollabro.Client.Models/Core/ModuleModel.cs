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
    /// ModuleModel
    /// </summary>
    public class ModuleModel
    {
        public ModuleModel()
        {
            this.ModuleFeatures = new List<ModuleFeatureModel>();
        }

        public int ModuleId { get; set; }
        
        public string ModuleCode { get; set; }
        
        [Required]
        [Display(Name = "Module_ModuleName", ResourceType = typeof(FieldNames))]
        [StringLength(100, ErrorMessage = "Module Name cannot be longer than 100 characters.")]
        public string ModuleName { get; set; }
        
        [Required]
        [Display(Name = "Module_ModuleDescription", ResourceType = typeof(FieldNames))]
        [StringLength(510, ErrorMessage = "Module Description cannot be longer than 510 characters.")]
        public string ModuleDescription { get; set; }
        
        public System.DateTime CreatedOn { get; set; }
        
        public int CreatedById { get; set; }
        
        public Nullable<int> ModifiedById { get; set; }
        
        public Nullable<System.DateTime> ModifiedOn { get; set; }

        public List<ModuleFeatureModel> ModuleFeatures { get; set; }

        //public int ModuleId { get; set; }
        //public string ModuleName { get; set; }
        //public string ModuleDescription { get; set; }
        //public System.DateTime CreatedOn { get; set; }
        //public int CreatedById { get; set; }
        //public Nullable<int> ModifiedById { get; set; }
        //public Nullable<System.DateTime> ModifiedOn { get; set; }
        //public List<ModuleFeatureModel> ModuleFeatures { get; set; }
    }
}
