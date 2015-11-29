//Global Method
function getRolesController() {
    var scope = angular.element(document.getElementById("divRoles")).scope();
    return scope;
}

function showRoles(summaryMessage) {
    $("#placeHolderRoles").html("");
    $("#divRoles").show();
    if (summaryMessage != "") {
        showMessage("divSummaryMessageRoles", summaryMessage, "success");
    }
    //refresh grid
    getRolesController().loadRoles();

}

//RolesController for Angular
ecollabroApp.controller('rolesController', ['$scope','$compile','securityService',function ($scope, $compile, securityService) {
    $scope.roles = [];

    //Method initialize
    $scope.initialize = function (canEdit, canDelete) {
        $scope.canEdit = canEdit;
        $scope.canDelete = canDelete;
        $scope.fields = [ { mDataProp: "RoleCode" }, { mDataProp: "RoleName" }, { mDataProp: "IsActive" }, { mDataProp: "Action", bSortable: false }];
        $scope.loadRoles();
    };

    // Method loadRoles
    $scope.loadRoles = function () {
        if ($scope.oTable)
            $('#tblRoles').dataTable().fnDestroy();

        $scope.oTable = $("#tblRoles").dataTable(
            {
                "processing": true,
                "serverSide": false,
                aoColumns: $scope.fields,
                "ajax": $.fn.dataTable.setData = function (request, drawCallback, settings) {
                    securityService.getRoles(request).then(function (resp) {
                        if (resp.businessException == null) {
                            $scope.roles = resp.result.data;
                            angular.forEach(resp.result.data, function (value, key) {
                                value.IsActive = checkActiveDisplay(value.IsActive);
                                value.Action = $scope.getTableLink(value);
                            })
                            drawCallback(resp.result);
                        }
                        else {
                            showMessage("divSummaryMessageRoles", resp.businessException.ExceptionMessage, "danger");
                        }
                    });

                }

            });
        bindEnterEventOnTable("tblRoles", $scope.oTable);
    };

    //Method openRole
    $scope.openRole = function (roleId) {
        securityService.getRoleView(roleId).then(function (resp) {
            if (resp.businessException == null) {
                $("#placeHolderRoles").html(resp.result);
                $("#divRoles").hide();
                $compile($("#placeHolderRoles").contents())($scope);
            }
            else {
                showMessage("divSummaryMessageRoles", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };


    //Method deleteRole
    $scope.deleteRole = function (roleId) {
        bootbox.confirm("Are you sure to delete selected Role?", function (result) {
            if (result) {
                securityService.deleteRole(roleId).then(function (resp) {
                    if (resp.businessException == null) {
                        showRoles(resp.result);
                    }
                    else {
                        showMessage("divSummaryMessageRoles", resp.businessException.ExceptionMessage, "danger");
                    }
                });
            }
        });
    };

    //Method manageFeatures
    $scope.manageFeatures = function (roleId) {
        securityService.getRoleFeaturesView(roleId).then(function (resp) {
            if (resp.businessException == null) {
                $("#placeHolderRoles").html(resp.result);
                $("#divRoles").hide();
                $compile($("#placeHolderRoles").contents())($scope);
            }
            else {
                showMessage("divSummaryMessageRoles", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method getTableLink
    $scope.getTableLink = function (role) {
        var tableLinks = "";
        if (parseBool(role.IsSystem) == false) {
            if ($scope.canEdit)
                tableLinks = "<a class='text-success' href='javascript:getRolesController().openRole(" + role.RoleId + ")'><span class='glyphicon glyphicon-pencil'></span> Edit</a> ";
            if ($scope.canDelete) {
                if (tableLinks != "")
                    tableLinks += "&nbsp;&nbsp;";
                tableLinks += "<a class='text-danger' href='javascript:getRolesController().deleteRole(" + role.RoleId + ");'><span class='glyphicon glyphicon-trash'></span> Delete</a>"
            }
            if (tableLinks != "")
                tableLinks += "&nbsp;&nbsp;";
            tableLinks += "<a class='text-warning' href='javascript:getRolesController().manageFeatures(" + role.RoleId + ");'><span class='glyphicon glyphicon-tasks'></span> Features</a>";
        }

        return tableLinks;
    };

    //method openRoles
    $scope.openRoles = function () {
        location.href = "/security/roles";
    };
}]);