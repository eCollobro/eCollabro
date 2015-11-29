
ecollabroApp.service('serviceHandler',['$http', '$q', function ($http, $q) {
    this.executeGetService = function (url,request) {
        var deferred = $q.defer();
        $http({
            url: url,
            method: "get",
            params: request,
            headers: { 'Content-Type': 'application/json; charset=utf-8' }
        }).success(function (data, status, headers, config) {
            deferred.resolve({ result:data, businessException: null });
        }).error(function (data, status, headers, config) {
            if (status == 417) // business exception
            {
                deferred.resolve({ result: null, businessException: data });
            }
            else {
                deferred.reject(data);
                showException(data, status, headers, config);
            }
        });

        return deferred.promise;
    }

    this.executePostService = function (url,model) {
        var deferred = $q.defer();
        $http({
            url: url,
            method: "post",
            data: angular.toJson(model),
            headers: { 'Content-Type': 'application/json; charset=utf-8' }
        }).success(function (data, status, headers, config) {
            deferred.resolve({ result: data, businessException: null });
        }).error(function (data, status, headers, config) {
            if (status == 417) // business exception
            {
                deferred.resolve({ result: null, businessException: data });
            }
            else {
                deferred.reject(data);
                showException(data, status, headers, config);
            }
        });

        return deferred.promise;
    }

}]);