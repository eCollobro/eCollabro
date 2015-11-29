//setup service
ecollabroApp.service('setupService',['serviceHandler', function (serviceHandler) {

    this.getEmailConfiguration = function () {
        return serviceHandler.executeGetService("/SetupApi/GetEmailConfiguration/");
    }
    
    this.saveEmailConfiguration = function (emailConfiguration) {
        return serviceHandler.executePostService("/SetupApi/SaveEmailConfiguration/", emailConfiguration);
    }

    this.getSiteCollectionAdmins = function () {
        return serviceHandler.executeGetService("/SetupApi/GetSiteCollectionAdmins");
    }

    this.saveSiteCollectionAdmin = function (siteCollectionAdmin) {
        return serviceHandler.executePostService("/SetupApi/SaveSiteCollectionAdmin/", siteCollectionAdmin);
    }

    this.deleteSiteCollectionAdmin = function (userId) {
        return serviceHandler.executeGetService("/SetupApi/DeleteSiteCollectionAdmin/" + userId);
    }
}]);