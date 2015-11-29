// userTaskController for AngularJS
ecollabroApp.controller('userTaskController', ['$scope', 'workflowService', 'securityService', function ($scope, workflowService, securityService) {
    $scope.userTask = {};
    $scope.taskAllowedStatus = [];
    $scope.taskAllowedStatus.push({ TaskStatus: "New" });
    $scope.taskAllowedStatus.push({ TaskStatus: "In Process" });
    $scope.taskAllowedStatus.push({ TaskStatus: "Approved" });
    $scope.taskAllowedStatus.push({ TaskStatus: "Rejected" });
    $scope.taskAllowedStatus.push({ TaskStatus: "Completed" });
    $scope.selectedStatus = $scope.taskAllowedStatus[0];
    $scope.currentSelectedStatus = $scope.taskAllowedStatus[0];
    $scope.currentAssignedUser = "";
    //Method loadUserTask
    $scope.loadUserTask = function (taskId) {
        $scope.userTask.TaskId = taskId;
        workflowService.getUserTask(taskId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.userTask = resp.result;
                $scope.setStatus();
                $scope.currentAssignedUser = $scope.userTask.AssignedUserName;
                //getWorkflowCommentsController().initialize($scope.userTask.Context, $scope.userTask.ContextId);
                //getWorkflowCommentsController().loadWorkflowComments();
            }
            else {
                showMessage("divSummaryMessageUserTask", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method setStatus
    $scope.setStatus = function () {
        for (ctr = 0;ctr<=$scope.taskAllowedStatus.length;ctr++)
        {
            if($scope.taskAllowedStatus[ctr].TaskStatus==$scope.userTask.TaskStatus)
            {
                $scope.selectedStatus = $scope.taskAllowedStatus[ctr];
                $scope.currentSelectedStatus = $scope.taskAllowedStatus[ctr];
                break;
            }
        }
    };


    //Method loadWorkflowComments
    $scope.loadWorkflowComments = function () {

        $http({
            url: "/Api/WorkflowApi/GetWorkflowComments/?context=" + $scope.UserTask.Context +"&contextId=" + $scope.UserTask.ContextId,
            method: "GET",
        }).success(function (data, status, headers, config) {
            $scope.WorkflowComments = data;
        }).error(function (data, status, headers, config) {
            showException("divSummaryMessageUserTask", data, status, headers, config);
        });
    };


    // Method updateTaskStatus
    $scope.updateTaskStatus = function () {
        $scope.userTask.TaskStatus = $scope.selectedStatus.TaskStatus;
    };

    //Method saveUserTask
    $scope.saveUserTask = function () {
        if (!$("#frmUserTask").valid()) {
            return;
        }
        else {
            workflowService.saveUserTask($scope.userTask).then(function (resp) {
                if (resp.businessException == null) {
                    $scope.userTask.TaskId = resp.result.Id;
                    var divUserTasks = document.getElementById("divUserTasks");
                    if (divUserTasks) {
                        showUserTasks(resp.result.Message); // calling parent's method
                    }
                    else {
                        showMessage("divSummaryMessageUserTask", resp.result.Message, "success");
                    }
                }
                else {
                    showMessage("divSummaryMessageUserTask", resp.businessException.ExceptionMessage, "danger");
                }
            });
        }
    };
}]);