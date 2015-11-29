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
    /// SiteCollectionAdminDC
    /// </summary>
    [DataContract]
    public class SiteCollectionAdminDC
    {
        [DataMember]
        public int SiteCollectionAdminid { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Email { get; set; }
    }
}
