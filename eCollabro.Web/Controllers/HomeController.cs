// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using Microsoft.Practices.Unity;
using System;
using System.Web.Mvc;
using eCollabro.Utilities;
using eCollabro.Web.Base;
using eCollabro.Client.Models.Content;
using eCollabro.Client.Interface;

#endregion 
namespace eCollabro.Web.Controllers
{
    /// <summary>
    /// HomeController
    /// </summary>
    public class HomeController : BaseController
    {
        #region Property

        /// <summary>
        /// ContentClientProxy
        /// </summary>
        private IContentClient ContentClientProcessor { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// HomeController
        /// </summary>
        public HomeController()
        {
            this.ContentClientProcessor = ApplicationContext.Getinstance().UnityContainer.Resolve<IContentClient>();
        }

        #endregion

        public ActionResult Index()
        {
            ContentPageModel contentPage = null;
            try
            {
                if (!ApplicationContext.Getinstance().eCollabroSetupReady) // check for first time
                {
                    ISetupClient setupClient = ApplicationContext.Getinstance().UnityContainer.Resolve<ISetupClient>();

                    if (!setupClient.CheckEcollabroSetup())
                    {
                        TempData["NeedSetup"] = true;
                        return Redirect("/Setup");
                    }
                    else
                    {
                        ApplicationContext.Getinstance().eCollabroSetupReady = true;
                    }
                }

                contentPage = ContentClientProcessor.GetHomePage();

            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return View(contentPage);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Unauthorized()
        {
            return View();
        }

        public ActionResult TestPage()
        {
            return View();
        }
    }
}