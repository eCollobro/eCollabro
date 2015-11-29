// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#endregion 

namespace eCollabro.Client.Models.Core
{
    /// <summary>
    /// EmailConfigurationModel
    /// </summary>
    public class EmailConfigurationModel
    {
        public int EmailConfigurationId { get; set; }

        [Required,EmailAddress,DisplayName("Default Sender Email")]
        public string DefaultSenderEmail { get; set; }

        [Required,DisplayName("Host")]
        public string HostName { get; set; }

        [Required, DisplayName("SMTP Port")]
        public int PortNumber { get; set; }

        [DisplayName("Enable SSL")]
        public bool EnableSSL { get; set; }

        [DisplayName("Require Crenditials")]
        public bool RequireCredentials { get; set; }

        [Required, DisplayName("Username")]
        public string Username { get; set; }

        [Required, DisplayName("Password")]
        public string Password { get; set; }
    }
}
