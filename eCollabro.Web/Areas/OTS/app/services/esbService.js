//blog service
ecollabroApp.service('esbService',['serviceHandler', function (serviceHandler) {

 
    this.getESBApps = function () {
        return serviceHandler.executeGetService("/ESBApi/GetESBApps/");
    }

    this.getAppSchedulerView = function (appId) {
        return serviceHandler.executeGetService("/OTS/ESB/AppScheduler/" + appId);
    }
}]);