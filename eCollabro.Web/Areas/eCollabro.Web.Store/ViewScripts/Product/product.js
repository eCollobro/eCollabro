// productController for AngularJS
ecollabroApp.controller('productController', ['$scope', 'productService', function ($scope, productService) {
    $scope.product = {};
    $scope.isVisible = false;
    $scope.productId = {};
    //Method initialize 
    $scope.initialize = function (productId) {
        $scope.productId = productId;
        $scope.loadProduct();
    }

    //Method loadProduct
    $scope.loadProduct = function () {
        productService.getProduct($scope.productId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.product = resp.result;
                $scope.isVisible = true;
                $("#divProductDescription").html(jQuery.parseHTML(resp.result.ProductDescription));
                $("#divProductSpecifications").html(jQuery.parseHTML(resp.result.ProductSpecifications));
                if ($scope.product.IsCommentsAllowed) {
                    getContentCommentsController().loadComments();
                }
            }
            else {
                showMessage("divSummaryMessageProduct", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };
}]);