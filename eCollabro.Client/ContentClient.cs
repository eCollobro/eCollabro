#region References
using eCollabro.DataMapper;
using System.Collections.Generic;
using System.Linq;
using eCollabro.Service.DataContracts;
using eCollabro.Client.ServiceProxy;
using eCollabro.Client.Models.Content;
using eCollabro.Service.DataContracts.Content;
using eCollabro.Client.Interface;
using eCollabro.Common;
using eCollabro.Client.Models.Core;
using eCollabro.Client.ServiceProxy.Interface;
#endregion

namespace eCollabro.Client
{
    public class ContentClient : BaseClient, IContentClient
    {
        private IContentProxy _contentProxy = null;

        public ContentClient()
        {
            _contentProxy = new ContentServiceProxy();
            _contentProxy.Initialize(SecurityClientTranslate.Convert(UserContext));
        }

        #region Blog Category

        /// <summary>
        /// GetBlogCategories
        /// </summary>
        /// <param name="BlogCategoryLibraryId"></param>
        /// <returns></returns>
        public List<BlogCategoryModel> GetBlogCategories()
        {
            List<BlogCategoryModel> BlogCategoryResults = new List<BlogCategoryModel>();
            ServiceResponse<List<BlogCategoryDC>> blogCategoriesResponse = _contentProxy.Execute(opt => opt.GetBlogCategories());
            if (blogCategoriesResponse.Status == ResponseStatus.Success)
            {
                blogCategoriesResponse.Result.ForEach(
                    BlogCategory => BlogCategoryResults.Add(Mapper.Map<BlogCategoryDC, BlogCategoryModel>(BlogCategory))
                   );
            }
            else
            {
                HandleError(blogCategoriesResponse.Status, blogCategoriesResponse.ResponseMessage);
            }
            return BlogCategoryResults;
        }

        /// <summary>
        /// GetBlogCategory
        /// </summary>
        /// <param name="blogCategoryId"></param>
        /// <returns></returns>
        public BlogCategoryModel GetBlogCategory(int blogCategoryId)
        {
            BlogCategoryModel BlogCategoryResult = new BlogCategoryModel();
            ServiceResponse<BlogCategoryDC> BlogCategoryResponse = _contentProxy.Execute(opt => opt.GetBlogCategory(blogCategoryId));
            if (BlogCategoryResponse.Status == ResponseStatus.Success)
            {
                BlogCategoryResult = Mapper.Map<BlogCategoryDC, BlogCategoryModel>(BlogCategoryResponse.Result);
            }
            else
            {
                HandleError(BlogCategoryResponse.Status, BlogCategoryResponse.ResponseMessage);
            }
            return BlogCategoryResult;
        }

        /// <summary>
        /// SaveBlogCategory
        /// </summary>
        /// <param name="blogCategory"></param>
        public void SaveBlogCategory(BlogCategoryModel blogCategory)
        {
            BlogCategoryDC blogCategoryDC = Mapper.Map<BlogCategoryModel, BlogCategoryDC>(blogCategory);
            ServiceResponse<int> saveBlogCategoryResponse = _contentProxy.Execute(opt => opt.SaveBlogCategory(blogCategoryDC));
            if (saveBlogCategoryResponse.Status != ResponseStatus.Success)
                HandleError(saveBlogCategoryResponse.Status, saveBlogCategoryResponse.ResponseMessage);
            else
                blogCategory.BlogCategoryId = saveBlogCategoryResponse.Result;
        }

        /// <summary>
        /// DeleteBlogCategory
        /// </summary>
        /// <param name="blogCategoryId"></param>
        public void DeleteBlogCategory(int blogCategoryId)
        {
            ServiceResponse deleteBlogCategoryResponse = _contentProxy.Execute(opt => opt.DeleteBlogCategory(blogCategoryId));
            if (deleteBlogCategoryResponse.Status != ResponseStatus.Success)
                HandleError(deleteBlogCategoryResponse.Status, deleteBlogCategoryResponse.ResponseMessage);
        }

        #endregion

        #region Blog

