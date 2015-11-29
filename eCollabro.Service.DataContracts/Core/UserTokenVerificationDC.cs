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
    /// PasswordResetTokenDC
    /// </summary>
    [DataContract]
    public class UserTokenVerificationDC
    {

        [DataMember]
        public string UserName{get;set;}

        [DataMember]
        public string Token{get;set;}
    }
}
