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
    /// AuthenticateUserRequest
    /// </summary>
    [DataContract]
    public class AuthenticateUserRequest : BaseServiceRequest
    {
        [DataMember]
        public string Username {get; set;}
        [DataMember]
        public string Password { get; set; }
        
        public AuthenticateUserRequest()
        {
           
        }

    }
}