        /// <summary>
        /// GetBlogs
        /// </summary>
        /// <param name="blogCategoryId"></param>
        /// <returns></returns>
        public BlogCategoryModel GetBlogs(int blogCategoryId)
        {
            BlogCategoryModel blogCategoryModel = null;
            //BlogsRequest.RequestContextParameters = Mapper.Map<RequestContextParameter,RequestContextParameterDC>(this.RequestContext);
            ServiceResponse<BlogCategoryDC> BlogsResponse = _contentProxy.Execute(opt => opt.GetBlogs(blogCategoryId));
            if (BlogsResponse.Status == ResponseStatus.Success)
            {
                blogCategoryModel = Mapper.Map<BlogCategoryDC, BlogCategoryModel>(BlogsResponse.Result);
                blogCategoryModel.Blogs = new List<BlogModel>();
                BlogsResponse.Result.Blogs.ForEach(
                    Blog => blogCategoryModel.Blogs.Add(Mapper.Map<BlogDC, BlogModel>(Blog))
                   );
                this.ResponseContext.NumberOfRecords = BlogsResponse.ResponseParameters.NumberOfRecords;
            }
            else
            {
                HandleError(BlogsResponse.Status, BlogsResponse.ResponseMessage);
            }
            return blogCategoryModel;
        }


        /// <summary>
        /// GetBlog
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public BlogModel GetBlog(int blogId)
        {
            BlogModel blogResult = new BlogModel();
            ServiceResponse<BlogDC> blogResponse = _contentProxy.Execute(opt => opt.GetBlog(blogId));
            if (blogResponse.Status == ResponseStatus.Success)
            {
                blogResult = Mapper.Map<BlogDC, BlogModel>(blogResponse.Result);
            }
            else
            {
                HandleError(blogResponse.Status, blogResponse.ResponseMessage);
            }
            return blogResult;
        }

        /// <summary>
        /// GeRecentBlogs
        /// </summary>
        /// <returns></returns>
        public List<BlogModel> GetRecentBlogs()
        {
            List<BlogModel> BlogResults = new List<BlogModel>();
            ServiceResponse<List<BlogDC>> RecentBlogsResponse = _contentProxy.Execute(opt => opt.GetRecentBlogs());
            if (RecentBlogsResponse.Status == ResponseStatus.Success)
            {
                RecentBlogsResponse.Result.ForEach(
                    Blog => BlogResults.Add(Mapper.Map<BlogDC, BlogModel>(Blog))
                   );
            }
            else
            {
                HandleError(RecentBlogsResponse.Status, RecentBlogsResponse.ResponseMessage);
            }
            return BlogResults;
        }

        /// <summary>
        /// SaveBlog
        /// </summary>
        /// <param name="blog"></param>
        public void SaveBlog(BlogModel blog)
        {
            BlogDC blogDC = Mapper.Map<BlogModel, BlogDC>(blog);
            ServiceResponse<int> saveBlogResponse = _contentProxy.Execute(opt => opt.SaveBlog(blogDC));

            if (saveBlogResponse.Status != ResponseStatus.Success)
                HandleError(saveBlogResponse.Status, saveBlogResponse.ResponseMessage);
            else
                blog.BlogId = saveBlogResponse.Result;
        }

        /// <summary>
        /// DeleteBlog
        /// </summary>
        /// <param name="blogId"></param>
        public void DeleteBlog(int blogId)
        {
            ServiceResponse deleteBlogResponse = _contentProxy.Execute(opt => opt.DeleteBlog(blogId));
            if (deleteBlogResponse.Status != ResponseStatus.Success)
                HandleError(deleteBlogResponse.Status, deleteBlogResponse.ResponseMessage);
        }

        #endregion

        #region Content Page Category

        /// <summary>
        /// GetContentPageCategories
        /// </summary>
        /// <returns></returns>
        public List<ContentPageCategoryModel> GetContentPageCategories()
        {
            List<ContentPageCategoryModel> ContentPageCategoryResults = new List<ContentPageCategoryModel>();
            ServiceResponse<List<ContentPageCategoryDC>> ContentPageCategoriesResponse = _contentProxy.Execute(opt => ContentPageCategoriesResponse = opt.GetContentPageCategories());
            if (ContentPageCategoriesResponse.Status == ResponseStatus.Success)
            {
                ContentPageCategoriesResponse.Result.ForEach(
                    ContentPageCategory => ContentPageCategoryResults.Add(Mapper.Map<ContentPageCategoryDC, ContentPageCategoryModel>(ContentPageCategory))
                   );
            }
            else
            {
                HandleError(ContentPageCategoriesResponse.Status, ContentPageCategoriesResponse.ResponseMessage);
            }
            return ContentPageCategoryResults;
        }


        /// <summary>
        /// GetContentPageCategory
        /// </summary>
        /// <param name="contentPageCategoryId"></param>
        /// <returns></returns>
        public ContentPageCategoryModel GetContentPageCategory(int contentPageCategoryId)
        {
            ContentPageCategoryModel ContentPageCategoryResult = new ContentPageCategoryModel();
            ServiceResponse<ContentPageCategoryDC> contentPageCategoryResponse = _contentProxy.Execute(opt => opt.GetContentPageCategory(contentPageCategoryId));
            if (contentPageCategoryResponse.Status == ResponseStatus.Success)
            {
                ContentPageCategoryResult = Mapper.Map<ContentPageCategoryDC, ContentPageCategoryModel>(contentPageCategoryResponse.Result);
            }
            else
            {
                HandleError(contentPageCategoryResponse.Status, contentPageCategoryResponse.ResponseMessage);
            }
            return ContentPageCategoryResult;
        }

