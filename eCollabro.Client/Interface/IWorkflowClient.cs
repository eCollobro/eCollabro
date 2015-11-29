#region References
using System.Collections.Generic;
using eCollabro.Client.Models.Workflow;
using System;
using eCollabro.Common;
#endregion 

namespace eCollabro.Client.Interface
{
    /// <summary>
    /// IWorkflowClient
    /// </summary>
    public interface IWorkflowClient: IBaseClient
    {
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
        List<UserTaskModel> GetUserTasks(ContextEnum context, string assignedTo, DateTime? fromDate, DateTime? toDate, bool activeTasks);

        /// <summary>
        /// GetUserTask
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        UserTaskModel GetUserTask(int taskId);

        /// <summary>
        /// SaveUserTask
        /// </summary>
        /// <param name="userTask"></param>
        void SaveUserTask(UserTaskModel userTask);

        #endregion 

        #region Workflow Comments
        /// <summary>
        /// GetUserTaskComments
        /// </summary>
        /// <returns></returns>
        List<WorkflowCommentModel> GetWorkflowComments(ContextEnum context, int contextContentId);

        /// <summary>
        /// SaveWorkflowComment
        /// </summary>
        /// <param name="workflowComment"></param>
        void SaveWorkflowComment(WorkflowCommentModel workflowComment);

        #endregion 
    }
}
