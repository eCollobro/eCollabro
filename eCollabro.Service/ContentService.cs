// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.BAL;
using eCollabro.DataMapper;
using eCollabro.Service.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using eCollabro.BAL.Entities.Models;
using eCollabro.Service.DataContracts.Content;
using eCollabro.Common;
using eCollabro.Service.ServiceContracts;
using eCollabro.BAL.Entities.CustomModels;

#endregion

namespace eCollabro.Service
{
    /// <summary>
    /// ContentService
    /// </summary>
    public class ContentService : BaseService, IContentService
    {
        #region Property

        private ContentManager _contentManager;

        #endregion

        #region Constructor

        public ContentService()
        {
            _contentManager = new ContentManager();
        }

        #endregion

        #region Methods

        #region Blog Categories

        /// <summary>
        /// GetBlogCategories
        /// </summary>
        /// <param name="blogCategoriesRequest"></param>
        /// <returns></returns>
        public ServiceResponse<List<BlogCategoryDC>> GetBlogCategories()
        {
            ServiceResponse<List<BlogCategoryDC>> blogCategoriesResponse = new ServiceResponse<List<BlogCategoryDC>>();
            try
            {
                SetContext();
                blogCategoriesResponse.Result = new List<BlogCategoryDC>();
                List<BlogCategory> BlogCategories = _contentManager.GetBlogCategories();
                BlogCategories.ForEach(result =>
                {
                    blogCategoriesResponse.Result.Add(Mapper.Map<BlogCategory, BlogCategoryDC>(result));
                });

            }
            catch (Exception ex)
            {
                HandleError(ex, blogCategoriesResponse);
            }
            return blogCategoriesResponse;
        }

        /// <summary>
        /// GetBlogCategory
        /// </summary>
        /// <param name="blogCategoryId"></param>
        /// <returns></returns>
        public ServiceResponse<BlogCategoryDC> GetBlogCategory(int blogCategoryId)
        {
            ServiceResponse<BlogCategoryDC> blogCategoryResponse = new ServiceResponse<BlogCategoryDC>();
            try
            {
                SetContext();
                BlogCategory blogCategoryResult = _contentManager.GetBlogCategory(blogCategoryId);
                blogCategoryResponse.Result = Mapper.Map<BlogCategory, BlogCategoryDC>(blogCategoryResult);
            }
            catch (Exception ex)
            {
                HandleError(ex, blogCategoryResponse);
            }
            return blogCategoryResponse;
        }

        /// <summary>
        /// SaveBlogCategory
        /// </summary>
        /// <param name="blogCategory"></param>
        /// <returns></returns>
        public ServiceResponse<int> SaveBlogCategory(BlogCategoryDC blogCategory)
        {
            ServiceResponse<int> blogCategoryResponse = new ServiceResponse<int>();
            try
            {
                SetContext();
                BlogCategory blogCategoryModel = Mapper.Map<BlogCategoryDC, BlogCategory>(blogCategory);
                _contentManager.SaveBlogCategory(blogCategoryModel);
                blogCategoryResponse.Result = blogCategoryModel.BlogCategoryId;
            }
            catch (Exception ex)
            {
                HandleError(ex, blogCategoryResponse);
            }
            return blogCategoryResponse;
        }

        /// <summary>
        /// DeleteBlogCategory
        /// </summary>
        /// <param name="blogCategoryId"></param>
        /// <returns></returns>
        public ServiceResponse DeleteBlogCategory(int blogCategoryId)
        {
            ServiceResponse deleteBlogCategoryResponse = new ServiceResponse();
            try
            {
                SetContext();
                _contentManager.DeleteBlogCategory(blogCategoryId);
            }
            catch (Exception ex)
            {
                HandleError(ex, deleteBlogCategoryResponse);
            }
            return deleteBlogCategoryResponse;
        }

        #endregion

        #region Blog

        /// <summary>
        /// GetBlogs
        /// </summary>
        /// <param name="blogCategoryId"></param>
        /// <returns></returns>
        public ServiceResponse<BlogCategoryDC> GetBlogs(int blogCategoryId)
        {
            ServiceResponse<BlogCategoryDC> BlogsResponse = new ServiceResponse<BlogCategoryDC>();
            try
            {
                SetContext();
                BlogCategory BlogCategory = _contentManager.GetBlogs(blogCategoryId);
                BlogsResponse.Result = Mapper.Map<BlogCategory, BlogCategoryDC>(BlogCategory);
                BlogsResponse.Result.Blogs = new List<BlogDC>();
                BlogCategory.Blogs.ToList().ForEach(result =>
                {
                    BlogsResponse.Result.Blogs.Add(Mapper.Map<Blog, BlogDC>(result));
                });
                ResponseContextParameter responseParameter=RequestContext.Current.Get<ResponseContextParameter>("ResponseParameter");
                BlogsResponse.ResponseParameters.NumberOfRecords =responseParameter.NumberOfRecords;

            }
            catch (Exception ex)
            {
                HandleError(ex, BlogsResponse);
            }
            return BlogsResponse;
        }

