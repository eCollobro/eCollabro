using eCollabro.Client.Models.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCollabro.Web.Collaborate.Controllers
{
    [Authorize]
    public class UserTaskController : Controller
    {
        //
        // GET: /TaskList/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManageUserTask(int Id = 0)
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
