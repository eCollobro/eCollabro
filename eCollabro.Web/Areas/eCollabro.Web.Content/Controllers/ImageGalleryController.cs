// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using eCollabro.Web.Base;
using Microsoft.Practices.Unity;
using eCollabro.Client.Models.Content;
using eCollabro.Utilities;
using System.Web;
using System.IO;
using eCollabro.Client.Interface;
using eCollabro.Client.Models.Core;
using eCollabro.Common;

#endregion

namespace eCollabro.Web.Content.Controllers
{
    /// <summary>
    /// ImageGalleryController
    /// </summary>
    [Authorize]
    public class ImageGalleryController : BaseController
    {
        #region Property

        /// <summary>
        /// SecurityClientProxy
        /// </summary>
        public ISecurityClient SecurityClientProcessor { get; set; }

        /// <summary>
        /// ContentClientProcessor
        /// </summary>
        public IContentClient ContentClientProcessor { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// SiteImageController
        /// </summary>
        public ImageGalleryController()
        {
            this.SecurityClientProcessor = ApplicationContext.Getinstance().UnityContainer.Resolve<ISecurityClient>();
            this.ContentClientProcessor = ApplicationContext.Getinstance().UnityContainer.Resolve<IContentClient>();

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

        #region Methods/Actions

        #region Image Galleries

        /// <summary>
        /// /ImageGalleries/
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// ImageGalleries 
        /// </summary>
        /// <returns></returns>
        public ActionResult ImageGalleries()
        {
            if (!SavePermissionsToViewBag(FeatureEnum.ImageGallery))
                return Redirect("~/home/unauthorized");
            return View();
        }

        /// <summary>
        /// Manage ImageGallery - Add /Edit 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ImageGallery(int Id = 0)
        {
            if (!SavePermissionsToViewBag(FeatureEnum.ImageGallery))
                return Redirect("~/home/unauthorized");

            ImageGalleryModel imageGalleryModel = new ImageGalleryModel();
            imageGalleryModel.ImageGalleryId = Id;
            if (Request.IsAjaxRequest())
                return PartialView(imageGalleryModel);
            else
                return View(imageGalleryModel);

        }

        #endregion

        #region Images

        /// <summary>
        /// Images
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Images(int Id)
        {
            if (!SavePermissionsToViewBag(FeatureEnum.ImageGallery))
                return Redirect("~/home/unauthorized");

            ImageGalleryModel imageGalleryModel = new ImageGalleryModel();
            imageGalleryModel.ImageGalleryId = Id;
            if (Request.IsAjaxRequest())
                return PartialView(imageGalleryModel);
            else
                return View(imageGalleryModel);
        }

        /// <summary>
        /// Manage Image
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ManageImage(int Id = 0, int LibId = 0)
        {
            if (!SavePermissionsToViewBag(FeatureEnum.ImageGallery))
                return Redirect("~/home/unauthorized");

            ImageModel imageModel = new ImageModel();
            imageModel.ImageId = Id;
            imageModel.ImageGalleryId = LibId;
            if (Request.IsAjaxRequest())
                return PartialView(imageModel);
            else
                return View(imageModel);
        }

        /// <summary>
        /// ManageImage
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ManageImage(ImageModel imageModel, HttpPostedFileBase file)
        {
            try
            {
                // Verify that the user selected a file
                if (file != null && file.ContentLength > 0)
                {
                    // extract only the fielname
                    imageModel.ImageFileName = Path.GetFileName(file.FileName);
                    // TODO: need to define destination
                    using (var reader = new BinaryReader(file.InputStream))
                    {
                        imageModel.ImageFile = reader.ReadBytes(file.ContentLength);
                    }

                }

                ContentClientProcessor.SaveImage(imageModel);
                TempData["Message"] = new ControllerMessage{ Message="Image saved successfully.", MessageType=ControllerMessageType.Info} ;
                return RedirectToAction("Images", new { Id = imageModel.ImageGalleryId });
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return View(imageModel);
        }

        /// <summary>
        /// Image
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Image(int Id)
        {
            try
            {
                ImageModel imageModel = ContentClientProcessor.GetImage(Id);
                return File(imageModel.ImageFile, System.Net.Mime.MediaTypeNames.Application.Octet, imageModel.ImageFileName);
            }
            catch (Exception ex)
            {
                HandleError(ex);
                return View();
            }
        }

        /// <summary>
        /// Image Thumb Nail
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult ImageThumbnail(int Id)
        {
            try
            {
                ImageModel imageModel = ContentClientProcessor.GetImage(Id);
                return File(imageModel.ImageThumbnail, System.Net.Mime.MediaTypeNames.Application.Octet, imageModel.ImageFileName);
            }
            catch (Exception ex)
            {
                HandleError(ex);
                return View();
            }
        }

        #endregion

        #endregion

    }
}
