// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.BAL.Entities.Models;
using eCollabro.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using eCollabro.DataMapper;
using eCollabro.Exceptions;
using eCollabro.Common;

#endregion

namespace eCollabro.BAL
{

    /// <summary>
    /// WorkflowManager
    /// </summary>
    public class WorkflowManager : BaseManager
    {
        #region Methods

        /// <summary>
        /// WorkFlowEventHandler
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IWorkflowEventHandler WorkFlowEventHandler(ContextEnum context)
        {
            IWorkflowEventHandler workflowEventHandler = null;
            if (context.Equals(ContextEnum.Blog) || context.Equals(ContextEnum.Document) || context.Equals(ContextEnum.Image) || context.Equals(ContextEnum.ContentPage) || context.Equals(ContextEnum.Product))
            {
                workflowEventHandler = new ContentManager();
            }
            else
            {
                return null;
            }
            return workflowEventHandler;
        }

        /// <summary>
        /// CreateWorkflowTask
        /// </summary>
        /// <param name="context"></param>
        /// <param name="contextId"></param>
        /// <param name="taskTitle"></param>
        /// <param name="taskDescription"></param>
        /// <param name="assignRoleId"></param>
        /// <returns></returns>
        public UserTask CreateWorkflowTask(ContextEnum contextId, int contextContentId, string taskTitle, string taskDescription)
        {
            UserTask userTask = eCollabroDbContext.Repository<UserTask>().Query().Filter(qry => qry.SiteId.Equals(UserContextDetails.SiteId) && qry.ContextId.Equals((int)contextId) && qry.ContexContentId.Equals(contextContentId) && qry.IsActive.Equals(true)).Get().FirstOrDefault(); // check if task already created and active
            if (userTask == null)
            {
                userTask = new UserTask();
                userTask.CreatedById = UserContextDetails.UserId;
                userTask.SiteId = UserContextDetails.SiteId;
                userTask.CreatedOn = DateTime.UtcNow;
            }
            else
            {
                userTask.ModifiedById = UserContextDetails.UserId;
                userTask.ModifiedOn = DateTime.UtcNow;
            }
            userTask.TaskTitle = taskTitle;
            userTask.TaskDescription = taskDescription;
            userTask.CompletionPercentage = 0;
            userTask.TaskStatus = "New";
            userTask.TaskType = "ApproveReject";
            userTask.ContextId =(int) contextId;
            userTask.IsActive = true;
            userTask.ContexContentId = contextContentId;
            eCollabroDbContext.Repository<UserTask>().Insert(userTask);
            eCollabroDbContext.Save();
            return userTask;
        }


