//document service
ecollabroApp.service('documentLibraryService',['serviceHandler', function (serviceHandler) {

    this.getDocumentLibraries = function () {
        return serviceHandler.executeGetService("/DocumentLibraryApi/GetDocumentLibraries/" + headerController().currentSiteId);
    }

    this.getDocumentLibrary = function (categoryId) {
        return serviceHandler.executeGetService("/DocumentLibraryApi/GetDocumentLibrary/" + headerController().currentSiteId + "/" + categoryId);
    }

    this.deleteDocumentLibrary = function (categoryId) {
        return serviceHandler.executeGetService("/DocumentLibraryApi/DeleteDocumentLibrary/" + headerController().currentSiteId + "/" + categoryId);
    }

    this.saveDocumentLibrary = function (documentLibrary) {
        return serviceHandler.executePostService("/DocumentLibraryApi/SaveDocumentLibrary/" + headerController().currentSiteId, documentLibrary);
    }

    this.getRecentDocuments = function () {
        return serviceHandler.executeGetService("/DocumentLibraryApi/GetRecentDocuments/" + headerController().currentSiteId);
    }

    this.getDocuments = function (categoryId) {
        return serviceHandler.executeGetService("/DocumentLibraryApi/GetDocuments/" + headerController().currentSiteId + "/" + categoryId);
    }

    this.getDocument = function (documentId) {
        return serviceHandler.executeGetService("/DocumentLibraryApi/GetDocument/" + headerController().currentSiteId + "/" + documentId);
    }

    this.deleteDocument = function (documentId) {
        return serviceHandler.executeGetService("/DocumentLibraryApi/DeleteDocument/" + headerController().currentSiteId + "/" + documentId);
    }

    this.saveDocument = function (document) {
        return serviceHandler.executePostService("/DocumentLibraryApi/SaveDocument/" + headerController().currentSiteId, document);
    }

    // controllers 
    this.getDocumentLibraryView = function (documentLibraryId) {
        return serviceHandler.executeGetService("/Content/DocumentLibrary/DocumentLibrary/"+ documentLibraryId);
    }

    this.getDocumentView = function (documentLibraryId,documentId) {
        return serviceHandler.executeGetService("/Content/DocumentLibrary/ManageDocument/" + documentId + "?libId=" + documentLibraryId);
    }
}]);