        /// <summary>
        /// GetRecentBlogs
        /// </summary>
        /// <param name="blogsRequest"></param>
        /// <returns></returns>
        public ServiceResponse<List<BlogDC>> GetRecentBlogs()
        {
            ServiceResponse<List<BlogDC>> blogsResponse = new ServiceResponse<List<BlogDC>>();
            try
            {
                SetContext();
                List<Blog> blogs = _contentManager.GetRecentBlogs();
                blogsResponse.Result = new List<BlogDC>();
                blogs.ToList().ForEach(result =>
                {
                    blogsResponse.Result.Add(Mapper.Map<Blog, BlogDC>(result));
                });

            }
            catch (Exception ex)
            {
                HandleError(ex, blogsResponse);
            }
            return blogsResponse;
        }

        /// <summary>
        /// GetBlog
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public ServiceResponse<BlogDC> GetBlog(int blogId)
        {
            ServiceResponse<BlogDC> BlogResponse = new ServiceResponse<BlogDC>();
            try
            {
                SetContext();
                Blog blogResult = _contentManager.GetBlog(blogId);
                BlogResponse.Result = Mapper.Map<Blog, BlogDC>(blogResult);
            }
            catch (Exception ex)
            {
                HandleError(ex, BlogResponse);
            }
            return BlogResponse;
        }


        /// <summary>
        /// SaveBlog
        /// </summary>
        /// <param name="blogRequest"></param>
        /// <returns></returns>
        public ServiceResponse<int> SaveBlog(BlogDC blog)
        {
            ServiceResponse<int> blogResponse = new ServiceResponse<int>();
            try
            {
                SetContext();
                Blog blogModel = Mapper.Map<BlogDC, Blog>(blog);
                _contentManager.SaveBlog(blogModel);
                blogResponse.Result = blogModel.BlogId;
            }
            catch (Exception ex)
            {
                HandleError(ex, blogResponse);
            }
            return blogResponse;
        }

        /// <summary>
        /// DeleteBlog
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public ServiceResponse DeleteBlog(int blogId)
        {
            ServiceResponse BlogResponse = new ServiceResponse();
            try
            {
                SetContext();
                _contentManager.DeleteBlog(blogId);
            }
            catch (Exception ex)
            {
                HandleError(ex, BlogResponse);
            }
            return BlogResponse;
        }

        #endregion

        #region Content Page Categories

        /// <summary>
        /// GetContentPageCategories
        /// </summary>
        /// <param name="contentPageCategoriesRequest"></param>
        /// <returns></returns>
        public ServiceResponse<List<ContentPageCategoryDC>> GetContentPageCategories()
        {
            ServiceResponse<List<ContentPageCategoryDC>> contentPageCategoriesResponse = new ServiceResponse<List<ContentPageCategoryDC>>();
            try
            {
                SetContext();
                contentPageCategoriesResponse.Result = new List<ContentPageCategoryDC>();
                List<ContentPageCategory> ContentPageCategories = _contentManager.GetContentPageCategories();
                ContentPageCategories.ForEach(result =>
                {
                    contentPageCategoriesResponse.Result.Add(Mapper.Map<ContentPageCategory, ContentPageCategoryDC>(result));
                });

            }
            catch (Exception ex)
            {
                HandleError(ex, contentPageCategoriesResponse);
            }
            return contentPageCategoriesResponse;
        }

        /// <summary>
        /// GetContentPageCategory
        /// </summary>
        /// <param name="contentPageCategoryId"></param>
        /// <returns></returns>
        public ServiceResponse<ContentPageCategoryDC> GetContentPageCategory(int contentPageCategoryId)
        {
            ServiceResponse<ContentPageCategoryDC> contentPageCategoryResponse = new ServiceResponse<ContentPageCategoryDC>();
            try
            {
                SetContext();
                ContentPageCategory contentPageCategoryResult = _contentManager.GetContentPageCategory(contentPageCategoryId);
                contentPageCategoryResponse.Result = Mapper.Map<ContentPageCategory, ContentPageCategoryDC>(contentPageCategoryResult);
            }
            catch (Exception ex)
            {
                HandleError(ex, contentPageCategoryResponse);
            }
            return contentPageCategoryResponse;
        }

