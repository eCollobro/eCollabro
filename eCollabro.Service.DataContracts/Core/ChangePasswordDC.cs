// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region references

using System.Runtime.Serialization;

#endregion

namespace eCollabro.Service.DataContracts.Core
{
    /// <summary>
    /// ChangePasswordDC
    /// </summary>
    [DataContract]
    public class ChangePasswordDC
    {
        [DataMember]
        public string UserName { get; set; }
        
        [DataMember]
        public string OldPassword { get; set; }

        [DataMember]
        public string NewPassword { get; set; }
    }
}
