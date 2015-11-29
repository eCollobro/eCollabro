// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System.Runtime.Serialization;

#endregion
namespace eCollabro.Service.DataMembers.Core
{
    /// <summary>
    /// FeaturePermissionDC
    /// </summary>
    [DataContract]
    public class FeaturePermissionDC
    {
        [DataMember]
        public int FeatureId { get; set; }

        [DataMember]
        public int ContentPermissionId { get; set; }

        [DataMember]
        public string ContentPermissionName { get; set; }

        [DataMember]
        public string ContentPermissionDescription { get; set; }

        [DataMember]
        public bool IsAssigned { get; set; }

    }
}
