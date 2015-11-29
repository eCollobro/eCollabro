// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using eCollabro.Service.Core;
using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion
namespace eCollabro.Service.DataContracts.ResponseWrapper
{
    /// <summary>
    /// SiteFeatures
    /// </summary>
    [DataContract]
    public class SiteFeatures
    {

        [DataMember]
        public int SiteId { get; set; }

        [DataMember]
        public string SiteCode { get; set; }

        [DataMember]
        public string SiteName { get; set; }

        public SiteFeatures()
        {
            this.Features = new List<ModuleDC>();
        }

        [DataMember]
        public List<ModuleDC> Features { get;set;}
        
    }
}
