var $blogContentEditor;


// BlogController for AngularJS
ecollabroApp.controller('manageBlogController',['$scope', 'blogService', function ($scope, blogService) {
    $scope.blog = {};
    $scope.blogCategories = [];
    $scope.selectedBlogCategory = {};

    //Method loadBlog
    $scope.loadBlog = function (blogId, blogCategoryId) {
        $scope.blog.BlogId = blogId;
        if ($scope.blog.BlogId == 0) {
            $scope.blog.BlogCategoryId = blogCategoryId;
            $scope.loadBlogCategories();
            $scope.blog.IsActive = true;

            return;
        }
        blogService.getBlog(blogId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.blog = resp.result;
                $scope.loadBlogCategories();
            }
            else {
                showMessage("divSummaryMessageBlog", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    // Method loadBlogCategories
    $scope.loadBlogCategories = function () {
        blogService.getBlogCategories().then(function (resp) {
            if (resp.businessException == null) {
                $scope.blogCategories = resp.result.data;
                if ($scope.blogCategories.length > 0) {
                    $scope.selectedBlogCategory = $scope.blogCategories[0];
                    $scope.blog.BlogCategoryId = $scope.selectedBlogCategory.BlogCategoryId;
                }
                for (var ctr = 0; ctr < $scope.blogCategories.length; ctr++) {
                    var blogCat = $scope.blogCategories[ctr];
                    if (blogCat.BlogCategoryId == $scope.blog.BlogCategoryId) {
                        $scope.selectedBlogCategory = blogCat;
                        break;
                    }
                }
                // update cleditor with the latest html contents
                $blogContentEditor.updateFrame();
            }
            else {
                showMessage("divSummaryMessageBlog", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    // Method updateBlogCategory
    $scope.updateBlogCategory = function () {
        $scope.blog.BlogCategoryId = $scope.selectedBlogCategory.BlogCategoryId;
    };

    //Method saveBlog
    $scope.saveBlog = function () {
        $blogContentEditor.updateTextArea();
        $("#frmBlog").data("validator").settings.ignore = "";
        $scope.blog.BlogContent = $("#BlogContent").val();
        if (!$("#frmBlog").valid()) {
            return;
        }
        else {
            blogService.saveBlog($scope.blog).then(function (resp) {
                if (resp.businessException == null) {
                    $scope.blog.blogId = resp.result.Id;
                    var divBlogs = document.getElementById("divBlogs");
                    if (divBlogs) {
                        showBlogs(resp.result.Message); // calling parent's method
                    }
                    else {
                        showMessage("divSummaryMessageBlog", resp.result.Message, "success");
                    }

                }
                else {
                    showMessage("divSummaryMessageBlog", resp.businessException.ExceptionMessage, "danger");
                }
            });
        }
    };

    //Method openBlogs
    $scope.openBlogs = function (blogCategoryId) {
        location.href = "/content/blog/blogs/"+blogCategoryId;
    };

}]);