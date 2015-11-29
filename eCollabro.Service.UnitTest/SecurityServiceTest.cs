using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eCollabro.Service.DataContracts;
using eCollabro.Service.DataContracts.Core;
using eCollabro.Utilities;
using eCollabro.DAL;
using Microsoft.Practices.Unity;
using eCollabro.Service.UnitTest;
using eCollabro.Service.DataContracts.Content;
using eCollabro.BAL.Entities.Models;
using System.Collections.Generic;
using eCollabro.DAL.Interface;
using System.Linq;
using eCollabro.Common;
using eCollabro.BAL;

namespace eCollabro.Service.UnitTest
{
    [TestClass]
    public class SecurityServiceTest
    {

        public SecurityServiceTest()
        {
            // with mock

            ApplicationContext.Getinstance().UnityContainer.RegisterType(typeof(ISecurityRepository), typeof(MockSecurityRepository));
            ApplicationContext.Getinstance().UnityContainer.RegisterType(typeof(IContentRepository), typeof(ContentRepository));
            ApplicationContext.Getinstance().UnityContainer.RegisterType(typeof(IUnitOfWork), typeof(UnitOfWork));


            //// actual test
            //UnityFactory.Getinstance().RegisterType(typeof(ICommonRepository), typeof(CommonRepository));
            //UnityFactory.Getinstance().RegisterType(typeof(ISecurityRepository), typeof(SecurityRepository));
            //UnityFactory.Getinstance().RegisterType(typeof(IContentRepository), typeof(ContentRepository));
        }

        [TestMethod]
        public void GetRolesTest()
        {
            SecurityService securityService = new SecurityService();
            ServiceResponse<List<RoleDC>> site = securityService.GetRoles();
            Assert.AreEqual(site.Result.Count > 0, true);
        }

        [TestMethod]
        public void GetSiteTest()
        {
            SecurityService securityService = new SecurityService();
            ServiceResponse<SiteDC> site = securityService.GetSite(1);
            Assert.AreEqual(site.Result.SiteCode, "S001");
            Assert.AreEqual(site.Result.SiteId, 1);
        }


        [TestMethod]
        public void GetSiteExceptionTest()
        {
            SecurityService securityService = new SecurityService();
            ServiceResponse<SiteDC> site = securityService.GetSite(1);
            Assert.AreNotEqual(site.Status, ResponseStatus.BusinessException);
        }

        [TestMethod]
        public void GetUserTasks()
        {
            RequestContext.Current.Add<UserContext>("UserContext", new UserContext { UserId = 1, LanguageId = 1, UserName = "admin", SiteId = 1 });
            WorkflowManager workflowManager = new WorkflowManager();
            List<UserTask> tasks = workflowManager.GetUserTasks(ContextEnum.None, null, null, null, true);

        }

        [TestMethod]
        public void ChangePassword()
        {
            RequestContext.Current.Add<UserContext>("UserContext", new UserContext { UserId = 1, LanguageId = 1, UserName = "admin", SiteId = 1 });
            SecurityManager securityManager = new SecurityManager();
            string password = DataEncryption.Encrypt("Password1");
            // securityManager.CreateAccount(new UserMembership { UserName = "anand", Password = password });
            securityManager.ChangePassword("admin", "oldPassword", password);
        }

        [TestMethod]
        public void ExecuteScriptsTest()
        {
            RequestContext.Current.Add<UserContext>("UserContext", new UserContext { UserId = 1, LanguageId = 1, UserName = "admin", SiteId = 1 });
            SetupManager setupManager = new SetupManager();
            setupManager.ExecuteScripts();
        }
    }
}
