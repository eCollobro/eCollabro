//esbDashboardWidgetController for Angular
//Global Method
function getEsbDashboardWidgetController() {
    var scope = angular.element(document.getElementById("divESBHome")).scope();
    return scope;
}

ecollabroApp.controller('esbDashboardWidgetController', ['$scope', '$compile','esbService', function ($scope, $compile, esbService) {
    $scope.esbApps = [];
    $scope.canControll = true;
    $scope.caption = "glyphicon glyphicon-chevron-down=";
    // Method initialize
    $scope.initialize = function () {
        $scope.fields = [{ mDataProp: "AppId" }, { mDataProp: "AppName" }, { mDataProp: "IsActive" }, { mDataProp: "ComponentStatusName" },  { mDataProp: "IsExternal" },  { mDataProp: "Action", bSortable: false }];
        $scope.loadESBApps();
    };

    // Method LoadESBApps
    $scope.loadESBApps = function () {
        if ($scope.oTable)
            $('#tblESBApps').dataTable().fnDestroy();

        $scope.oTable = $("#tblESBApps").dataTable(
            {
                "processing": true,
                "serverSide": false,
                aoColumns: $scope.fields,
                "ajax": $.fn.dataTable.setData = function (request, drawCallback, settings) {
                    esbService.getESBApps(request).then(function (resp) {
                        if (resp.businessException == null) {
                            $scope.esbApps = resp.result.data;
                            angular.forEach(resp.result.data, function (value, key) {
                                value.IsActive = checkActiveDisplay(value.IsActive);
                                value.IsExternal = checkActiveDisplay(value.IsExternal);
                                if (value.ComponentStatusCode != "STARELE")
                                    value.ComponentStatusName = "<span class=\"glyphicon glyphicon-play-circle\" aria-hidden=\"true\"></span>&nbsp;" + value.ComponentStatusName;
                                else
                                    value.ComponentStatusName = "<span class=\"glyphicon glyphicon-off\" aria-hidden=\"true\"></span>&nbsp;" + value.ComponentStatusName;

                                value.Action = $scope.getTableLink(value.AppId);
                            })
                            drawCallback(resp.result);
                        }
                        else {
                            showMessage("divSummaryMessageESBWidget", resp.businessException.ExceptionMessage, "danger");
                        }
                    });

                }

            });
        bindEnterEventOnTable("tblESBApps", $scope.oTable);
    };

    //Method openAppScheduler
    $scope.openAppScheduler = function (appId) {
        esbService.getAppSchedulerView(appId).then(function (resp) {
            if (resp.businessException == null) {
                $("#placeholderAppScheduler").html(resp.result);
                $('.datepicker').datepicker(); //Initialise any date pickers
                $compile($("#placeholderAppScheduler").contents())($scope);
                
              
            }
            else {
                showMessage("divSummaryMessageRoles", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method getTableLink
    $scope.getTableLink = function (Id) {
        var tableLinks = "";
        if ($scope.canControll) {
            tableLinks += "<a href='javascript:getBlogCategoriesController().openBlogs(" + Id + ");'><span class=\"glyphicon glyphicon-random\" aria-hidden=\"true\"></span>&nbsp;Execute</a> ";
            tableLinks += " | ";
            tableLinks += "<a class='ajaxLink' href='javascript:getBlogCategoriesController().openBlogCategory(" + Id + ")'><span class=\"glyphicon glyphicon-cog\" aria-hidden=\"true\"></span>&nbsp;Configuration</a> ";
            tableLinks += " | ";
            tableLinks += "<a href='javascript:getEsbDashboardWidgetController().openAppScheduler(" + Id + ");'><span class=\"glyphicon glyphicon-time\" aria-hidden=\"true\"></span>&nbsp;Schedule</a>"
            tableLinks += " | ";
            tableLinks += "<a href='javascript:getBlogCategoriesController().openBlogs(" + Id + ");'><span class=\"glyphicon glyphicon-eye-open\" aria-hidden=\"true\"></span>&nbsp;Log</a> ";
            tableLinks += " | ";
            tableLinks += "<a href='javascript:getBlogCategoriesController().openBlogs(" + Id + ");'><span class=\"glyphicon glyphicon-warning-sign\" aria-hidden=\"true\"></span>&nbsp;Exceptions</a> ";
            return tableLinks;
        }

    };
    //Method changeCaption()
    $scope.changeCaption = function () {
        if ($scope.caption == "glyphicon glyphicon-chevron-right")
            $scope.caption = "glyphicon glyphicon-chevron-down";
        else
            $scope.caption = "glyphicon glyphicon-chevron-right";
    };
}]);