$(document).ready(function () {
    $.validator.unobtrusive.parse($("frmAddSiteCollectionAdmin"));
});
// siteCollectionAdminController for AngularJS
ecollabroApp.controller('siteCollectionAdminController', ['$scope', 'setupService',function ($scope, setupService) {
    $scope.siteCollectionAdmins = [];

    //Method initialize
    $scope.initialize = function () {
        setupService.getSiteCollectionAdmins().then(function (resp) {
            if (resp.businessException == null) {
                $scope.siteCollectionAdmins = resp.result;
            }
            else {
                showMessage("divSummaryMessageSiteCollectionAdmin", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    // Method addSiteCollectionAdmin
    $scope.addSiteCollectionAdmin = function () {
        $("#divSummaryMessageAddSiteCollectionAdmin").html();
        $("#UserName").val("");
        $('#divAddSiteCollectionAdmin').modal({
            keyboard: false,
            backdrop: 'static'
        })
    }

    //Method saveSiteCollectionAdmin
    $scope.saveSiteCollectionAdmin = function () {
        if (!$("#frmAddSiteCollectionAdmin").valid()) {
            return;
        }
        $scope.siteCollectionAdmin = {};
        $scope.siteCollectionAdmin.UserName = $("#UserName").val();
        setupService.saveSiteCollectionAdmin($scope.siteCollectionAdmin).then(function (resp) {
            if (resp.businessException == null) {
                showMessage("divSummaryMessageSiteCollectionAdmin", resp.result, "success");
                $('#divAddSiteCollectionAdmin').modal('hide')
                $scope.initialize();
            }
            else {
                showMessage("divSummaryMessageAddSiteCollectionAdmin", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method deleteSiteCollectionAdmin
    $scope.deleteSiteCollectionAdmin = function (siteCollectionAdmin) {
        bootbox.confirm("Are you sure to delete selected user from Site Collection Admin group?", function (result) {
            if (result) {

                setupService.deleteSiteCollectionAdmin(siteCollectionAdmin.UserId).then(function (resp) {
                    if (resp.businessException == null) {
                        showMessage("divSummaryMessageSiteCollectionAdmin", resp.result, "success");
                        $scope.initialize();
                    }
                    else {
                        showMessage("divSummaryMessageSiteCollectionAdmin", resp.businessException.ExceptionMessage, "danger");
                    }
                });
            }
        });
    };

}]);