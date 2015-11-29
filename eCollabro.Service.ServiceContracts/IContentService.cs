// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System.ServiceModel;
using eCollabro.Service.DataContracts;
using System.Collections.Generic;
using eCollabro.Service.DataContracts.Content;

#endregion
namespace eCollabro.Service.ServiceContracts
{
    /// <summary>
    /// IContentService
    /// </summary>
    [ServiceContract]
    public interface IContentService
    {
        #region Blog Categories

        /// <summary>
        /// GetBlogCategories
        /// </summary>
        /// <param name="blogCategoriesRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<List<BlogCategoryDC>> GetBlogCategories();


        /// <summary>
        /// GetBlogCategory
        /// </summary>
        /// <param name="blogCategoryId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<BlogCategoryDC> GetBlogCategory(int blogCategoryId);

        /// <summary>
        /// SaveBlogCategory
        /// </summary>
        /// <param name="blogCategory"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<int> SaveBlogCategory(BlogCategoryDC blogCategory);

        /// <summary>
        /// DeleteBlogCategory
        /// </summary>
        /// <param name="blogCategoryId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse DeleteBlogCategory(int blogCategoryId);

        #endregion

        #region Blog

        /// <summary>
        /// GetBlogs
        /// </summary>
        /// <param name="blogCategoryId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<BlogCategoryDC> GetBlogs(int blogCategoryId);

        /// <summary>
        /// GetRecentBlogs
        /// </summary>
        /// <param name="blogsRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<List<BlogDC>> GetRecentBlogs();

        /// <summary>
        /// GetBlog
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<BlogDC> GetBlog(int blogId);

        /// <summary>
        /// SaveBlog
        /// </summary>
        /// <param name="blogRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<int> SaveBlog(BlogDC blog);

        /// <summary>
        /// DeleteBlog
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse DeleteBlog(int blogId);

        #endregion

        #region ContentPage Categories

        /// <summary>
        /// GetContentPageCategories
        /// </summary>
        /// <param name="contentPageCategoriesRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<List<ContentPageCategoryDC>> GetContentPageCategories();

        /// <summary>
        /// GetContentPageCategory
        /// </summary>
        /// <param name="contentPageCategoryId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<ContentPageCategoryDC> GetContentPageCategory(int contentPageCategoryId);

        /// <summary>
        /// SaveContentPageCategory
        /// </summary>
        /// <param name="contentPageCategory"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<int> SaveContentPageCategory(ContentPageCategoryDC contentPageCategory);

        /// <summary>
        /// DeleteContentPageCategory
        /// </summary>
        /// <param name="contentPageCategoryId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse DeleteContentPageCategory(int contentPageCategoryId);

        #endregion

        #region ContentPage

        /// <summary>
        /// GetContentPages
        /// </summary>
        /// <param name="contentPageCategoryId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<ContentPageCategoryDC> GetContentPages(int contentPageCategoryId);

        /// <summary>
        /// GetContentPage
        /// </summary>
        /// <param name="contentPageId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<ContentPageDC> GetContentPage(int contentPageId);

        /// <summary>
        /// GetHomePage
        /// </summary>
        /// <param name="contentPageRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<ContentPageDC> GetHomePage();

        /// <summary>
        /// SaveContentPage
        /// </summary>
        /// <param name="contentPage"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<int> SaveContentPage(ContentPageDC contentPage);

        /// <summary>
        /// DeleteContentPage
        /// </summary>
        /// <param name="contentPageId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse DeleteContentPage(int contentPageId);

        #endregion

        #region Document Library

        /// <summary>
        /// GetDocumentLibraries
        /// </summary>
        /// <param name="documentLibrariesRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<List<DocumentLibraryDC>> GetDocumentLibraries();


        /// <summary>
        /// GetDocumentLibrary
        /// </summary>
        /// <param name="documentLibraryId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<DocumentLibraryDC> GetDocumentLibrary(int documentLibraryId);

        /// <summary>
        /// SaveDocumentLibrary
        /// </summary>
        /// <param name="documentLibrary"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<int> SaveDocumentLibrary(DocumentLibraryDC documentLibrary);

        /// <summary>
        /// DeleteDocumentLibrary
        /// </summary>
        /// <param name="documentLibraryId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse DeleteDocumentLibrary(int documentLibraryId);


