// ProductCategoryController for AngularJS
ecollabroApp.controller('productCategoryController',['$scope', 'productService',function ($scope, productService) {
    $scope.productCategory = {};

    //Method loadProductCategory
    $scope.loadProductCategory = function (categoryId) {
        $scope.productCategory.ProductCategoryId = categoryId;
        if ($scope.productCategory.ProductCategoryId == 0) {
            $scope.productCategory.IsActive = true;
            return;
        }
        productService.getProductCategory(categoryId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.productCategory = resp.result;
            }
            else {
                showMessage("divSummaryMessageProductCategory", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method saveProductCategory
    $scope.saveProductCategory = function () {
        if (!$("#frmProductCategory").valid()) {
            return;
        }
        productService.saveProductCategory($scope.productCategory).then(function (resp) {
            if (resp.businessException == null) {
                $scope.productCategory.ProductCategoryId = resp.result.Id;
                var divProductCategories = document.getElementById("divProductCategories");
                if (divProductCategories) {
                    showProductCategories(resp.result.Message); // calling parent's method
                }
                else {
                    showMessage("divSummaryMessageProductCategory", resp.result.Message, "success");
                }
            }
            else {
                showMessage("divSummaryMessageProductCategory", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method openProductCategories
    $scope.openProductCategories = function () {
        location.href = "/store/product/productcategories";
    };

}]);