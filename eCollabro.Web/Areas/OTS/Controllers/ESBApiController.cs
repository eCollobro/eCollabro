// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.Client.Interface;
using eCollabro.Client.Models.Content;
using eCollabro.Client.Models.ESB;
using eCollabro.Resources;
using eCollabro.Utilities;
using eCollabro.Web.Base;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

#endregion

namespace eCollabro.Web.Areas.Content.Controllers
{
     
    /// <summary>
    /// ESBApiController
    /// </summary>
    [Authorize,WebApiExceptionFiler]
    public class ESBApiController : BaseApiController
    {
        #region Property

        /// <summary>
        /// ESBClientProcessor
        /// </summary>
        private IESBClient ESBClientProcessor { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// ESBApiController
        /// </summary>
        public ESBApiController()
        {
                this.ESBClientProcessor = ApplicationContext.Getinstance().UnityContainer.Resolve<IESBClient>();
        }

        #endregion

        #region ESB Api Methods

 
        /// <summary>
        /// GetESBApps
        /// </summary>
        /// <returns></returns>
        [Route("ESBApi/GetESBApps/")]
        public HttpResponseMessage GetESBApps()
        {
            List<ESBAppModel> esbApps= ESBClientProcessor.GetESBApps(0); // get apps for latest service id
            return GetListResult<List<ESBAppModel>>(esbApps,ESBClientProcessor.RequestContext,ESBClientProcessor.ResponseContext);
        }
        #endregion

    }
}