        /// <summary>
        /// SaveContentPageCategory
        /// </summary>
        /// <param name="contentPageCategory"></param>
        /// <returns></returns>
        public ServiceResponse<int> SaveContentPageCategory(ContentPageCategoryDC contentPageCategory)
        {
            ServiceResponse<int> contentPageCategoryResponse = new ServiceResponse<int>();
            try
            {
                SetContext();
                ContentPageCategory contentPageCategoryModel = Mapper.Map<ContentPageCategoryDC, ContentPageCategory>(contentPageCategory);
                _contentManager.SaveContentPageCategory(contentPageCategoryModel);
                contentPageCategoryResponse.Result = contentPageCategoryModel.ContentPageCategoryId;
            }
            catch (Exception ex)
            {
                HandleError(ex, contentPageCategoryResponse);
            }
            return contentPageCategoryResponse;
        }

        /// <summary>
        /// DeleteContentPageCategory
        /// </summary>
        /// <param name="contentPageCategoryId"></param>
        /// <returns></returns>
        public ServiceResponse DeleteContentPageCategory(int contentPageCategoryId)
        {
            ServiceResponse deleteContentPageCategoryResponse = new ServiceResponse();
            try
            {
                SetContext();
                _contentManager.DeleteContentPageCategory(contentPageCategoryId);
            }
            catch (Exception ex)
            {
                HandleError(ex, deleteContentPageCategoryResponse);
            }
            return deleteContentPageCategoryResponse;
        }

        #endregion

        #region ContentPage

        /// <summary>
        /// GetContentPages
        /// </summary>
        /// <param name="contentPageCategoryId"></param>
        /// <returns></returns>
        public ServiceResponse<ContentPageCategoryDC> GetContentPages(int contentPageCategoryId)
        {
            ServiceResponse<ContentPageCategoryDC> ContentPagesResponse = new ServiceResponse<ContentPageCategoryDC>();
            try
            {
                SetContext();
                ContentPageCategory ContentPageCategory = _contentManager.GetContentPages(contentPageCategoryId);
                ContentPagesResponse.Result = Mapper.Map<ContentPageCategory, ContentPageCategoryDC>(ContentPageCategory);
                ContentPagesResponse.Result.ContentPages = new List<ContentPageDC>();
                ContentPageCategory.ContentPages.ToList().ForEach(result =>
                {
                    ContentPagesResponse.Result.ContentPages.Add(Mapper.Map<ContentPage, ContentPageDC>(result));
                });

            }
            catch (Exception ex)
            {
                HandleError(ex, ContentPagesResponse);
            }
            return ContentPagesResponse;
        }

        /// <summary>
        /// GetContentPage
        /// </summary>
        /// <param name="contentPageId"></param>
        /// <returns></returns>
        public ServiceResponse<ContentPageDC> GetContentPage(int contentPageId)
        {
            ServiceResponse<ContentPageDC> contentPageResponse = new ServiceResponse<ContentPageDC>();
            try
            {
                SetContext();
                ContentPage contentPageResult = _contentManager.GetContentPage(contentPageId);
                contentPageResponse.Result = Mapper.Map<ContentPage, ContentPageDC>(contentPageResult);
            }
            catch (Exception ex)
            {
                HandleError(ex, contentPageResponse);
            }
            return contentPageResponse;
        }

        /// <summary>
        /// GetHomePage
        /// </summary>
        /// <param name="contentPageRequest"></param>
        /// <returns></returns>
        public ServiceResponse<ContentPageDC> GetHomePage()
        {
            ServiceResponse<ContentPageDC> contentPageResponse = new ServiceResponse<ContentPageDC>();
            try
            {
                SetContext();
                ContentPage contentPage = _contentManager.GetHomePage();
                if (contentPage != null)
                    contentPageResponse.Result = Mapper.Map<ContentPage, ContentPageDC>(contentPage);
            }
            catch (Exception ex)
            {
                HandleError(ex, contentPageResponse);
            }
            return contentPageResponse;
        }