        /// <summary>
        /// SaveUserTask
        /// </summary>
        /// <param name="userTask"></param>
        public void SaveUserTask(UserTask userTask)
        {
            if (userTask.TaskId.Equals(0)) // New Task
            {
                userTask.CreatedById = UserContextDetails.UserId;
                userTask.CreatedOn = DateTime.UtcNow;
                eCollabroDbContext.Repository<UserTask>().Insert(userTask);
                eCollabroDbContext.Save();
            }
            else
            {
                UserTask oldUserTask = eCollabroDbContext.Repository<UserTask>().Find(userTask.TaskId);
                if (oldUserTask != null)
                {
                    UserTask beforeUpdate = Mapper.Map<UserTask, UserTask>(oldUserTask);
                    if (userTask.AssignedUserId.HasValue && oldUserTask.AssignedUserId != userTask.AssignedUserId)
                    {
                        UserRole userRole = eCollabroDbContext.Repository<UserRole>().Query().Filter(qry => qry.UserId.Equals(userTask.AssignedUserId.Value) && qry.SiteId.Equals(UserContextDetails.SiteId)).Get().FirstOrDefault();
                        if (userRole == null) // assigned user not exist on site 
                        {
                            throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UserNotAssignedToSite), CoreValidationMessagesConstants.UserNotAssignedToSite);
                        }
                        else
                        {
                            oldUserTask.AssignedUserId = userTask.AssignedUserId;
                            oldUserTask.AssignedByUserId = UserContextDetails.UserId;
                            oldUserTask.AssignedDate = DateTime.UtcNow;
                        }
                    }

                    oldUserTask.CompletionPercentage = userTask.CompletionPercentage;
                    if (userTask.TaskStatus.Equals("Complete") || userTask.TaskStatus.Equals("Approved") || userTask.TaskStatus.Equals("Rejected"))
                    {
                        oldUserTask.CompletionDate = DateTime.UtcNow;
                        oldUserTask.CompletionPercentage = 100;
                        oldUserTask.IsActive = false;
                    }
                    else
                    {
                        oldUserTask.IsActive = true;
                    }
                    oldUserTask.TaskStatus = userTask.TaskStatus;
                    userTask.ModifiedById = UserContextDetails.UserId;
                    userTask.ModifiedOn = DateTime.UtcNow;
                    eCollabroDbContext.Save();
                    // raise event to Context for handling task updated
                    IWorkflowEventHandler workflowEventHandler = WorkFlowEventHandler((ContextEnum)userTask.ContextId);
                    workflowEventHandler.TaskUpdated(beforeUpdate, oldUserTask);
                }
                else
                {
                    throw new DBConcurrencyException();
                }
            }

        }

        /// <summary>
        /// GetUserTasks
        /// </summary>
        /// <returns></returns>
        public List<UserTask> GetUserTasks(ContextEnum contextId, string assignedTo, DateTime? fromDate, DateTime? toDate,bool activeTasks)
        {
            RepositoryQuery<UserTask> userTasks = eCollabroDbContext.Repository<UserTask>().Query().Include(inc => inc.UserMembership).Include(inc => inc.UserMembership1).Filter(qry => qry.SiteId.Equals(UserContextDetails.SiteId));
            if (contextId!=0)
                userTasks = userTasks.Filter(qry => qry.ContextId.Equals((int)contextId));
            if (!string.IsNullOrEmpty(assignedTo))
                userTasks = userTasks.Filter(qry => qry.UserMembership.UserName.Equals(assignedTo));
            if (fromDate.HasValue)
            {
                DateTime dtFrom = fromDate.Value.ToUniversalTime();
                userTasks.Filter(qry => qry.CreatedOn >= dtFrom);
            }
            if (toDate.HasValue)
            {
                DateTime dtTo = toDate.Value.ToUniversalTime();
                userTasks.Filter(qry => qry.CreatedOn <= dtTo);
            }
                    if (activeTasks)
                userTasks.Filter(qry => qry.IsActive);
            SecurityManager securityManager = new SecurityManager();
            if ((securityManager.CheckSiteCollectionAdmin(UserContextDetails.UserId) || securityManager.CheckSiteAdmin(UserContextDetails.UserId, UserContextDetails.SiteId)))
            {
                return userTasks.Get().ToList();
            }
            else
            {
                List<FeaturePermissionResult> userFeaturePermissionResults = eCollabroDbContext.ExtendedRepository().SecurityRepository.GetUserPermissions(UserContextDetails.SiteId, UserContextDetails.UserId).Where(qry => qry.ContentPermissionId.Equals((int)PermissionEnum.ApproveContent)).ToList();
                List<int> contentContexts = new List<int>();
                if (userFeaturePermissionResults.Count > 0)
                {
                    foreach (FeaturePermissionResult featurePermissionResult in userFeaturePermissionResults)
                    {
                        ContextEnum context=GetContextForFeature((FeatureEnum)featurePermissionResult.FeatureId);
                        if (context!=ContextEnum.None)
                            contentContexts.Add((int)context);
                    }
                }
                if (contentContexts.Count > 0)
                    userTasks = userTasks.Filter(qry => contentContexts.Contains(qry.ContextId) || (qry.AssignedUserId == UserContextDetails.UserId));
                else
                    userTasks = userTasks.Filter(qry => qry.AssignedUserId.HasValue && qry.AssignedUserId == UserContextDetails.UserId);
                return userTasks.Get().ToList();
            }

        }

        /// <summary>
        /// GetContextForFeature
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        private ContextEnum GetContextForFeature(FeatureEnum feature)
        {
            if (feature == FeatureEnum.Blog)
                return ContextEnum.Blog;
            else if (feature == FeatureEnum.ContentPage)
                return ContextEnum.ContentPage;
            else if (feature == FeatureEnum.DocumentLibrary)
                return ContextEnum.Document;
            else
                return ContextEnum.None;
                
        }
        /// <summary>
        /// GetUserTask
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public UserTask GetUserTask(int taskId)
        {
            UserTask userTask = eCollabroDbContext.Repository<UserTask>().Query().Include(inc => inc.UserMembership).Include(inc => inc.UserMembership1).Filter(qry => qry.TaskId.Equals(taskId)).Get().FirstOrDefault();
            if (userTask == null)
            {
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.RecordNotFound), CoreValidationMessagesConstants.RecordNotFound);
            }
            else
            {
                return userTask;
            }
        }

        /// <summary>
        /// GetUsetTaskComments
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public List<WorkflowComment> GetWorkflowComments(ContextEnum context, int contextContentId)
        {
            List<WorkflowComment> workflowComments = eCollabroDbContext.Repository<WorkflowComment>().Query().Filter(qry => qry.ContextId.Equals(contextContentId) && qry.ContextId.Equals((int)context)).Get().ToList();
            SecurityManager securityManager = new SecurityManager();
            List<UserMembership> users = securityManager.GetUsers(workflowComments.Select(fld => fld.CreatedById).ToList());
            foreach (WorkflowComment comment in workflowComments)
            {
                UserMembership user = users.Where(qry => qry.UserId.Equals(comment.CreatedById)).FirstOrDefault();
                comment.CreatedBy = user == null ? "Unknown" : user.UserName;
                comment.TimeInterval = CommonFunctions.GetTimeInterval(comment.CreatedOn);
            }
            return workflowComments;
        }

        /// <summary>
        /// SaveWorkflowComment
        /// </summary>
        /// <param name="workflowComment"></param>
        public void SaveWorkflowComment(WorkflowComment workflowComment)
        {
            workflowComment.CreatedById = UserContextDetails.UserId;
            workflowComment.CreatedOn = DateTime.UtcNow;
            eCollabroDbContext.Repository<WorkflowComment>().Insert(workflowComment);
            eCollabroDbContext.Save();
        }


         /// <summary>
        /// SaveToQueue
        /// </summary>
        /// <typeparam name="TempModel"></typeparam>
        /// <param name="model"></param>
        /// <param name="context"></param>
        /// <param name="contextId"></param>
        internal void SaveToQueue<TempModel>(TempModel model,ContextEnum context,int contextContentId)
        {
            ApprovalQueue approvalQueue = eCollabroDbContext.Repository<ApprovalQueue>().Query().Filter(qry => qry.ContextId.Equals((int)context) && qry.ContextContentId.Equals(contextContentId)).Get().FirstOrDefault();
            if (approvalQueue == null)
            {
                approvalQueue = new ApprovalQueue();
                approvalQueue.ContextContentId = contextContentId;
                approvalQueue.ContextId=(int)context;
                eCollabroDbContext.Repository<ApprovalQueue>().Insert(approvalQueue);
            }
            approvalQueue.ModifiedById=UserContextDetails.UserId;
            approvalQueue.ModifiedOn=DateTime.UtcNow;
            approvalQueue.ObjectData= Serialize<TempModel>(model);
            eCollabroDbContext.Save();
        }

        /// <summary>
        /// GetFromQueue
        /// </summary>
        /// <typeparam name="TempModel"></typeparam>
        /// <param name="context"></param>
        /// <param name="contextId"></param>
        /// <returns></returns>
        internal TempModel GetFromQueue<TempModel>(ContextEnum context,int contextContentId) where TempModel :class
        {
            ApprovalQueue approvalQueue = eCollabroDbContext.Repository<ApprovalQueue>().Query().Filter(qry => qry.ContextId.Equals((int)context) && qry.ContextContentId.Equals(contextContentId)).Get().FirstOrDefault();
            if (approvalQueue != null)
            {
                return Desrialize<TempModel>(approvalQueue.ObjectData);
            }
            return null;
        }
        
        /// <summary>
        /// GetFromQueue
        /// </summary>
        /// <typeparam name="TempModel"></typeparam>
        /// <param name="context"></param>
        /// <param name="contextIds"></param>
        /// <returns></returns>
        internal List<TempModel> GetFromQueue<TempModel>(ContextEnum context, List<int> contextContentIds) where TempModel : class
        {
            List<TempModel> tempModels = new List<TempModel>();
            List<ApprovalQueue> approvalQueues = eCollabroDbContext.Repository<ApprovalQueue>().Query().Filter(qry => qry.ContextId.Equals((int)context) && contextContentIds.Contains( qry.ContextContentId)).Get().ToList();
            foreach(ApprovalQueue approvalQueue in approvalQueues)
            {
                tempModels.Add(Desrialize<TempModel>(approvalQueue.ObjectData));
            }
            return tempModels;
        }

        /// <summary>
        /// DeleteFromQueue
        /// </summary>
        /// <param name="context"></param>
        /// <param name="contextId"></param>
        internal void DeleteFromQueue(ContextEnum context,int contextContentId)
        {
            ApprovalQueue approvalQueue = eCollabroDbContext.Repository<ApprovalQueue>().Query().Filter(qry => qry.ContextId.Equals((int)context) && qry.ContextContentId.Equals(contextContentId)).Get().FirstOrDefault();
            if (approvalQueue != null)
            {
                eCollabroDbContext.Repository<ApprovalQueue>().Delete(approvalQueue); 
            }
            eCollabroDbContext.Save();
        }

        #endregion
    }
}
