// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using System.Runtime.Serialization;

#endregion
namespace eCollabro.Service.DataContracts
{
    /// <summary>
    /// BaseServiceRequest
    /// </summary>
    [DataContract]
    public class BaseServiceRequest :IBaseServiceRequest
    {
        [DataMember]
        public UserContextDC UserContext { get; set; }

        [DataMember]
        public RequestContextParameterDC RequestContextParameters { get; set; }

        public BaseServiceRequest()
        {
            this.UserContext = new UserContextDC();
        }
    }
}