        /// <summary>
        /// SaveContentPage
        /// </summary>
        /// <param name="contentPage"></param>
        /// <returns></returns>
        public ServiceResponse<int> SaveContentPage(ContentPageDC contentPage)
        {
            ServiceResponse<int> contentPageResponse = new ServiceResponse<int>();
            try
            {
                SetContext();
                ContentPage contentPageModel = Mapper.Map<ContentPageDC, ContentPage>(contentPage);
                _contentManager.SaveContentPage(contentPageModel);
                contentPageResponse.Result = contentPageModel.ContentPageId;
                SecurityManager securityManager = null;
                // Create Navigation 
                if (contentPage.AddToNavigation)
                {
                    securityManager = new SecurityManager();
                    int? navigationParentId = null;
                    if (!contentPage.NavigationParentId.Equals(0))
                        navigationParentId = contentPage.NavigationParentId;
                    securityManager.SaveNavigation(new Navigation
                    {
                        NavigationText = contentPage.MenuTitle,
                        ContentPageId = contentPageModel.ContentPageId,
                        CreatedById = securityManager.UserContextDetails.UserId,
                        CreatedOn = DateTime.UtcNow,
                        NavigationTypeId = (int)NavigationTypeEnum.Content,
                        IsActive = true,
                        SiteId = securityManager.UserContextDetails.SiteId,
                        NavigationParentId = navigationParentId
                    });
                }
                // set as Home Page
                if (contentPage.SetToHomePage)
                {
                    if (securityManager == null)
                        securityManager = new SecurityManager();
                    SiteConfiguration siteConfiguration = securityManager.GetSiteConfiguration();
                    siteConfiguration.HomePageContentPageId = contentPageModel.ContentPageId;
                    securityManager.SaveSiteConfiguration(siteConfiguration);
                }

            }
            catch (Exception ex)
            {
                HandleError(ex, contentPageResponse);
            }
            return contentPageResponse;
        }

        /// <summary>
        /// DeleteContentPage
        /// </summary>
        /// <param name="contentPageId"></param>
        /// <returns></returns>
        public ServiceResponse DeleteContentPage(int contentPageId)
        {
            ServiceResponse contentPageResponse = new ServiceResponse();
            try
            {
                SetContext();
                _contentManager.DeleteContentPage(contentPageId);
            }
            catch (Exception ex)
            {
                HandleError(ex, contentPageResponse);
            }
            return contentPageResponse;
        }

        #endregion

        #region Document Library

        /// <summary>
        /// GetDocumentLibraries
        /// </summary>
        /// <param name="documentLibrariesRequest"></param>
        /// <returns></returns>
        public ServiceResponse<List<DocumentLibraryDC>> GetDocumentLibraries()
        {
            ServiceResponse<List<DocumentLibraryDC>> documentLibrariesResponse = new ServiceResponse<List<DocumentLibraryDC>>();
            try
            {
                SetContext();
                documentLibrariesResponse.Result = new List<DocumentLibraryDC>();
                List<DocumentLibrary> DocumentLibraries = _contentManager.GetDocumentLibraries();
                DocumentLibraries.ForEach(result =>
                {
                    documentLibrariesResponse.Result.Add(Mapper.Map<DocumentLibrary, DocumentLibraryDC>(result));
                });

            }
            catch (Exception ex)
            {
                HandleError(ex, documentLibrariesResponse);
            }
            return documentLibrariesResponse;
        }

        /// <summary>
        /// GetDocumentLibrary
        /// </summary>
        /// <param name="documentLibraryId"></param>
        /// <returns></returns>
        public ServiceResponse<DocumentLibraryDC> GetDocumentLibrary(int documentLibraryId)
        {
            ServiceResponse<DocumentLibraryDC> documentLibraryResponse = new ServiceResponse<DocumentLibraryDC>();
            try
            {
                SetContext();
                DocumentLibrary documentLibraryResult = _contentManager.GetDocumentLibrary(documentLibraryId);
                documentLibraryResponse.Result = Mapper.Map<DocumentLibrary, DocumentLibraryDC>(documentLibraryResult);
            }
            catch (Exception ex)
            {
                HandleError(ex, documentLibraryResponse);
            }
            return documentLibraryResponse;
        }

        /// <summary>
        /// SaveDocumentLibrary
        /// </summary>
        /// <param name="documentLibrary"></param>
        /// <returns></returns>
        public ServiceResponse<int> SaveDocumentLibrary(DocumentLibraryDC documentLibrary)
        {
            ServiceResponse<int> documentLibraryResponse = new ServiceResponse<int>();
            try
            {
                SetContext();
                DocumentLibrary documentLibraryModel = Mapper.Map<DocumentLibraryDC, DocumentLibrary>(documentLibrary);
                _contentManager.SaveDocumentLibrary(documentLibraryModel);
                documentLibraryResponse.Result = documentLibraryModel.DocumentLibraryId;
            }
            catch (Exception ex)
            {
                HandleError(ex, documentLibraryResponse);
            }
            return documentLibraryResponse;
        }

