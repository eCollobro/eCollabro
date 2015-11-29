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
    /// ImageGalleryApiController
    /// </summary>
    [Authorize, WebApiExceptionFiler]
    public class ImageGalleryApiController : BaseApiController
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
        public ImageGalleryApiController()
        {
            this.ContentClientProcessor = ApplicationContext.Getinstance().UnityContainer.Resolve<IContentClient>();
        }

        #endregion

        #region Image Gallery

        /// <summary>
        /// GetImageGalleries
        /// </summary>
        /// <returns></returns>
        [Route("ImageGalleryApi/GetImageGalleries/{siteId}"),AllowAnonymous]
        public HttpResponseMessage GetImageGalleries(int siteId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            SetPagingParameters(ContentClientProcessor.RequestContext);
            List<ImageGalleryModel> imageGalleries = ContentClientProcessor.GetImageGalleries();
            return GetListResult<List<ImageGalleryModel>>(imageGalleries,ContentClientProcessor.RequestContext,ContentClientProcessor.ResponseContext);
        }

        /// <summary>
        /// GetImageGallery
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        [Route("ImageGalleryApi/GetImageGallery/{siteId}/{galleryId}")]
        public HttpResponseMessage GetImageGallery(int siteId, int galleryId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            ImageGalleryModel siteImageLibrary = ContentClientProcessor.GetImageGallery(galleryId);
            return Request.CreateResponse(HttpStatusCode.OK, siteImageLibrary);
        }

        /// <summary>
        /// DeleteImageGallery
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        [Route("ImageGalleryApi/DeleteImageGallery/{siteId}/{galleryId}"), HttpGet]
        public HttpResponseMessage DeleteImageGallery(int siteId, int galleryId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            ContentClientProcessor.DeleteImageGallery(galleryId);
            return Request.CreateResponse(HttpStatusCode.OK, CoreMessages.DeletedSuccessfully);
        }


        /// <summary>
        /// SaveImageGallery - Post Back 
        /// </summary>
        /// <param name="fm"></param>
        /// <returns></returns>
        [Route("ImageGalleryApi/SaveImageGallery/{siteId}"), HttpPost]
        public HttpResponseMessage SaveImageGallery(ImageGalleryModel imageGalleryModel, int siteId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            ContentClientProcessor.SaveImageGallery(imageGalleryModel);
            return Request.CreateResponse(HttpStatusCode.OK, new { Message = CoreMessages.SavedSuccessfully, Id = imageGalleryModel.ImageGalleryId });
        }

        #endregion

        #region Image

        /// <summary>
        /// GetImages
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        [Route("ImageGalleryApi/GetImages/{siteId}/{galleryId}")]
        public HttpResponseMessage GetImages(int siteId, int galleryId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            SetPagingParameters(ContentClientProcessor.RequestContext);
            ImageGalleryModel siteImageLibrary = ContentClientProcessor.GetImages(galleryId);
            return GetListResult<ImageGalleryModel>(siteImageLibrary,ContentClientProcessor.RequestContext,ContentClientProcessor.ResponseContext);
        }

        /// <summary>
        /// GetRecentImages
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        [Route("ImageGalleryApi/GetRecentImages/{siteId}"), AllowAnonymous]
        public HttpResponseMessage GetRecentImages(int siteId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            ContentClientProcessor.RequestContext.PageSize = 10;
            List<ImageModel> images = ContentClientProcessor.GetRecentImages();
            return GetListResult<List<ImageModel>>(images,ContentClientProcessor.RequestContext,ContentClientProcessor.ResponseContext);
        }

        /// <summary>
        /// GetImage
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        [Route("ImageGalleryApi/GetImage/{siteId}/{imageId}")]
        public HttpResponseMessage GetImage(int siteId, int imageId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            ImageModel siteImage = ContentClientProcessor.GetImage(imageId);
            return Request.CreateResponse(HttpStatusCode.OK, siteImage);
        }

        /// <summary>
        /// DeleteImage
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        [Route("ImageGalleryApi/DeleteImage/{siteId}/{imageId}"),HttpGet]
        public HttpResponseMessage DeleteImage(int siteId, int imageId)
        {
            ContentClientProcessor.UserContext.SiteId = siteId;
            ContentClientProcessor.DeleteImage(imageId);
            return Request.CreateResponse(HttpStatusCode.OK, CoreMessages.DeletedSuccessfully);
        }

        #endregion

    }
}