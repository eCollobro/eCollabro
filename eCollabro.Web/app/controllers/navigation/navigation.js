// NavigationController for AngularJS
ecollabroApp.controller('navigationController', ['$scope', 'securityService', function ($scope, securityService) {
    $scope.navigation = {};
    $scope.parentNavigations = [];
    $scope.navigationTypes = [];

    //Method loadNavigation
    $scope.loadNavigation = function (navigationId) {
        $scope.navigation.NavigationId = navigationId;
        if ($scope.navigation.NavigationId == 0) {
            $scope.navigation.IsActive = true;
            $scope.loadParentNavigations();
            $scope.loadNavigationTypes();
            return;
        }

        securityService.getNavigation($scope.navigation.NavigationId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.navigation = resp.result;
                $scope.loadParentNavigations();
                $scope.loadNavigationTypes();

            }
            else {
                showMessage("divSummaryMessageNavigation", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method SaveNavigation
    $scope.saveNavigation = function () {
        if (!$("#frmNavigation").valid()) {
            return;
        }
        securityService.saveNavigation($scope.navigation).then(function (resp) {
            if (resp.businessException == null) {
                $scope.navigation.NavigationId = resp.result.Id;
                var divNavigations = document.getElementById("divNavigations");
                if (divNavigations) {
                    headerController().loadMenu();
                    showNavigations(resp.result.Message); // calling parent's method
                }
                else {
                    showMessage("divSummaryMessageNavigation", resp.result.Message, "success");
                }
            }
            else {
                showMessage("divSummaryMessageNavigation", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    // Method loadParentNavigations
    $scope.loadParentNavigations = function () {
        securityService.getNavigations($scope.navigation.NavigationId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.parentNavigations = resp.result;
                for (var ctr = 0; ctr < $scope.parentNavigations.length; ctr++) {
                    var nav = $scope.parentNavigations[ctr];
                    if (nav.NavigationId == $scope.navigation.NavigationParentId) {
                        $scope.selectedParentNavigation = nav;
                        break;
                    }
                }

            }
            else {
                showMessage("divSummaryMessageNavigation", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    // Method updateParentNavigation
    $scope.updateParentNavigation = function () {
        $scope.navigation.NavigationParentId = $scope.selectedParentNavigation.NavigationId;
    };


    // Method loadNavigationTypes
    $scope.loadNavigationTypes = function () {
        securityService.getNavigationTypes().then(function (resp) {
            if (resp.businessException == null) {
                $scope.navigationTypes = resp.result;
                if ($scope.navigationTypes.length > 0) {
                    $scope.selectedNavigationType = $scope.navigationTypes[0];
                    $scope.updateNavigationType();
                }
                for (var ctr = 0; ctr < $scope.navigationTypes.length; ctr++) {
                    var navtype = $scope.navigationTypes[ctr];
                    if (navtype.NavigationTypeId == $scope.navigation.NavigationTypeId) {
                        $scope.selectedNavigationType = navtype;
                        break;
                    }
                }
            }
            else {
                showMessage("divSummaryMessageNavigation", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    // Method updateNavigationType
    $scope.updateNavigationType = function () {
        $scope.navigation.NavigationTypeId = $scope.selectedNavigationType.NavigationTypeId;
    };

    // Method cancel
    $scope.cancel = function () {
        location.href = "/security/navigations";
    };

}]);