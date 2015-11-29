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
    /// RegisterDC
    /// </summary>
    [DataContract]
    public class RegisterDC
    {
        /// <summary>
        /// UserName
        /// </summary>
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [DataMember]
        public string Password { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [DataMember]
        public string Email { get; set; }
    }
}
