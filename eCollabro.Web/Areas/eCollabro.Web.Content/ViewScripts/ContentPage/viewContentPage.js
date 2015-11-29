var $ContentPageContentEditor;
// ContentPageController for AngularJS
ecollabroApp.controller('viewContentPageController', ['$scope', 'contentPageService', function ($scope, contentPageService) {
    $scope.contentPage = {};
    $scope.isVisible = false;
    $scope.contentPageId = 0;
    //Method initialize 
    $scope.initialize = function (contentPageId) {
        $scope.contentPageId = contentPageId;
        $scope.loadContentPage();
    }
    //Method loadContentPage
    $scope.loadContentPage = function () {
        contentPageService.getContentPage($scope.contentPageId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.contentPage = resp.result;
                $scope.Visible = true;
                $("#divContentPageContent").html(jQuery.parseHTML(resp.result.ContentPageContent));
                getContentCommentsController().loadComments($scope.contentPage.IsCommentsAllowed, $scope.contentPage.IsLikeAllowed, $scope.contentPage.IsRatingAllowed, $scope.contentPage.IsVotingAllowed);
            }
            else {
                showMessage("divSummaryMessageContentPage", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

}]);