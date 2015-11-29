// RoleController for AngularJS
ecollabroApp.controller('roleController', ['$scope', 'securityService', function ($scope, securityService) {
    $scope.role = {};

    //Method loadRole
    $scope.loadRole = function (roleId) {
        $scope.role.RoleId = roleId;
        if ($scope.role.RoleId == 0) {
            $scope.role.IsActive = true;
            return;
        }
        securityService.getRole(roleId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.role = resp.result;
            }
            else {
                showMessage("divSummaryMessageRole", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method SaveRole
    $scope.saveRole = function () {
        if (!$("#frmRole").valid()) {
            return;
        }
        securityService.saveRole($scope.role).then(function (resp) {
            if (resp.businessException == null) {
                $scope.role.RoleId = resp.result.Id;
                var divRoles = document.getElementById("divRoles");
                if (divRoles) {
                    showRoles(resp.result.Message); // calling parent's method
                }
                else {
                    showMessage("divSummaryMessageRole", resp.result.Message, "success");
                }
            }
            else {
                showMessage("divSummaryMessageRole", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };
}]);