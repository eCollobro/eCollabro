// ImageController for AngularJS
ecollabroApp.controller('imageController',['$scope', 'imageGalleryService', function ($scope, imageGalleryService) {
    $scope.image = {};
    $scope.imageGalleries = [];
    $scope.selectedImageGallery = {};

    //Method loadImage
    $scope.loadImage = function (imageId, imageGalleryId) {
        $scope.image.ImageId = imageId;
        if ($scope.image.ImageId == 0) {
            $scope.image.ImageGalleryId = imageGalleryId;
            $scope.loadImageGalleries();
            $scope.image.IsActive = true;

            return;
        }
        imageGalleryService.getImage(imageId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.image = resp.result;
                $scope.loadImageGalleries();
            }
            else {
                showMessage("divSummaryMessageImage", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    // Method loadImageGalleries
    $scope.loadImageGalleries = function () {
        imageGalleryService.getImageGalleries().then(function (resp) {
            if (resp.businessException == null) {
                $scope.imageGalleries = resp.result.data;
                if ($scope.imageGalleries.length > 0)
                {
                    $scope.selectedImageGallery = $scope.imageGalleries[0];
                    $scope.image.ImageGalleryId = $scope.selectedImageGallery.ImageGalleryId;
                }
                for (var ctr = 0; ctr < $scope.imageGalleries.length; ctr++) {
                    var imageCat = $scope.imageGalleries[ctr];
                    if (imageCat.ImageGalleryId == $scope.image.ImageGalleryId) {
                        $scope.selectedImageGallery = imageCat;
                        break;
                    }
                }
            }
            else {
                showMessage("divSummaryMessageImage", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    // Method updateImageGallery
    $scope.updateImageGallery = function () {
        $scope.image.ImageGalleryId = $scope.selectedImageGallery.ImageGalleryId;
    };

    //Method saveImage
    $scope.saveImage = function () {
        if (!$("#frmImage").valid()) {
            return;
        }
        else {
            if ($("#ImageFile").val() == "" && ($scope.image.ImageId == null || $scope.image.ImageId == 0))
                showMessage("divSummaryMessageImage", "Please select a image to upload.", "danger");
            else {
                $("#ImageGalleryId").val($scope.image.ImageGalleryId);
                $("#frmImage").submit();
            }
        }
    };

    //Method openImages
    $scope.openImages = function (imageLibraryId) {
        location.href = "/content/imagegallery/images/" + imageLibraryId;
    };
}]);