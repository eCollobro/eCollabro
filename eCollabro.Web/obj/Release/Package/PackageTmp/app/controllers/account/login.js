/// <reference path="Login.js" />
$(document).ready(function () {
    $.validator.unobtrusive.parse($("frmForgetPassword"));
    if ($("#hdnPassword").val() != "") {
        $("#Password").val($("#hdnPassword").val());
    }
});
// LoginController for AngularJS
ecollabroApp.controller('loginController', ['$scope','securityService',function ($scope, securityService) {
    $scope.forgetPasswordButtonDisabled = false;
    //Method forgetPassword
    $scope.forgetPassword = function () {
        $('#divForgetPassword').modal({
            keyboard: false,
            backdrop: 'static'
        })
    };

    //Method sendForgetPasswordEmail
    $scope.sendForgetPasswordEmail = function () {
        if (!$("#frmForgetPassword").valid()) {
            return;
        }
        else {
            securityService.resetPassword($("#ForgetPasswordUserName").val()).then(function (resp) {
                if (resp.businessException == null) {
                    showMessage("divSummaryMessageLogin", resp.result, "success");
                    $('#divForgetPassword').modal('hide');
                }
                else {
                    showMessage("divSummaryMessageForgetPassword", resp.businessException.ExceptionMessage, "danger");
                }
            });
        }
    };

}]);