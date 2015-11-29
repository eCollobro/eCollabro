var ecollabroApp = angular.module('ecollabroApp', []);
ecollabroApp.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.defaults.headers.common["X-Requested-With"] = 'XMLHttpRequest';
}]);
ecollabroApp.directive('loading', ['$http', function ($http) {
        return {
            restrict: 'A',
            link: function (scope, elm, attrs) {
                scope.isLoading = function () {
                    return $http.pendingRequests.length > 0;
                };

                scope.$watch(scope.isLoading, function (v) {
                    if (v) {
                        elm.show();
                    } else {
                        elm.hide();
                    }
                });
            }
        };

    }]);
ecollabroApp.factory('$eCollabroCache', ['$cacheFactory', function ($cacheFactory) {
    return $cacheFactory('$eCollabroCache');
}]);

ecollabroApp.directive('onRepeatFinished', function() {
    return function(scope, element, attrs) {
        if (scope.$last) setTimeout(function(){
            scope.$emit('onRepeatFinished', element, attrs);
        }, 1);
    };
})

ecollabroApp.config(['$compileProvider', function ($compileProvider) {
    $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|file|javascript):/);
}]);