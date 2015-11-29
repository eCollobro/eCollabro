//Global Methods
function getOrderCartController() {
    var scope = angular.element(document.getElementById("divOrderCart")).scope();
    return scope;
}

function getProductLink(productId, productTitle) {
    return "<a  href='/Store/Product/Product/" + productId + "'>" + productTitle + "</a>";
}
//ProductsController for Angular
ecollabroApp.controller('orderCartController', ['$scope','orderService', function ($scope, orderService) {

    //Method initialize
    $scope.initialize = function () {
        $scope.loadOrderCart();
    };



    // Method loadOrderCart
    $scope.loadOrderCart = function () {
        orderService.getOrderCart().then(function (resp) {
            if (resp.businessException == null) {
                $scope.orderCart = resp.result;
            }
            else {
                showMessage("divSummaryMessageOrderCart", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method getCartTotal
    $scope.getCartTotal = function () {
        var total = 0;
        angular.forEach($scope.orderCart.OrderCartItems, function (value, key) {
           total+= value.Amount;
        })
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

}]);