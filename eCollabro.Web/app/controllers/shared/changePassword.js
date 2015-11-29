function includeChangePassword($scope,securityService,mode)
{
    $scope.mode = mode;
    // Method openChangePassword
    $scope.openChangePassword = function () {
        $("#divSummaryMessageChangePassword").html();
        $('#divChangePassword').modal({
            keyboard: false,
            backdrop: 'static'
        })
    }

    // Method changePassword
    $scope.changePassword = function () {
        if (!$("#frmChangePassword").valid()) {
            return;
        }
        else {
            $scope.changePasswordModal = {};
            if ($scope.mode == 'admin')
                $scope.changePasswordModal.UserName = $scope.user.UserName;
            $scope.changePasswordModal.OldPassword = $("#OldPassword").val();
            $scope.changePasswordModal.NewPassword = $("#NewPassword").val();
            securityService.changePassword($scope.changePasswordModal).then(function (resp) {
                if (resp.businessException == null) {
                    if(document.getElementById("divSummaryMessageDashboard"))
                        showMessage("divSummaryMessageDashboard", resp.result, "success");
                    else if (document.getElementById("divSummaryMessageUser"))
                        showMessage("divSummaryMessageUser", resp.result, "success");
                    $('#divChangePassword').modal('hide');
                }
                else {
                    showMessage("divSummaryMessageChangePassword", resp.businessException.ExceptionMessage, "danger");
                }
            });
        }
    };
}