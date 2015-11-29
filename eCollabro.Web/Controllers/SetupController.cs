// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.Client.Interface;
using eCollabro.Client.Models.Core;
using eCollabro.Utilities;
using eCollabro.Web.Base;
using Microsoft.Practices.Unity;
using System;
using System.Web.Mvc;

#endregion

namespace eCollabro.Web.Controllers
{
    /// <summary>
    /// SetupController
    /// </summary>
    public class SetupController : BaseController
    {
        #region Property

        /// <summary>
        /// SecurityClientProxy
        /// </summary>
        [Dependency]
        public ISetupClient SetupClientProcessor { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// SetupController
        /// </summary>
        public SetupController()
        {
            this.SetupClientProcessor = ApplicationContext.Getinstance().UnityContainer.Resolve<ISetupClient>();
        }

        #endregion

        //
        // GET: /Setup/
        public ActionResult Index()
        {
            if (TempData["NeedSetup"] == null) //called from home controller and already checked
            {
                if (SetupClientProcessor.CheckEcollabroSetup())
                    return Redirect("/?setup=eCollabroReady");
            }
            return View();
        }

        /// <summary>
        /// Index
        /// </summary>
        /// <param name="siteCollectionAdmin"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(RegisterModel siteCollectionAdmin)
        {
            try
            {
                if (SetupClientProcessor.CheckEcollabroSetup())
                    return Redirect("/?setup=eCollabroReady");
                else
                {
                    SetupClientProcessor.eCollabroSetup(siteCollectionAdmin);
                    return Redirect("/?setup=eCollabroReady");
                }

            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return View();
        }

        /// <summary>
        /// EmailConfiguration
        /// </summary>
        /// <returns></returns>
        public ActionResult EmailConfiguration()
        {
            return View();
        }

        /// <summary>
        /// SiteCollectionAdmin
        /// </summary>
        /// <returns></returns>
        public ActionResult SiteCollectionAdmin()
        {
            return View();
        }
    }
}