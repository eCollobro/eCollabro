//Global Methodsc
function getUserTasksController() {
    var scope = angular.element(document.getElementById("divUserTasks")).scope();
    return scope;
}

function ShowUserTasks(summaryMessage) {
    $("#placeHolderUserTasks").html("");
    $("#divUserTasks").show();
    if (summaryMessage != "") {
        showMessage("divSummaryMessageUserTasks", summaryMessage, "success");
    }
    //refresh grid
    getUserTasksController().LoadUserTasks();

}



function getTableLink(Id) {
    var tableLinks = "<a class='ajaxLink' href='javascript:getUserTasksController().OpenUserTask(" + Id + ")'>Edit</a> | " +
                  "<a href='javascript:getUserTasksController().DeleteUserTask(" + Id + ");'>Delete</a> | ";
    return tableLinks;

}

//UserTasksController for Angular
ecollabroApp.controller('UserTasksController', ['$scope', '$compile', '$http', function ($scope, $compile, $http) {
    $scope.UserTasks = [];

    // Method LoadUserTasks
    $scope.LoadUserTasks = function () {

        $http({
            url: "/Api/UserTaskApi/GetUserTasks",
            method: "GET",
        }).success(function (data, status, headers, config) {
            $scope.UserTasks = data;
            var oTable = $("#tblUserTasks").dataTable();

            oTable.fnClearTable();
            for (var ctr = 0; ctr < $scope.UserTasks.length; ctr++) {
                var UserTask = $scope.UserTasks[ctr];
                oTable.fnAddData([UserTask.TaskId, UserTask.TaskTitle, checkActiveDisplay(UserTask.IsActive), getTableLink(UserTask.TaskId)]);
                //, , 
            }
        }).error(function (data, status, headers, config) {
            showException("divSummaryMessageUserTasks", data, status, headers, config);
        });
    };

    //Method OpenUserTask
    $scope.OpenUserTask = function (UserTaskId) {
        $http({
            url: "/Collaborate/UserTask/ManageUserTask/" + UserTaskId,
            method: "GET",
        }).success(function (data, status, headers, config) {
            $("#placeHolderUserTasks").html(data);
            $("#divUserTasks").hide();
            $compile($("#placeHolderUserTasks").contents())($scope);
        }).error(function (data, status, headers, config) {
            showException("divSummaryMessageUserTasks", data, status, headers, config);
        });
    };


    //Method DeleteUserTask
    $scope.DeleteUserTask = function (UserTaskId) {
        var res = confirm('Are you sure to delete selected Task?')
        if (res == false)
            return;
        $http({
            url: "/Api/UserTaskApi/DeleteUserTask/" + UserTaskId,
            method: "GET",
        }).success(function (data, status, headers, config) {
            ShowUserTasks(data);
        }).error(function (data, status, headers, config) {
            showException("divSummaryMessageUserTasks", data, status, headers, config);
        });
    };

    //Method OpenBlogs
    $scope.OpenBlogs = function (UserTaskId) {
        location.href = "/Content/Blog/Blogs/" + UserTaskId
    };

}]);