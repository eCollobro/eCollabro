// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

#endregion 
namespace eCollabro.Web.Controllers
{
    /// <summary>
    /// ErrorController
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// Index - Generic Error Page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// PageNotFound
        /// </summary>
        /// <returns></returns>
        public ActionResult PageNotFound()
        {
            Response.StatusCode = 404;
            return View();
        }
    }
}