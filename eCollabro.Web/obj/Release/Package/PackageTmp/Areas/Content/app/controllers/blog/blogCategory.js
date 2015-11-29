// BlogCategoryController for AngularJS
ecollabroApp.controller('blogCategoryController',['$scope', 'blogService',function ($scope, blogService) {
    $scope.blogCategory = {};

    //Method loadBlogCategory
    $scope.loadBlogCategory = function (categoryId) {
        $scope.blogCategory.BlogCategoryId = categoryId;
        if ($scope.blogCategory.BlogCategoryId == 0) {
            $scope.blogCategory.IsActive = true;
            return;
        }
        blogService.getBlogCategory(categoryId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.blogCategory = resp.result;
            }
            else {
                showMessage("divSummaryMessageBlogCategory", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method saveBlogCategory
    $scope.saveBlogCategory = function () {
        if (!$("#frmBlogCategory").valid()) {
            return;
        }
        blogService.saveBlogCategory($scope.blogCategory).then(function (resp) {
            if (resp.businessException == null) {
                $scope.blogCategory.BlogCategoryId = resp.result.Id;
                var divBlogCategories = document.getElementById("divBlogCategories");
                if (divBlogCategories) {
                    showBlogCategories(resp.result.Message); // calling parent's method
                }
                else {
                    showMessage("divSummaryMessageBlogCategory", resp.result.Message, "success");
                }
            }
            else {
                showMessage("divSummaryMessageBlogCategory", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method openBlogCategories
    $scope.openBlogCategories = function () {
        location.href = "/content/blog/blogcategories";
    };

}]);