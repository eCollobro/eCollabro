//workflow service
ecollabroApp.service('workflowService',['serviceHandler', function (serviceHandler) {

    this.getUserTasks = function (searchCriteria) {
        return serviceHandler.executePostService("/WorkflowApi/GetUserTasks/" + headerController().currentSiteId,searchCriteria);
    }
    
    this.getUserTask=function(taskId)
    {
        return serviceHandler.executeGetService("/WorkflowApi/GetUserTask/" + headerController().currentSiteId +"/"+ taskId);
    }
    
    this.saveUserTask=function(userTask) {
        return serviceHandler.executePostService("/WorkflowApi/SaveUserTask/" + headerController().currentSiteId,userTask);
    }

 
    this.getWorkflowComments = function (context,contextId) {
        return serviceHandler.executeGetService("/WorkflowApi/GetWorkflowComments/" + headerController().currentSiteId +"/" + context + "/"+contextId);
    }

    this.saveWorkflowComment = function (workflowComment) {
        return serviceHandler.executePostService("/WorkflowApi/SaveWorkflowComment/" + headerController().currentSiteId, workflowComment);
    }

    //view
    this.getUserTaskView = function (taskId) {
        return serviceHandler.executeGetService("/Workflow/UserTask/" + taskId );
    }

}]);