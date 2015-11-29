// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.Client.Interface;
using eCollabro.Client.Models.Core;
using eCollabro.Resources;
using eCollabro.Utilities;
using eCollabro.Web.Base;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

#endregion 

namespace eCollabro.Web.Controllers
{
    /// <summary>
    /// SetupApiController
    /// </summary>
    [Authorize, WebApiExceptionFiler]
    public class SetupApiController : ApiController
    {
        #region Property

        /// <summary>
        /// ContentClientProcessor
        /// </summary>
        public ISetupClient SetupClientProcessor { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// SetupApiController
        /// </summary>
        public SetupApiController()
        {
            this.SetupClientProcessor = ApplicationContext.Getinstance().UnityContainer.Resolve<ISetupClient>();
        }

        #endregion

        #region Methods 

        #region Email Configuration

        /// <summary>
        /// GetEmailConfiguration
        /// </summary>
        /// <returns></returns>
        [Route("SetupApi/GetEmailConfiguration")]
        public HttpResponseMessage GetEmailConfiguration() 
        {
            EmailConfigurationModel emailConfigurationModel = SetupClientProcessor.GetEmailConfiguration();
            return Request.CreateResponse(HttpStatusCode.OK, emailConfigurationModel);
        }

        /// <summary>
        /// SaveEmailConfiguration
        /// </summary>
        /// <param name="emailConfiguration"></param>
        /// <returns></returns>
        [Route("SetupApi/SaveEmailConfiguration")]
        public HttpResponseMessage SaveEmailConfiguration(EmailConfigurationModel emailConfiguration)
        {
            SetupClientProcessor.SaveEmailConfiguration(emailConfiguration);
            return Request.CreateResponse(HttpStatusCode.OK, CoreMessages.SavedSuccessfully);
        }

        #endregion

        #region Site Collection Admin

        /// <summary>
        /// GetSiteCollectionAdmins
        /// </summary>
        /// <returns></returns>
        [Route("SetupApi/GetSiteCollectionAdmins")]
        public HttpResponseMessage GetSiteCollectionAdmins()
        {
            List<SiteCollectionAdminModel> siteCollectionAdmins = SetupClientProcessor.GetSiteCollectionAdmins();
            return Request.CreateResponse(HttpStatusCode.OK, siteCollectionAdmins);
            
        }

        /// <summary>
        /// SaveSiteCollectionAdmin
        /// </summary>
        /// <param name="siteCollectionAdmin"></param>
        /// <returns></returns>
        [Route("SetupApi/SaveSiteCollectionAdmin"),HttpPost]
        public HttpResponseMessage SaveSiteCollectionAdmin(SiteCollectionAdminModel siteCollectionAdmin)
        {
            SetupClientProcessor.SaveSiteCollectionAdmin(siteCollectionAdmin);
            return Request.CreateResponse(HttpStatusCode.OK,CoreMessages.SavedSuccessfully);
        }

        /// <summary>
        /// DeleteSiteCollectionAdmin
        /// </summary>
        /// <param name="siteCollectionAdminId"></param>
        /// <returns></returns>
        [Route("SetupApi/DeleteSiteCollectionAdmin/{userId}"), HttpGet]
        public HttpResponseMessage DeleteSiteCollectionAdmin(int userId)
        {
            SetupClientProcessor.DeleteSiteCollectionAdmin(userId);
            return Request.CreateResponse(HttpStatusCode.OK, CoreMessages.DeletedSuccessfully);
        }

        #endregion 

        #endregion

    }
}