// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using eCollabro.Service.DataContracts.Core;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion
namespace eCollabro.Service.Core
{
    /// <summary>
    /// ModuleDC
    /// </summary>
    [DataContract]
    public class ModuleDC
    {
        public ModuleDC()
        {
            this.Features = new List<ModuleFeatureDC>();
        }

        [DataMember]
        public int ModuleId { get; set; }
        [DataMember]
        public string ModuleCode { get; set; }
        [DataMember]
        public string ModuleName { get; set; }
        [DataMember]
        public string ModuleDescription { get; set; }
        [DataMember]
        public System.DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
        [DataMember]
        public Nullable<int> ModifiedById { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        [DataMember]
        public Nullable<bool> IsDeleted { get; set; }
        [DataMember]
        public virtual List<ModuleFeatureDC> Features { get; set; }

        //[DataMember]
        //public int ModuleId { get; set; }
        //[DataMember]
        //public string ModuleCode { get; set; }
        //[DataMember]
        //public string ModuleName { get; set; }
        //[DataMember]
        //public string ModuleDescription { get; set; }
        //[DataMember]
        //public System.DateTime CreatedOn { get; set; }
        //[DataMember]
        //public int CreatedById { get; set; }
        //[DataMember]
        //public Nullable<int> ModifiedById { get; set; }
        //[DataMember]
        //public Nullable<System.DateTime> ModifiedOn { get; set; }
        //[DataMember]
        //public List<ModuleFeatureDC> Features { get; set; }
    }
}
