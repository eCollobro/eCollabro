// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System.Runtime.Serialization;

#endregion

namespace eCollabro.Service.DataContracts.Core
{
    /// <summary>
    /// FeatureSettingDC
    /// </summary>
    [DataContract]
    public class FeatureSettingDC
    {
        [DataMember]
        public int ContentSettingId { get; set; }

        [DataMember]
        public string ContentSettingName { get; set; }
        
        [DataMember]
        public string ContentSettingDescription { get; set; }
        
        [DataMember]
        public bool IsAssigned { get; set; }
    }
}
