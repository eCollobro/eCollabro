// DocumentLibraryController for AngularJS
ecollabroApp.controller('documentLibraryController', ['$scope', 'documentLibraryService', function ($scope, documentLibraryService) {
    $scope.documentLibrary = {};

    //Method loadDocumentLibrary
    $scope.loadDocumentLibrary = function (categoryId) {
        $scope.documentLibrary.DocumentLibraryId = categoryId;
        if ($scope.documentLibrary.DocumentLibraryId == 0) {
            $scope.documentLibrary.IsActive = true;
            return;
        }
        documentLibraryService.getDocumentLibrary(categoryId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.documentLibrary = resp.result;
            }
            else {
                showMessage("divSummaryMessageDocumentLibrary", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method saveDocumentLibrary
    $scope.saveDocumentLibrary = function () {
        if (!$("#frmDocumentLibrary").valid()) {
            return;
        }
        documentLibraryService.saveDocumentLibrary($scope.documentLibrary).then(function (resp) {
            if (resp.businessException == null) {
                $scope.documentLibrary.DocumentLibraryId = resp.result.Id;
                var divDocumentLibraries = document.getElementById("divDocumentLibraries");
                if (divDocumentLibraries) {
                    showDocumentLibraries(resp.result.Message); // calling parent's method
                }
                else {
                    showMessage("divSummaryMessageDocumentLibrary", resp.result.Message, "success");
                }
            }
            else {
                showMessage("divSummaryMessageDocumentLibrary", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method openDocumentLibraries
    $scope.openDocumentLibraries = function () {
        location.href="/content/documentlibrary/documentlibraries"
    };

}]);