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
    /// IBaseServiceRequest
    /// </summary>
    public interface IBaseServiceRequest
    {

        [DataMember]
        UserContextDC UserContext { get; set; }

        [DataMember]
        RequestContextParameterDC RequestContextParameters { get; set; }
    }
}
