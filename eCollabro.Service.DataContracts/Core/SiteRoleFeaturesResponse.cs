// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using System.Collections.Generic;
using System.Runtime.Serialization;
using eCollabro.Service.Core;

#endregion 
namespace eCollabro.Service.DataContracts.ResponseWrapper
{
    /// <summary>
    /// SiteRoleFeaturesResponse
    /// </summary>
    [DataContract]
    public class SiteRoleFeaturesResponse : BaseServiceResponse
    {
        private List<ModuleDC> _SiteFeatures;

        public SiteRoleFeaturesResponse() 
        { 
            _SiteFeatures = new List<ModuleDC>(); 
        }

        [DataMember]
        public List<ModuleDC> SiteFeatures { get { return _SiteFeatures; } set { _SiteFeatures = value; } }
    }
}
