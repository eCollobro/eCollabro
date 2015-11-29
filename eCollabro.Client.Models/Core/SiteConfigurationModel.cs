// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;
using System.ComponentModel;

#endregion 
namespace eCollabro.Client.Models.Core
{
    /// <summary>
    /// SiteConfigurationModel
    /// </summary>
    public class SiteConfigurationModel
    {

        public int SiteConfigurationId { get; set; }

        [DisplayName("Site")]
        public int SiteId { get; set; }

        [DisplayName("Allow registration on Site")]
        public bool AllowRegistration { get; set; }

        [DisplayName("Default assigned Role for registration")]
        public Nullable<int> RegistrationDefaultRoleId { get; set; }

        [DisplayName("Require Email Verification")]
        public bool AccountRequireEmailVerification { get; set; }


        public int ModifiedById { get; set; }

        public System.DateTime ModifiedOn { get; set; }
    }
}
