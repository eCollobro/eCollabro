//Global Methodsc
function getBlogCategoriesController() {
    var scope = angular.element(document.getElementById("divBlogCategories")).scope();
    return scope;
}

function showBlogCategories(summaryMessage) {
    $("#placeHolderBlogCategories").html("");
    $("#divBlogCategories").show();
    if (summaryMessage != "") {
        showMessage("divSummaryMessageBlogCategories", summaryMessage, "success");
    }
    //refresh grid
    getBlogCategoriesController().loadBlogCategories();

}

//BlogCategoriesController for Angular
ecollabroApp.controller('blogCategoriesController',['$scope', '$compile','blogService', function ($scope, $compile,blogService) {
    $scope.blogCategories = [];
    $scope.canAdd = false;
    $scope.canEdit = false;
    $scope.canDelete = false;

    //Method initialize
    $scope.initialize = function (canAdd,canEdit, canDelete) {
        $scope.canAdd = canAdd;
        $scope.canEdit = canEdit;
        $scope.canDelete = canDelete;
        $scope.fields = [{ mDataProp: "BlogCategoryName" }];
        if ($scope.canEdit || $scope.canDelete || $scope.canAdd)
            $scope.fields = [{ mDataProp: "BlogCategoryId" }, { mDataProp: "BlogCategoryName" }, { mDataProp: "IsActive" }, { mDataProp: "Action", bSortable: false }];

        $scope.loadBlogCategories();
    };

    // Method loadBlogCategories
    $scope.loadBlogCategories = function () {
        if ($scope.oTable)
            $('#tblBlogCategories').dataTable().fnDestroy();

        $scope.oTable = $("#tblBlogCategories").dataTable(
            {
                "processing": true,
                "serverSide": false,
                aoColumns: $scope.fields,
                "ajax": $.fn.dataTable.setData = function (request, drawCallback, settings) {
                    blogService.getBlogCategories(request).then(function (resp) {
                        if (resp.businessException == null) {
                            $scope.blogCategories = resp.result.data;
                            angular.forEach(resp.result.data, function (value, key) {
                                value.IsActive = checkActiveDisplay(value.IsActive);
                                value.Action = $scope.getTableLink(value.BlogCategoryId);
                            })
                            drawCallback(resp.result);
                        }
                        else {
                            showMessage("divSummaryMessageBlogCategories", resp.businessException.ExceptionMessage, "danger");
                        }
                    });

                }

            });
        bindEnterEventOnTable("tblBlogCategories", $scope.oTable);
     };

    //Method openBlogCategory
    $scope.openBlogCategory = function (blogCategoryId) {
        blogService.getBlogCategoryView(blogCategoryId).then(function (resp) {
            if (resp.businessException == null) {
                $("#placeHolderBlogCategories").html(resp.result);
                $("#divBlogCategories").hide();
                $compile($("#placeHolderBlogCategories").contents())($scope);
            }
            else {
                showMessage("divSummaryMessageBlogCategories", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };


    //Method deleteBlogCategory
    $scope.deleteBlogCategory = function (categoryId) {
        bootbox.confirm("Are you sure to delete selected Blog Category?", function (result) {
            if (result) {
                blogService.deleteBlogCategory(categoryId).then(function (resp) {
                    if (resp.businessException == null) {
                        showBlogCategories(resp.result);
                    }
                    else {
                        showMessage("divSummaryMessageBlogCategories", resp.businessException.ExceptionMessage, "danger");
                    }
                });
            }
        });
     };

    //Method openBlogs
    $scope.openBlogs = function (categoryId) {
        location.href = "/Content/Blog/Blogs/" + categoryId
    };

    //Method getTableLink
    $scope.getTableLink = function (Id) {
        var tableLinks = "";
        if ($scope.canEdit)
            tableLinks = "<a class='ajaxLink' href='javascript:getBlogCategoriesController().openBlogCategory(" + Id + ")'>Edit</a> ";
        if ($scope.canDelete) {
            if (tableLinks != "")
                tableLinks += " | ";
            tableLinks += "<a href='javascript:getBlogCategoriesController().deleteBlogCategory(" + Id + ");'>Delete</a>"
        }
        if (tableLinks != "")
            tableLinks += " | ";
        tableLinks += "<a href='javascript:getBlogCategoriesController().openBlogs(" + Id + ");'>Blogs</a> ";
        return tableLinks;

    };

}]);