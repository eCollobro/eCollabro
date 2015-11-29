var $productContentEditor;


// ProductController for AngularJS
ecollabroApp.controller('manageProductController',['$scope', 'productService', function ($scope, productService) {
    $scope.product = {};
    $scope.productCategories = [];
    $scope.selectedProductCategory = {};
    $scope.productTypes = [{ ProductTypeId: 1, ProductType: "Physical" }, { ProductTypeId: 2, ProductType: "Digital" }];
    $scope.selectedProductType = $scope.productTypes[0];

    //Method loadProduct
    $scope.loadProduct = function (productId, productCategoryId) {
        $scope.product.ProductId = productId;
        if ($scope.product.ProductId == 0) {
            $scope.product.ProductCategoryId = productCategoryId;
            $scope.product.ProductPrice = 0;
            $scope.product.ProductTypeId = 1;
            $scope.loadProductCategories();
            $scope.product.IsActive = true;

            return;
        }
        productService.getProduct(productId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.product = resp.result;
                $scope.loadProductCategories();
                if($scope.product.ProductTypeId==2)
                 $scope.selectedProductType = $scope.productTypes[1];
            }
            else {
                showMessage("divSummaryMessageProduct", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    // Method loadProductCategories
    $scope.loadProductCategories = function () {
        productService.getProductCategories().then(function (resp) {
            if (resp.businessException == null) {
                $scope.productCategories = resp.result.data;
                if ($scope.productCategories.length > 0) {
                    $scope.selectedProductCategory = $scope.productCategories[0];
                    $scope.product.ProductCategoryId = $scope.selectedProductCategory.ProductCategoryId;
                }
                for (var ctr = 0; ctr < $scope.productCategories.length; ctr++) {
                    var productCat = $scope.productCategories[ctr];
                    if (productCat.ProductCategoryId == $scope.product.ProductCategoryId) {
                        $scope.selectedProductCategory = productCat;
                        break;
                    }
                }
                // update cleditor with the latest html contents
                $productContentEditor.updateFrame();
            }
            else {
                showMessage("divSummaryMessageProduct", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    // Method updateProductCategory
    $scope.updateProductCategory = function () {
        $scope.product.ProductCategoryId = $scope.selectedProductCategory.ProductCategoryId;
    };

    // Method updateProductType
    $scope.updateProductType = function () {
        $scope.product.ProductTypeId = $scope.selectedProductType.ProductTypeId;
    };

    //Method saveProduct
    $scope.saveProduct = function () {
        $productContentEditor.updateTextArea();
        $("#frmProduct").data("validator").settings.ignore = "";
        $scope.product.ProductSpecifications = $("#ProductSpecifications").val();
        if (!$("#frmProduct").valid()) {
            return;
        }
        else {
            productService.saveProduct($scope.product).then(function (resp) {
                if (resp.businessException == null) {
                    $scope.product.productId = resp.result.Id;
                    var divProducts = document.getElementById("divProducts");
                    if (divProducts) {
                        showProducts(resp.result.Message); // calling parent's method
                    }
                    else {
                        showMessage("divSummaryMessageProduct", resp.result.Message, "success");
                    }

                }
                else {
                    showMessage("divSummaryMessageProduct", resp.businessException.ExceptionMessage, "danger");
                }
            });
        }
    };

    //Method openProducts
    $scope.openProducts = function (productCategoryId) {
        location.href = "/store/product/products/"+productCategoryId;
    };

}]);