// SiteController for AngularJS
ecollabroApp.controller('siteController',['$scope', 'securityService' ,function ($scope, securityService) {
    $scope.site = {};

    //Method loadSite
    $scope.loadSite = function (siteId) {
        $scope.site.SiteId = siteId;
        if ($scope.site.SiteId == 0)
        {
            $scope.site.IsActive = true;
            return;
        }
        securityService.getSite($scope.site.SiteId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.site = resp.result;
            }
            else {
                showMessage("divSummaryMessageSite", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method saveSite
    $scope.saveSite = function () {
        if (!$("#frmSite").valid()) {
            return;
        }
        securityService.saveSite($scope.site).then(function (resp) {
            if (resp.businessException == null) {
                $scope.site.SiteId = resp.result.Id;
                var divContainerSites = document.getElementById("divSites");
                if (divContainerSites) {
                    showSites(resp.result.Message); // calling parent's method
                }
                else {
                    showMessage("divSummaryMessageSite", resp.result.Message, "success");
                }
            }
            else {
                showMessage("divSummaryMessageSite", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method sites
    $scope.openSites = function () {
        location.href = "/security/sites";
    };

}]);