using System.Collections.Generic;
using eCollabro.Client.Models.Content;
using eCollabro.Common;

namespace eCollabro.Client.Interface
{
    public interface IContentClient:IBaseClient
    {
       
        #region Blog Categories

        /// <summary>
        /// GetBlogCategories
        /// </summary>
        /// <returns></returns>
        List<BlogCategoryModel> GetBlogCategories();

        BlogCategoryModel GetBlogCategory(int blogCategoryId);

        void SaveBlogCategory(BlogCategoryModel blogCategory);

        void DeleteBlogCategory(int blogCategoryId);


        #endregion

        #region Blog

        /// <summary>
        /// GetBlogs
        /// </summary>
        /// <returns></returns>
        BlogCategoryModel GetBlogs(int BlogCategoryId);


        /// <summary>
        /// GetRecentBlogs
        /// </summary>
        /// <returns></returns>
        List<BlogModel> GetRecentBlogs();

        BlogModel GetBlog(int BlogId);

        void SaveBlog(BlogModel Blog);

        void DeleteBlog(int BlogCategoryId);


        #endregion

        #region ContentPage Categories

        /// <summary>
        /// GetContentPageCategories
        /// </summary>
        /// <returns></returns>
        List<ContentPageCategoryModel> GetContentPageCategories();

        ContentPageCategoryModel GetContentPageCategory(int contentId);

        void SaveContentPageCategory(ContentPageCategoryModel ContentPageCategory);

        void DeleteContentPageCategory(int ContentPageCategoryId);


        #endregion

        #region ContentPage

        /// <summary>
        /// GetContentPages
        /// </summary>
        /// <returns></returns>
        ContentPageCategoryModel GetContentPages(int contentPageCategoryId);


        ContentPageModel GetContentPage(int contentPageId);

        /// <summary>
        /// GetHomePage
        /// </summary>
        /// <returns></returns>
        ContentPageModel GetHomePage();

        void SaveContentPage(ContentPageModel contentPage);

        void DeleteContentPage(int contentPageCategoryId);


        #endregion

        #region Document Library 

        /// <summary>
        /// GetDocumentLibraries
        /// </summary>
        /// <returns></returns>
        List<DocumentLibraryModel> GetDocumentLibraries();
       
        DocumentLibraryModel GetDocumentLibrary(int contentId);
     
        void SaveDocumentLibrary(DocumentLibraryModel documentLibrary);

        void DeleteDocumentLibrary(int documentLibraryId);
       

        #endregion

        #region Document

        /// <summary>
        /// GetDocuments
        /// </summary>
        /// <returns></returns>
        DocumentLibraryModel GetDocuments(int documentLibraryId);


        /// <summary>
        /// GeRecentDocuments
        /// </summary>
        /// <returns></returns>
        List<DocumentModel> GetRecentDocuments();

        DocumentModel GetDocument(int documentId);

        void SaveDocument(DocumentModel document);

        void DeleteDocument(int documentLibraryId);


        #endregion

        #region Image Gallery

        /// <summary>
        /// GetImageGalleries
        /// </summary>
        /// <returns></returns>
        List<ImageGalleryModel> GetImageGalleries();

        ImageGalleryModel GetImageGallery(int contentId);

        void SaveImageGallery(ImageGalleryModel ImageGallery);

        void DeleteImageGallery(int ImageGalleryId);


        #endregion

        #region Image

        /// <summary>
        /// GetImages
        /// </summary>
        /// <returns></returns>
        ImageGalleryModel GetImages(int imageGalleryId);

        /// <summary>
        /// GetRecentImages
        /// </summary>
        /// <returns></returns>
        List<ImageModel> GetRecentImages();

        ImageModel GetImage(int siteImageId);

        void SaveImage(ImageModel siteImage);

        void DeleteImage(int imageGalleryId);


        #endregion

        #region Announcement

        /// <summary>
        /// GetAnnouncements
        /// </summary>
        /// <returns></returns>
        List<AnnouncementModel> GetAnnouncements();
        
        /// <summary>
        /// GetAnnouncement
        /// </summary>
        /// <param name="announcementId"></param>
        /// <returns></returns>
        AnnouncementModel GetAnnouncement(int announcementId);
        
        /// <summary>
        /// GeRecentAnnouncements
        /// </summary>
        /// <returns></returns>
        List<AnnouncementModel> GetRecentAnnouncements();

        /// <summary>
        /// SaveAnnouncement
        /// </summary>
        /// <param name="Announcement"></param>
        void SaveAnnouncement(AnnouncementModel Announcement);

        /// <summary>
        /// DeleteAnnouncement
        /// </summary>
        /// <param name="announcementId"></param>
        void DeleteAnnouncement(int announcementId);

        #endregion

        #region ContentComment

        /// <summary>
        /// GetContentComments
        /// </summary>
        /// <param name="context"></param>
        /// <param name="contextId"></param>
        /// <returns></returns>
        ContentCommentDataModel GetContentComments(ContextEnum context, int contextContentId);

        /// <summary>
        /// SaveContentComment
        /// </summary>
        /// <param name="ContentComment"></param>
        void SaveContentComment(ContentCommentModel ContentComment);

        /// <summary>
        /// DeleteContentComment
        /// </summary>
        /// <param name="contentCommentId"></param>
        void DeleteContentComment(int contentCommentId);

        /// <summary>
        /// ChangeContentLikeDislike
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="contextId"></param>
        /// <param name="liked"></param>
        void ChangeContentLikeDislike(int contentId, ContextEnum contextId, bool liked);

        /// <summary>
        /// ChangeContentVote
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="contextId"></param>
        /// <param name="vote"></param>
        void ChangeContentVote(int contentId, ContextEnum contextId, bool vote);

        /// <summary>
        /// ChangeContentRating
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="contextId"></param>
        /// <param name="rating"></param>
        void ChangeContentRating(int contentId, ContextEnum contextId, int rating);

        #endregion
    }
}
