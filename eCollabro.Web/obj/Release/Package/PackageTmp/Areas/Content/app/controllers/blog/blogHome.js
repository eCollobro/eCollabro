//Global Methodsc
function getBlogHomeController() {
    var scope = angular.element(document.getElementById("divBlogHome")).scope();
    return scope;
}
//BlogsController for Angular
ecollabroApp.controller('blogHomeController', ['$scope', 'blogService',function ($scope, blogService) {
    $scope.recentBlogs = [];
    $scope.blogCategories = [];

    // Method initialize
    $scope.initialize = function () {
        $scope.loadRecentBlogs();
        $scope.loadCategories();
        
    };

    // Method LoadRecentBlogs
    $scope.loadRecentBlogs = function () {
        blogService.getRecentBlogs().then(function (resp) {
            if (resp.businessException == null) {
                $scope.recentBlogs = resp.result.data;
            }
            else {
                showMessage("divSummaryMessageBlogHome", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    // Method loadCategories
    $scope.loadCategories = function () {
        blogService.getBlogCategories().then(function (resp) {
            if (resp.businessException == null) {
                $scope.blogCategories = resp.result.data;
            }
            else {
                showMessage("divSummaryMessageBlogHome", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

}]);