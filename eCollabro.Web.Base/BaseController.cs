// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;
using System.Web.Mvc;
using System.Threading;
using System.Text;
using eCollabro.Exceptions;
using eCollabro.Client.Models.Core;

#endregion

namespace eCollabro.Web.Base
{
    /// <summary>
    /// ControllerMessage
    /// </summary>
    public class ControllerMessage
    {
        public string Message { get; set; }

        public ControllerMessageType MessageType { get; set; }

        public Exception ExceptionObject { get; set; }
    }
    
    
    /// <summary>
    /// ControllerMessageType
    /// </summary>
    public enum ControllerMessageType
    {
        Info = 1,
        BusinessException = 2,
        CriticalException = 3,
    }

    /// <summary>
    /// BaseController
    /// </summary>
    public class BaseController : Controller
    {
        #region Data Member

        private UserContextModel _userContextModel = null;

        #endregion

        #region Property
        
        /// <summary>
        /// UserContext
        /// </summary>
        public UserContextModel UserContext
        {
            get
            {
                    int siteId = 0;
                    if(System.Configuration.ConfigurationManager.AppSettings["SiteId"]!=null)
                        siteId=Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SiteId"]);
                     if(Session["SiteId"]!=null) // MVC Controller context 
                         siteId=Convert.ToInt32(siteId);
                    _userContextModel = new UserContextModel();
                    _userContextModel.Language = Thread.CurrentThread.CurrentUICulture.Name;
                    _userContextModel.SiteId =siteId ;
                    _userContextModel.DomainName = Request.Url.Host.ToLower();
                    _userContextModel.UserName = Thread.CurrentPrincipal.Identity.Name;
                    return _userContextModel;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// HandleError
        /// </summary>
        /// <param name="ex"></param>
        protected void HandleError(Exception ex)
        {
            if (ex.GetType() == typeof(BusinessException))
                ViewBag.Message = new ControllerMessage { Message = ex.Message.Replace("'", "\""), MessageType = ControllerMessageType.BusinessException };
            else
            {
                ViewBag.Message = new ControllerMessage { Message = ex.Message.Replace("'", "\""), MessageType = ControllerMessageType.CriticalException, ExceptionObject=ex };
            }
        }

        
        /// <summary>
        /// ModelErrorSummary
        /// </summary>
        protected string ModelErrorSummary{
            get
            {
                StringBuilder error = new StringBuilder();
                foreach (ModelState modelState in ModelState.Values)
                {
                    foreach (ModelError modelError in modelState.Errors)
                    {
                        error.Append(modelError.ErrorMessage + Environment.NewLine);
                    }
                }
                return error.ToString();
            }
        }

        protected void ShowMessage(string message)
        {
            ViewBag.Message = new ControllerMessage { Message = message, MessageType = ControllerMessageType.Info };
        }

        

        #endregion
    }
}
