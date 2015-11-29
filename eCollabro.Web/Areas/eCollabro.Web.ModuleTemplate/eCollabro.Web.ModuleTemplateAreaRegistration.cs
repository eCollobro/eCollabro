using System.Web.Mvc;

namespace eCollabro.Web
{
    public class BLOGAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "eCollabro.Web.ModuleTemplate";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "eCollabro.Web.ModuleTemplate_default",
                "ModuleTemplate/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }, new string[] { "eCollabro.Web.ModuleTemplate.Controllers" }
            );
        }
    }
}
