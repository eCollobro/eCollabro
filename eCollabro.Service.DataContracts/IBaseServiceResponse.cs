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
    /// IBaseServiceResponse
    /// </summary>
    public interface IBaseServiceResponse
    {
        [DataMember]
        ResponseStatus Status { get; set; }

        [DataMember]
        ServiceResponseMessage ResponseMessage { get; set; }
        
    }
}
