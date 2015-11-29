//Global Methodsc
function getImageHomeController() {
    var scope = angular.element(image.getElementById("divImageHome")).scope();
    return scope;
}

//ImagesController for Angular
ecollabroApp.controller('imageGalleryHomeController',['$scope', 'imageGalleryService', function ($scope, imageGalleryService) {
    $scope.recentImages = [];
    $scope.imageGalleries = [];

    // Method initialize
    $scope.initialize = function () {
        $scope.loadRecentImages();
        $scope.loadActiveImageGalleries();

    };

    // Method LoadRecentImages
    $scope.loadRecentImages = function () {
        imageGalleryService.getRecentImages().then(function (resp) {
            if (resp.businessException == null) {
                $scope.recentImages = resp.result.data;
            }
            else {
                showMessage("divSummaryMessageImageHome", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    // Method loadActiveImageGalleries
    $scope.loadActiveImageGalleries = function () {
        imageGalleryService.getImageGalleries().then(function (resp) {
            if (resp.businessException == null) {
                $scope.imageGalleries = resp.result.data;
            }
            else {
                showMessage("divSummaryMessageImageHome", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

}]);