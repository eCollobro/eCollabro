//Global Methods
function getContentPagesController() {
    var scope = angular.element(document.getElementById("divContentPages")).scope();
    return scope;
}

function showContentPages(summaryMessage) {
    $("#placeHolderContentPages").html("");
    $("#divContentPages").show();
    if (summaryMessage != "") {
        showMessage("divSummaryMessageContentPages", summaryMessage, "success");
    }
    //refresh grid
    getContentPagesController().loadContentPages(getContentPagesController().contentPageCategoryId);

}

function getContentPageLink(contentPageId,contentPageTitle) {
    return "<a target='_blank' href='/Content/ContentPage/index/" + contentPageId + "'>" + contentPageTitle + "</a>";
}
//ContentPagesController for Angular
ecollabroApp.controller('contentPagesController', ['$scope', '$compile', 'contentPageService', function ($scope, $compile, contentPageService) {
    $scope.contentPageCategory = {};
    $scope.contentPageCategoryId = 0;

    //Method initialize
    $scope.initialize = function (contentPageCategoryId, canAdd, canEdit, canDelete) {
        $scope.canAdd = canAdd;
        $scope.canEdit = canEdit;
        $scope.canDelete = canDelete;
        $scope.contentPageCategoryId = contentPageCategoryId;
        $scope.loadContentPages();
    };

    // Method loadContentPages
    $scope.loadContentPages = function () {
        contentPageService.getContentPages($scope.contentPageCategoryId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.contentPageCategory = resp.result.data;
                var oTable = $("#tblContentPages").dataTable();
                oTable.fnClearTable();
                for (var ctr = 0; ctr < $scope.contentPageCategory.ContentPages.length; ctr++) {
                    var contentPage = $scope.contentPageCategory.ContentPages[ctr];
                    if ($scope.canEdit || $scope.canDelete || $scope.canAdd)
                        oTable.fnAddData([getContentPageLink(contentPage.ContentPageId, contentPage.ContentPageId), getContentPageLink(contentPage.ContentPageId, contentPage.ContentPageTitle), checkActiveDisplay(contentPage.IsActive), contentPage.ApprovalStatus, $scope.getTableLink(contentPage.ContentPageId, contentPage.ContentPageCategoryId)]);
                    else
                        oTable.fnAddData([getContentPageLink(contentPage.ContentPageId, contentPage.ContentPageTitle)]);
                }
            }
            else {
                showMessage("divSummaryMessageContentPages", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

 
    //Method openContentPage
    $scope.openContentPage = function (contentPageId, contentPageCategoryId) {
        contentPageService.getContentPageView(contentPageCategoryId,contentPageId).then(function (resp) {
            if (resp.businessException == null) {
                $("#placeHolderContentPages").html(resp.result);
                $("#divContentPages").hide();
                $compile($("#placeHolderContentPages").contents())($scope);
            }
            else {
                showMessage("divSummaryMessageContentPages", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };


    //Method deleteContentPage
    $scope.deleteContentPage = function (contentPageId) {
        bootbox.confirm("Are you sure to delete selected Content Page?", function (result) {
            if (result) {
                blogService.deleteContentPage(contentPageId).then(function (resp) {
                    if (resp.businessException == null) {
                        showContentPages(resp.result);
                    }
                    else {
                        showMessage("divSummaryMessageContentPages", resp.businessException.ExceptionMessage, "danger");
                    }
                });
            }
        });
     };

    //Method contentPageCategories
    $scope.contentPageCategories = function () {
        location.href = "/Content/ContentPage/Categories";
    }

    //Method getTableLink
    $scope.getTableLink = function (Id) {
        var tableLinks = "";
        if ($scope.canEdit)
            tableLinks = "<a  href='javascript:getContentPagesController().openContentPage(" + Id + "," + $scope.contentPageCategoryId + ")'>Edit</a> ";
        if ($scope.canDelete) {
            if (tableLinks != "")
                tableLinks += " | ";
            tableLinks += "<a href='javascript:getContentPagesController().deleteContentPage(" + Id + ");'>Delete</a>  ";
        }

        return tableLinks;

    };


}]);