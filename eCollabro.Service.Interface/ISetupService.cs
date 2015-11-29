// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System.ServiceModel;
using eCollabro.Service.DataContracts;
using System.Collections.Generic;
using eCollabro.Service.DataContracts.Core;

#endregion
namespace eCollabro.Service.Interface
{
    /// <summary>
    /// ISetupService
    /// </summary>
    [ServiceContract]
    public interface ISetupService
    {
        /// <summary>
        /// CheckECollabroSetup
        /// </summary>
        /// <param name="serviceRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<bool> CheckEcollabroSetup();
       
        /// <summary>
        /// eCollabroSetup
        /// </summary>
        /// <param name="serviceRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse eCollabroSetup(RegisterDC register);

        /// <summary>
        /// GetEmailConfiguration
        /// </summary>
        /// <param name="emailConfigurationServiceRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<EmailConfigurationDC> GetEmailConfiguration();

        /// <summary>
        /// SaveEmailConfiguration
        /// </summary>
        /// <param name="emailConfiguration"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse SaveEmailConfiguration(EmailConfigurationDC emailConfiguration);

        #region Site Collection Admins

        /// <summary>
        /// Get Site Collection Admins
        /// </summary>
        /// <param name="siteCollectionAdminRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<List<SiteCollectionAdminDC>> GetSiteCollectionAdmins();

        /// <summary>
        /// SaveSiteCollectionAdmin
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse SaveSiteCollectionAdmin(string userName);

        /// <summary>
        /// DeleteSiteCollectionAdmin
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse DeleteSiteCollectionAdmin(int siteCollectionAdminId);

        #endregion
    
    }

}
