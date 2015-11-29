// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using System.Runtime.Serialization;

#endregion
namespace eCollabro.Service.DataContracts.RequestWrapper
{
    /// <summary>
    /// ResetPasswordRequest
    /// </summary>
    [DataContract]
    public class ResetPasswordRequest : BaseServiceRequest
    {
        
            [DataMember]
            public string Password { get; set; }

            [DataMember]
            public string UserName { get; set; }

            [DataMember]
            public string Token { get; set; }
        }
    
}
