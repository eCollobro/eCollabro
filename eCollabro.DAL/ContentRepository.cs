// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System.Data.Entity;
using System.Linq;
using eCollabro.BAL.Entities.Models;
using eCollabro.DAL.Interface;

#endregion

namespace eCollabro.DAL
{
    #region Extended Repository Extension for Content Repository

    /// <summary>
    /// ExtendedRepository
    /// </summary>
    public partial class ExtendedRepository : IExtendedRepository
    {
        private IContentRepository _contentRepository;

        public IContentRepository ContentRepository
        {
            get
            {
                if (_contentRepository == null)
                {
                    _contentRepository = new ContentRepository(this._dbContext);
                }
                return _contentRepository;
            }
        }
    }

    #endregion 

    /// <summary>
    /// ContentRepository
    /// </summary>
    public class ContentRepository : IContentRepository
    {
        #region Data Members

        private eCollabroDbModel _dbContext = null;

        #endregion 

        #region Constructor

        /// <summary>
        /// ContentRepository - Constructor 
        /// </summary>
        /// <param name="dbContext"></param>
        public ContentRepository(DbContext dbContext)
        {
            _dbContext = dbContext as eCollabroDbModel;
        }

        #endregion 

        #region Methods

        #region Blog


        /// <summary>
        /// GetApprovedBlogs - Get All Approved Blogs for Site 
        /// </summary>
        /// <returns></returns>
        public IQueryable<Blog> GetApprovedBlogs(int siteId)
        {
            IQueryable<Blog> blogs = (from blg in _dbContext.Blogs
                                      join
                                          blogCategory in _dbContext.BlogCategories on blg.BlogCategoryId equals blogCategory.BlogCategoryId
                                      where (blg.IsActive.Equals(true) && blg.IsDeleted.Equals(false) && blg.ApprovalStatus.Equals("Approved"))
                                      && (blogCategory.IsActive.Equals(true) && blogCategory.IsDeleted.Equals(false) && blogCategory.SiteId.Equals(siteId))
                                      select blg).Include(qry => qry.BlogCategory);
            return blogs;
        }

        #endregion

        #region ContentPage

        /// <summary>
        /// GetApprovedContentPages - Get All Approved ContentPages for Site 
        /// </summary>
        /// <returns></returns>
        public IQueryable<ContentPage> GetApprovedContentPages(int siteId)
        {
            IQueryable<ContentPage> ContentPages = (from cntPg in _dbContext.ContentPages
                                                    join
                                                        ContentPageCategory in _dbContext.ContentPageCategories on cntPg.ContentPageCategoryId equals ContentPageCategory.ContentPageCategoryId
                                                    where (cntPg.IsActive.Equals(true) && cntPg.IsDeleted.Equals(false) && cntPg.ApprovalStatus.Equals("Approved"))
                                                    && (ContentPageCategory.IsActive.Equals(true) && ContentPageCategory.IsDeleted.Equals(false) && ContentPageCategory.SiteId.Equals(siteId))
                                                    select cntPg).Include(qry => qry.ContentPageCategory);
            return ContentPages;
        }

        #endregion

        #region Document


        /// <summary>
        /// GetApprovedDocuments - Get All Approved Documents for Site 
        /// </summary>
        /// <returns></returns>
        public IQueryable<Document> GetApprovedDocuments(int siteId)
        {
            IQueryable<Document> Documents = (from doc in _dbContext.Documents
                                              join
                                                  DocumentLibrary in _dbContext.DocumentLibraries on doc.DocumentLibraryId equals DocumentLibrary.DocumentLibraryId
                                              where (doc.IsActive.Equals(true) && doc.IsDeleted.Equals(false) && doc.ApprovalStatus.Equals("Approved"))
                                              && (DocumentLibrary.IsActive.Equals(true) && DocumentLibrary.IsDeleted.Equals(false) && DocumentLibrary.SiteId.Equals(siteId))
                                              select doc).Include(qry => qry.DocumentLibrary);
            return Documents;
        }

        #endregion

        #region SiteImage

        /// <summary>
        /// GetApprovedSiteImages - Get All Approved SiteImages for Site 
        /// </summary>
        /// <returns></returns>
        public IQueryable<SiteImage> GetApprovedSiteImages(int siteId)
        {
            IQueryable<SiteImage> SiteImages = (from siteImg in _dbContext.SiteImages
                                                join
                                                    ImageGallery in _dbContext.ImageGalleries on siteImg.ImageGalleryId equals ImageGallery.ImageGalleryId
                                                where (siteImg.IsActive.Equals(true) && siteImg.IsDeleted.Equals(false) && siteImg.ApprovalStatus.Equals("Approved"))
                                                && (ImageGallery.IsActive.Equals(true) && ImageGallery.IsDeleted.Equals(false) && ImageGallery.SiteId.Equals(siteId))
                                                select siteImg).Include(qry => qry.ImageGallery);
            return SiteImages;
        }

        #endregion


        #endregion
    }
}
