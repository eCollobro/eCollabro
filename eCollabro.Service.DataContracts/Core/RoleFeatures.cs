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
    /// RoleFeaturesResponse
    /// </summary>
    [DataContract]
    public class RoleFeatures
    {
        [DataMember]
        public int RoleId { get; set; }

        [DataMember]
        public string RoleCode { get; set; }

        [DataMember]
        public string RoleName { get; set; }

        public RoleFeatures()
        {
            this.Features = new List<ModuleDC>();
        }

        [DataMember]
        public List<ModuleDC> Features { get; set; }

    }
}