        /// <summary>
        /// DeleteDocumentLibrary
        /// </summary>
        /// <param name="documentLibraryId"></param>
        /// <returns></returns>
        public ServiceResponse DeleteDocumentLibrary(int documentLibraryId)
        {
            ServiceResponse documentLibraryResponse = new ServiceResponse();
            try
            {
                SetContext();
                _contentManager.DeleteDocumentLibrary(documentLibraryId);
            }
            catch (Exception ex)
            {
                HandleError(ex, documentLibraryResponse);
            }
            return documentLibraryResponse;
        }

        #endregion

        #region Document

        /// <summary>
        /// GetDocuments
        /// </summary>
        /// <param name="documentLibraryId"></param>
        /// <returns></returns>
        public ServiceResponse<DocumentLibraryDC> GetDocuments(int documentLibraryId)
        {
            ServiceResponse<DocumentLibraryDC> documentsResponse = new ServiceResponse<DocumentLibraryDC>();
            try
            {
                SetContext();
                DocumentLibrary documentLibrary = _contentManager.GetDocuments(documentLibraryId);
                documentsResponse.Result = Mapper.Map<DocumentLibrary, DocumentLibraryDC>(documentLibrary);
                documentsResponse.Result.Documents = new List<DocumentDC>();
                documentLibrary.Documents.ToList().ForEach(result =>
                {
                    documentsResponse.Result.Documents.Add(Mapper.Map<Document, DocumentDC>(result));
                });

            }
            catch (Exception ex)
            {
                HandleError(ex, documentsResponse);
            }
            return documentsResponse;
        }


        /// <summary>
        /// GetRecentDocuments
        /// </summary>
        /// <param name="documentsRequest"></param>
        /// <returns></returns>
        public ServiceResponse<List<DocumentDC>> GetRecentDocuments()
        {
            ServiceResponse<List<DocumentDC>> documentsResponse = new ServiceResponse<List<DocumentDC>>();
            try
            {
                SetContext();
                List<Document> documents = _contentManager.GetRecentDocuments();
                documentsResponse.Result = new List<DocumentDC>();
                documents.ToList().ForEach(result =>
                {
                    documentsResponse.Result.Add(Mapper.Map<Document, DocumentDC>(result));
                });

            }
            catch (Exception ex)
            {
                HandleError(ex, documentsResponse);
            }
            return documentsResponse;
        }

        /// <summary>
        /// GetDocument
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public ServiceResponse<DocumentDC> GetDocument(int documentId)
        {
            ServiceResponse<DocumentDC> documentResponse = new ServiceResponse<DocumentDC>();
            try
            {
                SetContext();
                Document documentResult = _contentManager.GetDocument(documentId);
                documentResponse.Result = Mapper.Map<Document, DocumentDC>(documentResult);
                if (documentResult.FileObject != null)
                {
                    documentResponse.Result.DocumentFile = documentResult.FileObject.FileObjectData;
                }
            }
            catch (Exception ex)
            {
                HandleError(ex, documentResponse);
            }
            return documentResponse;
        }

        /// <summary>
        /// SaveDocument
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public ServiceResponse<int> SaveDocument(DocumentDC document)
        {
            ServiceResponse<int> documentResponse = new ServiceResponse<int>();
            try
            {
                SetContext();
                Document documentModel = Mapper.Map<DocumentDC, Document>(document);
                _contentManager.SaveDocument(documentModel, document.DocumentFile);
                documentResponse.Result = documentModel.DocumentId;
            }
            catch (Exception ex)
            {
                HandleError(ex, documentResponse);
            }
            return documentResponse;
        }

        /// <summary>
        /// DeleteDocument
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public ServiceResponse DeleteDocument(int documentId)
        {
            ServiceResponse documentResponse = new ServiceResponse();
            try
            {
                SetContext();
                _contentManager.DeleteDocument(documentId);
            }
            catch (Exception ex)
            {
                HandleError(ex, documentResponse);
            }
            return documentResponse;
        }

        #endregion

        #region Image Gallery

        /// <summary>
        /// GetImageGalleries
        /// </summary>
        /// <param name="imageGalleriesRequest"></param>
        /// <returns></returns>
        public ServiceResponse<List<ImageGalleryDC>> GetImageGalleries()
        {
            ServiceResponse<List<ImageGalleryDC>> imageGalleriesResponse = new ServiceResponse<List<ImageGalleryDC>>();
            try
            {
                SetContext();
                imageGalleriesResponse.Result = new List<ImageGalleryDC>();
                List<ImageGallery> ImageGalleries = _contentManager.GetImageGalleries();
                ImageGalleries.ForEach(result =>
                {
                    imageGalleriesResponse.Result.Add(Mapper.Map<ImageGallery, ImageGalleryDC>(result));
                });

            }
            catch (Exception ex)
            {
                HandleError(ex, imageGalleriesResponse);
            }
            return imageGalleriesResponse;
        }

