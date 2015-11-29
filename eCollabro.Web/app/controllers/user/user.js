// UserController for AngularJS
ecollabroApp.controller('userController', ['$scope', 'securityService', function ($scope, securityService) {
    $scope.user = {};
    $scope.activeRoles = [];
    $scope.userId = 0;

    //Method initialize
    $scope.initialize = function (userId) {
        $scope.userId = userId;
        includeChangePassword($scope, securityService, 'admin');
        $scope.loadUser();
    };

    //Method loadUser
    $scope.loadUser = function () {
        
        if ($scope.userId == 0) {
            $scope.user.IsActive = true;
            $scope.loadActiveRoles();
            return;
        }
        securityService.getUser($scope.userId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.user = resp.result;
                $scope.loadActiveRoles();
            }
            else {
                showMessage("divSummaryMessageUser", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method saveUser
    $scope.saveUser = function () {

        if (!$("#frmUser").valid()) {
            return;
        }
        else {
            if ($scope.user.UserRoles == null || $scope.user.UserRoles.length == 0) {
                showMessage("divSummaryMessageUser", "Select at-least one role for user!", "danger");
                return;
            }
        }
        securityService.saveUser($scope.user).then(function (resp) {
            if (resp.businessException == null) {
                $scope.user.UserId = resp.result.Id;
                var divUsers = document.getElementById("divUsers");
                if (divUsers) {
                    showUsers(resp.result.Message); // calling parent's method
                }
                else {
                    showMessage("divSummaryMessageUser", resp.result.Message, "success");
                }
            }
            else {
                showMessage("divSummaryMessageUser", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method restPassword
    $scope.resetPassword = function () {
        bootbox.confirm("This will reset User's password and will send email with new credential? Are you sure to reset the password for selected User?", function (result) {
            if (result) {
                securityService.resetUserPassword($scope.user.UserId).then(function (resp) {
                    if (resp.businessException == null) {
                        showMessage("divSummaryMessageUser", resp.result, "success");
                    }
                    else {
                        showMessage("divSummaryMessageUser", resp.businessException.ExceptionMessage, "danger");
                    }
                });
            }
        });
    };

    //Method confirmUser
    $scope.confirmUser = function () {
        securityService.confirmUser($scope.user.UserId).then(function (resp) {
            if (resp.businessException == null) {
                showMessage("divSummaryMessageUser", resp.result, "success");
                $scope.user.IsConfirmed = true;
            }
            else {
                showMessage("divSummaryMessageUser", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method unlockUser
    $scope.unlockUser = function () {
        securityService.unlockUser($scope.user.UserId).then(function (resp) {
            if (resp.businessException == null) {
                showMessage("divSummaryMessageUser", resp.result, "success");
                $scope.user.IsLocked = false;
            }
            else {
                showMessage("divSummaryMessageUser", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method approveUser
    $scope.approveUser = function () {
        securityService.approveUser($scope.user.UserId).then(function (resp) {
            if (resp.businessException == null) {
                showMessage("divSummaryMessageUser", resp.result, "success");
                $scope.user.IsApproved = true;
            }
            else {
                showMessage("divSummaryMessageUser", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method getActiveRole
    $scope.getActiveRole = function (roleId) {
        var activeRole = null;
        for (var ctr = 0; ctr < $scope.activeRoles.length; ctr++) {
            if ($scope.activeRoles[ctr].RoleId == roleId) {
                activeRole = $scope.activeRoles[ctr];
                break;
            }
        }
        return activeRole;
    }

    // Method loadAactiveRoles
    $scope.loadActiveRoles = function () {
        securityService.getActiveRoles($scope.user).then(function (resp) {
            if (resp.businessException == null) {
                $scope.activeRoles = resp.result.data;
                if ($scope.user.UserRoles != null && $scope.user.UserRoles.length > 0) {
                    var existingRoles = $scope.user.UserRoles;
                    $scope.user.UserRoles = [];
                    for (var ctr = 0; ctr < existingRoles.length; ctr++) {
                        var activeRole = $scope.getActiveRole(existingRoles[ctr].RoleId);
                        if (activeRole != null)
                            $scope.user.UserRoles.push(activeRole);
                    }
                }
            }
            else {
                showMessage("divSummaryMessageUser", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //method openUsers
    $scope.openUsers = function () {
        location.href = "/security/users";
    };

}]);