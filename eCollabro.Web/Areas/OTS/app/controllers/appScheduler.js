// AppSchedulerController for AngularJS
ecollabroApp.controller('appSchedulerController', ['$scope', 'esbService', function ($scope, esbService) {
    $scope.appScheduler = {};

    //Method initialize
    $scope.initialize = function () {
    };

    //Method SaveAppScheduler
    $scope.saveAppScheduler = function () {
        if (!$("#frmAppScheduler").valid()) {
            return;
        }
        securityService.saveAppScheduler($scope.appScheduler).then(function (resp) {
            if (resp.businessException == null) {
                    showMessage("divSummaryMessageRole", resp.result.Message, "success");
            }
            else {
                showMessage("divSummaryMessageRole", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };
}]);