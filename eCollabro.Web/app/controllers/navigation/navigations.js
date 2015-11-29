//Global Methodsc
function getNavigationsController() {
    var scope = angular.element(document.getElementById("divNavigations")).scope();
    return scope;
}

function showNavigations(summaryMessage) {
    $("#placeHolderNavigations").html("");
    $("#divNavigations").show();
    if (summaryMessage != "") {
        showMessage("divSummaryMessageNavigations", summaryMessage, "success");
    }
    //refresh grid
    getNavigationsController().loadNavigations();

}

//NavigationsController for Angular
ecollabroApp.controller('navigationsController', ['$scope', '$compile', 'securityService' , function ($scope, $compile, securityService) {
    $scope.navigations = [];

    // Method loadNavigations
    $scope.loadNavigations = function () {
        securityService.getNavigations().then(function (resp) {
            if (resp.businessException == null) {
                $scope.navigations = resp.result;
            }
            else
            {
                showMessage("divSummaryMessageNavigations",resp.businessException,"danger");
            }
        });
    };

    //Method openNavigation
    $scope.openNavigation = function (navigationId) {
        securityService.getNavigationView(navigationId).then(function (resp) {
            if (resp.businessException == null) {
                $("#placeHolderNavigations").html(resp.result);
                $compile($("#placeHolderNavigations").contents())($scope);
            }
            else {
                showMessage("divSummaryMessageNavigations", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };


    //Method deleteNavigation
    $scope.deleteNavigation = function (navigationId) {
        bootbox.confirm("Are you sure to delete selected Navigation?", function (result) {
            if (result) {
                securityService.deleteNavigation(navigationId).then(function (resp) {
                    if (resp.businessException == null) {
                        showNavigations(resp.result);
                    }
                    else {
                        showMessage("divSummaryMessageNavigations", resp.businessException.ExceptionMessage, "danger");
                    }
                });
            }
        });
    };

 
    //Method getClass
    $scope.getClass = function (navigation) {
        var cls = "treegrid-" + navigation.NavigationId;
        if(navigation.NavigationParentId!=null)
            cls+=" treegrid-parent-" + navigation.NavigationParentId;
        return cls;
    };

    $scope.$on('onRepeatFinished', function(scope, element, attrs){
        $('.tree').treegrid({
            'initialState': 'collapsed',
            'saveState': true,
        });
    });
}]);
