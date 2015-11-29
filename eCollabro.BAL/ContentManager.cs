// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References
using eCollabro.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using eCollabro.BAL.Entities.Models;
using System.Data;
using eCollabro.DataMapper;
using eCollabro.Exceptions;
using System.Collections.ObjectModel;
using System.Resources;
using eCollabro.Common;
using eCollabro.BAL.Entities.TempModels;
using System.Linq.Expressions;
using eCollabro.DAL;
using eCollabro.BAL.Entities.CustomModels;
#endregion

namespace eCollabro.BAL
{
    /// <summary>
    /// ContentManager
    /// </summary>
    public class ContentManager : BaseManager, IWorkflowEventHandler
    {
        #region Data Members

        private SecurityManager _securityManager = null;
        private WorkflowManager _workflowManager = null;

        #endregion

        #region Constructor

        /// <summary>
        /// ContentManager
        /// </summary>
        public ContentManager()
        {
            _securityManager = new SecurityManager();
            _workflowManager = new WorkflowManager();
        }

        #endregion

        #region Methods

        #region Common

        /// <summary>
        /// TaskUpdated - Event Handler Raised by WorkflowManager SaveTask 
        /// </summary>
        /// <param name="beforeUpdate"></param>
        /// <param name="afterUpdate"></param>
        public void TaskUpdated(UserTask beforeUpdate, UserTask afterUpdate)
        {
            bool contentUpdateRequired = false;
            string taskNewStatus = afterUpdate.TaskStatus;
            if (!beforeUpdate.TaskStatus.Equals(WorkflowConstants.ApprovedStatus) && afterUpdate.TaskStatus.Equals(WorkflowConstants.ApprovedStatus))
            {
                contentUpdateRequired = true;
            }
            else if (!beforeUpdate.TaskStatus.Equals(WorkflowConstants.RejectedStatus) && afterUpdate.TaskStatus.Equals(WorkflowConstants.RejectedStatus))
            {
                contentUpdateRequired = true;
            }
            if (contentUpdateRequired)
            {
                if (afterUpdate.ContextId.Equals((int)ContextEnum.Blog))
                    BlogTaskUpdated(beforeUpdate, afterUpdate);
                else if (afterUpdate.ContextId.Equals((int)ContextEnum.ContentPage))
                    ContentPageTaskUpdated(beforeUpdate, afterUpdate);
                else if (afterUpdate.ContextId.Equals((int)ContextEnum.Document))
                    DocumentTaskUpdated(beforeUpdate, afterUpdate);
                else if (afterUpdate.ContextId.Equals((int)ContextEnum.Image))
                    ImageTaskUpdated(beforeUpdate, afterUpdate);
            }
        }

        #endregion

        #region Blog

        /// <summary>
        /// GetBlogCategory
        /// </summary>
        /// <param name="blogCategoryId"></param>
        /// <returns></returns>
        public BlogCategory GetBlogCategory(int blogCategoryId)
        {
            // Check Permission 
            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.Blog);

            BlogCategory blogCategory = eCollabroDbContext.Repository<BlogCategory>().Find(userPermissions, blogCategoryId);
            return GetContentResponse<BlogCategory>(blogCategory, userPermissions);
        }

