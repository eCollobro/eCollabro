using eCollabro.DataMapper;
using System;
using System.Collections.Generic;
using eCollabro.Service.DataContracts;
using eCollabro.Client.ServiceProxy;
using eCollabro.Client.Models.Workflow;
using eCollabro.Service.DataContracts.Workflow;
using eCollabro.Client.Interface;
using eCollabro.Common;
using eCollabro.Client.ServiceProxy.Interface;

namespace eCollabro.Client
{
    public class WorkflowClient : BaseClient, IWorkflowClient
    {
        private IWorkflowProxy _workflowProxy = null;

        public WorkflowClient()
        {
            _workflowProxy = new WorkflowServiceProxy();
            _workflowProxy.Initialize(SecurityClientTranslate.Convert(UserContext));
        }

        #region UserTasks

        /// <summary>
        /// GetUserTasks
        /// </summary>
        /// <param name="context"></param>
        /// <param name="assignedTo"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="activeTasks"></param>
        /// <returns></returns>
        public List<UserTaskModel> GetUserTasks(ContextEnum context, string assignedTo, DateTime? fromDate, DateTime? toDate, bool activeTasks)
        {
            List<UserTaskModel> userTasks = new List<UserTaskModel>();
            UserTasksRequestDC userTasksServiceRequest = new UserTasksRequestDC();
            userTasksServiceRequest.ContextId = (int)context;
            userTasksServiceRequest.AssignedTo = assignedTo;
            userTasksServiceRequest.FromDate = fromDate;
            userTasksServiceRequest.ToDate = toDate;
            userTasksServiceRequest.ActiveTasks = activeTasks;
            ServiceResponse<List<UserTaskDC>> userTasksServiceResponse = _workflowProxy.Execute(opt => opt.GetUserTasks(userTasksServiceRequest));
            if (userTasksServiceResponse.Status == ResponseStatus.Success)
            {
                foreach (UserTaskDC userTask in userTasksServiceResponse.Result)
                {
                    userTasks.Add(Mapper.Map<UserTaskDC, UserTaskModel>(userTask));
                }

            }
            else
            {
                HandleError(userTasksServiceResponse.Status, userTasksServiceResponse.ResponseMessage);
            }
            return userTasks;
        }

        /// <summary>
        /// GetUserTask
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public UserTaskModel GetUserTask(int taskId)
        {
            UserTaskModel userTask = null;
            ServiceResponse<UserTaskDC> userTaskServiceResponse = _workflowProxy.Execute(opt => opt.GetUserTask(taskId));
            if (userTaskServiceResponse.Status == ResponseStatus.Success)
            {
                userTask = Mapper.Map<UserTaskDC, UserTaskModel>(userTaskServiceResponse.Result);
            }
            else
            {
                HandleError(userTaskServiceResponse.Status, userTaskServiceResponse.ResponseMessage);
            }
            return userTask;
        }

        /// <summary>
        /// SaveUserTask
        /// </summary>
        /// <param name="userTask"></param>
        public void SaveUserTask(UserTaskModel userTask)
        {
            UserTaskDC userTaskDC = Mapper.Map<UserTaskModel, UserTaskDC>(userTask);

            ServiceResponse<int> saveUserTaskServiceResponse = _workflowProxy.Execute(opt => opt.SaveUserTask(userTaskDC));
            if (saveUserTaskServiceResponse.Status != ResponseStatus.Success)
            {
                HandleError(saveUserTaskServiceResponse.Status, saveUserTaskServiceResponse.ResponseMessage);
            }
            else
            {
                userTask.TaskId = saveUserTaskServiceResponse.Result;
            }
        }

        #endregion

        #region Workflow Comments

        /// <summary>
        /// GetUserTaskComments
        /// </summary>
        /// <returns></returns>
        public List<WorkflowCommentModel> GetWorkflowComments(ContextEnum context, int contextContentId)
        {
            List<WorkflowCommentModel> workflowComments = new List<WorkflowCommentModel>();
            WorkflowCommentRequestDC userTaskCommentsServiceRequest = new WorkflowCommentRequestDC();
            userTaskCommentsServiceRequest.ContextId = (int)context;
            userTaskCommentsServiceRequest.ContextContentId = contextContentId;
            ServiceResponse<List<WorkflowCommentDC>> UserTaskCommentsServiceResponse = _workflowProxy.Execute(opt => opt.GetWorkflowComments(userTaskCommentsServiceRequest));
            if (UserTaskCommentsServiceResponse.Status == ResponseStatus.Success)
            {
                foreach (WorkflowCommentDC UserTaskComment in UserTaskCommentsServiceResponse.Result)
                {
                    workflowComments.Add(Mapper.Map<WorkflowCommentDC, WorkflowCommentModel>(UserTaskComment));
                }

            }
            else
            {
                HandleError(UserTaskCommentsServiceResponse.Status, UserTaskCommentsServiceResponse.ResponseMessage);
            }
            return workflowComments;
        }


        /// <summary>
        /// SaveWorkflowComment
        /// </summary>
        /// <param name="workflowComment"></param>
        public void SaveWorkflowComment(WorkflowCommentModel workflowComment)
        {
            WorkflowCommentDC workflowCommentDC = Mapper.Map<WorkflowCommentModel, WorkflowCommentDC>(workflowComment);
            ServiceResponse<int> saveWorkflowCommentServiceResponse = _workflowProxy.Execute(opt => saveWorkflowCommentServiceResponse = opt.SaveWorkflowComment(workflowCommentDC));
            if (saveWorkflowCommentServiceResponse.Status != ResponseStatus.Success)
            {
                HandleError(saveWorkflowCommentServiceResponse.Status, saveWorkflowCommentServiceResponse.ResponseMessage);
            }
            else
            {
                workflowComment.WorkflowCommentId = saveWorkflowCommentServiceResponse.Result;
            }
        }


        #endregion
    }
}
