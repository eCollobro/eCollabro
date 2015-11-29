//Global Methodsc
function getProductHomeController() {
    var scope = angular.element(document.getElementById("divProductHome")).scope();
    return scope;
}
//ProductsController for Angular
ecollabroApp.controller('productHomeController', ['$scope', 'productService',function ($scope, productService) {
    $scope.recentProducts = [];
    $scope.productCategories = [];

    // Method initialize
    $scope.initialize = function () {
        $scope.loadRecentProducts();
        $scope.loadCategories();
        
    };

    // Method LoadRecentProducts
    $scope.loadRecentProducts = function () {
        productService.getRecentProducts().then(function (resp) {
            if (resp.businessException == null) {
                $scope.recentProducts = resp.result.data;
            }
            else {
                showMessage("divSummaryMessageProductHome", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    // Method loadCategories
    $scope.loadCategories = function () {
        productService.getProductCategories().then(function (resp) {
            if (resp.businessException == null) {
                $scope.productCategories = resp.result.data;
            }
            else {
                showMessage("divSummaryMessageProductHome", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

}]);