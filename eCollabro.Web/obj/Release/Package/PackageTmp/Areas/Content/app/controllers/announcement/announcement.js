// announcementController for AngularJS
ecollabroApp.controller('announcementController', ['$scope', 'announcementService', function ($scope, announcementService) {
    $scope.announcement = {};
    $scope.isVisible = false;
    $scope.announcementId = {};
    //Method initialize 
    $scope.initialize = function (announcementId) {
        $scope.announcementId = announcementId;
        $scope.loadAnnouncement();
    }

    //Method loadAnnouncement
    $scope.loadAnnouncement = function () {
        announcementService.getAnnouncement($scope.announcementId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.announcement = resp.result;
                $scope.isVisible = true;
                $("#divAnnouncementContent").html(jQuery.parseHTML(resp.result.AnnouncementContent));
                getContentCommentsController().setCommentFlags($scope.announcement.IsCommentsAllowed, $scope.announcement.IsLikeAllowed, $scope.announcement.IsRatingAllowed, $scope.announcement.IsVotingAllowed);
                getContentCommentsController().loadComments();
            }
            else {
                showMessage("divSummaryMessageAnnouncement", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };
}]);