        /// <summary>
        /// GetImageGallery
        /// </summary>
        /// <param name="imageGalleryId"></param>
        /// <returns></returns>
        public ServiceResponse<ImageGalleryDC> GetImageGallery(int imageGalleryId)
        {
            ServiceResponse<ImageGalleryDC> imageGalleryResponse = new ServiceResponse<ImageGalleryDC>();
            try
            {
                SetContext();
                ImageGallery imageGalleryResult = _contentManager.GetImageGallery(imageGalleryId);
                imageGalleryResponse.Result = Mapper.Map<ImageGallery, ImageGalleryDC>(imageGalleryResult);
            }
            catch (Exception ex)
            {
                HandleError(ex, imageGalleryResponse);
            }
            return imageGalleryResponse;
        }

        /// <summary>
        /// SaveImageGallery
        /// </summary>
        /// <param name="imageGalleryRequest"></param>
        /// <returns></returns>
        public ServiceResponse<int> SaveImageGallery(ImageGalleryDC imageGalleryRequest)
        {
            ServiceResponse<int> imageGalleryResponse = new ServiceResponse<int>();
            try
            {
                SetContext();
                ImageGallery imageGallery = Mapper.Map<ImageGalleryDC, ImageGallery>(imageGalleryRequest);
                _contentManager.SaveImageGallery(imageGallery);
                imageGalleryResponse.Result = imageGallery.ImageGalleryId;
            }
            catch (Exception ex)
            {
                HandleError(ex, imageGalleryResponse);
            }
            return imageGalleryResponse;
        }

        /// <summary>
        /// DeleteImageGallery
        /// </summary>
        /// <param name="imageGalleryId"></param>
        /// <returns></returns>
        public ServiceResponse DeleteImageGallery(int imageGalleryId)
        {
            ServiceResponse deleteImageGalleryResponse = new ServiceResponse();
            try
            {
                SetContext();
                _contentManager.DeleteImageGallery(imageGalleryId);
            }
            catch (Exception ex)
            {
                HandleError(ex, deleteImageGalleryResponse);
            }
            return deleteImageGalleryResponse;
        }

        #endregion

        #region Image

        /// <summary>
        /// GetImages
        /// </summary>
        /// <param name="imageGalleryId"></param>
        /// <returns></returns>
        public ServiceResponse<ImageGalleryDC> GetImages(int imageGalleryId)
        {
            ServiceResponse<ImageGalleryDC> imagesResponse = new ServiceResponse<ImageGalleryDC>();
            try
            {
                SetContext();
                ImageGallery imageGallery = _contentManager.GetImages(imageGalleryId);
                imagesResponse.Result = Mapper.Map<ImageGallery, ImageGalleryDC>(imageGallery);
                imagesResponse.Result.Images = new List<ImageDC>();
                imageGallery.SiteImages.ToList().ForEach(result =>
                {
                    imagesResponse.Result.Images.Add(Mapper.Map<SiteImage, ImageDC>(result));
                });

            }
            catch (Exception ex)
            {
                HandleError(ex, imagesResponse);
            }
            return imagesResponse;
        }


        /// <summary>
        /// GetRecentImages
        /// </summary>
        /// <param name="imagesRequest"></param>
        /// <returns></returns>
        public ServiceResponse<List<ImageDC>> GetRecentImages()
        {
            ServiceResponse<List<ImageDC>> ImagesResponse = new ServiceResponse<List<ImageDC>>();
            try
            {
                SetContext();
                List<SiteImage> Images = _contentManager.GetRecentImages();
                ImagesResponse.Result = new List<ImageDC>();
                Images.ToList().ForEach(result =>
                {
                    ImagesResponse.Result.Add(Mapper.Map<SiteImage, ImageDC>(result));
                });

            }
            catch (Exception ex)
            {
                HandleError(ex, ImagesResponse);
            }
            return ImagesResponse;
        }

        /// <summary>
        /// GetImage
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        public ServiceResponse<ImageDC> GetImage(int imageId)
        {
            ServiceResponse<ImageDC> imageResponse = new ServiceResponse<ImageDC>();
            try
            {
                SetContext();
                SiteImage siteImageResult = _contentManager.GetImage(imageId);
                imageResponse.Result = Mapper.Map<SiteImage, ImageDC>(siteImageResult);
                if (siteImageResult.ImageObject != null)
                {
                    imageResponse.Result.ImageFile = siteImageResult.ImageObject.ImageObjectData;
                    imageResponse.Result.ImageThumbnail = siteImageResult.ImageObject.ImageThumbnailData;
                }
            }
            catch (Exception ex)
            {
                HandleError(ex, imageResponse);
            }
            return imageResponse;
        }

