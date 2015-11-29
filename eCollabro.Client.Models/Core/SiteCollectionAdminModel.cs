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
    /// SiteCollectionAdminModel
    /// </summary>
    public class SiteCollectionAdminModel
    {
        public int SiteCollectionAdminId { get; set; }

        [Required (ErrorMessage="User is Required")]
        public int UserId { get; set; }

        [Required,DisplayName("Username")]
        public string UserName { get; set; }

        public string Email { get; set; }

    }


}
