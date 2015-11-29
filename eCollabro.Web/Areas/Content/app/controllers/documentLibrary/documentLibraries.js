//Global Methodsc
function getDocumentLibrariesController() {
    var scope = angular.element(document.getElementById("divDocumentLibraries")).scope();
    return scope;
}

function showDocumentLibraries(summaryMessage) {
    $("#placeHolderDocumentLibraries").html("");
    $("#divDocumentLibraries").show();
    if (summaryMessage != "") {
        showMessage("divSummaryMessageDocumentLibraries", summaryMessage, "success");
    }
    //refresh grid
    getDocumentLibrariesController().loadDocumentLibraries();

}

//DocumentLibrariesController for Angular
ecollabroApp.controller('documentLibrariesController',['$scope', '$compile','documentLibraryService', function ($scope, $compile,documentLibraryService) {
    $scope.documentLibraries = [];
    $scope.canAdd = false;
    $scope.canEdit = false;
    $scope.canDelete = false;

    //Method initialize
    $scope.initialize = function (canAdd, canEdit, canDelete) {
        $scope.canAdd = canAdd;
        $scope.canEdit = canEdit;
        $scope.canDelete = canDelete;
        $scope.fields = [{ mDataProp: "DocumentLibraryName" }];
        if ($scope.canEdit || $scope.canDelete || $scope.canAdd)
            $scope.fields = [{ mDataProp: "DocumentLibraryId" }, { mDataProp: "DocumentLibraryName" }, { mDataProp: "IsActive" }, { mDataProp: "Action", bSortable: false }];

        $scope.loadDocumentLibraries();
    };

    // Method loadDocumentLibraries
    $scope.loadDocumentLibraries = function () {

        if ($scope.oTable)
            $('#tblDocumentLibraries').dataTable().fnDestroy();

        $scope.oTable = $("#tblDocumentLibraries").dataTable(
            {
                "processing": true,
                "serverSide": false,
                aoColumns: $scope.fields,
                "ajax": $.fn.dataTable.setData = function (request, drawCallback, settings) {
                    documentLibraryService.getDocumentLibraries(request).then(function (resp) {
                        if (resp.businessException == null) {
                            $scope.documentLibraries = resp.result.data;
                            angular.forEach(resp.result.data, function (value, key) {
                                value.IsActive = checkActiveDisplay(value.IsActive);
                                value.Action = $scope.getTableLink(value.DocumentLibraryId);
                            })
                            drawCallback(resp.result);
                        }
                        else {
                            showMessage("divSummaryMessageDocumentLibraries", resp.businessException.ExceptionMessage, "danger");
                        }
                    });

                }

            });
        bindEnterEventOnTable("tblDocumentLibraries", $scope.oTable);
     };

    //Method openDocumentLibrary
    $scope.openDocumentLibrary = function (documentLibraryId) {
        documentLibraryService.getDocumentLibraryView(documentLibraryId).then(function (resp) {
            if (resp.businessException == null) {
                $("#placeHolderDocumentLibraries").html(resp.result);
                $("#divDocumentLibraries").hide();
                $compile($("#placeHolderDocumentLibraries").contents())($scope);
            }
            else {
                showMessage("divSummaryMessageDocumentLibraries", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };


    //Method deleteDocumentLibrary
    $scope.deleteDocumentLibrary = function (categoryId) {
        bootbox.confirm("Are you sure to delete selected Document Category?", function (result) {
            if (result) {
                documentLibraryService.deleteDocumentLibrary(categoryId).then(function (resp) {
                    if (resp.businessException == null) {
                        showDocumentLibraries(resp.result);
                    }
                    else {
                        showMessage("divSummaryMessageDocumentLibraries", resp.businessException.ExceptionMessage, "danger");
                    }
                });
            }
        });
     };

    //Method openDocuments
    $scope.openDocuments = function (libraryId) {
        location.href = "/Content/DocumentLibrary/Documents/" + libraryId
    };

    //Method getTableLink
    $scope.getTableLink = function (Id) {
        var tableLinks = "";
        if ($scope.canEdit)
            tableLinks = "<a class='ajaxLink' href='javascript:getDocumentLibrariesController().openDocumentLibrary(" + Id + ")'>Edit</a> ";
        if ($scope.canDelete) {
            if (tableLinks != "")
                tableLinks += " | ";
            tableLinks += "<a href='javascript:getDocumentLibrariesController().deleteDocumentLibrary(" + Id + ");'>Delete</a>"
        }
        if (tableLinks != "")
            tableLinks += " | ";
        tableLinks += "<a href='javascript:getDocumentLibrariesController().openDocuments(" + Id + ");'>Documents</a> ";
        return tableLinks;

    };

}]);