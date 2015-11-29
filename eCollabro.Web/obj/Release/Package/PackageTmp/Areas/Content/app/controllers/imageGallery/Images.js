//Global Methodsc
function getImagesController() {
    var scope = angular.element(document.getElementById("divImages")).scope();
    return scope;
}

function showImages(summaryMessage) {
    $("#placeHolderImages").html("");
    $("#divImages").show();
    if (summaryMessage != "") {
        showMessage("divSummaryMessageImages", summaryMessage, "success");
    }
    //refresh grid
    getImagesController().loadImages(getImagesController().imageGallery.ImageGalleryId);

}

function getImageLink(imageId, imageTitle) {
    return "<a  href='/Content/ImageGallery/Image/" + imageId + "'>" + imageTitle + "</a>";
}
//ImagesController for Angular
ecollabroApp.controller('imagesController',['$scope', '$compile', 'imageGalleryService', function ($scope, $compile, imageGalleryService) {
    $scope.imageGallery = {};
    $scope.imageGalleryId = 0;
    $scope.canEdit = false;
    $scope.canDelete = false;
    $scope.canAdd = false;

    //Method initialize
    $scope.initialize = function (imageGalleryId,canAdd, canEdit, canDelete) {
        $scope.canAdd = canAdd;
        $scope.canEdit = canEdit;
        $scope.canDelete = canDelete;
        $scope.imageGalleryId = imageGalleryId;
        $scope.loadImages();
    };

    // Method loadImages
    $scope.loadImages = function () {
        imageGalleryService.getImages($scope.imageGalleryId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.imageGallery = resp.result.data;
                var oTable = $("#tblImages").dataTable();

                oTable.fnClearTable();
                for (var ctr = 0; ctr < $scope.imageGallery.Images.length; ctr++) {
                    var image = $scope.imageGallery.Images[ctr];
                    if ($scope.canEdit || $scope.canDelete)
                        oTable.fnAddData([getImageLink(image.ImageId, image.ImageId),"<img src='/Content/ImageGallery/ImageThumbNail/"+image.ImageId+"/'> "+getImageLink(image.ImageId, image.ImageTitle), checkActiveDisplay(image.IsActive), image.ApprovalStatus, $scope.getTableLink(image.ImageId, $scope.imageGalleryId)]);
                    else
                        oTable.fnAddData([getImageLink(image.ImageId, image.ImageTitle)]);
                }
            }
            else {
                showMessage("divSummaryMessageImages", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };



    //Method openImage
    $scope.openImage = function (imageId, imageGalleryId) {
        imageGalleryService.getImageView(imageGalleryId, imageId).then(function (resp) {
            if (resp.businessException == null) {
                $("#placeHolderImages").html(resp.result);
                $("#divImages").hide();
                $compile($("#placeHolderImages").contents())($scope);
            }
            else {
                showMessage("divSummaryMessageImages", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };


    //Method deleteImage
    $scope.deleteImage = function (imageId) {
        bootbox.confirm("Are you sure to delete selected  image?", function (result) {
            if (result) {
                imageGalleryService.deleteImage(imageId).then(function (resp) {
                    if (resp.businessException == null) {
                        showImages(resp.result);
                    }
                    else {
                        showMessage("divSummaryMessageImages", resp.businessException.ExceptionMessage, "danger");
                    }
                });
            }
        });
    };

    //Method imageGalleries
    $scope.imageGalleries = function () {
        location.href = "/Content/ImageGallery/ImageGalleries";
    }

    //Method getTableLink
    $scope.getTableLink = function (imageId) {
        var tableLinks = "";
        if ($scope.canEdit)
            tableLinks = "<a class='ajaxLink' href='javascript:getImagesController().openImage(" + imageId + "," + $scope.imageGalleryId + ")'>Edit</a> ";
        if ($scope.canDelete) {
            if (tableLinks != "")
                tableLinks += " | ";
            tableLinks += "<a href='javascript:getImagesController().deleteImage(" + imageId + ");'>Delete</a> ";
        }

        return tableLinks;

    };

}]);