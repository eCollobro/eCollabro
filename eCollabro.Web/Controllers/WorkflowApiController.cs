// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using eCollabro.Client.Interface;
using eCollabro.Client.Models.Workflow;
using eCollabro.Common;
using eCollabro.Resources;
using eCollabro.Utilities;
using eCollabro.Web.Base;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

#endregion 
namespace eCollabro.Web.Controllers
{
    /// <summary>
    /// WorkflowApiController
    /// </summary>
    [Authorize,WebApiExceptionFiler]
    public class WorkflowApiController : ApiController
    {
        #region Property

        /// <summary>
        /// ContentClientProcessor
        /// </summary>
        public IWorkflowClient WorkflowClientProcessor { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// ContentClientProcessor
        /// </summary>
        public WorkflowApiController()
        {
           this.WorkflowClientProcessor = ApplicationContext.Getinstance().UnityContainer.Resolve<IWorkflowClient>();
        }

        #endregion

        #region Methods 

        /// <summary>
        /// GetUserTasks
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        [Route("WorkflowApi/GetUserTasks/{siteId}"),HttpPost]
        public HttpResponseMessage GetUserTasks(int siteId,UserTasksSearchModel searchCriteria)
        {
            if(searchCriteria==null)
            {
                searchCriteria = new UserTasksSearchModel();
                searchCriteria.ActiveTasks = true;
            }
            WorkflowClientProcessor.UserContext.SiteId = siteId;
            List<UserTaskModel> userTasks=WorkflowClientProcessor.GetUserTasks(searchCriteria.Context,searchCriteria.AssignedTo,searchCriteria.FromDate,searchCriteria.ToDate,searchCriteria.ActiveTasks);
            return Request.CreateResponse(HttpStatusCode.OK, userTasks);
        }

        /// <summary>
        /// GetUserTask
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [Route("WorkflowApi/GetUserTask/{siteId}/{taskId}")]
        public HttpResponseMessage GetUserTask(int siteId, int taskId)
        {
            WorkflowClientProcessor.UserContext.SiteId = siteId;
            UserTaskModel userTask = WorkflowClientProcessor.GetUserTask(taskId);
            return Request.CreateResponse(HttpStatusCode.OK, userTask);
        }

        /// <summary>
        /// SaveUserTask
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="userTask"></param>
        /// <returns></returns>
        [Route("WorkflowApi/SaveUserTask/{siteId}"),HttpPost]
        public HttpResponseMessage SaveUserTask(int siteId,UserTaskModel userTask)
        {
            WorkflowClientProcessor.UserContext.SiteId = siteId;
            WorkflowClientProcessor.SaveUserTask(userTask);
            return Request.CreateResponse(HttpStatusCode.OK, new { Message = CoreMessages.SavedSuccessfully, Id = userTask.TaskId });
        }

        /// <summary>
        /// GetWorkflowComments
        /// </summary>
        /// <param name="contextId"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("WorkflowApi/GetWorkflowComments/{siteId}/{context}/{contextContentId}")]
        public HttpResponseMessage GetWorkflowComments(int siteId, int context, int contextContentId)
        {
            WorkflowClientProcessor.UserContext.SiteId = siteId;
            List<WorkflowCommentModel> workflowComments=WorkflowClientProcessor.GetWorkflowComments((ContextEnum) context,contextContentId);
            return Request.CreateResponse(HttpStatusCode.OK, workflowComments);
        }

        /// <summary>
        /// SaveWorkflowComment
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="workflowComment"></param>
        /// <returns></returns>
        [Route("WorkflowApi/SaveWorkflowComment/{siteId}"), HttpPost]
        public HttpResponseMessage SaveWorkflowComment(int siteId,WorkflowCommentModel workflowComment)
        {
            WorkflowClientProcessor.UserContext.SiteId = siteId;
            WorkflowClientProcessor.SaveWorkflowComment(workflowComment);
            return Request.CreateResponse(HttpStatusCode.OK, new { Message = CoreMessages.SavedSuccessfully, Id = workflowComment.WorkflowCommentId });
        }

        #endregion 
    }
}