//Global Methods
function getSiteFeaturesController() {
    var scope = angular.element(document.getElementById("divSiteFeatures")).scope();
    return scope;
}

//siteFeaturesController for Angular
ecollabroApp.controller('siteFeaturesController', ['$scope', 'securityService', function ($scope, securityService) {
    $scope.siteFeatures = [];

    // Method loadSiteFeatures
    $scope.loadSiteFeatures = function (siteId) {
        securityService.getSiteFeatures(siteId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.siteFeatures = resp.result;
            }
            else {
                showMessage("divSummaryMessageSiteFeatures", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method SaveSiteFeatures
    $scope.saveSiteFeatures = function () {
        securityService.saveSiteFeatures($scope.siteFeatures).then(function (resp) {
            if (resp.businessException == null) {
                var divContainerSites = document.getElementById("divSites");
                if (divContainerSites) {
                    showSites(resp.result); // calling parent's method
                }
                else {
                    showMessage("divSummaryMessageSiteFeatures", data, "success");
                }
            }
            else {
                showMessage("divSummaryMessageSiteFeatures", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method sites
    $scope.sites = function () {
        location.href = "/security/sites";
    };
}]);