        /// <summary>
        /// SaveContentPageCategory
        /// </summary>
        /// <param name="contentPageCategory"></param>
        public void SaveContentPageCategory(ContentPageCategoryModel contentPageCategory)
        {
            ContentPageCategoryDC contentPageCategoryDC = Mapper.Map<ContentPageCategoryModel, ContentPageCategoryDC>(contentPageCategory);
            ServiceResponse<int> saveContentPageCategoryResponse = _contentProxy.Execute(opt => opt.SaveContentPageCategory(contentPageCategoryDC));

            if (saveContentPageCategoryResponse.Status != ResponseStatus.Success)
                HandleError(saveContentPageCategoryResponse.Status, saveContentPageCategoryResponse.ResponseMessage);
            else
                contentPageCategory.ContentPageCategoryId = saveContentPageCategoryResponse.Result;
        }

        /// <summary>
        /// DeleteContentPageCategory
        /// </summary>
        /// <param name="contentPageCategoryId"></param>
        public void DeleteContentPageCategory(int contentPageCategoryId)
        {
            ServiceResponse deleteContentPageCategoryResponse = _contentProxy.Execute(opt => opt.DeleteContentPageCategory(contentPageCategoryId));
            if (deleteContentPageCategoryResponse.Status != ResponseStatus.Success)
                HandleError(deleteContentPageCategoryResponse.Status, deleteContentPageCategoryResponse.ResponseMessage);
        }

        #endregion

        #region Content Page

        /// <summary>
        /// GetContentPages
        /// </summary>
        /// <param name="contentPageCategoryId"></param>
        /// <returns></returns>
        public ContentPageCategoryModel GetContentPages(int contentPageCategoryId)
        {
            ContentPageCategoryModel contentPageCategoryModel = null;
            ServiceResponse<ContentPageCategoryDC> contentPagesResponse = _contentProxy.Execute(opt => opt.GetContentPages(contentPageCategoryId));
            if (contentPagesResponse.Status == ResponseStatus.Success)
            {
                contentPageCategoryModel = Mapper.Map<ContentPageCategoryDC, ContentPageCategoryModel>(contentPagesResponse.Result);
                contentPageCategoryModel.ContentPages = new List<ContentPageModel>();
                contentPagesResponse.Result.ContentPages.ForEach(
                    ContentPage => contentPageCategoryModel.ContentPages.Add(Mapper.Map<ContentPageDC, ContentPageModel>(ContentPage))
                   );
            }
            else
            {
                HandleError(contentPagesResponse.Status, contentPagesResponse.ResponseMessage);
            }
            return contentPageCategoryModel;
        }

        /// <summary>
        /// GetContentPage
        /// </summary>
        /// <param name="contentPageId"></param>
        /// <returns></returns>
        public ContentPageModel GetContentPage(int contentPageId)
        {
            ContentPageModel contentPageResult = new ContentPageModel();
            ServiceResponse<ContentPageDC> contentPageResponse = _contentProxy.Execute(opt => opt.GetContentPage(contentPageId));
            if (contentPageResponse.Status == ResponseStatus.Success)
            {
                contentPageResult = Mapper.Map<ContentPageDC, ContentPageModel>(contentPageResponse.Result);
            }
            else
            {
                HandleError(contentPageResponse.Status, contentPageResponse.ResponseMessage);
            }
            return contentPageResult;
        }

        /// <summary>
        /// GetHomePage
        /// </summary>
        /// <returns></returns>
        public ContentPageModel GetHomePage()
        {
            ContentPageModel contentPageResult = new ContentPageModel();
            ServiceResponse<ContentPageDC> contentPageResponse = _contentProxy.Execute(opt => opt.GetHomePage());
            if (contentPageResponse.Status == ResponseStatus.Success)
            {
                if (contentPageResponse.Result != null)
                {
                    contentPageResult = Mapper.Map<ContentPageDC, ContentPageModel>(contentPageResponse.Result);
                }
            }
            else
            {
                HandleError(contentPageResponse.Status, contentPageResponse.ResponseMessage);
            }
            return contentPageResult;
        }

