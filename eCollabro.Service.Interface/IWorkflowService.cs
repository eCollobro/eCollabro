// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using eCollabro.Service.DataContracts;
using eCollabro.Service.DataContracts.Workflow;
using System.Collections.Generic;

#endregion 
#region References
using System.ServiceModel;

#endregion

namespace eCollabro.Service.Interface
{
    /// <summary>
    /// IWorkflowService
    /// </summary>
    [ServiceContract]
    public interface IWorkflowService
    {
        /// <summary>
        /// GetUserTasks
        /// </summary>
        /// <param name="userTask"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<List<UserTaskDC>> GetUserTasks(UserTasksRequestDC userTask);

        /// <summary>
        /// GetUserTask
        /// </summary>
        /// <param name="getUserTaskServiceRequest">Task Id</param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<UserTaskDC> GetUserTask(int userTaskId);

        /// <summary>
        /// SaveUserTask
        /// </summary>
        /// <param name="saveUserTaskRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<int> SaveUserTask(UserTaskDC userTask);
        
         /// <summary>
        /// GetWorkflowComments
        /// </summary>
        /// <param name="workflowComment"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<List<WorkflowCommentDC>> GetWorkflowComments(WorkflowCommentRequestDC workflowComment);
       
        /// <summary>
        /// SaveWorkflowComment
        /// </summary>
        /// <param name="workflowComment"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse<int> SaveWorkflowComment(WorkflowCommentDC workflowComment);

    }
}
