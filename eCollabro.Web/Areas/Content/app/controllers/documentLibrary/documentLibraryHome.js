//Global Methodsc
function getDocumentHomeController() {
    var scope = angular.element(document.getElementById("divDocumentHome")).scope();
    return scope;
}

//DocumentsController for Angular
ecollabroApp.controller('documentHomeController',['$scope', 'documentLibraryService', function ($scope, documentLibraryService) {
    $scope.recentDocuments = [];
    $scope.documentLibraries = [];

    // Method initialize
    $scope.initialize = function () {
        $scope.loadRecentDocuments();
        $scope.loadDocumentLibraries();

    };

    // Method LoadRecentDocuments
    $scope.loadRecentDocuments = function () {
        documentLibraryService.getRecentDocuments().then(function (resp) {
            if (resp.businessException == null) {
                $scope.recentDocuments = resp.result.data;
            }
            else {
                showMessage("divSummaryMessageDocumentHome", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    // Method loadDocumentLibraries
    $scope.loadDocumentLibraries = function () {
        documentLibraryService.getDocumentLibraries().then(function (resp) {
            if (resp.businessException == null) {
                $scope.documentLibraries = resp.result.data;
            }
            else {
                showMessage("divSummaryMessageDocumentHome", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

}]);