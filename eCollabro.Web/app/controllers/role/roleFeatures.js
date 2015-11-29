//Global Methods
function getRoleFeaturesController() {
    var scope = angular.element(document.getElementById("divRoleFeatures")).scope();
    return scope;
}

//RolesController for Angular
ecollabroApp.controller('roleFeaturesController', ['$scope', 'securityService', function ($scope, securityService) {
    $scope.roleFeatures = [];

    // Method loadRoleFeatures
    $scope.loadRoleFeatures = function (roleId) {
        securityService.getRoleFeatures(roleId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.roleFeatures = resp.result;
            }
            else {
                showMessage("divSummaryMessageRoleFeatures", resp.businessException.ExceptionMessage, "danger");
            }
        });
     };

    //Method saveRoleFeatures
    $scope.saveRoleFeatures = function () {
        securityService.saveRoleFeatures($scope.roleFeatures).then(function (resp) {
            if (resp.businessException == null) {
                var divRoles = document.getElementById("divRoles");
                if (divRoles) {
                    showRoles(resp.result); // calling parent's method
                }
                else {
                    showMessage("divSummaryMessageRoleFeatures", data, "success");
                }
            }
            else {
                showMessage("divSummaryMessageRoleFeatures", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method cancel
    $scope.cancel = function () {
        location.href = "/security/roles";
    };
}]);