// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>

namespace eCollabro.Service.DataContracts.Core
{
    /// <summary>
    /// FeaturePermissionModel
    /// </summary>
    public class FeaturePermissionModel
    {
        public int FeatureId { get; set; }

        public int ContentPermissionId { get; set; }

        public string ContentPermissionName { get; set; }

        public string ContentPermissionDescription { get; set; }

        public bool IsAssigned { get; set; }

    }
}
