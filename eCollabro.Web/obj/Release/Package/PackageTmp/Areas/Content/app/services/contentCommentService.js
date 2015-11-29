//contentCommentervice
ecollabroApp.service('contentCommentService',['serviceHandler', function (serviceHandler) {
    
    this.getContentComments=function(contextId,contextContentId){
        return serviceHandler.executeGetService("/ContentCommentApi/GetContentComments/" + headerController().currentSiteId + "/" + contextId +"/"+ contextContentId);
    };

    this.saveContentComment = function (contentComment) {
        return serviceHandler.executePostService("/ContentCommentApi/SaveContentComment/" + headerController().currentSiteId, contentComment);
    };

    this.deleteContentComment = function (contentCommentId) {
        return serviceHandler.executeGetService("/ContentCommentApi/DeleteContentComment/" + headerController().currentSiteId + "/" + contentCommentId);
    };
     
    this.changeContentLikeDislike=function(contentId,contextId,liked){
        return serviceHandler.executeGetService("/ContentCommentApi/ChangeContentLikeDislike?contentId=" + contentId + "&contextId=" + contextId+ "&liked="+liked);
    };

    this.changeContentVote = function (contentId, contextId, vote) {
        return serviceHandler.executeGetService("/ContentCommentApi/ChangeContentVote?contentId=" + contentId + "&contextId=" + contextId + "&vote=" + vote);
    };

    this.changeContentRating = function (contentId, contextId, rating) {
        return serviceHandler.executeGetService("/ContentCommentApi/ChangeContentRating?contentId=" + contentId + "&contextId=" + contextId + "&rating=" + rating);
    };

}]);