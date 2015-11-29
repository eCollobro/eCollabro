//Global Methodsc
function getImageGalleriesController() {
    var scope = angular.element(document.getElementById("divImageGalleries")).scope();
    return scope;
}

function showImageGalleries(summaryMessage) {
    $("#placeHolderImageGalleries").html("");
    $("#divImageGalleries").show();
    if (summaryMessage != "") {
        showMessage("divSummaryMessageImageGalleries", summaryMessage, "success");
    }
    //refresh grid
    getImageGalleriesController().loadImageGalleries();

}

//ImageGalleriesController for Angular
ecollabroApp.controller('imageGalleriesController', ['$scope', '$compile', 'imageGalleryService', function ($scope, $compile, imageGalleryService) {
    $scope.imageGalleries = [];
    $scope.canAdd = false;
    $scope.canEdit = false;
    $scope.canDelete = false;

    //Method initialize
    $scope.initialize = function (canAdd,canEdit, canDelete) {
        $scope.canAdd = canAdd;
        $scope.canEdit = canEdit;
        $scope.canDelete = canDelete;
        $scope.fields = [{ mDataProp: "ImageGalleryName" }];
        if ($scope.canEdit || $scope.canDelete || $scope.canAdd)
            $scope.fields = [{ mDataProp: "ImageGalleryId" }, { mDataProp: "ImageGalleryName" }, { mDataProp: "IsActive" }, { mDataProp: "Action", bSortable: false }];

        $scope.loadImageGalleries();
    };

    // Method loadImageGalleries
    $scope.loadImageGalleries = function () {
        if ($scope.oTable)
            $('#tblImageGalleries').dataTable().fnDestroy();

        $scope.oTable = $("#tblImageGalleries").dataTable(
            {
                "processing": true,
                "serverSide": false,
                aoColumns: $scope.fields,
                "ajax": $.fn.dataTable.setData = function (request, drawCallback, settings) {
                    imageGalleryService.getImageGalleries(request).then(function (resp) {
                        if (resp.businessException == null) {
                            $scope.blogCategories = resp.result.data;
                            angular.forEach(resp.result.data, function (value, key) {
                                value.IsActive = checkActiveDisplay(value.IsActive);
                                value.Action = $scope.getTableLink(value.ImageGalleryId);
                            })
                            drawCallback(resp.result);
                        }
                        else {
                            showMessage("divSummaryMessageImageGalleries", resp.businessException.ExceptionMessage, "danger");
                        }
                    });

                }

            });
        bindEnterEventOnTable("tblImageGalleries", $scope.oTable);
    };

    //Method openImageGallery
    $scope.openImageGallery = function (imageGalleryId) {
        imageGalleryService.getImageGalleryView(imageGalleryId).then(function (resp) {
            if (resp.businessException == null) {
                $("#placeHolderImageGalleries").html(resp.result);
                $("#divImageGalleries").hide();
                $compile($("#placeHolderImageGalleries").contents())($scope);
            }
            else {
                showMessage("divSummaryMessageImageGalleries", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };


    //Method deleteImageGallery
    $scope.deleteImageGallery = function (galleryId) {
        bootbox.confirm("Are you sure to delete selected Image Gallery?", function (result) {
            if (result) {
                imageGalleryService.deleteImageGallery(galleryId).then(function (resp) {
                    if (resp.businessException == null) {
                        showImageGalleries(resp.result);
                    }
                    else {
                        showMessage("divSummaryMessageImageGalleries", resp.businessException.ExceptionMessage, "danger");
                    }
                });
            }
        });
    };

    //Method openImages
    $scope.openImages = function (galleryId) {
        location.href = "/Content/ImageGallery/Images/" + galleryId
    };

    //Method getTableLink
    $scope.getTableLink = function (Id) {
        var tableLinks = "";
        if ($scope.canEdit)
            tableLinks = "<a class='ajaxLink' href='javascript:getImageGalleriesController().openImageGallery(" + Id + ")'>Edit</a> ";
        if ($scope.canDelete) {
            if (tableLinks != "")
                tableLinks += " | ";
            tableLinks += "<a href='javascript:getImageGalleriesController().deleteImageGallery(" + Id + ");'>Delete</a>"
        }
        if (tableLinks != "")
            tableLinks += " | ";
        tableLinks += "<a href='javascript:getImageGalleriesController().openImages(" + Id + ");'>Images</a> ";
        return tableLinks;

    }

}]);