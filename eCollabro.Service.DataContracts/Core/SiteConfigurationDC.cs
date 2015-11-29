// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

#endregion 
namespace eCollabro.Service.DataContracts.Core
{
    /// <summary>
    /// SiteConfigurationDC
    /// </summary>
    [DataContract]
    public class SiteConfigurationDC
    {
        [DataMember]
        public int SiteConfigurationId { get; set; }
        
        [DataMember]
        public int SiteId { get; set; }

        [DataMember]
        public bool AllowRegistration { get; set; }

        [DataMember]
        public Nullable<int> RegistrationDefaultRoleId { get; set; }

        [DataMember]
        public bool AccountRequireEmailVerification { get; set; }

        [DataMember]
        public int ModifiedById { get; set; }

        [DataMember]
        public System.DateTime ModifiedOn { get; set; }

    }
}
