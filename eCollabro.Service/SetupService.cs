// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.BAL;
using eCollabro.Service.DataContracts;
using eCollabro.Service.DataContracts.Core;
using System;
using System.Collections.Generic;
using eCollabro.BAL.Entities.Models;
using eCollabro.DataMapper;
using eCollabro.Service.ServiceContracts;

#endregion

namespace eCollabro.Service
{
    /// <summary>
    /// SetupService
    /// </summary>
    public class SetupService : BaseService, ISetupService
    {
        #region Data Members

        private SetupManager _setupManager;

        #endregion

        #region Constructor

        public SetupService()
        {
            _setupManager = new SetupManager();
        }

        #endregion

        #region Methods

        #region eCollabro Setup

        /// <summary>
        /// CheckECollabroSetup
        /// </summary>
        /// <param name="serviceRequest"></param>
        /// <returns></returns>
        public ServiceResponse<bool> CheckEcollabroSetup()
        {
            ServiceResponse<bool> checkECollabroSetupResponse = new ServiceResponse<bool>();
            try
            {
                checkECollabroSetupResponse.Result = _setupManager.IsEcollabroSetupReady();
            }
            catch (Exception ex)
            {
                HandleError(ex, checkECollabroSetupResponse);
            }

            return checkECollabroSetupResponse;

        }

        /// <summary>
        /// eCollabroSetup
        /// </summary>
        /// <param name="serviceRequest"></param>
        /// <returns></returns>
        public ServiceResponse eCollabroSetup(RegisterDC register)
        {

            ServiceResponse eCollabroSetupResponse = new ServiceResponse();
            try
            {
                UserMembership userMembership = new UserMembership();
                userMembership.UserName = register.UserName;
                userMembership.Password = register.Password;
                userMembership.Email = register.Email;
                _setupManager.SetupEcollabro(userMembership);
            }
            catch (Exception ex)
            {
                HandleError(ex, eCollabroSetupResponse);
            }
            return eCollabroSetupResponse;

        }

        #endregion

        #region Email Configuration

        /// <summary>
        /// GetEmailConfiguration
        /// </summary>
        /// <param name="emailConfigurationServiceRequest"></param>
        /// <returns></returns>
        public ServiceResponse<EmailConfigurationDC> GetEmailConfiguration()
        {
            ServiceResponse<EmailConfigurationDC> emailConfigurationServiceResponse = new ServiceResponse<EmailConfigurationDC>();
            try
            {
                SetContext();
                EmailConfiguration emailConfiguration = _setupManager.GetEmailConfiguration();
                if (emailConfiguration != null)
                    emailConfigurationServiceResponse.Result = Mapper.Map<EmailConfiguration, EmailConfigurationDC>(emailConfiguration);
                else
                    emailConfigurationServiceResponse.Result = new EmailConfigurationDC();
            }
            catch (Exception ex)
            {
                HandleError(ex, emailConfigurationServiceResponse);
            }
            return emailConfigurationServiceResponse;
        }

        /// <summary>
        /// SaveEmailConfiguration
        /// </summary>
        /// <param name="emailConfiguration"></param>
        /// <returns></returns>
        public ServiceResponse SaveEmailConfiguration(EmailConfigurationDC emailConfiguration)
        {
            ServiceResponse emailConfigurationServiceResponse = new ServiceResponse();
            try
            {
                SetContext();
                EmailConfiguration emailConfigurationModel = Mapper.Map<EmailConfigurationDC, EmailConfiguration>(emailConfiguration);
                _setupManager.SaveEmailConfiguration(emailConfigurationModel);
            }
            catch (Exception ex)
            {
                HandleError(ex, emailConfigurationServiceResponse);
            }
            return emailConfigurationServiceResponse;
        }

        #endregion

        #region Site Collection Admins

        /// <summary>
        /// Get Site Collection Admins
        /// </summary>
        /// <param name="featureId"></param>
        /// <returns></returns>
        public ServiceResponse<List<SiteCollectionAdminDC>> GetSiteCollectionAdmins()
        {
            ServiceResponse<List<SiteCollectionAdminDC>> siteCollectionAdminResponse = new ServiceResponse<List<SiteCollectionAdminDC>>();
            try
            {
                SetContext();
                siteCollectionAdminResponse.Result = new List<SiteCollectionAdminDC>();
                List<SiteCollectionAdmin> siteCollectionAdmins = _setupManager.GetSiteCollectionAdmins();
                foreach (SiteCollectionAdmin siteCollectionAdmin in siteCollectionAdmins)
                {
                    siteCollectionAdminResponse.Result.Add(new SiteCollectionAdminDC { UserId = siteCollectionAdmin.UserId, Email = siteCollectionAdmin.UserMembership.Email, UserName = siteCollectionAdmin.UserMembership.UserName, SiteCollectionAdminid = siteCollectionAdmin.SiteCollectionAdminId });
                }
            }
            catch (Exception ex)
            {
                HandleError(ex, siteCollectionAdminResponse);
            }
            return siteCollectionAdminResponse;
        }

        /// <summary>
        /// SaveSiteCollectionAdmin
        /// </summary>
        /// <returns></returns>
        public ServiceResponse SaveSiteCollectionAdmin(string username)
        {
            ServiceResponse siteCollectionAdminResponse = new ServiceResponse();
            try
            {
                SetContext();
                _setupManager.SaveSiteCollectionAdmin(username);
            }
            catch (Exception ex)
            {
                HandleError(ex, siteCollectionAdminResponse);
            }
            return siteCollectionAdminResponse;
        }

        /// <summary>
        /// DeleteSiteCollectionAdmin
        /// </summary>
        /// <returns></returns>
        public ServiceResponse DeleteSiteCollectionAdmin(int userId)
        {
            ServiceResponse siteCollectionAdminResponse = new ServiceResponse();
            try
            {
                SetContext();
                _setupManager.DeleteSiteCollectionAdmin(userId);
            }
            catch (Exception ex)
            {
                HandleError(ex, siteCollectionAdminResponse);
            }
            return siteCollectionAdminResponse;
        }

        #endregion

        #endregion
    }
}
