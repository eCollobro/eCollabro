//UserTasksController for Angular
ecollabroApp.controller('userTasksDashboardWidgetController',['$scope', 'workflowService', function ($scope, workflowService) {
    $scope.userTasks = [];
    $scope.caption = "glyphicon glyphicon-chevron-down";
    //Method initialize()
    $scope.initialize = function () {
        $scope.loadUserTasks();
    };

    // Method loadUserTasks
    $scope.loadUserTasks = function () {
        workflowService.getUserTasks().then(function (resp) {
            if (resp.businessException == null) {
                $scope.userTasks = resp.result;
            }
            else {
                showMessage("divSummaryMessageUserTasksDashboard", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method changeCaption()
    $scope.changeCaption = function () {
        if ($scope.caption == "glyphicon glyphicon-chevron-right")
            $scope.caption = "glyphicon glyphicon-chevron-down";
        else
            $scope.caption = "glyphicon glyphicon-chevron-right";
    };
}]);