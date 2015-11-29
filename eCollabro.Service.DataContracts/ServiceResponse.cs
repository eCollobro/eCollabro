// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion
namespace eCollabro.Service.DataContracts
{
    /// <summary>
    /// ResponseStatus
    /// </summary>
    public enum ResponseStatus
    {
        Success = 1,
        Exception = 2,
        BusinessException = 3
    }

    /// <summary>
    /// ErrorDC
    /// </summary>
    [DataContract]
    public class ErrorDC
    {
        [DataMember]
        public string ErrorCode { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }
    }


    /// <summary>
    /// ResponseMessage
    /// </summary>
    [DataContract]
    public class ServiceResponseMessage
    {
        [DataMember]
        public string MessageCode { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public bool Overridable { get; set; }

        [DataMember]
        public List<ErrorDC> Errors { get; set; }

        public ServiceResponseMessage()
        {
            this.Errors = new List<ErrorDC>();
        }

    }


    /// <summary>
    /// BaseServiceResponse
    /// </summary>
    [DataContract]
    public class BaseServiceResponse : IBaseServiceResponse
    {
        [DataMember]
        public ResponseStatus Status { get; set; }

        [DataMember]
        public ResponseContextParameterDC ResponseParameters { get; set; }

        [DataMember]
        public ServiceResponseMessage ResponseMessage { get; set; }

        public BaseServiceResponse()
        {
            this.Status = ResponseStatus.Success;
            this.ResponseMessage = new ServiceResponseMessage();
            this.ResponseParameters=new ResponseContextParameterDC();
        }

    }

    /// <summary>
    /// ServiceResponse
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract]
    public class ServiceResponse<T>:BaseServiceResponse
    {
        [DataMember]
        public T Result{get;set;}
    }

    /// <summary>
    /// ServiceResponse
    /// </summary>
    [DataContract]
    public class ServiceResponse : BaseServiceResponse
    {
    }
}
