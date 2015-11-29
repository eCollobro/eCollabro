using eCollabro.Client;
using eCollabro.Client.Models.Workflow;
using eCollabro.Client.Resources;
using eCollabro.Utilities;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eCollabro.Web.Collaborate.Controllers
{
    [Authorize]
    public class UserTaskApiController : ApiController
    {
          #region Property

        /// <summary>
        /// LookupClientProxy
        /// </summary>
        [Dependency]
        public ILookupClient LookupClientProcessor { get; set; }


        /// <summary>
        /// ContentClientProcessor
        /// </summary>
        [Dependency]
        public IWorkflowClient WorkflowClientProcessor { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// ContentClientProcessor
        /// </summary>
        public UserTaskApiController()
        {
            if (this.WorkflowClientProcessor == null)
                this.WorkflowClientProcessor = UnityFactory.Getinstance().Resolve<IWorkflowClient>();
            if (this.LookupClientProcessor == null)
                this.LookupClientProcessor = UnityFactory.Getinstance().Resolve<ILookupClient>();
        }

        #endregion

        public List<UserTaskModel> GetUserTasks()
        {
            return WorkflowClientProcessor.GetUserTasks();
        }

        public UserTaskModel GetUserTask(int Id=0)
        {
            return WorkflowClientProcessor.GetUserTask(Id);
        }

        public string  SaveUserTask(UserTaskModel userTask)
        {
            WorkflowClientProcessor.SaveUserTask(userTask);
            return CoreMessages.Save_Success;
        }
    }
}