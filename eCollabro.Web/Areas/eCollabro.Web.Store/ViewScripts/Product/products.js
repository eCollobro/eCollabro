//Global Methods
function getProductsController() {
    var scope = angular.element(document.getElementById("divProducts")).scope();
    return scope;
}

function showProducts(summaryMessage) {
    $("#placeHolderProducts").html("");
    $("#divProducts").show();
    if (summaryMessage != "") {
        showMessage("divSummaryMessageProducts", summaryMessage, "success");
    }
    //refresh grid
    getProductsController().loadProducts(getProductsController().productCategory.ProductCategoryId);

}

function getProductLink(productId, productTitle) {
    return "<a  href='/Store/Product/Product/" + productId + "'>" + productTitle + "</a>";
}
//ProductsController for Angular
ecollabroApp.controller('productsController', ['$scope', '$compile', 'productService','orderService', function ($scope, $compile, productService,orderService) {
    $scope.productCategory = {};
    $scope.productCategoryId = 0;

    //Method initialize
    $scope.initialize = function (productCategoryId, canAdd, canEdit, canDelete) {
        $scope.canAdd = canAdd;
        $scope.canEdit = canEdit;
        $scope.canDelete = canDelete;
        $scope.productCategoryId = productCategoryId;
        $scope.Fields = [{ mDataProp: "ProductTitle" }, { mDataProp: "ProductDescription", bSortable: false }, { mDataProp: "ProductPrice" }, { mDataProp: "Action", bSortable: false }];
        if ($scope.canEdit || $scope.canDelete || $scope.canAdd)
            $scope.Fields = [{ mDataProp: "ProductId" }, { mDataProp: "ProductTitle" }, { mDataProp: "IsActive" }, { mDataProp: "ApprovalStatus" }, { mDataProp: "Action", bSortable: false }];
        $scope.loadProducts();
    };



    // Method loadProducts
    $scope.loadProducts = function () {
        if ($scope.oTable)
            $('#tblProducts').dataTable().fnDestroy();

        $scope.oTable = $("#tblProducts").dataTable(
            {
                "processing": true,
                "serverSide": false,
                aoColumns: $scope.Fields,
                "ajax": $.fn.dataTable.setData = function (request, drawCallback, settings) {
                    productService.getProducts($scope.productCategoryId, request).then(function (resp) {
                        if (resp.businessException == null) {
                            $scope.productCategory = resp.result.data;
                            // switch as per data table structure for server paging
                            resp.result.data = resp.result.data.Products;
                            angular.forEach(resp.result.data, function (value, key) {
                                var product=angular.copy(value);
                                value.ProductId = getProductLink(product.ProductId, product.ProductId);
                                value.ProductTitle = getProductLink(product.ProductId, product.ProductTitle);
                                value.IsActive = checkActiveDisplay(product.IsActive);
                                value.Action = $scope.getTableLink(product);
                            })
                            drawCallback(resp.result);
                        }
                        else {
                            showMessage("divSummaryMessageProducts", resp.businessException.ExceptionMessage, "danger");
                        }
                    });

                }

            });
        bindEnterEventOnTable("tblProducts", $scope.oTable);
    };



    //Method openProduct
    $scope.openProduct = function (productId, productCategoryId) {
        productService.getProductView(productCategoryId, productId).then(function (resp) {
            if (resp.businessException == null) {
                $("#placeHolderProducts").html(resp.result);
                $("#divProducts").hide();
                $compile($("#placeHolderProducts").contents())($scope);
            }
            else {
                showMessage("divSummaryMessageProducts", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };


    //Method deleteProduct
    $scope.deleteProduct = function (productId) {
        bootbox.confirm("Are you sure to delete selected Product?", function (result) {
            if (result) {
                productService.deleteProduct(productId).then(function (resp) {
                    if (resp.businessException == null) {
                        showProducts(resp.result);
                    }
                    else {
                        showMessage("divSummaryMessageProducts", resp.businessException.ExceptionMessage, "danger");
                    }
                });
            }
        });
    };

    //Method productCategories
    $scope.productCategories = function () {
        location.href = "/Store/Product/productCategories";
    }

    //Method addItemToCart
    $scope.addItemToCart = function (productId,productPrice) {
        $scope.orderCartItem = {};
        $scope.orderCartItem.ProductId = productId;
        $scope.orderCartItem.Quantity = 1;
        $scope.orderCartItem.Amount =productPrice;
        orderService.saveItemToOrderCart($scope.orderCartItem).then(function (resp) {
            if (resp.businessException == null) {
                showProducts(resp.result.Message + "<a href='/store/order/ordercart/'>Click here</a> to goto your cart and checkout product or continue shopping if you wish to add more products.");
            }
            else {
                showMessage("divSummaryMessageProducts", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method getTableLink
    $scope.getTableLink = function (product) {
        var tableLinks = "";
        if ($scope.canEdit)
            tableLinks = "<a  href='javascript:getProductsController().openProduct(" + product.ProductId + "," + $scope.productCategoryId + ")'>Edit</a> ";
        if ($scope.canDelete) {
            if (tableLinks != "")
                tableLinks += " | ";
            tableLinks += "<a href='javascript:getProductsController().deleteProduct(" +product.ProductId + ");'>Delete</a> ";
        }
        if (tableLinks != "")
            tableLinks += " | ";
        tableLinks += "<a href='javascript:getProductsController().addItemToCart(" + product.ProductId + ","+ product.ProductPrice  + ");'>Add to Cart</a> ";
        return tableLinks;

    };

}]);