        /// <summary>
        /// SaveImage
        /// </summary>
        /// <param name="saveImageRequest"></param>
        /// <returns></returns>
        public ServiceResponse<int> SaveImage(ImageDC siteImage)
        {
            ServiceResponse<int> saveImageResponse = new ServiceResponse<int>();
            try
            {
                SetContext();
                SiteImage image = Mapper.Map<ImageDC, SiteImage>(siteImage);
                _contentManager.SaveImage(image, siteImage.ImageFile);
                saveImageResponse.Result = image.ImageId;
            }
            catch (Exception ex)
            {
                HandleError(ex, saveImageResponse);
            }
            return saveImageResponse;
        }

        /// <summary>
        /// DeleteImage
        /// </summary>
        /// <param name="siteImageId"></param>
        /// <returns></returns>
        public ServiceResponse DeleteImage(int siteImageId)
        {
            ServiceResponse deleteImageResponse = new ServiceResponse();
            try
            {
                SetContext();
                _contentManager.DeleteImage(siteImageId);
            }
            catch (Exception ex)
            {
                HandleError(ex, deleteImageResponse);
            }
            return deleteImageResponse;
        }

        #endregion

        #region Announcement

        /// <summary>
        /// GetAnnouncements
        /// </summary>
        /// <param name="announcementsRequest"></param>
        /// <returns></returns>
        public ServiceResponse<List<AnnouncementDC>> GetAnnouncements()
        {
            ServiceResponse<List<AnnouncementDC>> announcementsResponse = new ServiceResponse<List<AnnouncementDC>>();
            try
            {
                SetContext();
                List<Announcement> announcements = _contentManager.GetAnnouncements(null, null, true);
                announcementsResponse.Result = new List<AnnouncementDC>();
                foreach (Announcement announcement in announcements)
                {
                    announcementsResponse.Result.Add(Mapper.Map<Announcement, AnnouncementDC>(announcement));
                }
            }
            catch (Exception ex)
            {
                HandleError(ex, announcementsResponse);
            }
            return announcementsResponse;
        }

        /// <summary>
        /// GetRecentAnnouncements
        /// </summary>
        /// <param name="announcementsRequest"></param>
        /// <returns></returns>
        public ServiceResponse<List<AnnouncementDC>> GetRecentAnnouncements()
        {
            ServiceResponse<List<AnnouncementDC>> announcementsResponse = new ServiceResponse<List<AnnouncementDC>>();
            try
            {
                SetContext();
                List<Announcement> announcements = _contentManager.GetRecentAnnouncements();
                announcementsResponse.Result = new List<AnnouncementDC>();
                announcements.ToList().ForEach(result =>
                {
                    announcementsResponse.Result.Add(Mapper.Map<Announcement, AnnouncementDC>(result));
                });

            }
            catch (Exception ex)
            {
                HandleError(ex, announcementsResponse);
            }
            return announcementsResponse;
        }

        /// <summary>
        /// GetAnnouncement
        /// </summary>
        /// <param name="announcementRequest"></param>
        /// <returns></returns>
        public ServiceResponse<AnnouncementDC> GetAnnouncement(int announcementRequest)
        {
            ServiceResponse<AnnouncementDC> announcementResponse = new ServiceResponse<AnnouncementDC>();
            try
            {
                SetContext();
                Announcement announcementResult = _contentManager.GetAnnouncement(announcementRequest);
                announcementResponse.Result = Mapper.Map<Announcement, AnnouncementDC>(announcementResult);
            }
            catch (Exception ex)
            {
                HandleError(ex, announcementResponse);
            }
            return announcementResponse;
        }


        /// <summary>
        /// SaveAnnouncement
        /// </summary>
        /// <param name="announcement"></param>
        /// <returns></returns>
        public ServiceResponse<int> SaveAnnouncement(AnnouncementDC announcement)
        {
            ServiceResponse<int> announcementResponse = new ServiceResponse<int>();
            try
            {
                SetContext();
                Announcement announcementModel = Mapper.Map<AnnouncementDC, Announcement>(announcement);
                _contentManager.SaveAnnouncement(announcementModel);
                announcementResponse.Result = announcement.AnnouncementId;

            }
            catch (Exception ex)
            {
                HandleError(ex, announcementResponse);
            }
            return announcementResponse;
        }

