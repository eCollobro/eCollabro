using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCollabro.Web.Areas.OTS.Controllers
{
    public class ESBController : Controller
    {
        // GET: OTS/ESB
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AppScheduler(int Id)
        {
            return PartialView();
        }
    }
}