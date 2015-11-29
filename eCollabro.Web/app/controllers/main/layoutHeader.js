function headerController() {
    var scope = angular.element(document.getElementById("divLayoutHeader")).scope();
    return scope;
}
//SiteMenuController for Angular
ecollabroApp.controller('layoutHeaderController', ['$scope', 'securityService',function ($scope, securityService) {
    $scope.userNavigations = {};
    $scope.siteMenuHtml = "";
    $scope.userSites = [];
    $scope.selectedSite = {};
    $scope.allowRegistration = false;
    $scope.userAuthenticated = false;
    $scope.currentSiteId = 0;
    $scope.currentUser ="";
    // Method initialize
    $scope.initialize = function (siteId, userAuthenticated,user) {
        if (window.location.pathname == "/Setup") // do nothing
            return;
        $scope.currentSiteId = siteId;
        $scope.userAuthenticated = userAuthenticated;
        $scope.currentUser = user;
        $scope.loadMenu();
        if ($scope.userAuthenticated)
            $scope.loadUserSites();
        else
           $scope.checkRegistrationAllowed();
    };

    //Method loadMenu
    $scope.loadMenu = function () {
        securityService.getUserNavigations().then(function (resp) {
            if (resp.businessException == null) {
                $scope.userNavigations = resp.result;

                // create menu
                $scope.siteMenuHtml = "";
                $scope.siteMenuHtml += "<!-- menu definition -->";
                $scope.siteMenuHtml += "<ul id='main-menu' class='sm sm-blue'>";
                $scope.prepareMenu($scope.userNavigations);
                $scope.siteMenuHtml += "</ul";
                $("#userMenu").html($scope.siteMenuHtml);
                $('#main-menu').smartmenus({
                    subMenusSubOffsetX: 1,
                    subMenusSubOffsetY: -8
                });
            }
            else {
                showMessage("divSummaryMessageHeader", resp.businessException.ExceptionMessage, "danger");
            }
        })
    };

    $scope.prepareMenu = function (navigations) {
        for (var ctr = 0; ctr < navigations.length; ctr++) {
            if (navigations[ctr].ChildNavigations.length == 0) {
                $scope.siteMenuHtml += "<li>";
                $scope.siteMenuHtml += "<a href='" + navigations[ctr].Link + "'>" + nullToBlank(navigations[ctr].AdditionalHtml) + navigations[ctr].NavigationText + "</a>";
                $scope.siteMenuHtml += "</li>";
            }
            else {
                $scope.siteMenuHtml += "<li>";
                $scope.siteMenuHtml += "<a href='" + navigations[ctr].Link + "'>" + nullToBlank(navigations[ctr].AdditionalHtml) + navigations[ctr].NavigationText + "</a>";
                $scope.siteMenuHtml += "<ul>";
                $scope.prepareMenu(navigations[ctr].ChildNavigations);
                $scope.siteMenuHtml += "</ul>";
                $scope.siteMenuHtml += "</li>";
            }
        }
    };

    // Method loadUserSites
    $scope.loadUserSites = function () {
        securityService.getSites().then(function (resp) {
            if (resp.businessException == null) {
                $scope.userSites = resp.result.data;
                var ctr = 0;
                if ($scope.userSites.length > 0) {
                    if ($scope.currentSiteId == 0) {
                        $scope.selectedSite = $scope.userSites[0];
                    }
                    else {
                        for (ctr = 0; ctr < $scope.userSites.length; ctr++) {
                            var site = $scope.userSites[ctr];
                            if (site.SiteId == $scope.currentSiteId) {
                                $scope.selectedSite = $scope.userSites[ctr];
                                break;
                            }
                        }
                    }
                    $scope.selectedSite = $scope.userSites[ctr];
                }
            }
            else {
                showMessage("divSummaryMessageHeader", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    // Method switchSite
    $scope.switchSite = function () {
        bootbox.confirm("This will reload current page. Are you sure to switch to site " + $scope.selectedSite.SiteName + "?", function (result) {
            if (result) {
                securityService.switchSite($scope.selectedSite.SiteId).then(function (resp) {
                    if (resp.businessException == null)
                        location.reload();
                    else
                        showMessage("divSummaryMessageHeader", resp.businessException.ExceptionMessage, "danger");
                })
            }
        });
    };

    //Method checkRegistrationAllowed
    $scope.checkRegistrationAllowed = function () {
        securityService.getSiteRegistrationAllowed().then(function (resp) {
            if (resp.businessException == null) {
                $scope.AllowRegistration = resp.result;
            }
            else {
                showMessage("divSummaryMessageHeader", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };
}]);