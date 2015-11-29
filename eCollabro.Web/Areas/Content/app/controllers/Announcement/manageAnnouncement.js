var $announcementContentEditor;


// AnnouncementController for AngularJS
ecollabroApp.controller('manageAnnouncementController',['$scope', 'announcementService', function ($scope, announcementService) {
    $scope.announcement = {};
    $scope.announcementCategories = [];
    $scope.selectedAnnouncementCategory = {};

    //Method loadAnnouncement
    $scope.loadAnnouncement = function (announcementId) {
        $scope.announcement.AnnouncementId = announcementId;
        if ($scope.announcement.AnnouncementId == 0) {
            $scope.announcement.IsActive = true;
            return;
        }
        announcementService.getAnnouncement(announcementId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.announcement = resp.result;
                // update cleditor with the latest html contents
                setInterval(function(){ $announcementContentEditor.updateFrame()},1);
            }
            else {
                showMessage("divSummaryMessageAnnouncement", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method saveAnnouncement
    $scope.saveAnnouncement = function () {
        $announcementContentEditor.updateTextArea();
        $("#frmAnnouncement").data("validator").settings.ignore = "";
        $scope.announcement.AnnouncementDescription = $("#AnnouncementDescription").val();
        if (!$("#frmAnnouncement").valid()) {
            return;
        }
        else {
            announcementService.saveAnnouncement($scope.announcement).then(function (resp) {
                if (resp.businessException == null) {
                    $scope.announcement.AnnouncementId = resp.result.Id;
                    var divAnnouncements = document.getElementById("divAnnouncements");
                    if (divAnnouncements) {
                        showAnnouncements(resp.result.Message); // calling parent's method
                    }
                    else {
                        showMessage("divSummaryMessageAnnouncement", resp.result.Message, "success");
                    }

                }
                else {
                    showMessage("divSummaryMessageAnnouncement", resp.businessException.ExceptionMessage, "danger");
                }
            });
        }
    };

    //Method openAnnouncements
    $scope.openAnnouncements = function () {
        location.href = "/content/announcement/announcements";
    };

}]);