        #endregion

        #region Document

        /// <summary>
        /// GetDocuments
        /// </summary>
        /// <param name="documentLibraryId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<DocumentLibraryDC> GetDocuments(int documentLibraryId);


        /// <summary>
        /// GetRecentDocuments
        /// </summary>
        /// <param name="documentsRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<List<DocumentDC>> GetRecentDocuments();


        /// <summary>
        /// GetDocument
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<DocumentDC> GetDocument(int documentId);

        /// <summary>
        /// SaveDocument
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<int> SaveDocument(DocumentDC document);

        /// <summary>
        /// DeleteDocument
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse DeleteDocument(int documentId);

        #endregion

        #region Image Gallery

        /// <summary>
        /// GetImageGalleries
        /// </summary>
        /// <param name="imageGalleriesRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<List<ImageGalleryDC>> GetImageGalleries();

        /// <summary>
        /// GetImageGallery
        /// </summary>
        /// <param name="imageGalleryId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<ImageGalleryDC> GetImageGallery(int imageGalleryId);

        /// <summary>
        /// SaveImageGallery
        /// </summary>
        /// <param name="imageGallery"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<int> SaveImageGallery(ImageGalleryDC imageGallery);

        /// <summary>
        /// DeleteImageGallery
        /// </summary>
        /// <param name="imageGalleryId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse DeleteImageGallery(int imageGalleryId);

        #endregion

        #region Image

        /// <summary>
        /// GetImages
        /// </summary>
        /// <param name="imageGalleryId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<ImageGalleryDC> GetImages(int imageGalleryId);

        /// <summary>
        /// GetRecentImages
        /// </summary>
        /// <param name="imagesRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<List<ImageDC>> GetRecentImages();

        /// <summary>
        /// GetImage
        /// </summary>
        /// <param name="siteImage"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<ImageDC> GetImage(int siteImage);

        /// <summary>
        /// SaveImage
        /// </summary>
        /// <param name="siteImage"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<int> SaveImage(ImageDC siteImage);

        /// <summary>
        /// DeleteImage
        /// </summary>
        /// <param name="siteImageId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse DeleteImage(int siteImageId);

        #endregion

        #region Announcement

        /// <summary>
        /// GetAnnouncements
        /// </summary>
        /// <param name="announcementsRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<List<AnnouncementDC>> GetAnnouncements();

        /// <summary>
        /// GetRecentAnnouncements
        /// </summary>
        /// <param name="announcementsRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<List<AnnouncementDC>> GetRecentAnnouncements();

        /// <summary>
        /// GetAnnouncement
        /// </summary>
        /// <param name="announcementId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<AnnouncementDC> GetAnnouncement(int announcementId);

        /// <summary>
        /// SaveAnnouncement
        /// </summary>
        /// <param name="announcement"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<int> SaveAnnouncement(AnnouncementDC announcement);

        /// <summary>
        /// DeleteAnnouncement
        /// </summary>
        /// <param name="AnnouncementId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse DeleteAnnouncement(int AnnouncementId);

        #endregion 

        #region ContentComment

        /// <summary>
        /// GetContentComments
        /// </summary>
        /// <param name="contentComment"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<ContentCommentDataDC> GetContentComments(ContentCommentRequestDC contentComment);

        /// <summary>
        /// OperationContract
        /// </summary>
        /// <param name="contentComment"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<int> SaveContentComment(ContentCommentDC contentComment);


        /// <summary>
        /// DeleteContentComment
        /// </summary>
        /// <param name="contentCommentId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse DeleteContentComment(int contentCommentId);

        /// <summary>
        /// ChangeContentLikeDislike
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="contextId"></param>
        /// <param name="liked"></param>
        [OperationContract]
        ServiceResponse ChangeContentLikeDislike(int contentId, int contextId, bool liked);

        /// <summary>
        /// ChangeContentVote
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="contextId"></param>
        /// <param name="vote"></param>
        [OperationContract]
        ServiceResponse ChangeContentVote(int contentId, int contextId, bool vote);

        /// <summary>
        /// ChangeContentRating
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="contextId"></param>
        /// <param name="rating"></param>
        [OperationContract]
        ServiceResponse ChangeContentRating(int contentId, int contextId, int rating);

        #endregion

    }
}