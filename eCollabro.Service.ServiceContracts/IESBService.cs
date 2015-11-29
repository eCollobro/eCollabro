﻿// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using eCollabro.Service.DataContracts;
using eCollabro.Service.DataContracts.ADP;
using eCollabro.Service.DataContracts.Core;
using System.Collections.Generic;
using System.ServiceModel;

#endregion
namespace eCollabro.Service.ServiceContracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IESBService
    {
        
         /// <summary>
        /// GetESBApps
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<List<ESBAppDC>> GetESBApps(int serviceId);


   }

}