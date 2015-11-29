using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eCollabro.Utilities;
using eCollabro.DAL;
using eCollabro.Service;
using eCollabro.Service.DataContracts;
using eCollabro.Service.DataContracts.Content;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using eCollabro.BAL.Entities;
using System.Data.Entity;
using System.Linq;
using eCollabro.DAL.Interface;

namespace eCollabro.Service.UnitTest
{
    [TestClass]
    public class ContentServiceTest
    {
        public ContentServiceTest()
        {
            // with mock
            /*
            UnityFactory.Getinstance().RegisterType(typeof(ICommonRepository), typeof(CommonRepository));
            UnityFactory.Getinstance().RegisterType(typeof(ISecurityRepository), typeof(MockSecurityRepository));
            UnityFactory.Getinstance().RegisterType(typeof(IContentRepository), typeof(ContentRepository));
            */

            // actual test
            ApplicationContext.Getinstance().UnityContainer.RegisterType(typeof(ISecurityRepository), typeof(SecurityRepository));
            ApplicationContext.Getinstance().UnityContainer.RegisterType(typeof(IContentRepository), typeof(ContentRepository));
        }

        [TestMethod]
        public void SaveBlogAddNewTest()
        {
            ContentService contentService = new ContentService();

            BlogDC serviceRequest=new BlogDC();
            serviceRequest.BlogCategoryId=1;
            serviceRequest.BlogTitle="Blog Test for Approval";
            serviceRequest.BlogDescription="Blog Test for Approval Description";
            serviceRequest.BlogContent="Test Content";
            contentService.SaveBlog(serviceRequest);
        }

        [TestMethod]
        public void SaveBlogUpdateTest()
        {
            ContentService contentService = new ContentService();

            BlogDC serviceRequest = new BlogDC();

            serviceRequest.BlogId = 8;
            serviceRequest.BlogCategoryId = 1;
            serviceRequest.BlogTitle = "Blog Test for Approval-Updated";
            serviceRequest.BlogDescription = "Blog Test for Approval Description-Updated";
            serviceRequest.BlogContent = "Test Content-Updated";
            contentService.SaveBlog(serviceRequest);
        }


        [TestMethod]
        public void GetBlogTest()
        {
            ContentService contentService = new ContentService();
            ServiceResponse<BlogDC> blog= contentService.GetBlog(8);
        }

        [TestMethod]
        public void GetRecentBlogsTest()
        {
            ContentService contentService = new ContentService();
            ServiceResponse<List<BlogDC>> blogs = contentService.GetRecentBlogs();
        }

        [TestMethod]
        public void GetApprovedBlogsTest()
        {
            ContentService contentService = new ContentService();
            ServiceResponse<BlogCategoryDC> blogs = contentService.GetBlogs(1);
        }

        [TestMethod]
        public void GetBlogCategoriesTest()
        {
            ContentService contentService = new ContentService();
            ServiceResponse<List<BlogCategoryDC>> blogCategories = contentService.GetBlogCategories();
        }


        [TestMethod]
        public void ContextTest()
        {
            //MockContext mc = new MockContext();
            //List<eCollabro.BAL.Entities.Models.Role> roles= mc.Roles.ToList();
        }
        
    }
}