        /// <summary>
        /// SaveContentPage
        /// </summary>
        /// <param name="contentPage"></param>
        public void SaveContentPage(ContentPageModel contentPage)
        {
            ContentPageDC saveContentPageRequest = Mapper.Map<ContentPageModel, ContentPageDC>(contentPage);
            ServiceResponse<int> saveContentPageResponse = _contentProxy.Execute(opt => opt.SaveContentPage(saveContentPageRequest));

            if (saveContentPageResponse.Status != ResponseStatus.Success)
                HandleError(saveContentPageResponse.Status, saveContentPageResponse.ResponseMessage);
            else
                contentPage.ContentPageId = saveContentPageResponse.Result;
        }

        /// <summary>
        /// DeleteContentPage
        /// </summary>
        /// <param name="contentPageId"></param>
        public void DeleteContentPage(int contentPageId)
        {
            ServiceResponse deleteContentPageResponse = _contentProxy.Execute(opt => opt.DeleteContentPage(contentPageId));
            if (deleteContentPageResponse.Status != ResponseStatus.Success)
                HandleError(deleteContentPageResponse.Status, deleteContentPageResponse.ResponseMessage);
        }

        #endregion

        #region Document Library

        /// <summary>
        /// GetDocumentLibraries
        /// </summary>
        /// <returns></returns>
        public List<DocumentLibraryModel> GetDocumentLibraries()
        {
            List<DocumentLibraryModel> documentLibraryResults = new List<DocumentLibraryModel>();
            ServiceResponse<List<DocumentLibraryDC>> DocumentLibrariesResponse = _contentProxy.Execute(opt => opt.GetDocumentLibraries());
            if (DocumentLibrariesResponse.Status == ResponseStatus.Success)
            {
                DocumentLibrariesResponse.Result.ForEach(
                    DocumentLibrary => documentLibraryResults.Add(Mapper.Map<DocumentLibraryDC, DocumentLibraryModel>(DocumentLibrary))
                   );
            }
            else
            {
                HandleError(DocumentLibrariesResponse.Status, DocumentLibrariesResponse.ResponseMessage);
            }
            return documentLibraryResults;
        }

        /// <summary>
        /// GetDocumentLibrary
        /// </summary>
        /// <param name="contentId"></param>
        /// <returns></returns>
        public DocumentLibraryModel GetDocumentLibrary(int contentId)
        {
            DocumentLibraryModel documentLibraryResult = new DocumentLibraryModel();
            ServiceResponse<DocumentLibraryDC> documentLibraryResponse = _contentProxy.Execute(opt => opt.GetDocumentLibrary(contentId));
            if (documentLibraryResponse.Status == ResponseStatus.Success)
            {
                documentLibraryResult = Mapper.Map<DocumentLibraryDC, DocumentLibraryModel>(documentLibraryResponse.Result);
            }
            else
            {
                HandleError(documentLibraryResponse.Status, documentLibraryResponse.ResponseMessage);
            }
            return documentLibraryResult;
        }

        /// <summary>
        /// SaveDocumentLibrary
        /// </summary>
        /// <param name="documentLibrary"></param>
        public void SaveDocumentLibrary(DocumentLibraryModel documentLibrary)
        {
            DocumentLibraryDC documentLibraryDC = Mapper.Map<DocumentLibraryModel, DocumentLibraryDC>(documentLibrary);
            ServiceResponse<int> saveDocumentLibraryResponse = _contentProxy.Execute(opt => opt.SaveDocumentLibrary(documentLibraryDC));

            if (saveDocumentLibraryResponse.Status != ResponseStatus.Success)
                HandleError(saveDocumentLibraryResponse.Status, saveDocumentLibraryResponse.ResponseMessage);
            else
                documentLibrary.DocumentLibraryId = saveDocumentLibraryResponse.Result;
        }

        /// <summary>
        /// DeleteDocumentLibrary
        /// </summary>
        /// <param name="documentLibraryId"></param>
        public void DeleteDocumentLibrary(int documentLibraryId)
        {
            ServiceResponse deleteDocumentLibraryResponse = _contentProxy.Execute(opt => opt.DeleteDocumentLibrary(documentLibraryId));
            if (deleteDocumentLibraryResponse.Status != ResponseStatus.Success)
                HandleError(deleteDocumentLibraryResponse.Status, deleteDocumentLibraryResponse.ResponseMessage);
        }

        #endregion

        #region Document

