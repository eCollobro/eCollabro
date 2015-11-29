// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.Client.Interface;
using eCollabro.Client.Models.Content;
using eCollabro.Resources;
using eCollabro.Utilities;
using eCollabro.Web.Base;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

#endregion

namespace eCollabro.Web.Content.Controllers
{
    /// <summary>
    /// DocumentLibraryApiController
    /// </summary>
    [Authorize,WebApiExceptionFiler]
    public class DocumentLibraryApiController : BaseApiController
    {
        #region Property

        /// <summary>
        /// ContentClientProcessor
        /// </summary>
        private IContentClient ContentClientProcessor { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// ContentClientProcessor
        /// </summary>
        public DocumentLibraryApiController()
        {
            this.ContentClientProcessor = ApplicationContext.Getinstance().UnityContainer.Resolve<IContentClient>();
        }

        #endregion

        #region Document Library 

        /// <summary>
        /// GetDocumentLibraries
        /// </summary>
        /// <returns></returns>
        [Route("DocumentLibraryApi/GetDocumentLibraries/{siteId}"),AllowAnonymous]
        public HttpResponseMessage GetDocumentLibraries(int siteId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            SetPagingParameters(ContentClientProcessor.RequestContext);
            List<DocumentLibraryModel> documentLibraries=ContentClientProcessor.GetDocumentLibraries();
            return GetListResult<List<DocumentLibraryModel>>(documentLibraries,ContentClientProcessor.RequestContext,ContentClientProcessor.ResponseContext);
        }

        /// <summary>
        /// GetDocumentLibrary
        /// </summary>
        /// <param name="libraryId"></param>
        /// <returns></returns>
        [Route("DocumentLibraryApi/GetDocumentLibrary/{siteId}/{libraryId}")]
        public HttpResponseMessage GetDocumentLibrary(int siteId,int libraryId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            DocumentLibraryModel documentLibrary= ContentClientProcessor.GetDocumentLibrary(libraryId);
            return Request.CreateResponse(HttpStatusCode.OK, documentLibrary);
        }

        /// <summary>
        /// DeleteDocumentLibrary
        /// </summary>
        /// <param name="libraryId"></param>
        /// <returns></returns>
        [Route("DocumentLibraryApi/DeleteDocumentLibrary/{siteId}/{libraryId}"), HttpGet]
        public HttpResponseMessage DeleteDocumentLibrary(int siteId, int libraryId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            ContentClientProcessor.DeleteDocumentLibrary(libraryId);
            return Request.CreateResponse(HttpStatusCode.OK,CoreMessages.DeletedSuccessfully);
        }

 
        /// <summary>
        /// SaveDocumentLibrary - Post Back 
        /// </summary>
        /// <param name="fm"></param>
        /// <returns></returns>
        [Route("DocumentLibraryApi/SaveDocumentLibrary/{siteId}"), HttpPost]
        public HttpResponseMessage SaveDocumentLibrary(DocumentLibraryModel documentLibraryModel,int siteId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            ContentClientProcessor.SaveDocumentLibrary(documentLibraryModel);
            return Request.CreateResponse(HttpStatusCode.OK, new { Message = CoreMessages.SavedSuccessfully, Id = documentLibraryModel.DocumentLibraryId });
        }

        #endregion

        #region Document

        /// <summary>
         /// GetDocuments
        /// </summary>
        /// <param name="libraryId"></param>
        /// <returns></returns>
        [Route("DocumentLibraryApi/GetDocuments/{siteId}/{libraryId}")]
        public HttpResponseMessage GetDocuments(int siteId, int libraryId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            SetPagingParameters(ContentClientProcessor.RequestContext);
            DocumentLibraryModel documentLibrary= ContentClientProcessor.GetDocuments(libraryId);
            return GetListResult<DocumentLibraryModel>(documentLibrary, ContentClientProcessor.RequestContext, ContentClientProcessor.ResponseContext);
        }

        /// <summary>
        /// GetRecentDocuments
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        [Route("DocumentLibraryApi/GetRecentDocuments/{siteId}"),AllowAnonymous]
        public HttpResponseMessage GetRecentDocuments(int siteId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            ContentClientProcessor.RequestContext.PageSize = 10;
            List<DocumentModel> documents = ContentClientProcessor.GetRecentDocuments();
            return GetListResult<List<DocumentModel>>(documents,ContentClientProcessor.RequestContext,ContentClientProcessor.ResponseContext);
        }


        /// <summary>
        /// GetDocument
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        [Route("DocumentLibraryApi/GetDocument/{siteId}/{documentId}")]
        public HttpResponseMessage GetDocument(int siteId, int documentId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            DocumentModel document=ContentClientProcessor.GetDocument(documentId);
            return Request.CreateResponse(HttpStatusCode.OK, document);
        }   

        /// <summary>
        /// DeleteDocument
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        [Route("DocumentLibraryApi/DeleteDocument/{siteId}/{documentId}"),HttpGet]
        public HttpResponseMessage DeleteDocument(int siteId, int documentId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            ContentClientProcessor.DeleteDocument(documentId);
            return Request.CreateResponse(HttpStatusCode.OK,CoreMessages.DeletedSuccessfully);
        }

        #endregion

    }
}