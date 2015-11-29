//Global Methods
function getAnnouncementsController() {
    var scope = angular.element(document.getElementById("divAnnouncements")).scope();
    return scope;
}

function showAnnouncements(summaryMessage) {
    $("#placeHolderAnnouncements").html("");
    $("#divAnnouncements").show();
    if (summaryMessage != "") {
        showMessage("divSummaryMessageAnnouncements", summaryMessage, "success");
    }
    //refresh grid
    getAnnouncementsController().loadAnnouncements();

}

function getAnnouncementLink(announcementId, announcementTitle) {
    return "<a  href='/Content/Announcement/Announcement/" + announcementId + "'>" + announcementTitle + "</a>";
}
//AnnouncementsController for Angular
ecollabroApp.controller('announcementsController', ['$scope', '$compile', 'announcementService', function ($scope, $compile, announcementService) {
    $scope.announcements = [];

    //Method initialize
    $scope.initialize = function (canAdd,canEdit, canDelete) {
        $scope.canAdd = canAdd;
        $scope.canEdit = canEdit;
        $scope.canDelete = canDelete;
        $scope.loadAnnouncements();
    };

    // Method loadAnnouncements
    $scope.loadAnnouncements = function () {
        announcementService.getAnnouncements().then(function (resp) {
            if (resp.businessException == null) {
                $scope.announcements = resp.result.data;
                var oTable = $("#tblAnnouncements").dataTable();

                oTable.fnClearTable();
                for (var ctr = 0; ctr < $scope.announcements.length; ctr++) {
                    var announcement = $scope.announcements[ctr];
                    if ($scope.canEdit || $scope.canDelete || $scope.canAdd)
                        oTable.fnAddData([getAnnouncementLink(announcement.AnnouncementId, announcement.AnnouncementId),getAnnouncementLink(announcement.AnnouncementId, announcement.AnnouncementTitle), checkActiveDisplay(announcement.IsActive), $scope.getTableLink(announcement.AnnouncementId)]);
                    else
                        oTable.fnAddData([getAnnouncementLink(announcement.AnnouncementId, announcement.AnnouncementTitle)]);
                }
            }
            else {
                showMessage("divSummaryMessageAnnouncements", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };



    //Method openAnnouncement
    $scope.openAnnouncement = function (announcementId) {
        announcementService.getAnnouncementView(announcementId).then(function (resp) {
            if (resp.businessException == null) {
                $("#placeHolderAnnouncements").html(resp.result);
                $("#divAnnouncements").hide();
                $compile($("#placeHolderAnnouncements").contents())($scope);
            }
            else {
                showMessage("divSummaryMessageAnnouncements", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };


    //Method deleteAnnouncement
    $scope.deleteAnnouncement = function (announcementId) {
        bootbox.confirm("Are you sure to delete selected Announcement?", function (result) {
            if (result) {
                announcementService.deleteAnnouncement(announcementId).then(function (resp) {
                    if (resp.businessException == null) {
                        showAnnouncements(resp.result);
                    }
                    else {
                        showMessage("divSummaryMessageAnnouncements", resp.businessException.ExceptionMessage, "danger");
                    }
                });
            }
        });
    };

    //Method getTableLink
    $scope.getTableLink = function (announcementId) {
        var tableLinks = "";
        if ($scope.canEdit)
            tableLinks = "<a class='ajaxLink' href='javascript:getAnnouncementsController().openAnnouncement(" + announcementId +")'>Edit</a> ";
        if ($scope.canDelete) {
            if (tableLinks != "")
                tableLinks += " | ";
            tableLinks += "<a href='javascript:getAnnouncementsController().deleteAnnouncement(" + announcementId + ");'>Delete</a> ";
        }

        return tableLinks;

    };

}]);