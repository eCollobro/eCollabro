// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.BAL;
using eCollabro.BAL.Entities.Models;
using eCollabro.Common;
using eCollabro.Service.DataContracts;
using eCollabro.Service.DataContracts.Workflow;
using eCollabro.Service.ServiceContracts;
using eCollabro.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion
namespace eCollabro.Service
{
    /// <summary>
    /// WorkflowService
    /// </summary>
    public class WorkflowService : BaseService, IWorkflowService
    {

        #region Data Members

        WorkflowManager _workflowManager;

        #endregion

        #region Constructor

        /// <summary>
        /// WorkflowService
        /// </summary>
        public WorkflowService()
        {
            _workflowManager = new WorkflowManager();
        }

        #endregion

        #region Methods

        /// <summary>
        /// GetUserTasks
        /// </summary>
        /// <param name="userTasksRequest"></param>
        /// <returns></returns>
        public ServiceResponse<List<UserTaskDC>> GetUserTasks(UserTasksRequestDC userTasksRequest)
        {
            ServiceResponse<List<UserTaskDC>> getUserTasksServiceResponse = new ServiceResponse<List<UserTaskDC>>();
            try
            {
                SetContext();
                List<UserTask> userTasks = _workflowManager.GetUserTasks((ContextEnum)userTasksRequest.ContextId, userTasksRequest.AssignedTo, userTasksRequest.FromDate, userTasksRequest.ToDate, userTasksRequest.ActiveTasks);
                getUserTasksServiceResponse.Result = new List<UserTaskDC>();
                foreach (UserTask userTask in userTasks)
                {
                    UserTaskDC userTaskDC = Mapper.Map<UserTask, UserTaskDC>(userTask);
                    userTaskDC.AssignedUserName = userTask.UserMembership != null ? userTask.UserMembership.UserName : null;
                    userTaskDC.AssignedByUserName = userTask.UserMembership != null ? userTask.UserMembership.UserName : null;
                    getUserTasksServiceResponse.Result.Add(userTaskDC);
                }
            }
            catch (Exception ex)
            {
                HandleError(ex, getUserTasksServiceResponse);
            }
            return getUserTasksServiceResponse;

        }

        /// <summary>
        /// GetUserTask
        /// </summary>
        /// <param name="userTaskId"></param>
        /// <returns></returns>
        public ServiceResponse<UserTaskDC> GetUserTask(int userTaskId)
        {
            ServiceResponse<UserTaskDC> getUserTaskServiceResponse = new ServiceResponse<UserTaskDC>();
            try
            {
                SetContext();
                UserTask userTask = _workflowManager.GetUserTask(userTaskId);
                UserTaskDC userTaskDC = Mapper.Map<UserTask, UserTaskDC>(userTask);
                userTaskDC.AssignedUserName = userTask.UserMembership != null ? userTask.UserMembership.UserName : null;
                userTaskDC.AssignedByUserName = userTask.UserMembership != null ? userTask.UserMembership.UserName : null;
                getUserTaskServiceResponse.Result = userTaskDC;
            }
            catch (Exception ex)
            {
                HandleError(ex, getUserTaskServiceResponse);
            }
            return getUserTaskServiceResponse;
        }

        /// <summary>
        /// SaveUserTask
        /// </summary>
        /// <param name="userTask"></param>
        /// <returns></returns>
        public ServiceResponse<int> SaveUserTask(UserTaskDC userTask)
        {
            ServiceResponse<int> saveUserTaskServiceResponse = new ServiceResponse<int>();
            try
            {
                SetContext();
                UserTask userTaskModel = Mapper.Map<UserTaskDC, UserTask>(userTask);
                if (!string.IsNullOrEmpty(userTask.AssignedUserName))
                {
                    SecurityManager securityManager = new SecurityManager();
                    UserMembership user = securityManager.FindUser(userTask.AssignedUserName);
                    userTask.AssignedUserId = user.UserId;
                }
                _workflowManager.SaveUserTask(userTaskModel);
                saveUserTaskServiceResponse.Result = userTask.TaskId;
            }
            catch (Exception ex)
            {
                HandleError(ex, saveUserTaskServiceResponse);
            }
            return saveUserTaskServiceResponse;

        }

        /// <summary>
        /// GetWorkflowComments
        /// </summary>
        /// <param name="workflowCommentsRequest"></param>
        /// <returns></returns>
        public ServiceResponse<List<WorkflowCommentDC>> GetWorkflowComments(WorkflowCommentRequestDC workflowCommentsRequest)
        {
            ServiceResponse<List<WorkflowCommentDC>> getUserTaskCommentsServiceResponse = new ServiceResponse<List<WorkflowCommentDC>>();
            try
            {
                SetContext();
                List<WorkflowComment> workflowComments = _workflowManager.GetWorkflowComments((ContextEnum)workflowCommentsRequest.ContextId, workflowCommentsRequest.ContextContentId);
                getUserTaskCommentsServiceResponse.Result = new List<WorkflowCommentDC>();
                workflowComments.ForEach(
                        workflowComment => getUserTaskCommentsServiceResponse.Result.Add(Mapper.Map<WorkflowComment, WorkflowCommentDC>(workflowComment))
                    );
            }
            catch (Exception ex)
            {
                HandleError(ex, getUserTaskCommentsServiceResponse);
            }
            return getUserTaskCommentsServiceResponse;

        }

        /// <summary>
        /// SaveWorkflowComment
        /// </summary>
        /// <param name="workflowComment"></param>
        /// <returns></returns>
        public ServiceResponse<int> SaveWorkflowComment(WorkflowCommentDC workflowComment)
        {
            ServiceResponse<int> saveUserTaskServiceResponse = new ServiceResponse<int>();
                try
                {
                    SetContext();
                    _workflowManager.SaveWorkflowComment(Mapper.Map<WorkflowCommentDC, WorkflowComment>(workflowComment));
                }
                catch (Exception ex)
                {
                    HandleError(ex, saveUserTaskServiceResponse);
                }
            return saveUserTaskServiceResponse;

        }

        #endregion
    }
}
