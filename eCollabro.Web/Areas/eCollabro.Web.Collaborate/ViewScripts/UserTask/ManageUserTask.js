var $UserTaskContentEditor;


// UserTaskController for AngularJS
ecollabroApp.controller('UserTaskController', ['$scope', '$http', function ($scope, $http) {
    $scope.UserTask = {};
    $scope.ActiveRoles = [];
    $scope.SelectedRole = {};

    //Method LoadUserTask
    $scope.LoadUserTask = function (taskId) {
        $scope.UserTask.TaskId = taskId;
        if ($scope.UserTask.TaskId == 0) {
            $scope.LoadActiveRoles();
            $scope.UserTask.IsActive = true;
            return;
        }

        $http({
            url: "/Api/UserTaskApi/GetUserTask/" + $scope.UserTask.TaskId,
            method: "GET",
            async: false
        }).success(function (data, status, headers, config) {
            $scope.UserTask = data;
            $scope.LoadActiveRoles();
        }).error(function (data, status, headers, config) {
            showException("divSummaryMessageUserTask", data, status, headers, config);
        });

    };

    // Method LoadActiveRoles
    $scope.LoadActiveRoles = function () {

        $http({
            url: "/Api/SecurityApi/GetActiveRoles",
            method: "GET",
        }).success(function (data, status, headers, config) {
            $scope.ActiveRoles = data;
            for (var ctr = 0; ctr < $scope.ActiveRoles.length; ctr++) {
                var role = $scope.ActiveRoles[ctr];
                if (role.RoleId == $scope.UserTask.AssignedRoleId) {
                    $scope.SelectedRole = role;
                    break;
                }
            }

        }).error(function (data, status, headers, config) {
            showException("divSummaryMessageUserTask", data, status, headers, config);
        });
    };

    // Method UpdateAssignedRole
    $scope.UpdateUserTaskCategory = function () {
        $scope.UserTask.AssignedRoleId = $scope.SelectedRole.RoleId;
    };

    //Method SaveUserTask
    $scope.SaveUserTask = function () {
        if (!$("#frmUserTask").valid()) {
            return;
        }
        else {
            $http({
                url: "/Api/UserTaskApi/SaveUserTask/",
                method: "POST",
                data: angular.toJson($scope.UserTask),
                headers: { 'Content-Type': 'application/json; charset=utf-8' }
            }).success(function (data, status, headers, config) {
                var divUserTasks = document.getElementById("divUserTasks");
                if (divUserTasks) {
                    ShowUserTasks(data); // calling parent's method
                }
                else {
                    showMessage("divSummaryMessageUserTask", data, "success");
                }
            }).error(function (data, status, headers, config) {
                showException("divSummaryMessageUserTask", data, status, headers, config);
            });
        }
    }
}]);