        /// <summary>
        /// GetDocuments
        /// </summary>
        /// <param name="documentLibraryId"></param>
        /// <returns></returns>
        public DocumentLibraryModel GetDocuments(int documentLibraryId)
        {
            DocumentLibraryModel documentLibraryModel = null;
            ServiceResponse<DocumentLibraryDC> documentsResponse = _contentProxy.Execute(opt => opt.GetDocuments(documentLibraryId));
            if (documentsResponse.Status == ResponseStatus.Success)
            {
                documentLibraryModel = Mapper.Map<DocumentLibraryDC, DocumentLibraryModel>(documentsResponse.Result);
                documentLibraryModel.Documents = new List<DocumentModel>();
                documentsResponse.Result.Documents.ForEach(
                    Document => documentLibraryModel.Documents.Add(Mapper.Map<DocumentDC, DocumentModel>(Document))
                   );
            }
            else
            {
                HandleError(documentsResponse.Status, documentsResponse.ResponseMessage);
            }
            return documentLibraryModel;
        }


        /// <summary>
        /// GeRecentDocuments
        /// </summary>
        /// <returns></returns>
        public List<DocumentModel> GetRecentDocuments()
        {
            List<DocumentModel> documentResults = new List<DocumentModel>();
            ServiceResponse<List<DocumentDC>> RecentDocumentsResponse = _contentProxy.Execute(opt => opt.GetRecentDocuments());
            if (RecentDocumentsResponse.Status == ResponseStatus.Success)
            {
                RecentDocumentsResponse.Result.ForEach(
                    Document => documentResults.Add(Mapper.Map<DocumentDC, DocumentModel>(Document))
                   );
            }
            else
            {
                HandleError(RecentDocumentsResponse.Status, RecentDocumentsResponse.ResponseMessage);
            }
            return documentResults;
        }

        /// <summary>
        /// GetDocument
        /// </summary>
        /// <param name="contentId"></param>
        /// <returns></returns>
        public DocumentModel GetDocument(int contentId)
        {
            DocumentModel documentResult = new DocumentModel();
            ServiceResponse<DocumentDC> documentResponse = _contentProxy.Execute(opt => opt.GetDocument(contentId));
            if (documentResponse.Status == ResponseStatus.Success)
            {
                documentResult = Mapper.Map<DocumentDC, DocumentModel>(documentResponse.Result);
            }
            else
            {
                HandleError(documentResponse.Status, documentResponse.ResponseMessage);
            }
            return documentResult;
        }

        /// <summary>
        /// SaveDocument
        /// </summary>
        /// <param name="document"></param>
        public void SaveDocument(DocumentModel document)
        {
            DocumentDC documentDC = Mapper.Map<DocumentModel, DocumentDC>(document);
            ServiceResponse<int> saveDocumentResponse = _contentProxy.Execute(opt => opt.SaveDocument(documentDC));
            if (saveDocumentResponse.Status != ResponseStatus.Success)
                HandleError(saveDocumentResponse.Status, saveDocumentResponse.ResponseMessage);
            else
                document.DocumentId = saveDocumentResponse.Result;
        }

        /// <summary>
        /// DeleteDocument
        /// </summary>
        /// <param name="documentId"></param>
        public void DeleteDocument(int documentId)
        {
            ServiceResponse deleteDocumentResponse = _contentProxy.Execute(opt => opt.DeleteDocument(documentId));
            if (deleteDocumentResponse.Status != ResponseStatus.Success)
                HandleError(deleteDocumentResponse.Status, deleteDocumentResponse.ResponseMessage);
        }

        #endregion

        #region Image Gallery

        /// <summary>
        /// GetImageGalleries
        /// </summary>
        /// <param name="imageGalleryLibraryId"></param>
        /// <returns></returns>
        public List<ImageGalleryModel> GetImageGalleries()
        {
            List<ImageGalleryModel> ImageGalleryResults = new List<ImageGalleryModel>();
            ServiceResponse<List<ImageGalleryDC>> imageGalleriesResponse = _contentProxy.Execute(opt => opt.GetImageGalleries());
            if (imageGalleriesResponse.Status == ResponseStatus.Success)
            {
                imageGalleriesResponse.Result.ForEach(
                    ImageGallery => ImageGalleryResults.Add(Mapper.Map<ImageGalleryDC, ImageGalleryModel>(ImageGallery))
                   );
            }
            else
            {
                HandleError(imageGalleriesResponse.Status, imageGalleriesResponse.ResponseMessage);
            }
            return ImageGalleryResults;
        }

        /// <summary>
        /// GetImageGallery
        /// </summary>
        /// <param name="imageGalleryId"></param>
        /// <returns></returns>
        public ImageGalleryModel GetImageGallery(int imageGalleryId)
        {
            ImageGalleryModel ImageGalleryResult = new ImageGalleryModel();
            ServiceResponse<ImageGalleryDC> imageGalleryResponse = _contentProxy.Execute(opt => opt.GetImageGallery(imageGalleryId));
            if (imageGalleryResponse.Status == ResponseStatus.Success)
            {
                ImageGalleryResult = Mapper.Map<ImageGalleryDC, ImageGalleryModel>(imageGalleryResponse.Result);
            }
            else
            {
                HandleError(imageGalleryResponse.Status, imageGalleryResponse.ResponseMessage);
            }
            return ImageGalleryResult;
        }

