//Global Methodsc
function getUsersController()
{
    var scope = angular.element(document.getElementById("divUsers")).scope();
    return scope;
}

function showUsers(summaryMessage) {
        $("#placeHolderUsers").html("");
        $("#divUsers").show();
        if (summaryMessage != "") {
            showMessage("divSummaryMessageUsers", summaryMessage, "success");
        }
        //refresh grid
        getUsersController().loadUsers();

    }

//UsersController for Angular
ecollabroApp.controller('usersController',['$scope', '$compile','securityService', function ($scope, $compile,securityService) {
    $scope.users = [];

    //Method initialize
    $scope.initialize = function (canAdd, canEdit, canDelete) {
        $scope.canAdd = canAdd;
        $scope.canEdit = canEdit;
        $scope.canDelete = canDelete;
        $scope.fields = [{ mDataProp: "UserName" }, { mDataProp: "Email" }, { mDataProp: "IsActive" },{ mDataProp: "IsConfirmed" }, { mDataProp: "IsApproved" },{ mDataProp: "IsLocked" },{ mDataProp: "Action", bSortable: false }];
        $scope.loadUsers();
    };

    // Method loadUsers
    $scope.loadUsers = function () {

        if ($scope.oTable)
            $('#tblUsers').dataTable().fnDestroy();

        $scope.oTable = $("#tblUsers").dataTable(
           {
               "processing": true,
               "serverSide": false,
               aoColumns: $scope.fields,
               "ajax": $.fn.dataTable.setData = function (request, drawCallback, settings) {
                   securityService.getUsers(request).then(function (resp) {
                       if (resp.businessException == null) {
                           $scope.users = resp.result.data;
                           angular.forEach(resp.result.data, function (value, key) {
                               value.IsActive = checkActiveDisplay(value.IsActive);
                               value.IsConfirmed = checkActiveDisplay(value.IsConfirmed);
                               value.IsLocked = checkActiveDisplay(value.IsLocked);
                               value.IsApproved = checkActiveDisplay(value.IsApproved);
                               value.Action = $scope.getTableLink(value.UserId);
                           })
                           drawCallback(resp.result);
                       }
                       else {
                           showMessage("divSummaryMessageUsers", resp.businessException.ExceptionMessage, "danger");
                       }
                   });

               }
           });
        bindEnterEventOnTable("tblUsers", $scope.oTable);
  };

    //Method openUser
    $scope.openUser = function (userId) {
        securityService.getUserView(userId).then(function (resp) {
            if (resp.businessException == null) {
                $("#placeHolderUsers").html(resp.result);
                $("#divUsers").hide();
                $compile($("#placeHolderUsers").contents())($scope);
            }
            else {
                showMessage("divSummaryMessageUsers", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };


    //Method deleteUser
    $scope.deleteUser = function (userId) {
        bootbox.confirm("Are you sure to delete selected User?", function (result) {
            if (result) {
                securityService.deleteUser(userId).then(function (resp) {
                    if (resp.businessException == null) {
                        showUsers(resp.result);
                    }
                    else {
                        showMessage("divSummaryMessageUsers", resp.businessException.ExceptionMessage, "danger");
                    }
                });
            }
        });
    };

    //Method getTableLink
    $scope.getTableLink = function (Id) {
        var editCaption = "View";
        if ($scope.canEdit)
            editCaption = "Edit";
        var tableLinks = "<a class='ajaxLink' href='javascript:getUsersController().openUser(" + Id + ")'>" + editCaption + "</a>";
        if ($scope.canDelete) {
            tableLinks += " | <a href='javascript:getUsersController().deleteUser(" + Id + ");'>Delete</a>"
        }
        return tableLinks;
    };
   
 }]);