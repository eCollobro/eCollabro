//blogDashboardWidgetController for Angular
ecollabroApp.controller('blogDashboardWidgetController', ['$scope', 'blogService', function ($scope, blogService) {
    $scope.recentBlogs = [];
    $scope.caption = "glyphicon glyphicon-chevron-down";
    // Method initialize
    $scope.initialize = function () {
        $scope.loadRecentBlogs();
    };

    // Method LoadRecentBlogs
    $scope.loadRecentBlogs = function () {
        blogService.getRecentBlogs().then(function (resp) {
            if (resp.businessException == null) {
                $scope.recentBlogs = resp.result.data;
            }
            else {
                showMessage("divSummaryMessageBlogWidget", resp.businessException.ExceptionMessage, "danger");
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