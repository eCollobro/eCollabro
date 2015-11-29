// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System.Web.Mvc;
using Microsoft.Practices.Unity;
using System.Web.Security;
using eCollabro.Exceptions;
using System;
using eCollabro.Web.Base;
using eCollabro.Client.Models.Core;
using eCollabro.Utilities;
using System.Web;
using eCollabro.Client.Interface;
using eCollabro.Common;

#endregion

namespace eCollabro.Web.Controllers
{
    /// <summary>
    /// AccountController
    /// </summary>
    [Authorize]
    public class AccountController : BaseController
    {
        #region Property

        /// <summary>
        /// SecurityClientProxy
        /// </summary>
        private ISecurityClient SecurityClientProcessor { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// SecurityClientProcessor
        /// </summary>
        public AccountController()
        {
            this.SecurityClientProcessor = ApplicationContext.Getinstance().UnityContainer.Resolve<ISecurityClient>();
        }

        #endregion

  
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            LoginModel login = new LoginModel();
            try
            {
                if (Request.Cookies["RememberMe"] != null)
                {
                    login.UserName = Request.Cookies["Username"] == null ? string.Empty : Request.Cookies["Username"].Value;
                    login.Password = Request.Cookies["Password"]==null?string.Empty: DataEncryption.Decrypt(Request.Cookies["Password"].Value);
                    login.RememberMe = true;
                }
                if (Request.QueryString["ReturnUrl"] != null)
                    base.ShowMessage("Session expired! please login");
                ViewBag.AllowRegistration = SecurityClientProcessor.GetSiteConfiguration().AllowRegistration;
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            ViewBag.ReturnUrl = returnUrl;
            return View(login);
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            try
            {
                if (model.RememberMe)
                {
                    Response.Cookies.Set(new HttpCookie("Password", DataEncryption.Encrypt(model.Password)));
                    Response.Cookies.Set(new HttpCookie("Username", model.UserName));
                    Response.Cookies.Set(new HttpCookie("RememberMe", "1"));
                }
                else
                {
                    if (Request.Cookies["RememberMe"] != null)
                        Response.Cookies["RememberMe"].Expires=DateTime.Now.AddDays(-1d);
                    if (Request.Cookies["Username"] != null)
                        Response.Cookies["Username"].Expires=DateTime.Now.AddDays(-1d);
                    if (Request.Cookies["Password"] != null)
                        Response.Cookies["Password"].Expires=DateTime.Now.AddDays(-1d);
                }
                SecurityClientProcessor.AuthenticateUser(model.UserName, model.Password);
                Session["SiteId"] = UserContext.SiteId;// will pick from config file for first time
                FormsAuthentication.RedirectFromLoginPage(model.UserName, false);
                if (Request.QueryString["ReturnUrl"] != null)
                    return Redirect(Request.QueryString["ReturnUrl"]);
                else
                    return RedirectToAction("Dashboard", "Home");
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return View(model);
        }

        /// <summary>
        /// LogOff
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return Redirect("/");
        }


        /// <summary>
        /// GET: /Account/Register
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View(new RegisterModel());
        }


        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel registerModel)// RegisterModel FormCollection frm
        {
            try
            {
                SecurityClientProcessor.CreateAccount(registerModel);
                SecurityClientProcessor.AuthenticateUser(registerModel.UserName, registerModel.Password);
                Session["SiteId"] = UserContext.SiteId;// will pick from config file for first time
                FormsAuthentication.RedirectFromLoginPage(registerModel.UserName, false);
                return RedirectToAction("Dashboard", "Home");
            }
            catch (BusinessException ex)
            {
                if (ex.Code.Equals(CoreValidationMessagesConstants.AccountNotConfirmed) || ex.Code.Equals(CoreValidationMessagesConstants.AccountNotApproved))
                {
                    base.ShowMessage("Thanks for registering. " + ex.Message); // not show as error
                    ViewBag.HideForm = true;
                }
                else
                    HandleError(ex);
            }
            return View();
        }


        /// <summary>
        /// ResetPassword
        /// </summary>
        /// <param name="uname"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ResetPassword(string uname, string token)
        {
            try
            {
                string password = SecurityClientProcessor.ResetPassword(uname, token);
                ControllerMessage controllerMessage = new ControllerMessage { MessageType=ControllerMessageType.Info, Message="Your will receive a new password in your email shortly." };
                ViewBag.Message = controllerMessage;
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return View();
        }

        /// <summary>
        /// VerifyAccount
        /// </summary>
        /// <param name="uname"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult VerifyAccount(string uname, string token)
        {
            try
            {
                SecurityClientProcessor.VerifyAccount(uname, token);
                ViewBag.SuccessMessage = "Thanks for verifying your account. Please login to proceed.";
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return View();
        }
        /// <summary>
        /// ChangePassword
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            return View();
        }

        #region Helpers


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion
    }
}