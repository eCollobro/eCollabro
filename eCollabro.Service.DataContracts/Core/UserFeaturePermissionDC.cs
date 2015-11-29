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

#endregion 
namespace eCollabro.Service.DataContracts.Core
{
    /// <summary>
    /// UserFeaturePermissionDC
    /// </summary>
    [DataContract]
    public class UserFeaturePermissionDC
    {
        [DataMember]
        public int PermissionId { get; set; }

        [DataMember]
        public string PermissionName { get; set; }
    }
}
