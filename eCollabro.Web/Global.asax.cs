#region References
using eCollabro.Client;
using eCollabro.Utilities;
using System;
using System.Threading;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using System.Globalization;
using eCollabro.Client.Interface;
#endregion 

namespace eCollabro.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Error(Object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex is ThreadAbortException || ex is HttpAntiForgeryException)
                Response.Redirect("/Account/Login");
            else
            {
                //ToDo
                // Logger.Error(LoggerType.Global, ex, "Exception");
                // Response.Redirect("unexpectederror.htm");
            }

        }

        protected void Application_Start()
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
           
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            GlobalConfiguration.Configuration.EnsureInitialized();
            ApplicationContext.Getinstance().UnityContainer.RegisterType(typeof(ISecurityClient), typeof(SecurityClient));
            ApplicationContext.Getinstance().UnityContainer.RegisterType(typeof(IContentClient), typeof(ContentClient));
            ApplicationContext.Getinstance().UnityContainer.RegisterType(typeof(IWorkflowClient), typeof(WorkflowClient));
            ApplicationContext.Getinstance().UnityContainer.RegisterType(typeof(ILookupClient), typeof(LookupClient));
            ApplicationContext.Getinstance().UnityContainer.RegisterType(typeof(ISetupClient), typeof(SetupClient));
            ApplicationContext.Getinstance().UnityContainer.RegisterType(typeof(IESBClient), typeof(ESBClient));
            //ControllerBuilder.Current.SetControllerFactory(typeof(UnityControllerFactory));
        }

        public void Application_BeginRequest()
        {
            string myCookie = "en";
            if (Context.Request.Cookies["myLang"] != null)
            {
                myCookie = Context.Request.Cookies["myLang"].Value;
            }

            if (!string.IsNullOrEmpty(myCookie))
            {
                CultureInfo cl = CultureInfo.CreateSpecificCulture(myCookie);
                Thread.CurrentThread.CurrentCulture = cl;
                Thread.CurrentThread.CurrentUICulture = cl;
            }

        }
        
    }
}
