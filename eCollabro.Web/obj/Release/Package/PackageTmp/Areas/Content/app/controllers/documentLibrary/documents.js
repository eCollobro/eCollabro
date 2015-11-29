//Global Methodsc
function getDocumentsController() {
    var scope = angular.element(document.getElementById("divDocuments")).scope();
    return scope;
}

function showDocuments(summaryMessage) {
    $("#placeHolderDocuments").html("");
    $("#divDocuments").show();
    if (summaryMessage != "") {
        showMessage("divSummaryMessageDocuments", summaryMessage, "success");
    }
    //refresh grid
    getDocumentsController().loadDocuments(getDocumentsController().documentLibrary.DocumentLibraryId);

}

function getDocumentLink(documentId, documentTitle) {
    return "<a  href='/Content/DocumentLibrary/Document/" + documentId + "'>" + documentTitle + "</a>";
}
//DocumentsController for Angular
ecollabroApp.controller('documentsController', ['$scope', '$compile', 'documentLibraryService', function ($scope, $compile, documentLibraryService) {
    $scope.documentLibrary = {};
    $scope.documentLibraryId = 0;

    //Method initialize
    $scope.initialize = function (documentLibraryId,canAdd, canEdit, canDelete) {
        $scope.canAdd = canAdd;
        $scope.canEdit = canEdit;
        $scope.canDelete = canDelete;
        $scope.documentLibraryId = documentLibraryId;
        $scope.loadDocuments();
    };

    // Method loadDocuments
    $scope.loadDocuments = function () {
        documentLibraryService.getDocuments($scope.documentLibraryId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.documentLibrary = resp.result.data;
                var oTable = $("#tblDocuments").dataTable();

                oTable.fnClearTable();
                for (var ctr = 0; ctr < $scope.documentLibrary.Documents.length; ctr++) {
                    var document = $scope.documentLibrary.Documents[ctr];
                    if ($scope.canAdd || $scope.canEdit || $scope.canDelete)
                        oTable.fnAddData([getDocumentLink(document.DocumentId, document.DocumentId),getDocumentLink(document.DocumentId, document.DocumentTitle), checkActiveDisplay(document.IsActive), document.ApprovalStatus, $scope.getTableLink(document.DocumentId, $scope.documentLibraryId)]);
                    else
                        oTable.fnAddData([getDocumentLink(document.DocumentId, document.DocumentTitle)]);
                }
            }
            else {
                showMessage("divSummaryMessageDocuments", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };



    //Method openDocument
    $scope.openDocument = function (documentId, documentLibraryId) {
        documentLibraryService.getDocumentView(documentLibraryId, documentId).then(function (resp) {
            if (resp.businessException == null) {
                $("#placeHolderDocuments").html(resp.result);
                $("#divDocuments").hide();
                $compile($("#placeHolderDocuments").contents())($scope);
            }
            else {
                showMessage("divSummaryMessageDocuments", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };


    //Method deleteDocument
    $scope.deleteDocument = function (documentId) {
        bootbox.confirm("Are you sure to delete selected Document?", function (result) {
            if (result) {
                documentLibraryService.deleteDocument(documentId).then(function (resp) {
                    if (resp.businessException == null) {
                        showDocuments(resp.result);
                    }
                    else {
                        showMessage("divSummaryMessageDocuments", resp.businessException.ExceptionMessage, "danger");
                    }
                });
            }
        });
    };

    //Method documentLibraries
    $scope.documentLibraries = function () {
        location.href = "/Content/DocumentLibrary/DocumentLibraries";
    }

    //Method getTableLink
    $scope.getTableLink = function (documentId) {
        var tableLinks = "";
        if ($scope.canEdit)
            tableLinks = "<a class='ajaxLink' href='javascript:getDocumentsController().openDocument(" + documentId + "," + $scope.documentLibraryId + ")'>Edit</a> ";
        if ($scope.canDelete) {
            if (tableLinks != "")
                tableLinks += " | ";
            tableLinks += "<a href='javascript:getDocumentsController().deleteDocument(" + documentId + ");'>Delete</a> ";
        }

        return tableLinks;

    };

}]);