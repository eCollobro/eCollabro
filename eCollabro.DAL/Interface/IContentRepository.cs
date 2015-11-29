// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System.Collections.Generic;
using System;
using eCollabro.BAL.Entities.Models;
using System.Linq;

#endregion

namespace eCollabro.DAL.Interface
{
    /// <summary>
    /// IContentRepository
    /// </summary>
    public interface IContentRepository
    {
        #region Methods

        #region Blog

          /// <summary>
        /// GetApprovedBlogs - Get All Approved Blogs for Site 
        /// </summary>
        /// <returns></returns>
        IQueryable<Blog> GetApprovedBlogs(int siteId);

        #endregion

        #region ContentPage

        /// <summary>
        /// GetApprovedContentPages - Get All Approved ContentPages for Site 
        /// </summary>
        /// <returns></returns>
        IQueryable<ContentPage> GetApprovedContentPages(int siteId);

 
        #endregion

        #region Document

        /// <summary>
        /// GetApprovedDocuments - Get All Approved Documents for Site 
        /// </summary>
        /// <returns></returns>
        IQueryable<Document> GetApprovedDocuments(int siteId);

        #endregion

        #region SiteImage

        /// <summary>
        /// GetApprovedSiteImages - Get All Approved SiteImages for Site 
        /// </summary>
        /// <returns></returns>
        IQueryable<SiteImage> GetApprovedSiteImages(int siteId);

        #endregion

        #endregion
    }
}
