using eCollabro.Client.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCollabro.Web.Store.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        //
        // GET: /Order/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult  OrderCart()
        {
            return View();
        }
	}
}