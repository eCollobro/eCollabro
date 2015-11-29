// siteCollectionAdminController for AngularJS
$(document).ready(function () {
    $.validator.unobtrusive.parse($("frmChangePassword"));
});

ecollabroApp.controller('dashboardController', ['$scope', 'securityService' ,function ($scope, securityService) {

    //Method initialize
    $scope.initialize = function () {
        includeChangePassword($scope,securityService,'user');
    };

    // Method manageProfile
    $scope.manageProfile = function () {
        location.href = "/account/manageprofile";
    };

   
}]);