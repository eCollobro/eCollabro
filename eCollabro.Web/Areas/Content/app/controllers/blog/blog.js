// blogController for AngularJS
ecollabroApp.controller('blogController', ['$scope', 'blogService', function ($scope, blogService) {
    $scope.blog = {};
    $scope.isVisible = false;
    $scope.blogId = {};
    //Method initialize 
    $scope.initialize = function (blogId) {
        $scope.blogId = blogId;
        $scope.loadBlog();
    }

    //Method loadBlog
    $scope.loadBlog = function () {
        blogService.getBlog($scope.blogId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.blog = resp.result;
                $scope.isVisible = true;
                $("#divBlogContent").html(jQuery.parseHTML(resp.result.BlogContent));
                getContentCommentsController().setCommentFlags($scope.blog.IsCommentsAllowed, $scope.blog.IsLikeAllowed, $scope.blog.IsRatingAllowed, $scope.blog.IsVotingAllowed);
                getContentCommentsController().loadComments();
            }
            else {
                showMessage("divSummaryMessageBlog", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };
}]);