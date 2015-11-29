//announcement service
ecollabroApp.service('announcementService', ['serviceHandler', function (serviceHandler) {

    this.getRecentAnnouncements = function () {
        return serviceHandler.executeGetService("/AnnouncementApi/GetRecentAnnouncements/" + headerController().currentSiteId);
    }

    this.getAnnouncements = function () {
        return serviceHandler.executeGetService("/AnnouncementApi/GetAnnouncements/" + headerController().currentSiteId);
    }

    this.getAnnouncement = function (announcementId) {
        return serviceHandler.executeGetService("/AnnouncementApi/GetAnnouncement/" + headerController().currentSiteId + "/" + announcementId);
    }

    this.deleteAnnouncement = function (announcementId) {
        return serviceHandler.executeGetService("/AnnouncementApi/DeleteAnnouncement/" + headerController().currentSiteId + "/" + announcementId);
    }

    this.saveAnnouncement = function (announcement) {
        return serviceHandler.executePostService("/AnnouncementApi/SaveAnnouncement/" + headerController().currentSiteId, announcement);
    }

    // controllers 
    this.getAnnouncementView = function (announcementId) {
        return serviceHandler.executeGetService("/Content/Announcement/ManageAnnouncement/" + announcementId);
    }
}]);
