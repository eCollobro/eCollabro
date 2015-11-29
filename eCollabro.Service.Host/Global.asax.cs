using eCollabro.Utilities;
using eCollabro.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Microsoft.Practices.Unity;
using eCollabro.DAL.Interface;
namespace eCollabro.Service.Host
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            ApplicationContext.Getinstance().UnityContainer.RegisterType(typeof(ISecurityRepository), typeof(SecurityRepository));
            ApplicationContext.Getinstance().UnityContainer.RegisterType(typeof(IContentRepository), typeof(ContentRepository));
            ApplicationContext.Getinstance().UnityContainer.RegisterType(typeof(IUnitOfWork), typeof(UnitOfWork));
            log4net.Config.XmlConfigurator.Configure();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}