        /// <summary>
        /// GetBlogCategories
        /// </summary>
        /// <returns></returns>
        public List<BlogCategory> GetBlogCategories()
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.Blog);
            if (!(userPermissions.Contains(PermissionEnum.ViewContent) || userPermissions.Contains(PermissionEnum.ViewAnomynousContent)))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            List<BlogCategory> blogCategories = eCollabroDbContext.Repository<BlogCategory>().Query(userPermissions).Filter(qry => qry.SiteId.Equals(UserContextDetails.SiteId)).Get().ToList();

            List<int> blogCategoryIds = blogCategories.Select(qry => qry.BlogCategoryId).ToList();
            Dictionary<int, int> blogCounts = GetBlogsCountForCategories(blogCategoryIds, userPermissions);
            foreach (var blogCountDetails in blogCounts)
            {
                blogCategories.Where(qry => qry.BlogCategoryId.Equals(blogCountDetails.Key)).FirstOrDefault().NumberOfBlogs = blogCountDetails.Value;
            }

            return blogCategories;
        }

        /// <summary>
        /// SaveBlogCategory
        /// </summary>
        /// <param name="blogCategory"></param>
        /// <returns></returns>
        public void SaveBlogCategory(BlogCategory blogCategory)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.Blog);
            if ((blogCategory.BlogCategoryId.Equals(0) && !userPermissions.Contains(PermissionEnum.AddContent)) || (!blogCategory.BlogCategoryId.Equals(0) && !userPermissions.Contains(PermissionEnum.EditContent)))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            if (blogCategory.BlogCategoryId.Equals(0)) // New
            {
                blogCategory.CreatedById = UserContextDetails.UserId;
                blogCategory.CreatedOn = DateTime.UtcNow;
                blogCategory.SiteId = UserContextDetails.SiteId;
                eCollabroDbContext.Repository<BlogCategory>().Insert(blogCategory);
                eCollabroDbContext.Save();
            }
            else  // Update 
            {
                BlogCategory oldBlogCategory = eCollabroDbContext.Repository<BlogCategory>().Find(blogCategory.BlogCategoryId);
                if (oldBlogCategory != null)
                {
                    oldBlogCategory.BlogCategoryName = blogCategory.BlogCategoryName;
                    oldBlogCategory.BlogCategoryDescription = blogCategory.BlogCategoryDescription;
                    oldBlogCategory.IsActive = blogCategory.IsActive;
                    oldBlogCategory.IsAnomynousAccess = blogCategory.IsAnomynousAccess;
                    oldBlogCategory.ModifiedById = UserContextDetails.UserId;
                    oldBlogCategory.ModifiedOn = DateTime.UtcNow;
                    eCollabroDbContext.Save();
                }
                else
                {
                    throw new DBConcurrencyException();
                }
            }
        }

        /// <summary>
        /// DeleteBlogCategory
        /// </summary>
        /// <param name="blogCategoryId"></param>
        /// <returns></returns>
        public void DeleteBlogCategory(int blogCategoryId)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.Blog);
            if (!userPermissions.Contains(PermissionEnum.DeleteContent))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            BlogCategory oldBlogCategory = eCollabroDbContext.Repository<BlogCategory>().Find(blogCategoryId);
            if (oldBlogCategory != null)
            {
                oldBlogCategory.IsDeleted = true;
                oldBlogCategory.ModifiedById = UserContextDetails.UserId;
                oldBlogCategory.ModifiedOn = DateTime.UtcNow;
                eCollabroDbContext.Save();
            }
            else
            {
                throw new DBConcurrencyException();
            }
        }

        /// <summary>
        /// GetBlog
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public Blog GetBlog(int blogId)
        {
            #region // Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.Blog);
            if (!(userPermissions.Contains(PermissionEnum.ViewContent) || userPermissions.Contains(PermissionEnum.ViewAnomynousContent)))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            Blog blog = eCollabroDbContext.Repository<Blog>().Find(userPermissions, blogId);

            if (blog != null && userPermissions.Contains(PermissionEnum.ViewUnapprovedContent))
            {
                TempBlog blogInQueue = _workflowManager.GetFromQueue<TempBlog>(ContextEnum.Blog, blog.BlogId);
                if (blogInQueue != null)// no records in queue for pending approval/draft
                {
                    MapBlogQueueToBlog(blog, blogInQueue);
                }
            }
            return GetContentResponse<Blog>(blog, userPermissions);
        }

        /// <summary>
        /// MapBlogQueueToBlog
        /// </summary>
        /// <param name="blog"></param>
        /// <param name="bloginQueue"></param>
        private void MapBlogQueueToBlog(Blog blog, TempBlog bloginQueue)
        {
            Mapper.Map<TempBlog, Blog>(bloginQueue, blog);
        }

        /// <summary>
        /// GetBlogsCountForCategories
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, int> GetBlogsCountForCategories(List<int> blogCategoryIds, List<PermissionEnum> permissions)
        {
            Dictionary<int, int> blogCounts = new Dictionary<int, int>();
            var list = eCollabroDbContext.Repository<Blog>().Query(permissions).Filter(qry => blogCategoryIds.Contains(qry.BlogCategoryId)).Get().GroupBy(grp => grp.BlogCategoryId).Select(rec => new { rec.Key, BlogCount = rec.Count() });
            foreach (var countDetails in list)
            {
                blogCounts.Add(countDetails.Key, countDetails.BlogCount);
            }
            return blogCounts;
        }

        /// <summary>
        /// GetRecentBlogs
        /// </summary>
        /// <returns></returns>
        public List<Blog> GetRecentBlogs()
        {
            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.Blog);
            RequestContextParameter requestParameters = RequestContext.Current.Get<RequestContextParameter>("RequestParameter");
            List<Blog> blogs = eCollabroDbContext.Repository<Blog>().Query(userPermissions).Include(inc => inc.BlogCategory).Filter(qry => qry.BlogCategory.SiteId.Equals(UserContextDetails.SiteId)).Get().OrderBy(ord => ord.BlogId).Take(requestParameters.PageSize == 0 ? 10 : requestParameters.PageSize).ToList();
            return blogs;
        }


        /// <summary>
        /// GetBlogs
        /// </summary>
        /// <param name="blogCategoryId"></param>
        /// <returns></returns>
        public BlogCategory GetBlogs(int blogCategoryId)
        {
            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.Blog);
            BlogCategory blogCategory = eCollabroDbContext.Repository<BlogCategory>().Find(userPermissions, blogCategoryId);
            if (blogCategory == null)
                return GetContentResponse<BlogCategory>(blogCategory, userPermissions);
            else
            {
                eCollabroDbContext.Repository<BlogCategory>().Dettach(blogCategory);

                IQueryable<Blog> blogsOriginalQuery = eCollabroDbContext.Repository<Blog>().Query(userPermissions).Filter(qry => qry.BlogCategoryId.Equals(blogCategoryId)).Get();
                IQueryable<Blog> blogsFilteredQuery = blogsOriginalQuery;
                RequestContextParameter requestParameter = RequestContext.Current.Get<RequestContextParameter>("RequestParameter");
                if (!string.IsNullOrEmpty(requestParameter.OrderByColumn))
                {
                    if (requestParameter.OrderByDirection.Equals("asc"))
                        blogsFilteredQuery = blogsFilteredQuery.OrderBy(requestParameter.OrderByColumn);
                    else
                        blogsFilteredQuery = blogsFilteredQuery.OrderByDescending(requestParameter.OrderByColumn);
                }

                if (requestParameter.PageSize != 0)
                {
                    blogsFilteredQuery = blogsFilteredQuery.Skip(requestParameter.PageNumber).Take(requestParameter.PageSize);
                }
                blogCategory.Blogs = blogsFilteredQuery.ToList();
                ResponseContextParameter res = new ResponseContextParameter();
                res.NumberOfRecords = blogsOriginalQuery.Count();
                RequestContext.Current.Add<ResponseContextParameter>("ResponseParameter", res);
                if (userPermissions.Contains(PermissionEnum.ViewUnapprovedContent))
                {
                    List<int> blogIds = blogCategory.Blogs.Select(slt => slt.BlogId).ToList();
                    List<TempBlog> blogsInQueue = _workflowManager.GetFromQueue<TempBlog>(ContextEnum.Blog, blogIds);

                    foreach (TempBlog tempBlog in blogsInQueue)
                    {
                        Blog blg = blogCategory.Blogs.Where(qry => qry.BlogId.Equals(tempBlog.BlogId)).FirstOrDefault();
                        if (blg != null)// no records in queue for pending approval/draft
                        {
                            MapBlogQueueToBlog(blg, tempBlog);
                        }
                    }
                }
            }
            return blogCategory;
        }

        /// <summary>
        /// SaveBlog
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        public void SaveBlog(Blog blog)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.Blog);
            if ((blog.BlogId.Equals(0) && !userPermissions.Contains(PermissionEnum.AddContent)) || (!blog.BlogId.Equals(0) && !userPermissions.Contains(PermissionEnum.EditContent)))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            //check contentSetting
            SecurityManager securityManager = new SecurityManager();
            List<SiteContentSettingResult> siteContentSettingResults = securityManager.GetSiteFeatureSettings(FeatureEnum.Blog);

            bool approvalRequired = siteContentSettingResults.Where(qry => qry.ContentSettingId.Equals((int)FeatureSettingEnum.ApprovalRequired)).FirstOrDefault().IsAssigned;
            //self approved in case approval not required
            if (!approvalRequired)
            {
                blog.ApprovalStatus = WorkflowConstants.ApprovedStatus;
                blog.ApproveRejectDate = DateTime.UtcNow;
                blog.ApproveRejectById = UserContextDetails.UserId;
            }
            else
            {
                blog.ApprovalStatus = WorkflowConstants.ApprovalWaitingStatus;
                blog.ApproveRejectById = null;
                blog.ApproveRejectDate = null;
            }

            if (blog.BlogId.Equals(0)) // New Blog
            {
                blog.CreatedById = UserContextDetails.UserId;
                blog.CreatedOn = DateTime.UtcNow;
                if (approvalRequired)
                    blog.ApprovalStatus = WorkflowConstants.ApprovalWaitingStatus;

                eCollabroDbContext.Repository<Blog>().Insert(blog);
                eCollabroDbContext.Save();

                if (approvalRequired)
                {
                    _workflowManager.CreateWorkflowTask(ContextEnum.Blog, blog.BlogId, "New Blog [" + blog.BlogTitle + "] ", "Blog Description : " + blog.BlogDescription);
                }

            }
            else  // Update Blog
            {
                Blog oldBlog = eCollabroDbContext.Repository<Blog>().Find(blog.BlogId);
                if (oldBlog != null)
                {
                    if (approvalRequired && oldBlog.ApprovalStatus.Equals(WorkflowConstants.ApprovedStatus)) // Save to Queue 
                    {
                        blog.ModifiedById = UserContextDetails.UserId;
                        blog.ModifiedOn = DateTime.UtcNow;
                        blog.ApprovalStatus = WorkflowConstants.ApprovalWaitingStatus;
                        TempBlog tempBlog = Mapper.Map<Blog, TempBlog>(blog);
                        _workflowManager.SaveToQueue<TempBlog>(tempBlog, ContextEnum.Blog, blog.BlogId);
                        _workflowManager.CreateWorkflowTask(ContextEnum.Blog, blog.BlogId, "New Blog [" + blog.BlogTitle + "] ", "Blog Description : " + blog.BlogDescription);

                    }
                    else // Record is new and not in Queue 
                    {
                        oldBlog.BlogTitle = blog.BlogTitle;
                        oldBlog.BlogDescription = blog.BlogDescription;
                        oldBlog.BlogContent = blog.BlogContent;
                        oldBlog.BlogCategoryId = blog.BlogCategoryId;
                        oldBlog.IsActive = blog.IsActive;
                        oldBlog.IsAnomynousAccess = blog.IsAnomynousAccess;
                        oldBlog.IsCommentsAllowed = blog.IsCommentsAllowed;
                        oldBlog.IsLikeAllowed = blog.IsLikeAllowed;
                        oldBlog.IsRatingAllowed = blog.IsRatingAllowed;
                        oldBlog.IsVotingAllowed = blog.IsVotingAllowed;
                        oldBlog.ModifiedById = UserContextDetails.UserId;
                        oldBlog.ModifiedOn = DateTime.UtcNow;

                        oldBlog.ApprovalStatus = blog.ApprovalStatus;
                        oldBlog.ApproveRejectDate = blog.ApproveRejectDate;
                        oldBlog.ApproveRejectById = blog.ApproveRejectById;
                        eCollabroDbContext.Save();
                        if (approvalRequired) // approval required // create or update task
                        {
                            _workflowManager.CreateWorkflowTask(ContextEnum.Blog, blog.BlogId, "New Blog [" + blog.BlogTitle + "] ", "Blog Description : " + blog.BlogDescription);
                        }
                    }
                }
                else
                {
                    throw new DBConcurrencyException();
                }
            }
        }

        /// <summary>
        /// DeleteBlog
        /// </summary>
        /// <param name="BlogId"></param>
        /// <returns></returns>
        public void DeleteBlog(int BlogId)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.Blog);
            if (!userPermissions.Contains(PermissionEnum.DeleteContent))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            Blog oldBlog = eCollabroDbContext.Repository<Blog>().Find(BlogId);
            if (oldBlog != null)
            {
                oldBlog.IsDeleted = true;
                oldBlog.ModifiedById = UserContextDetails.UserId;
                oldBlog.ModifiedOn = DateTime.UtcNow;
                eCollabroDbContext.Save();
            }
            else
            {
                throw new DBConcurrencyException();
            }
        }

        /// <summary>
        /// BlogTaskUpdated - Event Handler Raised by WorkflowManager SaveTask 
        /// </summary>
        /// <param name="beforeUpdate"></param>
        /// <param name="afterUpdate"></param>
        public void BlogTaskUpdated(UserTask beforeUpdate, UserTask afterUpdate)
        {
            Blog blog = eCollabroDbContext.Repository<Blog>().Find(afterUpdate.ContexContentId);
            if (blog != null)
            {
                TempBlog blogInQueue = _workflowManager.GetFromQueue<TempBlog>(ContextEnum.Blog, blog.BlogId);
                if (blogInQueue != null)// record in queue for pending approval/draft
                {
                    blogInQueue.ApprovalStatus = afterUpdate.TaskStatus;
                    blogInQueue.ApproveRejectById = UserContextDetails.UserId;
                    blogInQueue.ApproveRejectDate = DateTime.UtcNow;
                    if (afterUpdate.TaskStatus.Equals(WorkflowConstants.ApprovedStatus))
                    {
                        MapBlogQueueToBlog(blog, blogInQueue);
                        _workflowManager.DeleteFromQueue(ContextEnum.Blog, blog.BlogId);
                    }
                }
                else
                {
                    blog.ApprovalStatus = afterUpdate.TaskStatus;
                    blog.ApproveRejectById = UserContextDetails.UserId;
                    blog.ApproveRejectDate = DateTime.UtcNow;
                }
                eCollabroDbContext.Save();
            }
        }
        #endregion

        #region ContentPage

        /// <summary>
        /// GetContentPageCategory
        /// </summary>
        /// <param name="contentPageCategoryId"></param>
        /// <returns></returns>
        public ContentPageCategory GetContentPageCategory(int contentPageCategoryId)
        {
            // Check Permission 
            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.ContentPage);

            ContentPageCategory contentPageCategory = eCollabroDbContext.Repository<ContentPageCategory>().Find(userPermissions, contentPageCategoryId);
            return GetContentResponse<ContentPageCategory>(contentPageCategory, userPermissions);
        }

        /// <summary>
        /// GetContentPageCategories
        /// </summary>
        /// <returns></returns>
        public List<ContentPageCategory> GetContentPageCategories()
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.ContentPage);
            if (!(userPermissions.Contains(PermissionEnum.ViewContent) || userPermissions.Contains(PermissionEnum.ViewAnomynousContent)))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            List<ContentPageCategory> contentPageCategories = eCollabroDbContext.Repository<ContentPageCategory>().Query(userPermissions).Filter(qry => qry.SiteId.Equals(UserContextDetails.SiteId)).Get().ToList();

            List<int> contentPageCategoryIds = contentPageCategories.Select(qry => qry.ContentPageCategoryId).ToList();
            Dictionary<int, int> contentPageCounts = GetContentPagesCountForCategories(contentPageCategoryIds, userPermissions);
            foreach (var contentPageCountDetails in contentPageCounts)
            {
                contentPageCategories.Where(qry => qry.ContentPageCategoryId.Equals(contentPageCountDetails.Key)).FirstOrDefault().NumberOfContentPages = contentPageCountDetails.Value;
            }

            return contentPageCategories;
        }

        /// <summary>
        /// SaveContentPageCategory
        /// </summary>
        /// <param name="contentPageCategory"></param>
        /// <returns></returns>
        public void SaveContentPageCategory(ContentPageCategory contentPageCategory)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.ContentPage);
            if ((contentPageCategory.ContentPageCategoryId.Equals(0) && !userPermissions.Contains(PermissionEnum.AddContent)) || (!contentPageCategory.ContentPageCategoryId.Equals(0) && !userPermissions.Contains(PermissionEnum.EditContent)))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            if (contentPageCategory.ContentPageCategoryId.Equals(0)) // New
            {
                contentPageCategory.CreatedById = UserContextDetails.UserId;
                contentPageCategory.CreatedOn = DateTime.UtcNow;
                contentPageCategory.SiteId = UserContextDetails.SiteId;
                eCollabroDbContext.Repository<ContentPageCategory>().Insert(contentPageCategory);
                eCollabroDbContext.Save();
            }
            else  // Update 
            {
                ContentPageCategory oldContentPageCategory = eCollabroDbContext.Repository<ContentPageCategory>().Find(contentPageCategory.ContentPageCategoryId);
                if (oldContentPageCategory != null)
                {
                    oldContentPageCategory.ContentPageCategoryName = contentPageCategory.ContentPageCategoryName;
                    oldContentPageCategory.ContentPageCategoryDescription = contentPageCategory.ContentPageCategoryDescription;
                    oldContentPageCategory.IsActive = contentPageCategory.IsActive;
                    oldContentPageCategory.IsAnomynousAccess = contentPageCategory.IsAnomynousAccess;
                    oldContentPageCategory.ModifiedById = UserContextDetails.UserId;
                    oldContentPageCategory.ModifiedOn = DateTime.UtcNow;
                    eCollabroDbContext.Save();
                }
                else
                {
                    throw new DBConcurrencyException();
                }
            }
        }

        /// <summary>
        /// DeleteContentPageCategory
        /// </summary>
        /// <param name="contentPageCategoryId"></param>
        /// <returns></returns>
        public void DeleteContentPageCategory(int contentPageCategoryId)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.ContentPage);
            if (!userPermissions.Contains(PermissionEnum.DeleteContent))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            ContentPageCategory oldContentPageCategory = eCollabroDbContext.Repository<ContentPageCategory>().Find(contentPageCategoryId);
            if (oldContentPageCategory != null)
            {
                oldContentPageCategory.IsDeleted = true;
                oldContentPageCategory.ModifiedById = UserContextDetails.UserId;
                oldContentPageCategory.ModifiedOn = DateTime.UtcNow;
                eCollabroDbContext.Save();
            }
            else
            {
                throw new DBConcurrencyException();
            }
        }

        /// <summary>
        /// GetContentPage
        /// </summary>
        /// <param name="contentPageId"></param>
        /// <returns></returns>
        public ContentPage GetContentPage(int contentPageId)
        {
            #region // Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.ContentPage);
            if (!(userPermissions.Contains(PermissionEnum.ViewContent) || userPermissions.Contains(PermissionEnum.ViewAnomynousContent)))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            ContentPage contentPage = eCollabroDbContext.Repository<ContentPage>().Find(userPermissions, contentPageId);

            if (contentPage != null && userPermissions.Contains(PermissionEnum.ViewUnapprovedContent))
            {
                TempContentPage contentPageInQueue = _workflowManager.GetFromQueue<TempContentPage>(ContextEnum.ContentPage, contentPage.ContentPageId);
                if (contentPageInQueue != null)// no records in queue for pending approval/draft
                {
                    MapContentPageQueueToContentPage(contentPage, contentPageInQueue);
                }

            }
            return GetContentResponse<ContentPage>(contentPage, userPermissions);
        }

        /// <summary>
        /// MapContentPageQueueToContentPage
        /// </summary>
        /// <param name="contentPage"></param>
        /// <param name="contentPageinQueue"></param>
        private void MapContentPageQueueToContentPage(ContentPage contentPage, TempContentPage contentPageInQueue)
        {
            Mapper.Map<TempContentPage, ContentPage>(contentPageInQueue, contentPage);
        }

        /// <summary>
        /// GetHomePage
        /// </summary>
        /// <returns></returns>
        public ContentPage GetHomePage()
        {
            ContentPage contentPage = null;
            SiteConfiguration siteConfiguration = _securityManager.GetSiteConfiguration();
            if (siteConfiguration != null && siteConfiguration.HomePageContentPageId != null)
            {
                contentPage = eCollabroDbContext.Repository<ContentPage>().Find(siteConfiguration.HomePageContentPageId.Value);
            }
            return contentPage;
        }

        /// <summary>
        /// GetContentPagesCountForCategories
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, int> GetContentPagesCountForCategories(List<int> contentPageCategoryIds, List<PermissionEnum> permissions)
        {
            Dictionary<int, int> contentPageCounts = new Dictionary<int, int>();
            var list = eCollabroDbContext.Repository<ContentPage>().Query(permissions).Filter(qry => contentPageCategoryIds.Contains(qry.ContentPageCategoryId)).Get().GroupBy(grp => grp.ContentPageCategoryId).Select(rec => new { rec.Key, ContentPageCount = rec.Count() });
            foreach (var countDetails in list)
            {
                contentPageCounts.Add(countDetails.Key, countDetails.ContentPageCount);
            }
            return contentPageCounts;
        }

        /// <summary>
        /// GetRecentContentPages
        /// </summary>
        /// <returns></returns>
        public List<ContentPage> GetRecentContentPages()
        {
            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.ContentPage);
            List<ContentPage> contentPages = eCollabroDbContext.Repository<ContentPage>().Query(userPermissions).Include(inc => inc.ContentPageCategory).Filter(qry => qry.ContentPageCategory.SiteId.Equals(UserContextDetails.SiteId)).Get().OrderBy(ord => ord.ContentPageId).Take(10).ToList();
            return contentPages;
        }


        /// <summary>
        /// GetContentPages
        /// </summary>
        /// <param name="contentPageCategoryId"></param>
        /// <returns></returns>
        public ContentPageCategory GetContentPages(int contentPageCategoryId)
        {
            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.ContentPage);
            ContentPageCategory contentPageCategory = eCollabroDbContext.Repository<ContentPageCategory>().Find(userPermissions, contentPageCategoryId);
            if (contentPageCategory == null)
                return GetContentResponse<ContentPageCategory>(contentPageCategory, userPermissions);
            else
            {
                eCollabroDbContext.Repository<ContentPageCategory>().Dettach(contentPageCategory);
                contentPageCategory.ContentPages = eCollabroDbContext.Repository<ContentPage>().Query(userPermissions).Filter(qry => qry.ContentPageCategoryId.Equals(contentPageCategoryId)).Get().ToList();
                if (userPermissions.Contains(PermissionEnum.ViewUnapprovedContent))
                {
                    List<int> contentPageIds = contentPageCategory.ContentPages.Select(slt => slt.ContentPageId).ToList();
                    List<TempContentPage> contentPagesInQueue = _workflowManager.GetFromQueue<TempContentPage>(ContextEnum.ContentPage, contentPageIds);

                    foreach (TempContentPage tempConttentPage in contentPagesInQueue)
                    {
                        ContentPage contentPage = contentPageCategory.ContentPages.Where(qry => qry.ContentPageId.Equals(tempConttentPage.ContentPageId)).FirstOrDefault();
                        if (contentPage != null)// no records in queue for pending approval/draft
                        {
                            MapContentPageQueueToContentPage(contentPage, tempConttentPage);
                        }
                    }
                }
            }
            return contentPageCategory;
        }


        /// <summary>
        /// SaveContentPage
        /// </summary>
        /// <param name="contentPage"></param>
        /// <returns></returns>
        public void SaveContentPage(ContentPage contentPage)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.ContentPage);
            if ((contentPage.ContentPageId.Equals(0) && !userPermissions.Contains(PermissionEnum.AddContent)) || (!contentPage.ContentPageId.Equals(0) && !userPermissions.Contains(PermissionEnum.EditContent)))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            //check contentSetting
            SecurityManager securityManager = new SecurityManager();
            List<SiteContentSettingResult> siteContentSettingResults = securityManager.GetSiteFeatureSettings(FeatureEnum.ContentPage);

            bool approvalRequired = siteContentSettingResults.Where(qry => qry.ContentSettingId.Equals((int)FeatureSettingEnum.ApprovalRequired)).FirstOrDefault().IsAssigned;
            //self approved in case approval not required
            if (!approvalRequired)
            {
                contentPage.ApprovalStatus = WorkflowConstants.ApprovedStatus;
                contentPage.ApproveRejectDate = DateTime.UtcNow;
                contentPage.ApproveRejectById = UserContextDetails.UserId;
            }
            else
            {
                contentPage.ApprovalStatus = WorkflowConstants.ApprovalWaitingStatus;
                contentPage.ApproveRejectById = null;
                contentPage.ApproveRejectDate = null;
            }

            if (contentPage.ContentPageId.Equals(0)) // New ContentPage
            {
                contentPage.CreatedById = UserContextDetails.UserId;
                contentPage.CreatedOn = DateTime.UtcNow;
                if (approvalRequired)
                    contentPage.ApprovalStatus = WorkflowConstants.ApprovalWaitingStatus;

                eCollabroDbContext.Repository<ContentPage>().Insert(contentPage);
                eCollabroDbContext.Save();

                if (approvalRequired)
                {
                    _workflowManager.CreateWorkflowTask(ContextEnum.ContentPage, contentPage.ContentPageId, "New Content Page [" + contentPage.ContentPageTitle + "] ", "Content Page Description : " + contentPage.ContentPageDescription);
                }

            }
            else  // Update ContentPage
            {
                ContentPage oldContentPage = eCollabroDbContext.Repository<ContentPage>().Query().Filter(qry => qry.ContentPageId.Equals(contentPage.ContentPageId)).Get().FirstOrDefault();
                if (oldContentPage != null)
                {
                    if (approvalRequired && oldContentPage.ApprovalStatus.Equals(WorkflowConstants.ApprovedStatus)) // Save to Queue 
                    {
                        contentPage.ModifiedById = UserContextDetails.UserId;
                        contentPage.ModifiedOn = DateTime.UtcNow;
                        contentPage.ApprovalStatus = WorkflowConstants.ApprovalWaitingStatus;
                        TempContentPage tempContentPage = Mapper.Map<ContentPage, TempContentPage>(contentPage);
                        _workflowManager.SaveToQueue<TempContentPage>(tempContentPage, ContextEnum.ContentPage, tempContentPage.ContentPageId);
                        _workflowManager.CreateWorkflowTask(ContextEnum.ContentPage, contentPage.ContentPageId, "New Content Page [" + contentPage.ContentPageTitle + "] ", "Content Page Description : " + contentPage.ContentPageDescription);

                    }
                    else // Record is new and not in Queue 
                    {
                        oldContentPage.ContentPageTitle = contentPage.ContentPageTitle;
                        oldContentPage.ContentPageDescription = contentPage.ContentPageDescription;
                        oldContentPage.ContentPageContent = contentPage.ContentPageContent;
                        oldContentPage.ContentPageCategoryId = contentPage.ContentPageCategoryId;
                        oldContentPage.IsActive = contentPage.IsActive;
                        oldContentPage.IsAnomynousAccess = contentPage.IsAnomynousAccess;
                        oldContentPage.IsCommentsAllowed = contentPage.IsCommentsAllowed;
                        oldContentPage.IsLikeAllowed = contentPage.IsLikeAllowed;
                        oldContentPage.IsRatingAllowed = contentPage.IsRatingAllowed;
                        oldContentPage.IsVotingAllowed = contentPage.IsVotingAllowed;
                        oldContentPage.ModifiedById = UserContextDetails.UserId;
                        oldContentPage.ModifiedOn = DateTime.UtcNow;

                        oldContentPage.ApprovalStatus = contentPage.ApprovalStatus;
                        oldContentPage.ApproveRejectDate = contentPage.ApproveRejectDate;
                        oldContentPage.ApproveRejectById = contentPage.ApproveRejectById;
                        eCollabroDbContext.Save();
                        if (approvalRequired) // approval required // create or update task
                        {
                            _workflowManager.CreateWorkflowTask(ContextEnum.ContentPage, contentPage.ContentPageId, "New Content Page [" + contentPage.ContentPageTitle + "] ", "Content Page Description : " + contentPage.ContentPageDescription);
                        }
                    }
                }
                else
                {
                    throw new DBConcurrencyException();
                }
            }
        }

        /// <summary>
        /// DeleteContentPage
        /// </summary>
        /// <param name="ContentPageId"></param>
        /// <returns></returns>
        public void DeleteContentPage(int ContentPageId)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.ContentPage);
            if (!userPermissions.Contains(PermissionEnum.DeleteContent))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            ContentPage oldContentPage = eCollabroDbContext.Repository<ContentPage>().Find(ContentPageId);
            if (oldContentPage != null)
            {
                oldContentPage.IsDeleted = true;
                oldContentPage.ModifiedById = UserContextDetails.UserId;
                oldContentPage.ModifiedOn = DateTime.UtcNow;
                eCollabroDbContext.Save();
            }
            else
            {
                throw new DBConcurrencyException();
            }
        }

        /// <summary>
        /// ContentPageTaskUpdated - Event Handler Raised by WorkflowManager SaveTask 
        /// </summary>
        /// <param name="beforeUpdate"></param>
        /// <param name="afterUpdate"></param>
        public void ContentPageTaskUpdated(UserTask beforeUpdate, UserTask afterUpdate)
        {
            ContentPage contentPage = eCollabroDbContext.Repository<ContentPage>().Find(afterUpdate.ContexContentId);
            if (contentPage != null)
            {
                TempContentPage contentPageInQueue = _workflowManager.GetFromQueue<TempContentPage>(ContextEnum.ContentPage, contentPage.ContentPageId);
                if (contentPageInQueue != null)// record in queue for pending approval/draft
                {
                    contentPageInQueue.ApprovalStatus = afterUpdate.TaskStatus;
                    contentPageInQueue.ApproveRejectById = UserContextDetails.UserId;
                    contentPageInQueue.ApproveRejectDate = DateTime.UtcNow;
                    if (afterUpdate.TaskStatus.Equals(WorkflowConstants.ApprovedStatus))
                    {
                        MapContentPageQueueToContentPage(contentPage, contentPageInQueue);
                        _workflowManager.DeleteFromQueue(ContextEnum.ContentPage, contentPage.ContentPageId);
                    }
                }
                else
                {
                    contentPage.ApprovalStatus = afterUpdate.TaskStatus;
                    contentPage.ApproveRejectById = UserContextDetails.UserId;
                    contentPage.ApproveRejectDate = DateTime.UtcNow;
                }
                eCollabroDbContext.Save();
            }
        }

        #endregion

        #region Document

        /// <summary>
        /// GetDocumentLibrary
        /// </summary>
        /// <param name="documentLibraryId"></param>
        /// <returns></returns>
        public DocumentLibrary GetDocumentLibrary(int documentLibraryId)
        {
            // Check Permission 
            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.DocumentLibrary);

            DocumentLibrary documentLibrary = eCollabroDbContext.Repository<DocumentLibrary>().Find(userPermissions, documentLibraryId);
            return GetContentResponse<DocumentLibrary>(documentLibrary, userPermissions);
        }

        /// <summary>
        /// GetDocumentLibraries
        /// </summary>
        /// <returns></returns>
        public List<DocumentLibrary> GetDocumentLibraries()
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.DocumentLibrary);
            if (!(userPermissions.Contains(PermissionEnum.ViewContent) || userPermissions.Contains(PermissionEnum.ViewAnomynousContent)))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            List<DocumentLibrary> documentCategories = eCollabroDbContext.Repository<DocumentLibrary>().Query(userPermissions).Filter(qry => qry.SiteId.Equals(UserContextDetails.SiteId)).Get().ToList();

            List<int> documentLibraryIds = documentCategories.Select(qry => qry.DocumentLibraryId).ToList();
            Dictionary<int, int> documentCounts = GetDocumentsCountForCategories(documentLibraryIds, userPermissions);
            foreach (var documentCountDetails in documentCounts)
            {
                documentCategories.Where(qry => qry.DocumentLibraryId.Equals(documentCountDetails.Key)).FirstOrDefault().NumberOfDocuments = documentCountDetails.Value;
            }

            return documentCategories;
        }

        /// <summary>
        /// SaveDocumentLibrary
        /// </summary>
        /// <param name="documentLibrary"></param>
        /// <returns></returns>
        public void SaveDocumentLibrary(DocumentLibrary documentLibrary)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.DocumentLibrary);
            if ((documentLibrary.DocumentLibraryId.Equals(0) && !userPermissions.Contains(PermissionEnum.AddContent)) || (!documentLibrary.DocumentLibraryId.Equals(0) && !userPermissions.Contains(PermissionEnum.EditContent)))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            if (documentLibrary.DocumentLibraryId.Equals(0)) // New
            {
                documentLibrary.CreatedById = UserContextDetails.UserId;
                documentLibrary.CreatedOn = DateTime.UtcNow;
                documentLibrary.SiteId = UserContextDetails.SiteId;
                eCollabroDbContext.Repository<DocumentLibrary>().Insert(documentLibrary);
                eCollabroDbContext.Save();
            }
            else  // Update 
            {
                DocumentLibrary oldDocumentLibrary = eCollabroDbContext.Repository<DocumentLibrary>().Find(documentLibrary.DocumentLibraryId);
                if (oldDocumentLibrary != null)
                {
                    oldDocumentLibrary.DocumentLibraryName = documentLibrary.DocumentLibraryName;
                    oldDocumentLibrary.DocumentLibraryDescription = documentLibrary.DocumentLibraryDescription;
                    oldDocumentLibrary.IsActive = documentLibrary.IsActive;
                    oldDocumentLibrary.IsAnomynousAccess = documentLibrary.IsAnomynousAccess;
                    oldDocumentLibrary.ModifiedById = UserContextDetails.UserId;
                    oldDocumentLibrary.ModifiedOn = DateTime.UtcNow;
                    eCollabroDbContext.Save();
                }
                else
                {
                    throw new DBConcurrencyException();
                }
            }
        }

        /// <summary>
        /// DeleteDocumentLibrary
        /// </summary>
        /// <param name="documentLibraryId"></param>
        /// <returns></returns>
        public void DeleteDocumentLibrary(int documentLibraryId)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.DocumentLibrary);
            if (!userPermissions.Contains(PermissionEnum.DeleteContent))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            DocumentLibrary oldDocumentLibrary = eCollabroDbContext.Repository<DocumentLibrary>().Find(documentLibraryId);
            if (oldDocumentLibrary != null)
            {
                oldDocumentLibrary.IsDeleted = true;
                oldDocumentLibrary.ModifiedById = UserContextDetails.UserId;
                oldDocumentLibrary.ModifiedOn = DateTime.UtcNow;
                eCollabroDbContext.Save();
            }
            else
            {
                throw new DBConcurrencyException();
            }
        }

        /// <summary>
        /// GetDocument
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public Document GetDocument(int documentId)
        {
            #region // Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.DocumentLibrary);
            if (!(userPermissions.Contains(PermissionEnum.ViewContent) || userPermissions.Contains(PermissionEnum.ViewAnomynousContent)))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            Document document = eCollabroDbContext.Repository<Document>().Query(userPermissions).Include(inc => inc.FileObject).Filter(qry => qry.DocumentId.Equals(documentId)).Get().FirstOrDefault();

            if (document != null && userPermissions.Contains(PermissionEnum.ViewUnapprovedContent))
            {
                TempDocument documentinQueue = _workflowManager.GetFromQueue<TempDocument>(ContextEnum.Document, document.DocumentId);
                if (documentinQueue != null)// no records in queue for pending approval/draft
                {
                    MapDocumentQueueToDocument(document, documentinQueue);
                }
            }
            return GetContentResponse<Document>(document, userPermissions);
        }

        /// <summary>
        /// MapDocumentQueueToDocument
        /// </summary>
        /// <param name="document"></param>
        /// <param name="documentinQueue"></param>
        private void MapDocumentQueueToDocument(Document document, TempDocument documentinQueue)
        {
            document.DocumentTitle = documentinQueue.DocumentTitle;
            document.DocumentDescription = documentinQueue.DocumentDescription;
            document.DocumentLibraryId = documentinQueue.DocumentLibraryId;
            if (!string.IsNullOrEmpty(documentinQueue.DocumentFileName))
                document.DocumentFileName = documentinQueue.DocumentFileName;
            if (documentinQueue.DocumentFile != null)
            {
                if (document.FileObject == null)
                {
                    document.FileObject = new FileObject();
                    document.FileObject.CreatedById = documentinQueue.CreatedById;
                    document.FileObject.CreatedOn = documentinQueue.CreatedOn;
                }
                else
                {
                    document.FileObject.ModifiedById = documentinQueue.ModifiedById = document.ModifiedById;
                    document.FileObject.ModifiedOn = document.ModifiedOn;
                }
                document.FileObject.FileObjectData = documentinQueue.DocumentFile;
            }
            document.ModifiedOn = documentinQueue.ModifiedOn;
            document.ModifiedById = documentinQueue.ModifiedById;
            document.IsAnomynousAccess = documentinQueue.IsAnomynousAccess;
            document.IsCommentsAllowed = documentinQueue.IsCommentsAllowed;
            document.IsLikeAllowed = documentinQueue.IsLikeAllowed;
            document.IsRatingAllowed = documentinQueue.IsRatingAllowed;
            document.IsVotingAllowed = documentinQueue.IsVotingAllowed;
            document.IsActive = documentinQueue.IsActive;
            document.ApprovalStatus = documentinQueue.ApprovalStatus;
            document.ApproveRejectById = documentinQueue.ApproveRejectById;
            document.ApproveRejectDate = documentinQueue.ApproveRejectDate;
        }

        /// <summary>
        /// GetDocumentsCountForCategories
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, int> GetDocumentsCountForCategories(List<int> documentLibraryIds, List<PermissionEnum> permissions)
        {
            Dictionary<int, int> documentCounts = new Dictionary<int, int>();
            var list = eCollabroDbContext.Repository<Document>().Query(permissions).Filter(qry => documentLibraryIds.Contains(qry.DocumentLibraryId)).Get().GroupBy(grp => grp.DocumentLibraryId).Select(rec => new { rec.Key, DocumentCount = rec.Count() });
            foreach (var countDetails in list)
            {
                documentCounts.Add(countDetails.Key, countDetails.DocumentCount);
            }
            return documentCounts;
        }

        /// <summary>
        /// GetRecentDocuments
        /// </summary>
        /// <returns></returns>
        public List<Document> GetRecentDocuments()
        {
            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.DocumentLibrary);
            List<Document> documents = eCollabroDbContext.Repository<Document>().Query(userPermissions).Include(inc => inc.DocumentLibrary).Filter(qry => qry.DocumentLibrary.SiteId.Equals(UserContextDetails.SiteId)).Get().OrderBy(ord => ord.DocumentId).Take(10).ToList();
            return documents;
        }


        /// <summary>
        /// GetDocuments
        /// </summary>
        /// <param name="documentLibraryId"></param>
        /// <returns></returns>
        public DocumentLibrary GetDocuments(int documentLibraryId)
        {
            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.DocumentLibrary);
            DocumentLibrary documentLibrary = eCollabroDbContext.Repository<DocumentLibrary>().Find(userPermissions, documentLibraryId);
            if (documentLibrary == null)
                return GetContentResponse<DocumentLibrary>(documentLibrary, userPermissions);
            else
            {
                eCollabroDbContext.Repository<DocumentLibrary>().Dettach(documentLibrary);
                documentLibrary.Documents = eCollabroDbContext.Repository<Document>().Query(userPermissions).Filter(qry => qry.DocumentLibraryId.Equals(documentLibraryId)).Get().ToList();
                if (userPermissions.Contains(PermissionEnum.ViewUnapprovedContent))
                {
                    List<int> documentIds = documentLibrary.Documents.Select(slt => slt.DocumentId).ToList();
                    List<TempDocument> documentsInQueue = _workflowManager.GetFromQueue<TempDocument>(ContextEnum.Document, documentIds);

                    foreach (TempDocument tempDocument in documentsInQueue)
                    {
                        Document doc = documentLibrary.Documents.Where(qry => qry.DocumentId.Equals(tempDocument.DocumentId)).FirstOrDefault();
                        if (doc != null)// no records in queue for pending approval/draft
                        {
                            MapDocumentQueueToDocument(doc, tempDocument);
                        }
                    }
                }
            }
            return documentLibrary;
        }


        /// <summary>
        /// SaveDocument
        /// </summary>
        /// <param name="document"></param>
        /// <param name="fileData">file data if file need to be saved in DB</param>
        public void SaveDocument(Document document, byte[] fileData)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.DocumentLibrary);
            if ((document.DocumentId.Equals(0) && !userPermissions.Contains(PermissionEnum.AddContent)) || (!document.DocumentId.Equals(0) && !userPermissions.Contains(PermissionEnum.EditContent)))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            //check contentSetting
            SecurityManager securityManager = new SecurityManager();
            List<SiteContentSettingResult> siteContentSettingResults = securityManager.GetSiteFeatureSettings(FeatureEnum.DocumentLibrary);

            bool approvalRequired = siteContentSettingResults.Where(qry => qry.ContentSettingId.Equals((int)FeatureSettingEnum.ApprovalRequired)).FirstOrDefault().IsAssigned;
            //self approved in case approval not required
            if (!approvalRequired)
            {
                document.ApprovalStatus = WorkflowConstants.ApprovedStatus;
                document.ApproveRejectDate = DateTime.UtcNow;
                document.ApproveRejectById = UserContextDetails.UserId;
            }
            else
            {
                document.ApprovalStatus = WorkflowConstants.ApprovalWaitingStatus;
                document.ApproveRejectById = null;
                document.ApproveRejectDate = null;
            }

            if (document.DocumentId.Equals(0)) // New Document
            {
                AddDocument(document, fileData, approvalRequired);
            }
            else  // Update Document
            {
                UpdateDocument(document, fileData, approvalRequired);
            }
        }

        /// <summary>
        /// UpdateDocument
        /// </summary>
        /// <param name="document"></param>
        /// <param name="fileData"></param>
        /// <param name="approvalRequired"></param>
        private void UpdateDocument(Document document, byte[] fileData, bool approvalRequired)
        {
            Document oldDocument = eCollabroDbContext.Repository<Document>().Query().Include(inc => inc.FileObject).Filter(qry => qry.DocumentId.Equals(document.DocumentId)).Get().FirstOrDefault();
            if (oldDocument != null)
            {
                if (approvalRequired && oldDocument.ApprovalStatus.Equals(WorkflowConstants.ApprovedStatus)) // Save to Queue 
                {
                    document.ModifiedById = UserContextDetails.UserId;
                    document.ModifiedOn = DateTime.UtcNow;
                    document.ApprovalStatus = WorkflowConstants.ApprovalWaitingStatus;
                    TempDocument tempDocument = Mapper.Map<Document, TempDocument>(document);
                    if (fileData != null)
                    {
                        tempDocument.DocumentFile = fileData;
                    }
                    _workflowManager.SaveToQueue<TempDocument>(tempDocument, ContextEnum.Document, document.DocumentId);
                    _workflowManager.CreateWorkflowTask(ContextEnum.Document, document.DocumentId, "New Document [" + document.DocumentTitle + "] ", "Document Description : " + document.DocumentDescription);
                }
                else // Record is new and not in Queue 
                {
                    oldDocument.DocumentTitle = document.DocumentTitle;
                    oldDocument.DocumentDescription = document.DocumentDescription;
                    if (!string.IsNullOrEmpty(document.DocumentFileName))
                        oldDocument.DocumentFileName = document.DocumentFileName;
                    if (fileData != null)
                    {
                        if (oldDocument.FileObject != null)
                        {
                            oldDocument.FileObject.ModifiedById = UserContextDetails.UserId;
                            oldDocument.FileObject.ModifiedOn = DateTime.UtcNow;
                        }
                        else
                        {
                            oldDocument.FileObject = new FileObject();
                            oldDocument.FileObject.CreatedOn = DateTime.UtcNow;
                            oldDocument.FileObject.CreatedById = UserContextDetails.UserId;
                        }
                        oldDocument.FileObject.FileObjectData = fileData;
                    }
                    oldDocument.DocumentLibraryId = document.DocumentLibraryId;
                    oldDocument.IsActive = document.IsActive;
                    oldDocument.ModifiedById = UserContextDetails.UserId;
                    oldDocument.ModifiedOn = DateTime.UtcNow;
                    oldDocument.IsAnomynousAccess = document.IsAnomynousAccess;
                    oldDocument.IsCommentsAllowed = document.IsCommentsAllowed;
                    oldDocument.IsLikeAllowed = document.IsLikeAllowed;
                    oldDocument.IsRatingAllowed = document.IsRatingAllowed;
                    oldDocument.IsVotingAllowed = document.IsVotingAllowed;
                    oldDocument.ApprovalStatus = document.ApprovalStatus;
                    oldDocument.ApproveRejectDate = document.ApproveRejectDate;
                    oldDocument.ApproveRejectById = document.ApproveRejectById;
                    eCollabroDbContext.Save();
                    if (approvalRequired) // approval required // create or update task
                    {
                        _workflowManager.CreateWorkflowTask(ContextEnum.Document, document.DocumentId, "New Document [" + document.DocumentTitle + "] ", "Document Description : " + document.DocumentDescription);
                    }
                }
            }
            else
            {
                throw new DBConcurrencyException();
            }
        }

        /// <summary>
        /// AddDocument
        /// </summary>
        /// <param name="document"></param>
        /// <param name="fileData"></param>
        /// <param name="approvalRequired"></param>
        private void AddDocument(Document document, byte[] fileData, bool approvalRequired)
        {
            document.CreatedById = UserContextDetails.UserId;
            document.CreatedOn = DateTime.UtcNow;
            if (approvalRequired)
                document.ApprovalStatus = WorkflowConstants.ApprovalWaitingStatus;

            if (fileData != null)
            {
                document.FileObject = new FileObject();
                document.FileObject.CreatedById = UserContextDetails.UserId;
                document.FileObject.CreatedOn = DateTime.UtcNow;
                document.FileObject.ContextId = (int)ContextEnum.Document;
                document.FileObject.FileObjectData = fileData;
            }
            eCollabroDbContext.Repository<Document>().Insert(document);
            eCollabroDbContext.Save();

            if (approvalRequired)
            {
                _workflowManager.CreateWorkflowTask(ContextEnum.Document, document.DocumentId, "New Document [" + document.DocumentTitle + "] ", "Document Description : " + document.DocumentDescription);
            }
        }

        /// <summary>
        /// DeleteDocument
        /// </summary>
        /// <param name="DocumentId"></param>
        /// <returns></returns>
        public void DeleteDocument(int DocumentId)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.DocumentLibrary);
            if (!userPermissions.Contains(PermissionEnum.DeleteContent))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            Document oldDocument = eCollabroDbContext.Repository<Document>().Find(DocumentId);
            if (oldDocument != null)
            {
                oldDocument.IsDeleted = true;
                oldDocument.ModifiedById = UserContextDetails.UserId;
                oldDocument.ModifiedOn = DateTime.UtcNow;
                eCollabroDbContext.Save();
            }
            else
            {
                throw new DBConcurrencyException();
            }
        }

        /// <summary>
        /// DocumentTaskUpdated - Event Handler Raised by WorkflowManager SaveTask 
        /// </summary>
        /// <param name="beforeUpdate"></param>
        /// <param name="afterUpdate"></param>
        public void DocumentTaskUpdated(UserTask beforeUpdate, UserTask afterUpdate)
        {
            Document document = eCollabroDbContext.Repository<Document>().Query().Include(inc => inc.FileObject).Filter(qry => qry.DocumentId.Equals(afterUpdate.ContexContentId)).Get().FirstOrDefault();
            if (document != null)
            {
                TempDocument documentInQueue = _workflowManager.GetFromQueue<TempDocument>(ContextEnum.Document, document.DocumentId);
                if (documentInQueue != null)// record in queue for pending approval/draft
                {
                    documentInQueue.ApprovalStatus = afterUpdate.TaskStatus;
                    documentInQueue.ApproveRejectById = UserContextDetails.UserId;
                    documentInQueue.ApproveRejectDate = DateTime.UtcNow;
                    if (afterUpdate.TaskStatus.Equals(WorkflowConstants.ApprovedStatus))
                    {
                        MapDocumentQueueToDocument(document, documentInQueue);
                        _workflowManager.DeleteFromQueue(ContextEnum.Document, document.DocumentId);
                    }
                }
                else
                {
                    document.ApprovalStatus = afterUpdate.TaskStatus;
                    document.ApproveRejectById = UserContextDetails.UserId;
                    document.ApproveRejectDate = DateTime.UtcNow;
                }
                eCollabroDbContext.Save();
            }
        }

        #endregion

        #region Image Gallery

        /// <summary>
        /// GetImageGallery
        /// </summary>
        /// <param name="imageGalleryId"></param>
        /// <returns></returns>
        public ImageGallery GetImageGallery(int imageGalleryId)
        {
            // Check Permission 
            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.ImageGallery);

            ImageGallery imageGallery = eCollabroDbContext.Repository<ImageGallery>().Find(userPermissions, imageGalleryId);
            return GetContentResponse<ImageGallery>(imageGallery, userPermissions);
        }

        /// <summary>
        /// GetImageGalleries
        /// </summary>
        /// <returns></returns>
        public List<ImageGallery> GetImageGalleries()
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.ImageGallery);
            if (!(userPermissions.Contains(PermissionEnum.ViewContent) || userPermissions.Contains(PermissionEnum.ViewAnomynousContent)))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            List<ImageGallery> siteImageCategories = eCollabroDbContext.Repository<ImageGallery>().Query(userPermissions).Filter(qry => qry.SiteId.Equals(UserContextDetails.SiteId)).Get().ToList();

            List<int> imageGalleryIds = siteImageCategories.Select(qry => qry.ImageGalleryId).ToList();
            Dictionary<int, int> siteImageCounts = GetImageCountForGalleries(imageGalleryIds, userPermissions);
            foreach (var siteImageCountDetails in siteImageCounts)
            {
                siteImageCategories.Where(qry => qry.ImageGalleryId.Equals(siteImageCountDetails.Key)).FirstOrDefault().NumberOfImages = siteImageCountDetails.Value;
            }

            return siteImageCategories;
        }

        /// <summary>
        /// SaveImageGallery
        /// </summary>
        /// <param name="imageGallery"></param>
        /// <returns></returns>
        public void SaveImageGallery(ImageGallery imageGallery)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.ImageGallery);
            if ((imageGallery.ImageGalleryId.Equals(0) && !userPermissions.Contains(PermissionEnum.AddContent)) || (!imageGallery.ImageGalleryId.Equals(0) && !userPermissions.Contains(PermissionEnum.EditContent)))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            if (imageGallery.ImageGalleryId.Equals(0)) // New
            {
                imageGallery.CreatedById = UserContextDetails.UserId;
                imageGallery.CreatedOn = DateTime.UtcNow;
                imageGallery.SiteId = UserContextDetails.SiteId;
                eCollabroDbContext.Repository<ImageGallery>().Insert(imageGallery);
                eCollabroDbContext.Save();
            }
            else  // Update 
            {
                ImageGallery oldImageGallery = eCollabroDbContext.Repository<ImageGallery>().Find(imageGallery.ImageGalleryId);
                if (oldImageGallery != null)
                {
                    oldImageGallery.ImageGalleryName = imageGallery.ImageGalleryName;
                    oldImageGallery.ImageGalleryDescription = imageGallery.ImageGalleryDescription;
                    oldImageGallery.IsActive = imageGallery.IsActive;
                    oldImageGallery.IsAnomynousAccess = imageGallery.IsAnomynousAccess;
                    oldImageGallery.ModifiedById = UserContextDetails.UserId;
                    oldImageGallery.ModifiedOn = DateTime.UtcNow;
                    eCollabroDbContext.Save();
                }
                else
                {
                    throw new DBConcurrencyException();
                }
            }
        }

        /// <summary>
        /// DeleteImageGallery
        /// </summary>
        /// <param name="imageGalleryId"></param>
        /// <returns></returns>
        public void DeleteImageGallery(int imageGalleryId)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.ImageGallery);
            if (!userPermissions.Contains(PermissionEnum.DeleteContent))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            ImageGallery oldImageGallery = eCollabroDbContext.Repository<ImageGallery>().Find(imageGalleryId);
            if (oldImageGallery != null)
            {
                oldImageGallery.IsDeleted = true;
                oldImageGallery.ModifiedById = UserContextDetails.UserId;
                oldImageGallery.ModifiedOn = DateTime.UtcNow;
                eCollabroDbContext.Save();
            }
            else
            {
                throw new DBConcurrencyException();
            }
        }

        /// <summary>
        /// GetImage
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        public SiteImage GetImage(int imageId)
        {
            #region // Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.ImageGallery);
            if (!(userPermissions.Contains(PermissionEnum.ViewContent) || userPermissions.Contains(PermissionEnum.ViewAnomynousContent)))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            SiteImage image = eCollabroDbContext.Repository<SiteImage>().Query(userPermissions).Filter(qry => qry.ImageId.Equals(imageId)).Include(inc => inc.ImageObject).Get().FirstOrDefault();

            if (image != null && userPermissions.Contains(PermissionEnum.ViewUnapprovedContent))
            {
                TempSiteImage imageInQueue = _workflowManager.GetFromQueue<TempSiteImage>(ContextEnum.Image, image.ImageId);
                if (imageInQueue != null)// no records in queue for pending approval/draft
                {
                    MapImageQueueToImage(image, imageInQueue);
                }
            }
            return GetContentResponse<SiteImage>(image, userPermissions);
        }

        /// <summary>
        /// MapImageQueueToImage
        /// </summary>
        /// <param name="image"></param>
        /// <param name="imageinQueue"></param>
        private void MapImageQueueToImage(SiteImage image, TempSiteImage imageinQueue)
        {
            image.ImageTitle = imageinQueue.ImageTitle;
            image.ImageDescription = imageinQueue.ImageDescription;
            image.ImageGalleryId = imageinQueue.ImageGalleryId;
            if (!string.IsNullOrEmpty(imageinQueue.ImageFileName))
                image.ImageFileName = imageinQueue.ImageFileName;
            if (imageinQueue.ImageFile != null)
            {
                if (image.ImageObject == null)
                {
                    image.ImageObject = new ImageObject();
                    image.ImageObject.CreatedById = imageinQueue.CreatedById;
                    image.ImageObject.CreatedOn = imageinQueue.CreatedOn;
                }
                else
                {
                    image.ImageObject.ModifiedById = imageinQueue.ModifiedById;
                    image.ImageObject.ModifiedOn = imageinQueue.ModifiedOn;
                }
                image.ImageObject.ImageObjectData = imageinQueue.ImageFile;
                image.ImageObject.ImageThumbnailData = ImageUtility.CreateThumbnail(imageinQueue.ImageFile, 60);
            }
            image.IsAnomynousAccess = imageinQueue.IsAnomynousAccess;
            image.IsCommentsAllowed = imageinQueue.IsCommentsAllowed;
            image.IsLikeAllowed = imageinQueue.IsLikeAllowed;
            image.IsRatingAllowed = imageinQueue.IsRatingAllowed;
            image.IsVotingAllowed = imageinQueue.IsVotingAllowed;
            image.ModifiedOn = imageinQueue.ModifiedOn;
            image.ModifiedById = imageinQueue.ModifiedById;
            image.IsActive = imageinQueue.IsActive;
            image.ApprovalStatus = imageinQueue.ApprovalStatus;
            image.ApproveRejectById = imageinQueue.ApproveRejectById;
            image.ApproveRejectDate = imageinQueue.ApproveRejectDate;
        }

        /// <summary>
        /// GetImageCountForGalleries
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, int> GetImageCountForGalleries(List<int> imageGalleryIds, List<PermissionEnum> permissions)
        {
            Dictionary<int, int> imageCounts = new Dictionary<int, int>();
            var list = eCollabroDbContext.Repository<SiteImage>().Query(permissions).Filter(qry => imageGalleryIds.Contains(qry.ImageGalleryId)).Get().GroupBy(grp => grp.ImageGalleryId).Select(rec => new { rec.Key, imageCount = rec.Count() });
            foreach (var countDetails in list)
            {
                imageCounts.Add(countDetails.Key, countDetails.imageCount);
            }
            return imageCounts;
        }

        /// <summary>
        /// GetRecentImages
        /// </summary>
        /// <returns></returns>
        public List<SiteImage> GetRecentImages()
        {
            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.ImageGallery);
            List<SiteImage> recentImages = eCollabroDbContext.Repository<SiteImage>().Query(userPermissions).Include(inc => inc.ImageGallery).Filter(qry => qry.ImageGallery.SiteId.Equals(UserContextDetails.SiteId)).Get().OrderBy(ord => ord.ImageId).Take(10).ToList();
            return recentImages;
        }


        /// <summary>
        /// GetImages
        /// </summary>
        /// <param name="imageGalleryId"></param>
        /// <returns></returns>
        public ImageGallery GetImages(int imageGalleryId)
        {
            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.ImageGallery);
            ImageGallery imageGallery = eCollabroDbContext.Repository<ImageGallery>().Find(userPermissions, imageGalleryId);
            if (imageGallery == null)
                return GetContentResponse<ImageGallery>(imageGallery, userPermissions);
            else
            {
                eCollabroDbContext.Repository<ImageGallery>().Dettach(imageGallery);
                imageGallery.SiteImages = eCollabroDbContext.Repository<SiteImage>().Query(userPermissions).Filter(qry => qry.ImageGalleryId.Equals(imageGalleryId)).Get().ToList();
                if (userPermissions.Contains(PermissionEnum.ViewUnapprovedContent))
                {
                    List<int> imageIds = imageGallery.SiteImages.Select(slt => slt.ImageId).ToList();
                    List<TempSiteImage> imagesInQueue = _workflowManager.GetFromQueue<TempSiteImage>(ContextEnum.Image, imageIds);

                    foreach (TempSiteImage tempImage in imagesInQueue)
                    {
                        SiteImage image = imageGallery.SiteImages.Where(qry => qry.ImageId.Equals(tempImage.ImageId)).FirstOrDefault();
                        if (image != null)// no records in queue for pending approval/draft
                        {
                            MapImageQueueToImage(image, tempImage);
                        }
                    }
                }
            }
            return imageGallery;
        }


        /// <summary>
        /// SaveImage
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public void SaveImage(SiteImage image, byte[] fileData)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.ImageGallery);
            if ((image.ImageId.Equals(0) && !userPermissions.Contains(PermissionEnum.AddContent)) || (!image.ImageId.Equals(0) && !userPermissions.Contains(PermissionEnum.EditContent)))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            //check contentSetting
            SecurityManager securityManager = new SecurityManager();
            List<SiteContentSettingResult> siteContentSettingResults = securityManager.GetSiteFeatureSettings(FeatureEnum.ImageGallery);

            bool approvalRequired = siteContentSettingResults.Where(qry => qry.ContentSettingId.Equals((int)FeatureSettingEnum.ApprovalRequired)).FirstOrDefault().IsAssigned;
            //self approved in case approval not required
            if (!approvalRequired)
            {
                image.ApprovalStatus = WorkflowConstants.ApprovedStatus;
                image.ApproveRejectDate = DateTime.UtcNow;
                image.ApproveRejectById = UserContextDetails.UserId;
            }
            else
            {
                image.ApprovalStatus = WorkflowConstants.ApprovalWaitingStatus;
                image.ApproveRejectById = null;
                image.ApproveRejectDate = null;
            }

            if (image.ImageId.Equals(0)) // New SiteImage
            {
                AddImage(image, fileData, approvalRequired);

            }
            else  // Update SiteImage
            {
                UpdateImage(image, fileData, approvalRequired);
            }
        }

        /// <summary>
        /// AddImage
        /// </summary>
        /// <param name="image"></param>
        /// <param name="fileData"></param>
        /// <param name="approvalRequired"></param>
        private void AddImage(SiteImage image, byte[] fileData, bool approvalRequired)
        {
            image.CreatedById = UserContextDetails.UserId;
            image.CreatedOn = DateTime.UtcNow;
            if (approvalRequired)
                image.ApprovalStatus = WorkflowConstants.ApprovalWaitingStatus;

            if (fileData != null)
            {
                image.ImageObject = new ImageObject();
                image.ImageObject.CreatedById = UserContextDetails.UserId;
                image.ImageObject.CreatedOn = DateTime.UtcNow;
                image.ImageObject.ContextId = (int)ContextEnum.Image;
                image.ImageObject.ImageObjectData = fileData;
                image.ImageObject.ImageThumbnailData = ImageUtility.CreateThumbnail(fileData, 60);
            }
            eCollabroDbContext.Repository<SiteImage>().Insert(image);
            eCollabroDbContext.Save();

            if (approvalRequired)
            {
                _workflowManager.CreateWorkflowTask(ContextEnum.Image, image.ImageId, "New Image [" + image.ImageTitle + "] ", "Image Description : " + image.ImageDescription);
            }
        }

        /// <summary>
        /// UpdateImage
        /// </summary>
        /// <param name="image"></param>
        /// <param name="fileData"></param>
        /// <param name="approvalRequired"></param>
        private void UpdateImage(SiteImage image, byte[] fileData, bool approvalRequired)
        {
            SiteImage oldImage = eCollabroDbContext.Repository<SiteImage>().Find(image.ImageId);
            if (oldImage != null)
            {
                if (approvalRequired && oldImage.ApprovalStatus.Equals(WorkflowConstants.ApprovedStatus)) // Save to Queue 
                {
                    image.ModifiedById = UserContextDetails.UserId;
                    image.ModifiedOn = DateTime.UtcNow;
                    image.ApprovalStatus = WorkflowConstants.ApprovalWaitingStatus;
                    TempSiteImage tempImage = Mapper.Map<SiteImage, TempSiteImage>(image);
                    if (fileData != null)
                    {
                        tempImage.ImageFile = fileData;
                    }
                    _workflowManager.SaveToQueue<TempSiteImage>(tempImage, ContextEnum.Image, image.ImageId);
                    _workflowManager.CreateWorkflowTask(ContextEnum.Image, image.ImageId, "New Image [" + image.ImageTitle + "] ", "Image Description : " + image.ImageDescription);

                }
                else // Record is new and not in Queue 
                {
                    oldImage.ImageTitle = image.ImageTitle;
                    oldImage.ImageDescription = image.ImageDescription;
                    if (!string.IsNullOrEmpty(image.ImageFileName))
                        oldImage.ImageFileName = image.ImageFileName;
                    if (fileData != null)
                    {
                        if (oldImage.ImageObject != null)
                        {
                            oldImage.ImageObject.ModifiedById = UserContextDetails.UserId;
                            oldImage.ImageObject.ModifiedOn = DateTime.UtcNow;
                        }
                        else
                        {
                            oldImage.ImageObject = new ImageObject();
                            oldImage.ImageObject.CreatedOn = DateTime.UtcNow;
                            oldImage.ImageObject.CreatedById = UserContextDetails.UserId;
                        }
                        oldImage.ImageObject.ImageObjectData = fileData;
                        oldImage.ImageObject.ImageThumbnailData = ImageUtility.CreateThumbnail(fileData, 60);
                    }
                    oldImage.ImageGalleryId = image.ImageGalleryId;
                    oldImage.IsActive = image.IsActive;
                    oldImage.ModifiedById = UserContextDetails.UserId;
                    oldImage.ModifiedOn = DateTime.UtcNow;
                    oldImage.IsAnomynousAccess = image.IsAnomynousAccess;
                    oldImage.IsCommentsAllowed = image.IsCommentsAllowed;
                    oldImage.IsLikeAllowed = image.IsLikeAllowed;
                    oldImage.IsRatingAllowed = image.IsRatingAllowed;
                    oldImage.IsVotingAllowed = image.IsVotingAllowed;
                    oldImage.ApprovalStatus = image.ApprovalStatus;
                    oldImage.ApproveRejectDate = image.ApproveRejectDate;
                    oldImage.ApproveRejectById = image.ApproveRejectById;
                    eCollabroDbContext.Save();
                    if (approvalRequired) // approval required // create or update task
                    {
                        _workflowManager.CreateWorkflowTask(ContextEnum.Image, image.ImageId, "New Image [" + image.ImageTitle + "] ", "Image Description : " + image.ImageDescription);
                    }
                }
            }
            else
            {
                throw new DBConcurrencyException();
            }
        }

        /// <summary>
        /// DeleteImage
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        public void DeleteImage(int imageId)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.ImageGallery);
            if (!userPermissions.Contains(PermissionEnum.DeleteContent))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            SiteImage oldImage = eCollabroDbContext.Repository<SiteImage>().Find(imageId);
            if (oldImage != null)
            {
                oldImage.IsDeleted = true;
                oldImage.ModifiedById = UserContextDetails.UserId;
                oldImage.ModifiedOn = DateTime.UtcNow;
                eCollabroDbContext.Save();
            }
            else
            {
                throw new DBConcurrencyException();
            }
        }

        /// <summary>
        /// ImageTaskUpdated - Event Handler Raised by WorkflowManager SaveTask 
        /// </summary>
        /// <param name="beforeUpdate"></param>
        /// <param name="afterUpdate"></param>
        public void ImageTaskUpdated(UserTask beforeUpdate, UserTask afterUpdate)
        {
            SiteImage siteImage = eCollabroDbContext.Repository<SiteImage>().Query().Filter(qry => qry.ImageId.Equals(afterUpdate.ContexContentId)).Include(inc => inc.ImageObject).Get().FirstOrDefault();
            if (siteImage != null)
            {
                TempSiteImage siteImageInQueue = _workflowManager.GetFromQueue<TempSiteImage>(ContextEnum.Image, siteImage.ImageId);
                if (siteImageInQueue != null)// record in queue for pending approval/draft
                {
                    siteImageInQueue.ApprovalStatus = afterUpdate.TaskStatus;
                    siteImageInQueue.ApproveRejectById = UserContextDetails.UserId;
                    siteImageInQueue.ApproveRejectDate = DateTime.UtcNow;
                    if (afterUpdate.TaskStatus.Equals(WorkflowConstants.ApprovedStatus))
                    {
                        MapImageQueueToImage(siteImage, siteImageInQueue);
                        _workflowManager.DeleteFromQueue(ContextEnum.Image, siteImage.ImageId);
                    }
                }
                else
                {
                    siteImage.ApprovalStatus = afterUpdate.TaskStatus;
                    siteImage.ApproveRejectById = UserContextDetails.UserId;
                    siteImage.ApproveRejectDate = DateTime.UtcNow;
                }
                eCollabroDbContext.Save();
            }
        }

        #endregion

        #region Announcement


        /// <summary>
        /// GetAnnouncement
        /// </summary>
        /// <param name="announcementId"></param>
        /// <returns></returns>
        public Announcement GetAnnouncement(int announcementId)
        {
            #region // Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.Announcement);
            if (!(userPermissions.Contains(PermissionEnum.ViewContent) || userPermissions.Contains(PermissionEnum.ViewAnomynousContent)))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            Announcement announcement = eCollabroDbContext.Repository<Announcement>().Find(userPermissions, announcementId);
            return GetContentResponse<Announcement>(announcement, userPermissions);
        }

        /// <summary>
        /// GetRecentAnnouncements
        /// </summary>
        /// <returns></returns>
        public List<Announcement> GetRecentAnnouncements()
        {
            RequestContextParameter requestParameter = RequestContext.Current.Get<RequestContextParameter>("RequestParameter");
            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.Announcement);
            List<Announcement> announcements = eCollabroDbContext.Repository<Announcement>().Query(userPermissions).Filter(qry => qry.SiteId.Equals(UserContextDetails.SiteId) && qry.IsActive && ((qry.ExpiryDate.HasValue && qry.ExpiryDate <= DateTime.UtcNow) || !qry.ExpiryDate.HasValue)).Get().OrderByDescending(ord => ord.AnnouncementId).Take(requestParameter.PageSize == 0 ? 10 : requestParameter.PageSize).ToList();
            return announcements;
        }


        public List<Announcement> GetAnnouncements(DateTime? fromDate, DateTime? toDate, bool expired)
        {
            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.Announcement);

            List<Announcement> announcements = eCollabroDbContext.Repository<Announcement>().Query(userPermissions).Filter(qry => qry.SiteId.Equals(UserContextDetails.SiteId)).Get().ToList();
            return announcements;
        }


        /// <summary>
        /// SaveAnnouncement
        /// </summary>
        /// <param name="announcement"></param>
        /// <returns></returns>
        public void SaveAnnouncement(Announcement announcement)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.Announcement);
            if ((announcement.AnnouncementId.Equals(0) && !userPermissions.Contains(PermissionEnum.AddContent)) || (!announcement.AnnouncementId.Equals(0) && !userPermissions.Contains(PermissionEnum.EditContent)))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            if (announcement.AnnouncementId.Equals(0)) // New Announcement
            {
                announcement.CreatedById = UserContextDetails.UserId;
                announcement.CreatedOn = DateTime.UtcNow;
                announcement.SiteId = UserContextDetails.SiteId;
                eCollabroDbContext.Repository<Announcement>().Insert(announcement);
                eCollabroDbContext.Save();
            }
            else  // Update Announcement
            {
                Announcement oldAnnouncement = eCollabroDbContext.Repository<Announcement>().Find(announcement.AnnouncementId);
                if (oldAnnouncement != null)
                {
                    oldAnnouncement.AnnouncementTitle = announcement.AnnouncementTitle;
                    oldAnnouncement.AnnouncementDescription = announcement.AnnouncementDescription;
                    oldAnnouncement.ExpiryDate = announcement.ExpiryDate;
                    oldAnnouncement.IsActive = announcement.IsActive;
                    oldAnnouncement.IsAnomynousAccess = announcement.IsAnomynousAccess;
                    oldAnnouncement.IsCommentsAllowed = announcement.IsCommentsAllowed;
                    oldAnnouncement.IsLikeAllowed = announcement.IsLikeAllowed;
                    oldAnnouncement.IsRatingAllowed = announcement.IsRatingAllowed;
                    oldAnnouncement.IsVotingAllowed = announcement.IsVotingAllowed;
                    oldAnnouncement.ModifiedById = UserContextDetails.UserId;
                    oldAnnouncement.ModifiedOn = DateTime.UtcNow;
                    eCollabroDbContext.Save();
                }
                else
                {
                    throw new DBConcurrencyException();
                }
            }
        }

        /// <summary>
        /// DeleteAnnouncement
        /// </summary>
        /// <param name="announcementId"></param>
        /// <returns></returns>
        public void DeleteAnnouncement(int announcementId)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = _securityManager.GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.Announcement);
            if (!userPermissions.Contains(PermissionEnum.DeleteContent))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            Announcement oldAnnouncement = eCollabroDbContext.Repository<Announcement>().Find(announcementId);
            if (oldAnnouncement != null)
            {
                oldAnnouncement.IsDeleted = true;
                oldAnnouncement.ModifiedById = UserContextDetails.UserId;
                oldAnnouncement.ModifiedOn = DateTime.UtcNow;
                eCollabroDbContext.Save();
            }
            else
            {
                throw new DBConcurrencyException();
            }
        }

        #endregion

        #region Content Comment

        /// <summary>
        /// GetContentComment
        /// </summary>
        /// <param name="contextId"></param>
        /// <param name="contextId"></param>
        /// <returns></returns>
        public ContentCommentData GetContentComments(ContextEnum context, int contextContentId)
        {
            ContentCommentData contentCommentData = new ContentCommentData();
            contentCommentData.ContentComments = eCollabroDbContext.Repository<ContentComment>().Query().Filter(qry => qry.ContextId.Equals((int)context) && qry.ContextContentId.Equals(contextContentId)).Get().ToList();

            SecurityManager securityManager = new SecurityManager();
            List<UserMembership> users = securityManager.GetUsers(contentCommentData.ContentComments.Select(fld => fld.CreatedById).ToList());
            foreach (ContentComment contentComment in contentCommentData.ContentComments)
            {
                UserMembership user = users.Where(qry => qry.UserId.Equals(contentComment.CreatedById)).FirstOrDefault();
                contentComment.CreatedBy = user == null ? "Unknown" : user.UserName;
                contentComment.TimeInterval = CommonFunctions.GetTimeInterval(contentComment.CreatedOn);
            }

            contentCommentData.NumberOfLikes = eCollabroDbContext.Repository<ContentLikeDislike>().Query().Get().Count();
            contentCommentData.NumberOfVotes = eCollabroDbContext.Repository<ContentVote>().Query().Get().Count();
            int totalRates = eCollabroDbContext.Repository<ContentRating>().Query().Get().Count();
            if (totalRates != 0)
                contentCommentData.AverageRatings =Math.Round(eCollabroDbContext.Repository<ContentRating>().Query().Get().Sum(op => op.Rating) / totalRates,2);
            contentCommentData.UserLiked = eCollabroDbContext.Repository<ContentLikeDislike>().Query().Filter(op => op.CreatedById.Equals(UserContextDetails.UserId)).Get().Any();
            ContentRating userRating = eCollabroDbContext.Repository<ContentRating>().Query().Filter(op => op.CreatedById.Equals(UserContextDetails.UserId)).Get().FirstOrDefault();
            if (userRating != null)
                contentCommentData.UserRating =Convert.ToInt32(userRating.Rating);
            contentCommentData.UserVoted = eCollabroDbContext.Repository<ContentVote>().Query().Filter(op => op.CreatedById.Equals(UserContextDetails.UserId)).Get().Any();
            return contentCommentData;
        }

        /// <summary>
        /// SaveContentComment
        /// </summary>
        /// <param name="contentComment"></param>
        public void SaveContentComment(ContentComment contentComment)
        {
            if (contentComment.ContentCommentId.Equals(0)) // new comment
            {
                contentComment.CreatedById = UserContextDetails.UserId;
                contentComment.CreatedOn = DateTime.UtcNow;
                eCollabroDbContext.Repository<ContentComment>().Insert(contentComment);
                //for now self approved
                contentComment.ApprovalStatus = WorkflowConstants.ApprovedStatus;
                contentComment.ApproveRejectById = UserContextDetails.UserId;
                contentComment.ApproveRejectDate = DateTime.UtcNow;
                eCollabroDbContext.Save();
            }
            else
            {
                ContentComment oldContentComment = eCollabroDbContext.Repository<ContentComment>().Find(contentComment.ContentCommentId);
                if (oldContentComment != null)
                {
                    oldContentComment.Comment = contentComment.Comment;
                    oldContentComment.ModifiedById = UserContextDetails.UserId;
                    oldContentComment.ModifiedOn = DateTime.UtcNow;
                    eCollabroDbContext.Save();
                }
                else
                {
                    throw new DBConcurrencyException();
                }
            }
        }

        /// <summary>
        /// DeleteContentComment
        /// </summary>
        /// <param name="contentCommentId"></param>
        public void DeleteContentComment(int contentCommentId)
        {
            eCollabroDbContext.Repository<ContentComment>().Delete(contentCommentId);
            eCollabroDbContext.Save();
        }


        /// <summary>
        /// ChangeContentLikeDislike
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="contextId"></param>
        /// <param name="liked"></param>
        public void ChangeContentLikeDislike(int contentId, ContextEnum contextId, bool liked)
        {
            ContentLikeDislike contentLikeDislike = eCollabroDbContext.Repository<ContentLikeDislike>().Query().Filter(op => op.CreatedById.Equals(UserContextDetails.UserId)).Get().FirstOrDefault();
            if (liked)
            {
                if (contentLikeDislike == null)
                {
                    contentLikeDislike = new ContentLikeDislike { ContextId = (int)contextId, ContextContentId = contentId, CreatedById = UserContextDetails.UserId, CreatedOn = DateTime.UtcNow, IsLiked = true };
                    eCollabroDbContext.Repository<ContentLikeDislike>().Insert(contentLikeDislike);
                }
                else
                {
                    contentLikeDislike.IsLiked = true;
                    contentLikeDislike.ModifiedById = UserContextDetails.UserId;
                    contentLikeDislike.ModifiedOn = DateTime.UtcNow;
                }
                eCollabroDbContext.Save();
            }
            else
            {
                if (contentLikeDislike != null)
                {
                    eCollabroDbContext.Repository<ContentLikeDislike>().Delete(contentLikeDislike);
                    eCollabroDbContext.Save();
                }
            }
        }

        /// <summary>
        /// ChangeContentVote
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="contextId"></param>
        /// <param name="vote"></param>
        public void ChangeContentVote(int contentId, ContextEnum contextId, bool vote)
        {
            ContentVote contentVote = eCollabroDbContext.Repository<ContentVote>().Query().Filter(op => op.CreatedById.Equals(UserContextDetails.UserId)).Get().FirstOrDefault();
            if (vote)
            {
                if (contentVote == null)
                {
                    contentVote = new ContentVote { ContextId = (int)contextId, ContextContentId = contentId, CreatedById = UserContextDetails.UserId, CreatedOn = DateTime.UtcNow, IsVoted = true };
                    eCollabroDbContext.Repository<ContentVote>().Insert(contentVote);
                }
                else
                {
                    contentVote.ModifiedById = UserContextDetails.UserId;
                    contentVote.ModifiedOn = DateTime.UtcNow;
                }
                eCollabroDbContext.Save();
            }
            else
            {
                if (contentVote != null)
                {
                    eCollabroDbContext.Repository<ContentVote>().Delete(contentVote);
                    eCollabroDbContext.Save();
                }
            }
        }

        /// <summary>
        /// ChangeContentRating
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="contextId"></param>
        /// <param name="rating"></param>
        public void ChangeContentRating(int contentId, ContextEnum contextId, int rating)
        {
            ContentRating contentRating = eCollabroDbContext.Repository<ContentRating>().Query().Filter(op => op.CreatedById.Equals(UserContextDetails.UserId)).Get().FirstOrDefault();
            if (rating!=0)
            {
                if (contentRating == null)
                {
                    contentRating = new ContentRating { ContextId = (int)contextId, ContextContentId = contentId, CreatedById = UserContextDetails.UserId, CreatedOn = DateTime.UtcNow, Rating = rating };
                    eCollabroDbContext.Repository<ContentRating>().Insert(contentRating);
                }
                else
                {
                    contentRating.Rating = rating;
                    contentRating.ModifiedById = UserContextDetails.UserId;
                    contentRating.ModifiedOn = DateTime.UtcNow;
                }
                eCollabroDbContext.Save();
            }
            else
            {
                if (contentRating != null)
                {
                    eCollabroDbContext.Repository<ContentRating>().Delete(contentRating);
                    eCollabroDbContext.Save();
                }
            }
        }




        #endregion

        #endregion

    }
}
