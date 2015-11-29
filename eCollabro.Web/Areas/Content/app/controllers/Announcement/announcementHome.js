//Global Methodsc
function getAnnouncementHomeController() {
    var scope = angular.element(document.getElementById("divAnnouncementHome")).scope();
    return scope;
}

//AnnouncementsController for Angular
ecollabroApp.controller('announcementHomeController', ['$scope', 'announcementService',function ($scope, announcementService) {
    $scope.recentAnnouncements = [];
    $scope.announcementCategories = [];

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
                showMessage("divSummaryMessageAnnouncementHome", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

}]);