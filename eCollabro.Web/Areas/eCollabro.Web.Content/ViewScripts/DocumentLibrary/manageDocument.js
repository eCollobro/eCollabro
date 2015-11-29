// DocumentController for AngularJS
ecollabroApp.controller('documentController',['$scope', 'documentLibraryService', function ($scope, documentLibraryService) {
    $scope.document = {};
    $scope.documentLibraries = [];
    $scope.selectedDocumentLibrary = {};

    //Method loadDocument
    $scope.loadDocument = function (documentId, documentLibraryId) {
        $scope.document.DocumentId = documentId;
        if ($scope.document.DocumentId == 0) {
            $scope.document.DocumentLibraryId = documentLibraryId;
            $scope.loadDocumentLibraries();
            $scope.document.IsActive = true;

            return;
        }
        documentLibraryService.getDocument(documentId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.document = resp.result;
                $scope.loadDocumentLibraries();
            }
            else {
                showMessage("divSummaryMessageDocument", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    // Method loadDocumentLibraries
    $scope.loadDocumentLibraries = function () {
        documentLibraryService.getDocumentLibraries().then(function (resp) {
            if (resp.businessException == null) {
                $scope.documentLibraries = resp.result.data;
                if ($scope.documentLibraries.length > 0)
                {
                    $scope.selectedDocumentLibrary = $scope.documentLibraries[0];
                    $scope.document.DocumentLibraryId=$scope.selectedDocumentLibrary.DocumentLibraryId;
                }
                for (var ctr = 0; ctr < $scope.documentLibraries.length; ctr++) {
                    var documentCat = $scope.documentLibraries[ctr];
                    if (documentCat.DocumentLibraryId == $scope.document.DocumentLibraryId) {
                        $scope.selectedDocumentLibrary = documentCat;
                        break;
                    }
                }
            }
            else {
                showMessage("divSummaryMessageDocument", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    // Method updateDocumentLibrary
    $scope.updateDocumentLibrary = function () {
        $scope.document.DocumentLibraryId = $scope.selectedDocumentLibrary.DocumentLibraryId;
    };

    //Method saveDocument
    $scope.saveDocument = function () {
        if (!$("#frmDocument").valid()) {
            return;
        }
        else {
            if ($("#DocumentFile").val() == "" && ($scope.document.DocumentId == null || $scope.document.DocumentId == 0))
                showMessage("divSummaryMessageDocument", "Please select a document to upload.", "danger");
            else {
                $("#DocumentLibraryId").val($scope.document.DocumentLibraryId);
                $("#frmDocument").submit();
            }
        }
    };

    //Method openDocuments
    $scope.openDocuments = function (documentLibraryId) {
        location.href = "/content/documentlibrary/documents/" + documentLibraryId;
    };

}]);