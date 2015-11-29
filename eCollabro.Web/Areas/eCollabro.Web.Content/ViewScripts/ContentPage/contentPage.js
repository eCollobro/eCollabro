var $ContentPageContentEditor;


// ContentPageController for AngularJS
ecollabroApp.controller('contentPageController', ['$scope', 'contentPageService','securityService', function ($scope, contentPageService,securityService) {
    $scope.contentPage = {};
    $scope.contentPageCategories = [];
    $scope.selectedContentPageCategory = {};
    $scope.parentNavigations = [];
    $scope.contentPage.AddToNavigation = false;

    //Method loadContentPage
    $scope.loadContentPage = function (contentPageId, contentPageCategoryId) {
        $scope.contentPage.ContentPageId = contentPageId;
        if ($scope.contentPage.ContentPageId == 0) {
            $scope.contentPage.ContentPageCategoryId = contentPageCategoryId;
            $scope.loadContentPageCategories();
            $scope.loadParentNavigations();
            $scope.contentPage.IsActive = true;
            return;
        }
        contentPageService.getContentPage( $scope.contentPage.ContentPageId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.contentPage = resp.result;
                $scope.loadContentPageCategories();
                $scope.loadParentNavigations();
            }
            else {
                showMessage("divSummaryMessageContentPage", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    // Method loadContentPageCategories
    $scope.loadContentPageCategories = function () {
        contentPageService.getCategories().then(function (resp) {
            if (resp.businessException == null) {
                $scope.contentPageCategories = resp.result.data;
                if ($scope.contentPageCategories.length > 0)
                {
                    $scope.selectedContentPageCategory = $scope.contentPageCategories[0];
                    $scope.contentPage.ContentPageCategoryId = $scope.selectedContentPageCategory.ContentPageCategoryId;
                }
                for (var ctr = 0; ctr < $scope.contentPageCategories.length; ctr++) {
                    var contentPageCat = $scope.contentPageCategories[ctr];
                    if (contentPageCat.ContentPageCategoryId == $scope.contentPage.ContentPageCategoryId) {
                        $scope.selectedContentPageCategory = contentPageCat;
                        break;
                    }
                }
                // update cleditor with the latest html contents
                $ContentPageContentEditor.updateFrame();
            }
            else {
                showMessage("divSummaryMessageContentPage", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    // Method updateContentPageCategory
    $scope.updateContentPageCategory = function () {
        $scope.contentPage.ContentPageCategoryId = $scope.selectedContentPageCategory.ContentPageCategoryId;
    };

    //Method saveContentPage
    $scope.saveContentPage = function () {
        $ContentPageContentEditor.updateTextArea();
        $scope.contentPage.ContentPageContent = $("#ContentPageContent").val();
        if ($scope.contentPage.AddToNavigation == false)
            $("#MenuTitle").val("-"); // ignore validation
        $("#frmContentPage").data("validator").settings.ignore = "";
        if (!$("#frmContentPage").valid()) {
            return;
        }
        else {
            contentPageService.saveContentPage($scope.contentPage).then(function (resp) {
                if (resp.businessException == null) {
                    $scope.contentPage.ContentPageId = resp.result.Id;
                    var divContentPages = document.getElementById("divContentPages");
                    if (divContentPages) {
                        showContentPages(resp.result.Message); // calling parent's method
                    }
                    else {
                        showMessage("divSummaryMessageContentPage",resp.result.Message, "success");
                    }
                }
                else {
                    showMessage("divSummaryMessageContentPage", resp.businessException.ExceptionMessage, "danger");
                }
            });
        }
    };

    // Method loadParentNavigations
    $scope.loadParentNavigations = function () {
        securityService.getNavigations().then(function (resp) {
            if (resp.businessException == null) {
                $scope.parentNavigations = resp.result;
                for (var ctr = 0; ctr < $scope.parentNavigations.length; ctr++) {
                    var nav = $scope.parentNavigations[ctr];
                    if (nav.NavigationId == $scope.contentPage.NavigationParentId) {
                        $scope.selectedParentNavigation = nav;
                        break;
                    }
                }
            }
            else {
                showMessage("divSummaryMessageContentPage", resp.businessException.ExceptionMessage, "danger");
            }
        }); 
    };

    // Method updateParentNavigation
    $scope.updateParentNavigation = function () {
        $scope.contentPage.NavigationParentId = $scope.selectedParentNavigation.NavigationId;
    };

    //Method openContentPages
    $scope.openContentPages = function (contentPageCategoryId) {
        location.href = "/content/contentpage/pages/" + contentPageCategoryId;
    };

}]);