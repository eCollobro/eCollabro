// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#endregion 

namespace eCollabro.Client.Models.Core
{
    /// <summary>
    /// UserModel
    /// </summary>
    public class UserModel
    {
        public UserModel()
        {
            this.UserRoles = new List<RoleModel>();
        }

        [DisplayName("User Id")]
        public int UserId { get; set; }

        [DisplayName("User Name")]
        [Required, StringLength(50)]
        public string UserName { get; set; }

        [Required, StringLength(100), EmailAddress]
        public string Email { get; set; }


        public string ConfirmationToken { get; set; }

        [DisplayName("Confirmed")]
        public bool IsConfirmed { get; set; }

        [DisplayName("Approved")]
        public bool IsApproved { get; set; }

        [DisplayName("Locked")]
        public bool IsLocked { get; set; }

        [DisplayName("Active")]
        public bool IsActive { get; set; }

       
        public int CreatedById { get; set; }


        public DateTime CreatedOn { get; set; }


        public Nullable<int> ModifiedById { get; set; }


        public Nullable<System.DateTime> ModifiedOn { get; set; }

        [DisplayName("Assigned Roles"),Required]
        public List<RoleModel> UserRoles { get; set; }

   }
}
