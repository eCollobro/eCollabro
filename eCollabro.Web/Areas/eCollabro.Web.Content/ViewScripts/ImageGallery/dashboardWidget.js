//ImagesController for Angular
ecollabroApp.controller('imageGalleryDashboardWidgetController',['$scope', 'imageGalleryService', function ($scope, imageGalleryService) {
    $scope.recentImages = {};
    $scope.caption = "glyphicon glyphicon-chevron-down";

    //Method initialize
    $scope.initialize = function () {
        $scope.loadImages();
    };

    // Method loadImages
    $scope.loadImages = function () {
        imageGalleryService.getRecentImages().then(function (resp) {
            if (resp.businessException == null) {
                $scope.recentImages = resp.result.data;
            }
            else {
                showMessage("divSummaryMessageImageGalleryWidget", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Mthod getImageSource
    $scope.getImageSource = function (image) {
        return "/Content/ImageGallery/ViewImage/" + image.ImageId;
    };

    //Method changeCaption()
    $scope.changeCaption = function () {
        if ($scope.caption =="glyphicon glyphicon-chevron-right")
            $scope.caption = "glyphicon glyphicon-chevron-down";
        else
            $scope.caption = "glyphicon glyphicon-chevron-right";
    }
}]);