        /// <summary>
        /// SaveImageGallery
        /// </summary>
        /// <param name="imageGallery"></param>
        public void SaveImageGallery(ImageGalleryModel imageGallery)
        {
            ImageGalleryDC imageGalleryDC = Mapper.Map<ImageGalleryModel, ImageGalleryDC>(imageGallery);
            ServiceResponse<int> saveImageGalleryResponse = _contentProxy.Execute(opt => opt.SaveImageGallery(imageGalleryDC));
            if (saveImageGalleryResponse.Status != ResponseStatus.Success)
                HandleError(saveImageGalleryResponse.Status, saveImageGalleryResponse.ResponseMessage);
            else
                imageGallery.ImageGalleryId = saveImageGalleryResponse.Result;
        }

        /// <summary>
        /// DeleteImageGallery
        /// </summary>
        /// <param name="imageGalleryId"></param>
        public void DeleteImageGallery(int imageGalleryId)
        {
            ServiceResponse deleteImageGalleryResponse = _contentProxy.Execute(opt => opt.DeleteImageGallery(imageGalleryId));
            if (deleteImageGalleryResponse.Status != ResponseStatus.Success)
                HandleError(deleteImageGalleryResponse.Status, deleteImageGalleryResponse.ResponseMessage);
        }

        #endregion

        #region Image

        /// <summary>
        /// GetImages
        /// </summary>
        /// <param name="imageGalleryId"></param>
        /// <returns></returns>
        public ImageGalleryModel GetImages(int imageGalleryId)
        {
            ImageGalleryModel imageGalleryModel = null;
            ServiceResponse<ImageGalleryDC> imagesResponse = _contentProxy.Execute(opt => imagesResponse = opt.GetImages(imageGalleryId));
            if (imagesResponse.Status == ResponseStatus.Success)
            {
                imageGalleryModel = Mapper.Map<ImageGalleryDC, ImageGalleryModel>(imagesResponse.Result);
                imageGalleryModel.Images = new List<ImageModel>();
                imagesResponse.Result.Images.ForEach(
                    image => imageGalleryModel.Images.Add(Mapper.Map<ImageDC, ImageModel>(image))
                   );
            }
            else
            {
                HandleError(imagesResponse.Status, imagesResponse.ResponseMessage);
            }
            return imageGalleryModel;
        }


        /// <summary>
        /// GetRecentImages
        /// </summary>
        /// <returns></returns>
        public List<ImageModel> GetRecentImages()
        {
            List<ImageModel> images = new List<ImageModel>();
            ServiceResponse<List<ImageDC>> imagesResponse = _contentProxy.Execute(opt => opt.GetRecentImages());
            if (imagesResponse.Status == ResponseStatus.Success)
            {
                imagesResponse.Result.ForEach(
                    image => images.Add(Mapper.Map<ImageDC, ImageModel>(image))
                   );
            }
            else
            {
                HandleError(imagesResponse.Status, imagesResponse.ResponseMessage);
            }
            return images;
        }

        /// <summary>
        /// GetImage
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        public ImageModel GetImage(int imageId)
        {
            ImageModel imageResult = new ImageModel();
            ServiceResponse<ImageDC> imageResponse = _contentProxy.Execute(opt => opt.GetImage(imageId));
            if (imageResponse.Status == ResponseStatus.Success)
            {
                imageResult = Mapper.Map<ImageDC, ImageModel>(imageResponse.Result);
            }
            else
            {
                HandleError(imageResponse.Status, imageResponse.ResponseMessage);
            }
            return imageResult;
        }

        /// <summary>
        /// SaveImage
        /// </summary>
        /// <param name="image"></param>
        public void SaveImage(ImageModel image)
        {
            ImageDC ImageDC = Mapper.Map<ImageModel, ImageDC>(image);
            ServiceResponse<int> saveImageResponse = _contentProxy.Execute(opt => opt.SaveImage(ImageDC));

            if (saveImageResponse.Status != ResponseStatus.Success)
                HandleError(saveImageResponse.Status, saveImageResponse.ResponseMessage);
            else
                image.ImageId = saveImageResponse.Result;
        }

        /// <summary>
        /// DeleteImage
        /// </summary>
        /// <param name="imageId"></param>
        public void DeleteImage(int imageId)
        {
            ServiceResponse deleteImageResponse = _contentProxy.Execute(opt => opt.DeleteImage(imageId));
            if (deleteImageResponse.Status != ResponseStatus.Success)
                HandleError(deleteImageResponse.Status, deleteImageResponse.ResponseMessage);
        }

