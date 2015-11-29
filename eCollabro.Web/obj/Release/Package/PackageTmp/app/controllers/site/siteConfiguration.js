// SiteController for AngularJS
ecollabroApp.controller('siteConfigurationController',['$scope', 'securityService',function ($scope, securityService) {
    $scope.siteConfiguration = {};
    $scope.roles = [];
    $scope.selectedRole = {};
    $scope.siteFeatures = [];

    //Method Initialize
    $scope.initialize = function () {
        $scope.loadSiteConfiguration();
        $scope.loadSiteFeatures();

    };

  
    //Method loadSiteConfiguration
    $scope.loadSiteConfiguration = function () {
        securityService.getSiteConfiguration().then(function (resp) {
            if (resp.businessException == null) {
                $scope.siteConfiguration = resp.result;
                $scope.loadRoles();
            }
            else {
                showMessage("divSummaryMessageSiteConfiguration", resp.businessException.ExceptionMessage, "danger");
            }
        });
   };

    // Method loadRoles
    $scope.loadRoles = function () {
        securityService.getRoles().then(function (resp) {
            if (resp.businessException == null) {
                $scope.roles = resp.result.data;
                var found = false;
                for (var ctr = 0; ctr < $scope.roles.length; ctr++) {
                    var role = $scope.roles[ctr];
                    if (role.RoleId == $scope.siteConfiguration.RegistrationDefaultRoleId) {
                        $scope.selectedRole = role;
                        found = true;
                        break;
                    }
                }
                if (!found && $scope.roles.length > 0)
                    $scope.selectedRole = $scope.roles[0];
            }
            else {
                showMessage("divSummaryMessageSiteConfiguration", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    // Method updateRole
    $scope.updateRole = function () {
        $scope.siteConfiguration.RegistrationDefaultRoleId = $scope.selectedRole.RoleId;
    };

    //Method SaveSiteConfiguration
    $scope.saveSiteConfiguration = function () {
        securityService.saveSiteConfiguration($scope.siteConfiguration).then(function (resp) {
            if (resp.businessException == null) {
                showMessage("divSummaryMessageSiteConfiguration", resp.result, "success");
            }
            else {
                showMessage("divSummaryMessageSiteConfiguration", resp.businessException.ExceptionMessage, "danger");
            }
        });
   };


    // Method loadSiteFeatures
    $scope.loadSiteFeatures = function () {
        securityService.getSiteFeaturesSettings().then(function (resp) {
            if (resp.businessException == null) {
                $scope.siteFeatures = resp.result;
            }
            else {
                showMessage("divSummaryMessageSiteFeaturesSettings", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method saveSiteFeaturesSettings
    $scope.saveSiteFeaturesSettings = function () {
        securityService.saveSiteFeaturesSettings($scope.siteFeatures).then(function (resp) {
            if (resp.businessException == null) {
                showMessage("divSummaryMessageSiteFeaturesSettings", resp.result, "success");
            }
            else {
                showMessage("divSummaryMessageSiteFeaturesSettings", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };
}]);