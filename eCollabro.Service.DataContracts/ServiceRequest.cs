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
    [DataContract]
    public class ServiceRequest<T>:BaseServiceRequest
    {
        [DataMember]
        public T Parameter { get; set; }
    }

    [DataContract]
    public class ServiceRequest : BaseServiceRequest
    {
    }
}