        #endregion

        #region Announcement

        /// <summary>
        /// GetAnnouncements
        /// </summary>
        /// <returns></returns>
        public List<AnnouncementModel> GetAnnouncements()
        {
            List<AnnouncementModel> announcements = new List<AnnouncementModel>();
            ServiceResponse<List<AnnouncementDC>> announcementsResponse = _contentProxy.Execute(opt => opt.GetAnnouncements());
            if (announcementsResponse.Status == ResponseStatus.Success)
            {
                announcementsResponse.Result.ForEach(
                    announcement => announcements.Add(Mapper.Map<AnnouncementDC, AnnouncementModel>(announcement))
                   );
            }
            else
            {
                HandleError(announcementsResponse.Status, announcementsResponse.ResponseMessage);
            }
            return announcements;
        }


        /// <summary>
        /// GetAnnouncement
        /// </summary>
        /// <param name="announcementId"></param>
        /// <returns></returns>
        public AnnouncementModel GetAnnouncement(int announcementId)
        {
            AnnouncementModel announcementResult = new AnnouncementModel();
            ServiceResponse<AnnouncementDC> announcementResponse = _contentProxy.Execute(opt => opt.GetAnnouncement(announcementId));
            if (announcementResponse.Status == ResponseStatus.Success)
            {
                announcementResult = Mapper.Map<AnnouncementDC, AnnouncementModel>(announcementResponse.Result);
            }
            else
            {
                HandleError(announcementResponse.Status, announcementResponse.ResponseMessage);
            }
            return announcementResult;
        }

        /// <summary>
        /// GeRecentAnnouncements
        /// </summary>
        /// <returns></returns>
        public List<AnnouncementModel> GetRecentAnnouncements()
        {
            List<AnnouncementModel> announcementResults = new List<AnnouncementModel>();
            ServiceResponse<List<AnnouncementDC>> recentAnnouncementsResponse = _contentProxy.Execute(opt => opt.GetRecentAnnouncements());
            if (recentAnnouncementsResponse.Status == ResponseStatus.Success)
            {
                recentAnnouncementsResponse.Result.ForEach(
                    Announcement => announcementResults.Add(Mapper.Map<AnnouncementDC, AnnouncementModel>(Announcement))
                   );
            }
            else
            {
                HandleError(recentAnnouncementsResponse.Status, recentAnnouncementsResponse.ResponseMessage);
            }
            return announcementResults;
        }

        /// <summary>
        /// SaveAnnouncement
        /// </summary>
        /// <param name="announcement"></param>
        public void SaveAnnouncement(AnnouncementModel announcement)
        {
            AnnouncementDC announcementDC = Mapper.Map<AnnouncementModel, AnnouncementDC>(announcement);
            ServiceResponse<int> saveAnnouncementResponse = _contentProxy.Execute(opt => opt.SaveAnnouncement(announcementDC));

            if (saveAnnouncementResponse.Status != ResponseStatus.Success)
                HandleError(saveAnnouncementResponse.Status, saveAnnouncementResponse.ResponseMessage);
            else
                announcement.AnnouncementId = saveAnnouncementResponse.Result;
        }

        /// <summary>
        /// DeleteAnnouncement
        /// </summary>
        /// <param name="announcementId"></param>
        public void DeleteAnnouncement(int announcementId)
        {
            ServiceResponse deleteAnnouncementResponse = _contentProxy.Execute(opt => opt.DeleteAnnouncement(announcementId));
            if (deleteAnnouncementResponse.Status != ResponseStatus.Success)
                HandleError(deleteAnnouncementResponse.Status, deleteAnnouncementResponse.ResponseMessage);
        }

        #endregion

        #region Content Comment

        /// <summary>
        /// GetContentComments
        /// </summary>
        /// <param name="contextId"></param>
        /// <returns></returns>
        public ContentCommentDataModel GetContentComments(ContextEnum context, int contextContentId)
        {
            ContentCommentDataModel contentCommentsData = null;
            ContentCommentRequestDC contentCommentsRequest = new ContentCommentRequestDC();
            contentCommentsRequest.ContextId = (int)context;
            contentCommentsRequest.ContextContentId = contextContentId;
            ServiceResponse<ContentCommentDataDC> contentCommentsResponse = _contentProxy.Execute(opt => opt.GetContentComments(contentCommentsRequest));
            if (contentCommentsResponse.Status == ResponseStatus.Success)
            {
                contentCommentsData=Mapper.Map<ContentCommentDataDC, ContentCommentDataModel>(contentCommentsResponse.Result);
                contentCommentsData.ContentComments = new List<ContentCommentModel>();
                AddChildContentComments(contentCommentsData.ContentComments, contentCommentsResponse.Result.ContentComments, 0);
            }
            else
            {
                HandleError(contentCommentsResponse.Status, contentCommentsResponse.ResponseMessage);
            }
            return contentCommentsData;
        }

