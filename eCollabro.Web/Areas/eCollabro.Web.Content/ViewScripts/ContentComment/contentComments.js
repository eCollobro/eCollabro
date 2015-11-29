function getContentCommentsController() {
    var scope = angular.element(document.getElementById("divContentComments")).scope();
    return scope;
}

var list = "";
function getCommentList(commentList) {

    for (var ctr = 0; ctr < commentList.length; ctr++) {
        var addedBy=commentList[ctr].CreatedBy;
        var self = false;
        if (addedBy == headerController().currentUser) {
            addedBy = "me";
            self = true;
        }
        list += "<div class='media'>";
        list += "<a class='pull-left' href='#'>";
        list += "<img class='media-object' data-src='holder.js/64x64' alt='64x64' src='data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSI2NCIgaGVpZ2h0PSI2NCI+PHJlY3Qgd2lkdGg9IjY0IiBoZWlnaHQ9IjY0IiBmaWxsPSIjZWVlIi8+PHRleHQgdGV4dC1hbmNob3I9Im1pZGRsZSIgeD0iMzIiIHk9IjMyIiBzdHlsZT0iZmlsbDojYWFhO2ZvbnQtd2VpZ2h0OmJvbGQ7Zm9udC1zaXplOjEycHg7Zm9udC1mYW1pbHk6QXJpYWwsSGVsdmV0aWNhLHNhbnMtc2VyaWY7ZG9taW5hbnQtYmFzZWxpbmU6Y2VudHJhbCI+NjR4NjQ8L3RleHQ+PC9zdmc+' style='width: 64px; height: 64px;'>";
        list += "</a>";
        list += "<div class='media-body'>";
        if (!self)
            list += "<p class='media-heading text-info'> By " + addedBy + ", " + commentList[ctr].TimeInterval + "<a href='javascript:getContentCommentsController().addComment(" + commentList[ctr].ContentCommentId + ")'><span class='glyphicon glyphicon-comment' title='Add Comment'></span></a></p>";
        else {
            list += "<p class='media-heading text-info'> By " + addedBy + ", " + commentList[ctr].TimeInterval + "<a href='javascript:getContentCommentsController().editComment(" + commentList[ctr].ContentCommentId + ")'> <span class='glyphicon glyphicon-comment' title='Edit Comment'></span></a>";
            list += "<a href='javascript:getContentCommentsController().deleteComment(" + commentList[ctr].ContentCommentId + ")'> <span class='glyphicon glyphicon-remove-circle' title='Delete Comment'></span></a></p>";
        }
        list += "<div id='divComment" + commentList[ctr].ContentCommentId + "'>" + commentList[ctr].Comment + "</div>";
        if (commentList[ctr].ContentComments) {
            if (commentList[ctr].ContentComments.length > 0)
                getCommentList(commentList[ctr].ContentComments);
            list += "</div>";
        }

        list += "</div>";
    }

}

// ContentCommentsController for AngularJS
ecollabroApp.controller('contentCommentsController', ['$scope', 'contentCommentService', function ($scope, contentCommentService) {
    $scope.contentComments = [];
    $scope.contextId = "";
    $scope.contextContentId = 0;
    $scope.contentComment = {};

    //Method Initialize
    $scope.Initialize = function (contextId, contextContentId) {
        $scope.contextId = contextId
        $scope.contextContentId = contextContentId;
    };

    //Method loadComments
    $scope.loadComments = function (isCommentAllowed,isLikeAllowed, isRatingAllowed,isVotingAllowed) {
        $scope.isCommentAllowed = isCommentAllowed;
        $scope.isLikeAllowed = isLikeAllowed;
        $scope.isRatingAllowed = isRatingAllowed;
        $scope.isVotingAllowed = isVotingAllowed;
        if ($scope.isCommentAllowed) {
            contentCommentService.getContentComments($scope.contextId, $scope.contextContentId).then(function (resp) {
                if (resp.businessException == null) {
                    $scope.contentComments = resp.result;
                    list = "";
                    getCommentList($scope.contentComments);
                    $("#placeHolderContentComments").html(list);
                    $scope.isVisible = true;
                }
                else {
                    showMessage("divSummaryMessageContentComments", resp.businessException.ExceptionMessage, "danger");
                }
            });
        }
        if(isLikeAllowed)
        {

        }
        if(isRatingAllowed)
        {

        }
        if(isVotingAllowed)
        {

        }
  
    };

    //Method addNewComment
    $scope.addComment = function (currentNodeId) {
        if (!currentNodeId)
            $("#hdnCurrentNode").val(0);
        else
            $("#hdnCurrentNode").val(currentNodeId);
        $("#txtComments").val("");
        $scope.contentComment.ContentCommentId = 0;
        $('#dlgAddComment').modal({
            keyboard: false,
            backdrop: 'static'

        })
    };

    //Method editComment
    $scope.editComment = function (commentId) {
        $("#txtComments").val($("#divComment" + commentId).text());
        $scope.contentComment.ContentCommentId = commentId;
        $('#dlgAddComment').modal({
            keyboard: false,
            backdrop: 'static'

        })
    };

    //Method saveComment
    $scope.saveComment = function () {
        if ($("#txtComments").val() == "")
        {
            showMessage("divAddCommentModalSummary", "Please enter comments.", "danger");
            return;
        }
        $scope.contentComment.Comment = $("#txtComments").val();
        $scope.contentComment.ParentContentCommentId = $("#hdnCurrentNode").val();
        $scope.contentComment.ContextId = $scope.contextId;
        $scope.contentComment.ContextContentId = $scope.contextContentId;
        contentCommentService.saveContentComment($scope.contentComment).then(function (resp) {
            if (resp.businessException == null) {
                $scope.contentComment.ContentCommentId = resp.result.Id;
                $scope.loadComments();
                showMessage("divSummaryMessageContentComments", resp.result.Message, "success");
                $('#dlgAddComment').modal('hide');
            }
            else {
                showMessage("divSummaryMessageAddContent", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method deleteComment
    $scope.deleteComment = function (contentCommentId) {
        bootbox.confirm("Are you sure to delete selected Comment?", function (result) {
            if (result) {
                contentCommentService.deleteContentComment(contentCommentId).then(function (resp) {
                    if (resp.businessException == null) {
                        $scope.loadComments();
                        showMessage("divSummaryMessageContentComments", resp.result, "success");
                    }
                    else {
                        showMessage("divSummaryMessageContentComments", resp.businessException.ExceptionMessage, "danger");
                    }
                });
            }
        });
    };

}]);