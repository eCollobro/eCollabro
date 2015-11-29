// ImageGalleryController for AngularJS
ecollabroApp.controller('imageGalleryController', ['$scope', 'imageGalleryService', function ($scope, imageGalleryService) {
    $scope.imageGallery = {};

    //Method loadImageGallery
    $scope.loadImageGallery = function (galleryId) {
        $scope.imageGallery.ImageGalleryId = galleryId;
        if ($scope.imageGallery.ImageGalleryId == 0) {
            $scope.imageGallery.IsActive = true;
            return;
        }
        imageGalleryService.getImageGallery(galleryId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.imageGallery = resp.result;
            }
            else {
                showMessage("divSummaryMessageImageGallery", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method saveImageGallery
    $scope.saveImageGallery = function () {
        if (!$("#frmImageGallery").valid()) {
            return;
        }
        imageGalleryService.saveImageGallery($scope.imageGallery).then(function (resp) {
            if (resp.businessException == null) {
                $scope.imageGallery.ImageGalleryId = resp.result.Id;
                var divImageGalleries = document.getElementById("divImageGalleries");
                if (divImageGalleries) {
                    showImageGalleries(resp.result.Message); // calling parent's method
                }
                else {
                    showMessage("divSummaryMessageImageGallery", resp.result.Message, "success");
                }
            }
            else {
                showMessage("divSummaryMessageImageGallery", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method openImageGalleries
    $scope.openImageGalleries = function () {
        location.href = "/content/imagegallery/imagegalleries";
    };

}]);