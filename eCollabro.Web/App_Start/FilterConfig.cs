#region References
using eCollabro.Exceptions;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
#endregion 

namespace eCollabro.Web
{
    /// <summary>
    /// FilterConfig
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// RegisterGlobalFilters
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }

    #region Commented Code

    //public class ControllerHandleErrorAttribute : HandleErrorAttribute
    //{
    //    public override void OnException(ExceptionContext filterContext)
    //    {
            
    //        // if the request is AJAX return JSON else view.
    //        if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
    //        {
    //                filterContext.Result = new JsonResult()
    //                {
    //                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
    //                    Data = filterContext.Exception
    //                };
    //                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    //        }
    //        else
    //        {
    //            base.OnException(filterContext);
    //        }
    //        filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
    //     }
    //}
    #endregion

}
