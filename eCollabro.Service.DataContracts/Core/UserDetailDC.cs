// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion 
namespace eCollabro.Service.DataContracts.Core
{
    /// <summary>
    /// UserDetailDC
    /// </summary>
    [DataContract]
    public class UserDetailDC
    {
        public UserDetailDC()
        {
            this.UserRoles = new List<RoleDC>();
        }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string ConfirmationToken { get; set; }

        [DataMember]
        public bool IsConfirmed { get; set; }

        [DataMember]
        public bool IsApproved { get; set; }

        [DataMember]
        public bool IsLocked { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public int CreatedById { get; set; }

        [DataMember]
        public DateTime CreatedOn { get; set; }

        [DataMember]
        public Nullable<int> ModifiedById { get; set; }

        [DataMember]
        public Nullable<System.DateTime> ModifiedOn { get; set; }

        [DataMember]
        public List<RoleDC> UserRoles { get; set; }
    }
}
