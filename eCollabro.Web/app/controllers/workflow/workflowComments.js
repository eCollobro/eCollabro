function getWorkflowCommentsController() {
    var scope = angular.element(document.getElementById("divWorkflowComments")).scope();
    return scope;
}


// ContentCommentsController for AngularJS
ecollabroApp.controller('WorkflowCommentsController',['$scope', 'workflowService', function ($scope, workflowService) {
    $scope.workflowComments = [];
    $scope.contextId=0;
    $scope.contextContentId = 0;

    //Method Initialize
    $scope.Initialize = function (contextId, contextContentId) {
        $scope.contextId = context
        $scope.ContextContentId = contextContentId;
    };

    //Method Initialize
    $scope.LoadWorkflowComments = function () {
        workflowService.getWorkflowComments($scope.contextId, $scope.contextContentId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.workflowComments = resp.result;
            }
            else {
                showMessage("divSummaryMessageWorkflowComments", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method AddComment
    $scope.AddComment = function () {
        $('#dlgAddComment').modal({
            keyboard: false,
            backdrop: 'static'

        })
    };

    //Method SaveComment
    $scope.saveComment = function () {
        $scope.ContentComment={};
        $scope.ContentComment.Comment=$("#txtComments").val();
        $scope.ContentComment.Context = $scope.Context;
        $scope.ContentComment.ContextId = $scope.ContextId;
        workflowService.saveWorkflowComment($scope.contentComment).then(function (resp) {
            if (resp.businessException == null) {
                $scope.loadWorkflowComments();
                showMessage("divSummaryMessageWorkflowComments", resp.result.Message, "success");
                $('#dlgAddComment').modal('hide');
            }
            else {
                showMessage("divSummaryMessageAddComment", resp.businessException.ExceptionMessage, "danger");
            }
        });   
    };

}]);