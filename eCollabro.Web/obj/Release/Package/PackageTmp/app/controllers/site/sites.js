//Global Methods
function getSitesController() {
    var scope = angular.element(document.getElementById("divSites")).scope();
    return scope;
}

function showSites(summaryMessage) {
    $("#placeHolderSites").html("");
    $("#divSites").show();
    if (summaryMessage != "") {
        showMessage("divSummaryMessageSites", summaryMessage, "success");
    }
    //refresh grid
    getSitesController().loadSites();
    headerController().loadUserSites();

}


//SitesController for Angular
ecollabroApp.controller('sitesController', ['$scope', '$compile', 'securityService',function ($scope, $compile, securityService) {
    $scope.sites = [];
    $scope.fields = [{ mDataProp: "SiteCode" }, { mDataProp: "SiteName" }, { mDataProp: "IsActive" }, { mDataProp: "Action", bSortable: false }];

    // Method loadSites
    $scope.loadSites = function () {
        if ($scope.oTable)
            $('#tblSites').dataTable().fnDestroy();

        $scope.oTable = $("#tblSites").dataTable(
            {
                "processing": true,
                "serverSide": false,
                aoColumns: $scope.fields,
                "ajax": $.fn.dataTable.setData = function (request, drawCallback, settings) {
                    securityService.getSites(request).then(function (resp) {
                        if (resp.businessException == null) {
                            $scope.sites = resp.result.data;
                            angular.forEach(resp.result.data, function (value, key) {
                                value.IsActive = checkActiveDisplay(value.IsActive);
                                value.Action = $scope.getTableLink(value.SiteId);
                            })
                            drawCallback(resp.result);
                        }
                        else {
                            showMessage("divSummaryMessageSites", resp.businessException.ExceptionMessage, "danger");
                        }
                    });

                }

            });
        bindEnterEventOnTable("tblSites", $scope.oTable);
    };

    //Method openSite
    $scope.openSite = function (siteId) {
        securityService.getSiteView(siteId).then(function (resp) {
            if (resp.businessException == null) {
                $("#placeHolderSites").html(resp.result);
                $compile($("#placeHolderSites").contents())($scope);
                $("#divSites").hide();
            }
            else {
                showMessage("divSummaryMessageSites", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };


    //Method deleteSite
    $scope.deleteSite = function (siteId) {
        bootbox.confirm("Are you sure to delete selected Site?", function (result) {
            if (result) {
                securityService.deleteSite(siteId).then(function (resp) {
                    if (resp.businessException == null) {
                        showSites(resp.result);
                    }
                    else {
                        showMessage("divSummaryMessageSites", resp.businessException.ExceptionMessage, "danger");
                    }
                });
            }
        });
    };

    // Method copySite
    $scope.copySite = function (siteId) {
        bootbox.confirm("Are you sure to create a copy for selected site?", function (result) {
            if (result) {
                securityService.copySite(siteId).then(function (resp) {
                    if (resp.businessException == null) {
                        showSites(resp.result);
                    }
                    else {
                        showMessage("divSummaryMessageSites", resp.businessException.ExceptionMessage, "danger");
                    }
                });
            }
        });
  };

    //Method manageFeatures
    $scope.manageFeatures = function (siteId) {
        securityService.getSiteFeaturesView(siteId).then(function (resp) {
            if (resp.businessException == null) {
                $("#placeHolderSites").html(resp.result);
                $("#divSites").hide();
                $compile($("#placeHolderSites").contents())($scope);
            }
            else {
                showMessage("divSummaryMessageSites", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method getTableLinks
    $scope.getTableLink = function (Id) {
        var tableLinks = "<a class='ajaxLink' href='javascript:getSitesController().openSite(" + Id + ")'>Edit</a> | " +
                    "<a href='javascript:getSitesController().copySite(" + Id + ");'>Copy</a> | " +
                     "<a href='javascript:getSitesController().deleteSite(" + Id + ");'>Delete</a> | " +
                    "<a href='javascript:getSitesController().manageFeatures(" + Id + ");'>Features</a> ";
        return tableLinks;

    };

}]);
