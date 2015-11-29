//Global Methodsc
function getUserTasksController() {
    var scope = angular.element(document.getElementById("divUserTasks")).scope();
    return scope;
}

function showUserTasks(summaryMessage) {
    $("#placeHolderUserTasks").html("");
    $("#divUserTasks").show();
    if (summaryMessage != "") {
        showMessage("divSummaryMessageUserTasks", summaryMessage, "success");
    }
    //refresh grid
    getUserTasksController().loadUserTasks();

}



function getTableLink(Id) {
    var tableLinks = "<a class='ajaxLink' href='javascript:getUserTasksController().openUserTask(" + Id + ")'>View</a> ";
    return tableLinks;
}

//UserTasksController for Angular
ecollabroApp.controller('userTasksController',['$scope', '$compile','workflowService', function ($scope, $compile,workflowService) {
    $scope.userTasks = [];
    $scope.searchCriteria = {};
    $scope.searchCriteria.ActiveTasks = true;
    // Method loadUserTasks
    $scope.loadUserTasks = function () {
        $scope.searchCriteria.FromDate = $("#FromDate").val();
        $scope.searchCriteria.ToDate = $("#ToDate").val();
        workflowService.getUserTasks($scope.searchCriteria).then(function (resp) {
            if (resp.businessException == null) {
                $scope.userTasks = resp.result;
                var oTable = $("#tblUserTasks").dataTable();

                oTable.fnClearTable();
                for (var ctr = 0; ctr < $scope.userTasks.length; ctr++) {
                    var userTask = $scope.userTasks[ctr];
                    oTable.fnAddData([userTask.TaskId, userTask.TaskTitle, checkActiveDisplay(userTask.IsActive), getTableLink(userTask.TaskId)]);
                }
            }
            else {
                showMessage("divSummaryMessageUserTasks", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method refresh
    $scope.refresh = function () {
        alert("Hello");
        $scope.loadUserTasks();
    };

    //Method openUserTask
    $scope.openUserTask = function (taskId) {
        workflowService.getUserTaskView(taskId).then(function (resp) {
            if (resp.businessException == null) {
                $("#placeHolderUserTasks").html(resp.result);
                $("#divUserTasks").hide();
                $compile($("#placeHolderUserTasks").contents())($scope);
            }
            else {
                showMessage("divSummaryMessageUserTasks", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

 }]);