        /// <summary>
        /// DeleteAnnouncement
        /// </summary>
        /// <param name="announcementId"></param>
        /// <returns></returns>
        public ServiceResponse DeleteAnnouncement(int announcementId)
        {
            ServiceResponse AnnouncementResponse = new ServiceResponse();
            try
            {
                SetContext();
                _contentManager.DeleteAnnouncement(announcementId);
            }
            catch (Exception ex)
            {
                HandleError(ex, AnnouncementResponse);
            }
            return AnnouncementResponse;
        }

        #endregion

        #region Content Comment

        /// <summary>
        /// GetContentComments
        /// </summary>
        /// <param name="contentCommentsRequest"></param>
        /// <returns></returns>
        public ServiceResponse<ContentCommentDataDC> GetContentComments(ContentCommentRequestDC contentCommentsRequest)
        {
            ServiceResponse<ContentCommentDataDC> contentCommentsResponse = new ServiceResponse<ContentCommentDataDC>();
            try
            {
                SetContext();
                ContentCommentData contentCommentData = _contentManager.GetContentComments((ContextEnum)contentCommentsRequest.ContextId, contentCommentsRequest.ContextContentId);
                contentCommentsResponse.Result=Mapper.Map<ContentCommentData, ContentCommentDataDC>(contentCommentData);
                contentCommentsResponse.Result.ContentComments = new List<ContentCommentDC>();
                contentCommentData.ContentComments.ForEach(result =>
                {
                    contentCommentsResponse.Result.ContentComments.Add(Mapper.Map<ContentComment, ContentCommentDC>(result));
                });
            }
            catch (Exception ex)
            {
                HandleError(ex, contentCommentsResponse);
            }
            return contentCommentsResponse;
        }

        /// <summary>
        /// SaveContentComment
        /// </summary>
        /// <param name="contentCommentRequest"></param>
        /// <returns></returns>
        public ServiceResponse<int> SaveContentComment(ContentCommentDC contentCommentRequest)
        {
            ServiceResponse<int> contentCommentResponse = new ServiceResponse<int>();
            try
            {
                SetContext();
                ContentComment comment = Mapper.Map<ContentCommentDC, ContentComment>(contentCommentRequest);
                _contentManager.SaveContentComment(comment);
                contentCommentResponse.Result = comment.ContentCommentId;
            }
            catch (Exception ex)
            {
                HandleError(ex, contentCommentResponse);
            }
            return contentCommentResponse;
        }


        /// <summary>
        /// DeleteContentComment
        /// </summary>
        /// <param name="contentCommentId"></param>
        /// <returns></returns>
        public ServiceResponse DeleteContentComment(int contentCommentId)
        {
            ServiceResponse contentCommentResponse = new ServiceResponse();
            try
            {
                SetContext();
                _contentManager.DeleteContentComment(contentCommentId);
            }
            catch (Exception ex)
            {
                HandleError(ex, contentCommentResponse);
            }
            return contentCommentResponse;
        }

        /// <summary>
        /// ChangeContentLikeDislike
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="contextId"></param>
        /// <param name="liked"></param>
        public ServiceResponse ChangeContentLikeDislike(int contentId, int contextId, bool liked)
        {
            ServiceResponse changeContentLikeDislikeResponse = new ServiceResponse();
            try
            {
                SetContext();
                _contentManager.ChangeContentLikeDislike(contentId,(ContextEnum)contextId, liked);
            }
            catch (Exception ex)
            {
                HandleError(ex, changeContentLikeDislikeResponse);
            }
            return changeContentLikeDislikeResponse;
        }

        /// <summary>
        /// ChangeContentVote
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="contextId"></param>
        /// <param name="vote"></param>
        public ServiceResponse ChangeContentVote(int contentId, int contextId, bool vote)
        {
            ServiceResponse changeContentVoteResponse = new ServiceResponse();
            try
            {
                SetContext();
                _contentManager.ChangeContentVote(contentId,(ContextEnum) contextId, vote);
            }
            catch (Exception ex)
            {
                HandleError(ex, changeContentVoteResponse);
            }
            return changeContentVoteResponse;
        }

        /// <summary>
        /// ChangeContentRating
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="contextId"></param>
        /// <param name="rating"></param>
        public ServiceResponse ChangeContentRating(int contentId, int contextId, int rating)
        {
            ServiceResponse changeContentRatingResponse = new ServiceResponse();
            try
            {
                SetContext();
                _contentManager.ChangeContentRating(contentId,(ContextEnum) contextId, rating);
            }
            catch (Exception ex)
            {
                HandleError(ex, changeContentRatingResponse);
            }
            return changeContentRatingResponse;
        }

        #endregion

        #endregion

    }
}
