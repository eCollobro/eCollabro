using eCollabro.Client.Interface;
using eCollabro.Client.Models.Core;
using eCollabro.Common;
using eCollabro.Utilities;
using eCollabro.Web.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using eCollabro.Client.Models.Store;

namespace eCollabro.Web.Store.Controllers
{
    public class ProductController : BaseController
    {
         #region Property

        /// <summary>
        /// SecurityClientProxy
        /// </summary>
        public ISecurityClient SecurityClientProcessor { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// StoreController
        /// </summary>
        public ProductController()
        {
            this.SecurityClientProcessor = ApplicationContext.Getinstance().UnityContainer.Resolve<ISecurityClient>();
        }

        #endregion

        #region Private Method

        /// <summary>
        /// SavePermissionsToViewBag
        /// </summary>
        /// <param name="userPermissions"></param>
        private bool SavePermissionsToViewBag(FeatureEnum feature)
        {
            try
            {
                List<UserFeaturePermissionModel> userPermissions = SecurityClientProcessor.GetUserFeaturePermissions(Convert.ToInt32(feature));
                List<PermissionEnum> permissions = new List<PermissionEnum>();

                foreach (UserFeaturePermissionModel userFeaturePermission in userPermissions)
                {
                    permissions.Add((PermissionEnum)userFeaturePermission.PermissionId);
                }
                ViewBag.UserPermissions = permissions;
                if (!(permissions.Contains(PermissionEnum.ViewContent) || permissions.Contains(PermissionEnum.ViewAnomynousContent)))
                    return false;
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return true;
        }


        #endregion 

        #region Actions & Methods

        /// <summary>
        /// Index - Product Home [Users Startup View - Visitors Role]
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// ProductCategories 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ProductCategories()
        {
            if (!SavePermissionsToViewBag(FeatureEnum.Ecommerce))
                return Redirect("~/home/unauthorized");
           
            return View();
        }

        /// <summary>
        /// Manage ProductCategory - Add /Edit 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ProductCategory(int Id = 0)
        {
            if (!SavePermissionsToViewBag(FeatureEnum.Ecommerce))
                return Redirect("~/home/unauthorized");

            ProductCategoryModel ProductCategoryModel = new ProductCategoryModel();
            ProductCategoryModel.ProductCategoryId = Id;
            if (Request.IsAjaxRequest())
                return PartialView(ProductCategoryModel);
            else
                return View(ProductCategoryModel);

        }

        /// <summary>
        /// Products
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Products(int Id)
        {
            if (!SavePermissionsToViewBag(FeatureEnum.Ecommerce))
                return Redirect("~/home/unauthorized");

            SavePermissionsToViewBag(FeatureEnum.Ecommerce);
            ProductCategoryModel ProductCategoryModel = new ProductCategoryModel();
            ProductCategoryModel.ProductCategoryId = Id;
            if (Request.IsAjaxRequest())
                return PartialView(ProductCategoryModel);
            else
                return View(ProductCategoryModel);
        }

        /// <summary>
        /// Manage Product
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ManageProduct(int Id = 0, int catId = 0)
        {
            if (!SavePermissionsToViewBag(FeatureEnum.User))
                return Redirect("~/home/unauthorized");


            ProductModel ProductModel = new ProductModel();
            ProductModel.ProductId = Id;
            ProductModel.ProductCategoryId = catId;
            if (Request.IsAjaxRequest())
                return PartialView(ProductModel);
            else
                return View(ProductModel);
        }

        /// <summary>
        /// Product - Users View - Visitors
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet,AllowAnonymous]
        public ActionResult Product(int Id = 0)
        {
            ProductModel productModel = new ProductModel();
            productModel.ProductId = Id;
            if (Request.IsAjaxRequest())
                return PartialView(productModel);
            else
                return View(productModel);
        }

        #endregion
	}
}