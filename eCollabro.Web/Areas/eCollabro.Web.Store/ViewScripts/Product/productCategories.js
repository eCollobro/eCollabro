//Global Methodsc
function getProductCategoriesController() {
    var scope = angular.element(document.getElementById("divProductCategories")).scope();
    return scope;
}

function showProductCategories(summaryMessage) {
    $("#placeHolderProductCategories").html("");
    $("#divProductCategories").show();
    if (summaryMessage != "") {
        showMessage("divSummaryMessageProductCategories", summaryMessage, "success");
    }
    //refresh grid
    getProductCategoriesController().loadProductCategories();

}

//ProductCategoriesController for Angular
ecollabroApp.controller('productCategoriesController',['$scope', '$compile','productService', function ($scope, $compile,productService) {
    $scope.productCategories = [];
    $scope.canAdd = false;
    $scope.canEdit = false;
    $scope.canDelete = false;

    //Method initialize
    $scope.initialize = function (canAdd,canEdit, canDelete) {
        $scope.canAdd = canAdd;
        $scope.canEdit = canEdit;
        $scope.canDelete = canDelete;
        $scope.fields = [{ mDataProp: "ProductCategoryName" }, { mDataProp: "Action", bSortable: false }];
        if ($scope.canEdit || $scope.canDelete || $scope.canAdd)
            $scope.fields = [{ mDataProp: "ProductCategoryId" }, { mDataProp: "ProductCategoryName" }, { mDataProp: "IsActive" }, { mDataProp: "Action", bSortable: false }];

        $scope.loadProductCategories();
    };

    // Method loadProductCategories
    $scope.loadProductCategories = function () {
        if ($scope.oTable)
            $('#tblProductCategories').dataTable().fnDestroy();

        $scope.oTable = $("#tblProductCategories").dataTable(
            {
                "processing": true,
                "serverSide": false,
                aoColumns: $scope.fields,
                "ajax": $.fn.dataTable.setData = function (request, drawCallback, settings) {
                    productService.getProductCategories(request).then(function (resp) {
                        if (resp.businessException == null) {
                            $scope.productCategories = resp.result.data;
                            angular.forEach(resp.result.data, function (value, key) {
                                value.IsActive = checkActiveDisplay(value.IsActive);
                                value.Action = $scope.getTableLink(value.ProductCategoryId);
                            })
                            drawCallback(resp.result);
                        }
                        else {
                            showMessage("divSummaryMessageProductCategories", resp.businessException.ExceptionMessage, "danger");
                        }
                    });

                }

            });
        bindEnterEventOnTable("tblProductCategories", $scope.oTable);
     };

    //Method openProductCategory
    $scope.openProductCategory = function (productCategoryId) {
        productService.getProductCategoryView(productCategoryId).then(function (resp) {
            if (resp.businessException == null) {
                $("#placeHolderProductCategories").html(resp.result);
                $("#divProductCategories").hide();
                $compile($("#placeHolderProductCategories").contents())($scope);
            }
            else {
                showMessage("divSummaryMessageProductCategories", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };


    //Method deleteProductCategory
    $scope.deleteProductCategory = function (categoryId) {
        bootbox.confirm("Are you sure to delete selected Product Category?", function (result) {
            if (result) {
                productService.deleteProductCategory(categoryId).then(function (resp) {
                    if (resp.businessException == null) {
                        showProductCategories(resp.result);
                    }
                    else {
                        showMessage("divSummaryMessageProductCategories", resp.businessException.ExceptionMessage, "danger");
                    }
                });
            }
        });
     };

    //Method openProducts
    $scope.openProducts = function (categoryId) {
        location.href = "/Store/Product/Products/" + categoryId
    };

    //Method getTableLink
    $scope.getTableLink = function (Id) {
        var tableLinks = "";
        if ($scope.canEdit)
            tableLinks = "<a class='ajaxLink' href='javascript:getProductCategoriesController().openProductCategory(" + Id + ")'>Edit</a> ";
        if ($scope.canDelete) {
            if (tableLinks != "")
                tableLinks += " | ";
            tableLinks += "<a href='javascript:getProductCategoriesController().deleteProductCategory(" + Id + ");'>Delete</a>"
        }
        if (tableLinks != "")
            tableLinks += " | ";
        tableLinks += "<a href='javascript:getProductCategoriesController().openProducts(" + Id + ");'>Products</a> ";
        return tableLinks;

    };

}]);