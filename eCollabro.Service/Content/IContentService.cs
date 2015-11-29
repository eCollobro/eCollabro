#region References
using eCollabro.Service.DataContracts.RequestWrapper;
using eCollabro.Service.DataContracts.ResponseWrapper;
using System.ServiceModel;
using System;
using eCollabro.Service.DataContracts;
using System.Collections.Generic;
using eCollabro.Service.DataContracts.Content;
#endregion

namespace eCollabro.Service.Content
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
        ServiceResponse<List<BlogCategoryDC>> GetBlogCategories(ServiceRequest blogCategoriesRequest);


        /// <summary>
        /// GetActiveBlogCategories
        /// </summary>
        /// <param name="blogCategoriesRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<List<BlogCategoryDC>> GetActiveBlogCategories(ServiceRequest blogCategoriesRequest);

        /// <summary>
        /// GetBlogCategory
        /// </summary>
        /// <param name="blogCategoryRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<BlogCategoryDC> GetBlogCategory(ServiceRequest<int> blogCategoryRequest);

        /// <summary>
        /// SaveBlogCategory
        /// </summary>
        /// <param name="blogCategoryRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse SaveBlogCategory(ServiceRequest<BlogCategoryDC> blogCategoryRequest);

        /// <summary>
        /// DeleteBlogCategory
        /// </summary>
        /// <param name="deleteBlogCategoryRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse DeleteBlogCategory(ServiceRequest<int> deleteBlogCategoryRequest);

        #endregion

        #region Blog

        /// <summary>
        /// GetBlogs
        /// </summary>
        /// <param name="blogsRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<BlogCategoryDC> GetBlogs(ServiceRequest<int> blogsRequest);

        /// <summary>
        /// GetApprovedBlogs
        /// </summary>
        /// <param name="blogsRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<BlogCategoryDC> GetApprovedBlogs(ServiceRequest<int> blogsRequest);

        /// <summary>
        /// GetRecentBlogs
        /// </summary>
        /// <param name="blogsRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<List<BlogDC>> GetRecentBlogs(ServiceRequest blogsRequest);

        /// <summary>
        /// GetBlog
        /// </summary>
        /// <param name="blogRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<BlogDC> GetBlog(ServiceRequest<int> blogRequest);

        /// <summary>
        /// GetActiveBlog
        /// </summary>
        /// <param name="blogRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<BlogDC> GetActiveBlog(ServiceRequest<int> blogRequest);

        /// <summary>
        /// SaveBlog
        /// </summary>
        /// <param name="blogRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse SaveBlog(ServiceRequest<BlogDC> blogRequest);

        /// <summary>
        /// DeleteBlog
        /// </summary>
        /// <param name="deleteBlogRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse DeleteBlog(ServiceRequest<int> deleteBlogRequest);

        #endregion

        #region ContentPage Categories

        /// <summary>
        /// GetContentPageCategories
        /// </summary>
        /// <param name="contentPageCategoriesRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<List<ContentPageCategoryDC>> GetContentPageCategories(ServiceRequest contentPageCategoriesRequest);

        /// <summary>
        /// GetContentPageCategory
        /// </summary>
        /// <param name="contentPageCategoryRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<ContentPageCategoryDC> GetContentPageCategory(ServiceRequest<int> contentPageCategoryRequest);

        /// <summary>
        /// SaveContentPageCategory
        /// </summary>
        /// <param name="contentPageCategoryRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse SaveContentPageCategory(ServiceRequest<ContentPageCategoryDC> contentPageCategoryRequest);

        /// <summary>
        /// DeleteContentPageCategory
        /// </summary>
        /// <param name="deleteContentPageCategoryRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse DeleteContentPageCategory(ServiceRequest<int> deleteContentPageCategoryRequest);

        #endregion

        #region ContentPage

        /// <summary>
        /// GetContentPages
        /// </summary>
        /// <param name="contentPagesRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<ContentPageCategoryDC> GetContentPages(ServiceRequest<int> contentPagesRequest);

        /// <summary>
        /// GetContentPage
        /// </summary>
        /// <param name="contentPageRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<ContentPageDC> GetContentPage(ServiceRequest<int> contentPageRequest);

        /// <summary>
        /// GetHomePage
        /// </summary>
        /// <param name="contentPageRequest"></param>
        /// <returns></returns>
         [OperationContract]
        ServiceResponse<ContentPageDC> GetHomePage(ServiceRequest homePageRequest);

        /// <summary>
        /// SaveContentPage
        /// </summary>
        /// <param name="contentPageRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse SaveContentPage(ServiceRequest<ContentPageDC> contentPageRequest);

        /// <summary>
        /// DeleteContentPage
        /// </summary>
        /// <param name="deleteContentPageRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse DeleteContentPage(ServiceRequest<int> deleteContentPageRequest);

        #endregion

        #region Document Library

        /// <summary>
        /// GetDocumentLibraries
        /// </summary>
        /// <param name="documentLibrariesRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<List<DocumentLibraryDC>> GetDocumentLibraries(ServiceRequest documentLibrariesRequest);


        /// <summary>
        /// GetDocumentLibrary
        /// </summary>
        /// <param name="documentLibraryRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<DocumentLibraryDC> GetDocumentLibrary(ServiceRequest<int> documentLibraryRequest);

        /// <summary>
        /// SaveDocumentLibrary
        /// </summary>
        /// <param name="DocumentLibraryRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse SaveDocumentLibrary(ServiceRequest<DocumentLibraryDC> DocumentLibraryRequest);

        /// <summary>
        /// DeleteDocumentLibrary
        /// </summary>
        /// <param name="deleteContentRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse DeleteDocumentLibrary(ServiceRequest<int> deleteContentRequest);


        #endregion

        #region Document

        /// <summary>
        /// GetDocuments
        /// </summary>
        /// <param name="documentsRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<DocumentLibraryDC> GetDocuments(ServiceRequest<int> documentsRequest);

        /// <summary>
        /// GetDocument
        /// </summary>
        /// <param name="documentRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<DocumentDC> GetDocument(ServiceRequest<int> documentRequest);

        /// <summary>
        /// SaveDocument
        /// </summary>
        /// <param name="documentRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse SaveDocument(ServiceRequest<DocumentDC> documentRequest);

        /// <summary>
        /// DeleteDocument
        /// </summary>
        /// <param name="deleteDocumentRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse DeleteDocument(ServiceRequest<int> deleteDocumentRequest);

        #endregion

        #region Image Gallery

        /// <summary>
        /// GetImageGalleries
        /// </summary>
        /// <param name="imageGalleriesRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<List<ImageGalleryDC>> GetImageGalleries(ServiceRequest imageGalleriesRequest);

        /// <summary>
        /// GetImageGallery
        /// </summary>
        /// <param name="imageGalleryRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<ImageGalleryDC> GetImageGallery(ServiceRequest<int> imageGalleryRequest);

        /// <summary>
        /// SaveImageGallery
        /// </summary>
        /// <param name="imageGalleryRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse SaveImageGallery(ServiceRequest<ImageGalleryDC> imageGalleryRequest);

        /// <summary>
        /// DeleteImageGallery
        /// </summary>
        /// <param name="deleteImageGalleryRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse DeleteImageGallery(ServiceRequest<int> deleteImageGalleryRequest);

        #endregion

        #region SiteImage

        /// <summary>
        /// GetSiteImages
        /// </summary>
        /// <param name="siteImagesRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<ImageGalleryDC> GetSiteImages(ServiceRequest<int> siteImagesRequest);

        /// <summary>
        /// GetSiteImage
        /// </summary>
        /// <param name="siteImageRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<SiteImageDC> GetSiteImage(ServiceRequest<int> siteImageRequest);

        /// <summary>
        /// SaveSiteImage
        /// </summary>
        /// <param name="siteImageRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse SaveSiteImage(ServiceRequest<SiteImageDC> siteImageRequest);

        /// <summary>
        /// DeleteSiteImage
        /// </summary>
        /// <param name="deleteSiteImageRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse DeleteSiteImage(ServiceRequest<int> deleteSiteImageRequest);

        #endregion

        #region ContentComment

        /// <summary>
        /// GetContentComments
        /// </summary>
        /// <param name="contentCommentsRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<List<ContentCommentDC>> GetContentComments(ServiceRequest<ContentCommentRequestDC> contentCommentsRequest);

        /// <summary>
        /// OperationContract
        /// </summary>
        /// <param name="contentCommentRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse SaveContentComment(ServiceRequest<ContentCommentDC> contentCommentRequest);

        #endregion
    }
}