        /// <summary>
        /// AddChildContentComments
        /// </summary>
        /// <param name="contentComments"></param>
        /// <param name="contentCommentsDC"></param>
        /// <param name="parentContentCommentId"></param>
        private void AddChildContentComments(List<ContentCommentModel> contentComments, List<ContentCommentDC> contentCommentsDC, int parentContentCommentId)
        {

            List<ContentCommentDC> rootComments = null;
            if (parentContentCommentId.Equals(0))
            {
                rootComments = contentCommentsDC.Where(qry => !qry.ParentContentCommentId.HasValue || (qry.ParentContentCommentId.HasValue && qry.ParentContentCommentId.Value.Equals(0))).ToList();
            }
            else
            {
                rootComments = contentCommentsDC.Where(qry => qry.ParentContentCommentId.HasValue && qry.ParentContentCommentId.Value.Equals(parentContentCommentId)).ToList();
            }

            foreach (ContentCommentDC contentComment in rootComments)
            {
                ContentCommentModel contentCommentModel = Mapper.Map<ContentCommentDC, ContentCommentModel>(contentComment);
                contentComments.Add(contentCommentModel);
                contentCommentModel.ContentComments = new List<ContentCommentModel>();
                AddChildContentComments(contentCommentModel.ContentComments, contentCommentsDC, contentComment.ContentCommentId);
            }
        }

        /// <summary>
        /// SaveContentComment
        /// </summary>
        /// <param name="contentComment"></param>
        public void SaveContentComment(ContentCommentModel contentComment)
        {
            ContentCommentDC contentCommentDC = Mapper.Map<ContentCommentModel, ContentCommentDC>(contentComment);
            ServiceResponse<int> saveContentCommentResponse = _contentProxy.Execute(opt => opt.SaveContentComment(contentCommentDC));

            if (saveContentCommentResponse.Status != ResponseStatus.Success)
                HandleError(saveContentCommentResponse.Status, saveContentCommentResponse.ResponseMessage);
            else
                contentComment.ContentCommentId = saveContentCommentResponse.Result;
        }


        /// <summary>
        /// DeleteContentComment
        /// </summary>
        /// <param name="contentCommentId"></param>
        public void DeleteContentComment(int contentCommentId)
        {
            ServiceResponse addContentCommentResponse = _contentProxy.Execute(opt => opt.DeleteContentComment(contentCommentId));

            if (addContentCommentResponse.Status != ResponseStatus.Success)
                HandleError(addContentCommentResponse.Status, addContentCommentResponse.ResponseMessage);
        }

        /// <summary>
        /// ChangeContentLikeDislike
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="contextId"></param>
        /// <param name="liked"></param>
        public void ChangeContentLikeDislike(int contentId, ContextEnum contextId, bool liked)
        {
            ServiceResponse changeContentLikeDislikeResponse = _contentProxy.Execute(opt => opt.ChangeContentLikeDislike(contentId,(int)contextId,liked));

            if (changeContentLikeDislikeResponse.Status != ResponseStatus.Success)
                HandleError(changeContentLikeDislikeResponse.Status, changeContentLikeDislikeResponse.ResponseMessage);
        }

        /// <summary>
        /// ChangeContentVote
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="contextId"></param>
        /// <param name="vote"></param>
        public void ChangeContentVote(int contentId, ContextEnum contextId, bool vote)
        {
            ServiceResponse changeContentVoteResponse = _contentProxy.Execute(opt => opt.ChangeContentVote(contentId,(int)contextId,vote));

            if (changeContentVoteResponse.Status != ResponseStatus.Success)
                HandleError(changeContentVoteResponse.Status, changeContentVoteResponse.ResponseMessage);
        }

        /// <summary>
        /// ChangeContentRating
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="contextId"></param>
        /// <param name="rating"></param>
        public void ChangeContentRating(int contentId, ContextEnum contextId, int rating)
        {
            ServiceResponse changeContentRatingResponse = _contentProxy.Execute(opt => opt.ChangeContentRating(contentId,(int)contextId,rating));

            if (changeContentRatingResponse.Status != ResponseStatus.Success)
                HandleError(changeContentRatingResponse.Status, changeContentRatingResponse.ResponseMessage);
        }

        #endregion
    }
}
