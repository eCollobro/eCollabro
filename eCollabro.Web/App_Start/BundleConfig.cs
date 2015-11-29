#region References
using System.Web;
using System.Web.Optimization;
#endregion

namespace eCollabro.Web
{
    /// <summary>
    /// BundleConfig
    /// </summary>
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            #region Core JS

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.1.0.min.js", "~/Scripts/jquery.unobtrusive-ajax.min.js", "~/Scripts/common.js",
                        "~/Scripts/jquery.dataTables.min.js", "~/Scripts/jquery.smartmenus.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                                "~/Scripts/jquery.validate.min.js", "~/Scripts/jquery.validate.unobtrusive.min.js"
                                  ));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                                "~/Scripts/angular.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js", "~/Scripts/bootbox.min.js", "~/Scripts/bootstrap-datepicker.js", "~/Scripts/dataTables.bootstrap.js"));

            #endregion

            #region CSS

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css", "~/Content/datepicker.css", "~/Content/dataTables.bootstrap.css",
                      "~/Content/sm-core-css.css", "~/Content/sm-blue.css"
                      ));

            #endregion

            #region Core eCollabro JS

            bundles.Add(new ScriptBundle("~/bundles/ecollabro").Include(
                         "~/app/directives/angularConfig.js",
                         "~/app/services/serviceHandler.js",
                          "~/app/services/securityService.js",
                          "~/app/services/setupService.js",
                          "~/app/controllers/main/layoutHeader.js"
                         ));

            bundles.Add(new ScriptBundle("~/bundles/dashboard").Include(
                "~/app/controllers/dashboard/dashboard.js",
                "~/app/controllers/shared/changePassword.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/user").Include(
                  "~/app/controllers/shared/changePassword.js",
                  "~/app/controllers/user/user.js",
                  "~/app/controllers/user/users.js"
                  ));

            bundles.Add(new ScriptBundle("~/bundles/role").Include(
                  "~/app/controllers/role/roles.js",
                  "~/app/controllers/role/role.js",
                  "~/app/controllers/role/roleFeatures.js"
                  ));

            bundles.Add(new ScriptBundle("~/bundles/navigation").Include(
                 "~/app/controllers/navigation/navigations.js",
                 "~/app/controllers/navigation/navigation.js"
                 ));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                "~/app/controllers/site/site.js",
                "~/app/controllers/site/sites.js",
                "~/app/controllers/site/siteFeatures.js"
                ));

            #endregion 

            #region Content eCollabro JS
 
            bundles.Add(new ScriptBundle("~/bundles/ecollabro-content").Include(
                         "~/Areas/Content/app/services/blogService.js",
                          "~/Areas/Content/app/services/contentCommentService.js",
                          "~/Areas/Content/app/services/contentPageService.js",
                          "~/Areas/Content/app/services/documentLibraryService.js",
                          "~/Areas/Content/app/services/imageGalleryService.js",
                          "~/Areas/Content/app/services/announcementService.js"
                         ));


            bundles.Add(new ScriptBundle("~/bundles/blog").Include(
                "~/Areas/Content/app/controllers/blog/blogCategory.js",
                "~/Areas/Content/app/controllers/blog/blogCategories.js",
                "~/Areas/Content/app/controllers/blog/manageBlog.js",
                "~/Areas/Content/app/controllers/blog/blogs.js",
                "~/Areas/Content/app/controllers/blog/blogHome.js",
                "~/Areas/Content/app/controllers/blog/blog.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/contentpage").Include(
                "~/Areas/Content/app/controllers/contentPage/contentPageCategories.js",
                "~/Areas/Content/app/controllers/contentPage/contentPageCategory.js",
                "~/Areas/Content/app/controllers/contentPage/contentPages.js",
                "~/Areas/Content/app/controllers/contentPage/contentPage.js",
                "~/Areas/Content/app/controllers/contentPage/viewContentPage.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/documentlibrary").Include(
                "~/Areas/Content/app/controllers/documentLibrary/documentLibraries.js",
                "~/Areas/Content/app/controllers/documentLibrary/documentLibrary.js",
                "~/Areas/Content/app/controllers/documentLibrary/documents.js",
                "~/Areas/Content/app/controllers/documentLibrary/manageDocument.js",
                "~/Areas/Content/app/controllers/documentLibrary/documentLibraryHome.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/imagegallery").Include(
                "~/Areas/Content/app/controllers/imageGallery/imageGalleries.js",
                "~/Areas/Content/app/controllers/imageGallery/imageGallery.js",
                "~/Areas/Content/app/controllers/imageGallery/images.js",
                "~/Areas/Content/app/controllers/imageGallery/manageImage.js",
                "~/Areas/Content/app/controllers/imageGallery/imageGalleryHome.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/announcement").Include(
               "~/Areas/Content/app/controllers/announcement/announcements.js",
               "~/Areas/Content/app/controllers/announcement/announcement.js",
               "~/Areas/Content/app/controllers/announcement/manageAnnouncement.js",
               "~/Areas/Content/app/controllers/announcement/announcementHome.js"
               ));

            #endregion 

            #region ESB eCollabro JS

            bundles.Add(new ScriptBundle("~/bundles/esb").Include(
                         "~/Areas/OTS/app/services/esbService.js",
                          "~/Areas/OTS/app/controllers/dashboardWidget.js",
                          "~/Areas/OTS/app/controllers/appScheduler.js"
                         ));

            #endregion 
        }
    }
}
