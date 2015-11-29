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
      
}]);