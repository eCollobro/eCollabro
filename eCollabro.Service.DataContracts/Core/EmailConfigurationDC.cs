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
    /// EmailConfigurationDC
    /// </summary>
    [DataContract]
    public class EmailConfigurationDC
    {
        [DataMember]
        public int EmailConfigurationId { get; set; }

        [DataMember]
        public string DefaultSenderEmail { get; set; }

        [DataMember]
        public string HostName { get; set; }

        [DataMember]
        public int PortNumber { get; set; }

        [DataMember]
        public bool EnableSSL { get; set; }

        [DataMember]
        public bool RequireCredentials { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}
