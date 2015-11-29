//Global Methodsc
function getContentPageCategoriesController() {
    var scope = angular.element(document.getElementById("divContentPageCategories")).scope();
    return scope;
}

function showContentPageCategories(summaryMessage) {
    $("#placeHolderContentPageCategories").html("");
    $("#divContentPageCategories").show();
    if (summaryMessage != "") {
        showMessage("divSummaryMessageContentPageCategories", summaryMessage, "success");
    }
    //refresh grid
    getContentPageCategoriesController().loadContentPageCategories();

}

//ContentPageCategoriesController for Angular
ecollabroApp.controller('contentPageCategoriesController',['$scope', '$compile', 'contentPageService', function ($scope, $compile, contentPageService) {
    $scope.contentPageCategories = [];
    $scope.canAdd = false;
    $scope.canEdit = false;
    $scope.canDelete = false;

    //Method initialize
    $scope.initialize = function (canAdd, canEdit, canDelete) {
        $scope.canAdd = canAdd;
        $scope.canEdit = canEdit;
        $scope.canDelete = canDelete;
        $scope.fields = [{ mDataProp: "ContentPageCategoryName" }, { mDataProp: "Action", bSortable: false }];
        if ($scope.canEdit || $scope.canDelete || $scope.canAdd)
            $scope.fields = [{ mDataProp: "ContentPageCategoryId" }, { mDataProp: "ContentPageCategoryName" }, { mDataProp: "IsActive" }, { mDataProp: "Action", bSortable: false }];

        $scope.loadContentPageCategories();
    };

    // Method loadContentPageCategories
    $scope.loadContentPageCategories = function () {
        if ($scope.oTable)
            $('#tblContentPageCategories').dataTable().fnDestroy();

        $scope.oTable = $("#tblContentPageCategories").dataTable(
            {
                "processing": true,
                "serverSide": false,
                aoColumns: $scope.fields,
                "ajax": $.fn.dataTable.setData = function (request, drawCallback, settings) {
                    contentPageService.getCategories(request).then(function (resp) {
                        if (resp.businessException == null) {
                            $scope.contentPageCategories = resp.result.data;
                            angular.forEach(resp.result.data, function (value, key) {
                                value.IsActive = checkActiveDisplay(value.IsActive);
                                value.Action = $scope.getTableLink(value.ContentPageCategoryId);
                            })
                            drawCallback(resp.result);
                        }
                        else {
                            showMessage("divSummaryMessageContentPageCategories", resp.businessException.ExceptionMessage, "danger");
                        }
                    });

                }

            });
        bindEnterEventOnTable("tblContentPageCategories", $scope.oTable);
    };

    //Method openContentPageCategory
    $scope.openContentPageCategory = function (contentPageCategoryId) {
        contentPageService.getCategoryView(contentPageCategoryId).then(function (resp) {
            if (resp.businessException == null) {
                $("#placeHolderContentPageCategories").html(resp.result);
                $("#divContentPageCategories").hide();
                $compile($("#placeHolderContentPageCategories").contents())($scope);
            }
            else {
                showMessage("divSummaryMessageContentPageCategories", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };


    //Method deleteContentPageCategory
    $scope.deleteContentPageCategory = function (contentPageCategoryId) {
        bootbox.confirm("Are you sure to delete selected Category?", function (result) {
            if (result) {
                contentPageService.deleteCategory(contentPageCategoryId).then(function (resp) {
                    if (resp.businessException == null) {
                        showContentPageCategories(resp.result);
                    }
                    else {
                        showMessage("divSummaryMessageContentPageCategories", resp.businessException.ExceptionMessage, "danger");
                    }
                });
            }
        });
    };

    //Method openContentPages
    $scope.openContentPages = function (contentPageCategoryId) {
        location.href = "/Content/ContentPage/Pages/" + contentPageCategoryId
    };

    //Method getTableLink
    $scope.getTableLink = function (Id) {
        var tableLinks = "";
        if ($scope.canEdit)
            tableLinks = "<a class='ajaxLink' href='javascript:getContentPageCategoriesController().openContentPageCategory(" + Id + ")'>Edit</a> ";
        if ($scope.canDelete) {
            if (tableLinks != "")
                tableLinks += " | ";
            tableLinks += "<a href='javascript:getContentPageCategoriesController().deleteContentPageCategory(" + Id + ");'>Delete</a>"
        }
        if (tableLinks != "")
            tableLinks += " | ";
        tableLinks += "<a href='javascript:getContentPageCategoriesController().openContentPages(" + Id + ");'>Pages</a> ";
        return tableLinks;

    };

}]);