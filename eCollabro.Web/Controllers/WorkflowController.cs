// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.Client.Models.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

#endregion 
namespace eCollabro.Web.Controllers
{
    /// <summary>
    /// WorkflowController
    /// </summary>
    [Authorize]
    public class WorkflowController : Controller
    {
        //
        // GET: /TaskList/

        public ActionResult UserTasks()
        {
            return View();
        }

        public ActionResult UserTask(int Id = 0)
        {
            UserTaskModel userTaskModel = new UserTaskModel();
            userTaskModel.TaskId = Id;
            if (Request.IsAjaxRequest())
                return PartialView(userTaskModel);
            else
                return View(userTaskModel);
        }
    }
}
