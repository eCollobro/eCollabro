// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using eCollabro.Service.DataContracts.Core;
using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion
namespace eCollabro.Service.DataContracts.RequestWrapper
{
    /// <summary>
    /// AddRoleFeaturesRequest
    /// </summary>
    [DataContract]
    public class AddRoleFeaturesRequest
    {
        [DataMember]
        public int RoleId { get; set; }
        [DataMember]
        public List<ModuleFeatureDC> Features { get; set; }

        public AddRoleFeaturesRequest()
        {

        }
    }
}
