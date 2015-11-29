// ContentPageCategoryController for AngularJS
ecollabroApp.controller('contentPageCategoryController', ['$scope', 'contentPageService', function ($scope, contentPageService) {
    $scope.contentPageCategory = {};

    //Method loadContentPageCategory
    $scope.loadContentPageCategory = function (contentPageCategoryId) {
        $scope.contentPageCategory.ContentPageCategoryId = contentPageCategoryId;
        if ($scope.contentPageCategory.ContentPageCategoryId == 0) {
            $scope.contentPageCategory.IsActive = true;
            return;
        }
        contentPageService.getCategory(contentPageCategoryId).then(function (resp) {
            if (resp.businessException == null) {
                $scope.contentPageCategory = resp.result;
            }
            else {
                showMessage("divSummaryMessageContentPageCategory", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method saveContentPageCategory
    $scope.saveContentPageCategory = function () {
        if (!$("#frmContentPageCategory").valid()) {
            return;
        }
        contentPageService.saveCategory($scope.contentPageCategory).then(function (resp) {
            if (resp.businessException == null) {
                $scope.contentPageCategory.ContentPageCategoryId = resp.result.Id;
                var divContentPageCategories = document.getElementById("divContentPageCategories");
                if (divContentPageCategories) {
                    showContentPageCategories(resp.result.Message); // calling parent's method
                }
                else {
                    showMessage("divSummaryMessageContentPageCategory", resp.result.Message, "success");
                }

            }
            else {
                showMessage("divSummaryMessageContentPageCategory", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };

    //Method openPageCategories
    $scope.openPageCategories = function () {
        location.href = "/content/contentpage/categories";
    };
}]);