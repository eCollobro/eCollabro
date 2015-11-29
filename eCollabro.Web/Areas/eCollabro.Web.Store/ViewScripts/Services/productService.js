//product service
ecollabroApp.service('productService',['serviceHandler', function (serviceHandler) {

 
    this.getProductCategories = function () {
        return serviceHandler.executeGetService("/ProductApi/GetProductCategories/" + headerController().currentSiteId);
    }

    this.getProductCategory = function (categoryId) {
        return serviceHandler.executeGetService("/ProductApi/GetProductCategory/" + headerController().currentSiteId + "/" + categoryId);
    }

    this.deleteProductCategory = function (categoryId) {
        return serviceHandler.executeGetService("/ProductApi/DeleteProductCategory/" + headerController().currentSiteId + "/" + categoryId);
    }

    this.saveProductCategory = function (productCategory) {
        return serviceHandler.executePostService("/ProductApi/SaveProductCategory/" + headerController().currentSiteId, productCategory);
    }

    this.getRecentProducts = function () {
        return serviceHandler.executeGetService("/ProductApi/GetRecentProducts/" + headerController().currentSiteId);
    }

    this.getApprovedProducts = function (categoryId) {
        return serviceHandler.executeGetService("/ProductApi/GetApprovedProducts/" + headerController().currentSiteId + "/" + categoryId);
    }

    this.getProducts = function (categoryId,request) {
        return serviceHandler.executeGetService("/ProductApi/GetProducts/" + headerController().currentSiteId + "/" + categoryId,request);
    }

    this.getProduct = function (productId) {
        return serviceHandler.executeGetService("/ProductApi/GetProduct/" + headerController().currentSiteId + "/" + productId);
    }

    this.getActiveProduct = function (productId) {
        return serviceHandler.executeGetService("/ProductApi/GetActiveProduct/" + headerController().currentSiteId + "/" + productId);
    }

    this.deleteProduct = function (productId) {
        return serviceHandler.executeGetService("/ProductApi/DeleteProduct/" + headerController().currentSiteId + "/" + productId);
    }

    this.saveProduct = function (product) {
        return serviceHandler.executePostService("/ProductApi/SaveProduct/" + headerController().currentSiteId, product);
    }

    // controllers 
    this.getProductCategoryView = function (productCategoryId) {
        return serviceHandler.executeGetService("/Store/Product/ProductCategory/"+ productCategoryId);
    }

    this.getProductView = function (productCategoryId,productId) {
        return serviceHandler.executeGetService("/Store/Product/ManageProduct/" + productId + "?catId=" + productCategoryId);
    }
}]);