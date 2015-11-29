//contentPage service
ecollabroApp.service('contentPageService',['serviceHandler', function (serviceHandler) {

    this.getCategories = function () {
        return serviceHandler.executeGetService("/ContentPageApi/GetCategories/" + headerController().currentSiteId);
    }

    this.getCategory = function (categoryId) {
        return serviceHandler.executeGetService("/ContentPageApi/GetCategory/" + headerController().currentSiteId + "/" + categoryId);
    }

    this.deleteCategory = function (categoryId) {
        return serviceHandler.executeGetService("/ContentPageApi/DeleteCategory/" + headerController().currentSiteId + "/" + categoryId);
    }

    this.saveCategory = function (contentPageCategory) {
        return serviceHandler.executePostService("/ContentPageApi/SaveCategory/" + headerController().currentSiteId, contentPageCategory);
    }

 
    this.getContentPages = function (categoryId) {
        return serviceHandler.executeGetService("/ContentPageApi/GetPages/" + headerController().currentSiteId + "/" + categoryId);
    }

    this.getContentPage = function (contentPageId) {
        return serviceHandler.executeGetService("/ContentPageApi/GetPage/" + headerController().currentSiteId + "/" + contentPageId);
    }

    this.deleteContentPage = function (contentPageId) {
        return serviceHandler.executeGetService("/ContentPageApi/DeletePage/" + headerController().currentSiteId + "/" + contentPageId);
    }

    this.saveContentPage = function (contentPage) {
        return serviceHandler.executePostService("/ContentPageApi/SavePage/" + headerController().currentSiteId, contentPage);
    }

    // controllers 
    this.getCategoryView = function (categoryId) {
        return serviceHandler.executeGetService("/Content/ContentPage/Category/" + categoryId);
    }

    this.getContentPageView = function (categoryId, pageId) {
        return serviceHandler.executeGetService("/Content/ContentPage/Page/" + pageId + "?catId=" + categoryId);
    }
}]);