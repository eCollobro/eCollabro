//announcementDashboardWidgetController for Angular
ecollabroApp.controller('announcementDashboardWidgetController', ['$scope', 'announcementService', function ($scope, announcementService) {
    $scope.recentAnnouncements = [];
    $scope.caption = "glyphicon glyphicon-chevron-down";
    // Method initialize
    $scope.initialize = function () {
        $scope.loadRecentAnnouncements();
    };

    // Method LoadRecentAnnouncements
    $scope.loadRecentAnnouncements = function () {
        announcementService.getRecentAnnouncements().then(function (resp) {
            if (resp.businessException == null) {
                $scope.recentAnnouncements = resp.result.data;
            }
            else {
                showMessage("divSummaryMessageAnnouncementWidget", resp.businessException.ExceptionMessage, "danger");
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