using System.Web.Mvc;

namespace eCollabro.Web.Areas.OTS
{
    public class OTSAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "OTS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "OTS_default",
                "OTS/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}