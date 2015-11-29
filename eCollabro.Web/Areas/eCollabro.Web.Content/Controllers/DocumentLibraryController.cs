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
    /// DocumentLibraryController
    /// </summary>
    [Authorize]
    public class DocumentLibraryController : BaseController
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
        /// DocumentController
        /// </summary>
        public DocumentLibraryController()
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

        #region Document Libraries 

        /// <summary>
        /// /DocumentLibraries/
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// DocumentLibraries 
        /// </summary>
        /// <returns></returns>
        public ActionResult DocumentLibraries()
        {
            if (!SavePermissionsToViewBag(FeatureEnum.DocumentLibrary))
                return Redirect("~/home/unauthorized");
            return View();
        }

        /// <summary>
        /// Manage DocumentLibrary - Add /Edit 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DocumentLibrary(int Id = 0)
        {
            if (!SavePermissionsToViewBag(FeatureEnum.DocumentLibrary))
                return Redirect("~/home/unauthorized");

            DocumentLibraryModel documentLibraryModel = new DocumentLibraryModel();
            documentLibraryModel.DocumentLibraryId = Id;
            if (Request.IsAjaxRequest())
                return PartialView(documentLibraryModel);
            else
                return View(documentLibraryModel);

        }

        #endregion

        #region Documents

        /// <summary>
        /// Documents
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Documents(int Id)
        {
            if (!SavePermissionsToViewBag(FeatureEnum.DocumentLibrary))
                return Redirect("~/home/unauthorized");

            DocumentLibraryModel documentLibraryModel = new DocumentLibraryModel();
            documentLibraryModel.DocumentLibraryId = Id;
            if (Request.IsAjaxRequest())
                return PartialView(documentLibraryModel);
            else
                return View(documentLibraryModel);
        }

        /// <summary>
        /// Manage Document
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ManageDocument(int Id = 0,int LibId=0)
        {
            if (!SavePermissionsToViewBag(FeatureEnum.DocumentLibrary))
                return Redirect("~/home/unauthorized");

            DocumentModel documentModel = new DocumentModel();
            documentModel.DocumentId = Id;
            documentModel.DocumentLibraryId = LibId;
            if (Request.IsAjaxRequest())
                return PartialView(documentModel);
            else
                return View(documentModel);
        }

        /// <summary>
        /// ManageDocument
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ManageDocument(DocumentModel documentModel, HttpPostedFileBase file)
        {
            try
            {
                // Verify that the user selected a file
                if (file != null && file.ContentLength > 0)
                {
                    // extract only the fielname
                    documentModel.DocumentFileName = Path.GetFileName(file.FileName);
                    // TODO: need to define destination
                    using (var reader = new BinaryReader(file.InputStream))
                    {
                        documentModel.DocumentFile = reader.ReadBytes(file.ContentLength);
                    }

                }

                ContentClientProcessor.SaveDocument(documentModel);
                TempData["Message"]= new ControllerMessage{ Message="Document saved successfully.", MessageType=ControllerMessageType.Info} ;
                return RedirectToAction("Documents", new { Id = documentModel.DocumentLibraryId });
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return View(documentModel);
        }

        /// <summary>
        /// Document
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Document(int Id)
        {
            try
            {
                DocumentModel documentModel = ContentClientProcessor.GetDocument(Id);
                return File(documentModel.DocumentFile, System.Net.Mime.MediaTypeNames.Application.Octet, documentModel.DocumentFileName);
            }
            catch(Exception ex)
            {
                HandleError(ex);
                return View();
            }
        }

        #endregion 

        #endregion

    }
}
