// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using System.Runtime.Serialization;

#endregion 
namespace eCollabro.Service.DataContracts.RequestWrapper
{
    /// <summary>
    /// SiteRoleFeaturesRequest
    /// </summary>
    [DataContract]
    public class SiteRoleFeaturesRequest:BaseServiceRequest
    {
        private int SiteId;
        private int roleId;

        [DataMember]
        public int SiteID { get { return SiteId; } set { SiteId = value; } }
        [DataMember]
        public int RoleID { get { return roleId; } set { roleId = value; } }

        public SiteRoleFeaturesRequest() { }
    }
}
