//DocumentsController for Angular
ecollabroApp.controller('documentLibraryDashboardWidgetController',['$scope', 'documentLibraryService', function ($scope, documentLibraryService) {
    $scope.recentDocuments = {};
    $scope.caption = "glyphicon glyphicon-chevron-down";
    //Method initialize
    $scope.initialize = function () {
        $scope.loadDocuments();
    };

    // Method loadDocuments
    $scope.loadDocuments = function () {
        documentLibraryService.getRecentDocuments().then(function (resp) {
            if (resp.businessException == null) {
                $scope.recentDocuments = resp.result.data;
            }
            else {
                showMessage("divSummaryMessageDocumentLibraryWidget", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method changeCaption()
    $scope.changeCaption = function () {
        if ($scope.caption == "glyphicon glyphicon-chevron-right")
            $scope.caption = "glyphicon glyphicon-chevron-down";
        else
            $scope.caption = "glyphicon glyphicon-chevron-right";
    }
}]);