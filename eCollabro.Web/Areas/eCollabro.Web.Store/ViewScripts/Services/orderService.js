//product service
ecollabroApp.service('orderService', ['serviceHandler', function (serviceHandler) {

    this.getOrderCart = function () {
        return serviceHandler.executeGetService("/OrderApi/GetOrderCart/" + headerController().currentSiteId );
    }

    this.saveItemToOrderCart = function (orderCartItem) {
        return serviceHandler.executePostService("/OrderApi/SaveItemToOrderCart/" + headerController().currentSiteId ,orderCartItem);
    }

}]);