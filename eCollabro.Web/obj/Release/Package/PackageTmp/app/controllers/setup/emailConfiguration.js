$(document).ready(function () {
    $.validator.unobtrusive.parse($("frmEmailConfiguration"));
});

// EmailConfigurationController for AngularJS
ecollabroApp.controller('emailConfigurationController', ['$scope', 'setupService',function ($scope, setupService) {
    $scope.emailConfiguration = {};

    //Method loadEmailConfiguration
    $scope.loadEmailConfiguration = function () {
        setupService.getEmailConfiguration().then(function (resp) {
            if (resp.businessException == null) {
                $scope.emailConfiguration = resp.result;
            }
            else {
                showMessage("divSummaryMessageEmailConfiguration", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method saveEmailConfiguration
    $scope.saveEmailConfiguration = function () {
        if (!$("#frmEmailConfiguration").valid()) {
            return;
        }
        setupService.saveEmailConfiguration($scope.emailConfiguration).then(function (resp) {
            if (resp.businessException == null) {
                showMessage("divSummaryMessageEmailConfiguration", resp.result, "success");
            }
            else {
                showMessage("divSummaryMessageEmailConfiguration", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };
}]);