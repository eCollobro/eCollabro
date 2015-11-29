//siteImage service
ecollabroApp.service('imageGalleryService',['serviceHandler', function (serviceHandler) {

     this.getImageGalleries = function () {
        return serviceHandler.executeGetService("/ImageGalleryApi/GetImageGalleries/" + headerController().currentSiteId);
    }

     this.getImageGallery = function (galleryId) {
        return serviceHandler.executeGetService("/ImageGalleryApi/GetImageGallery/" + headerController().currentSiteId + "/" + galleryId);
    }

    this.deleteImageGallery = function (galleryId) {
        return serviceHandler.executeGetService("/ImageGalleryApi/DeleteImageGallery/" + headerController().currentSiteId + "/" + galleryId);
    }

    this.saveImageGallery = function (imageGallery) {
        return serviceHandler.executePostService("/ImageGalleryApi/SaveImageGallery/" + headerController().currentSiteId, imageGallery);
    }

    this.getRecentImages = function () {
        return serviceHandler.executeGetService("/ImageGalleryApi/GetRecentImages/" + headerController().currentSiteId);
    }

    this.getImages = function (galleryId) {
        return serviceHandler.executeGetService("/ImageGalleryApi/GetImages/" + headerController().currentSiteId + "/" + galleryId);
    }

    this.getImage = function (imageId) {
        return serviceHandler.executeGetService("/ImageGalleryApi/GetImage/" + headerController().currentSiteId + "/" + imageId);
    }


    this.deleteImage = function (imageId) {
        return serviceHandler.executeGetService("/ImageGalleryApi/DeleteImage/" + headerController().currentSiteId + "/" + imageId);
    }

    this.saveImage = function (image) {
        return serviceHandler.executePostService("/ImageGalleryApi/SaveImage/" + headerController().currentSiteId, image);
    }

    // controllers 
    this.getImageGalleryView = function (imageGalleryId) {
        return serviceHandler.executeGetService("/Content/ImageGallery/ImageGallery/"+ imageGalleryId);
    }

    this.getImageView = function (imageGalleryId,imageId) {
        return serviceHandler.executeGetService("/Content/ImageGallery/ManageImage/" + imageId + "?libId=" + imageGalleryId);
    }
}]);