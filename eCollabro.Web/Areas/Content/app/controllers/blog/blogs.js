//Global Methods
function getBlogsController() {
    var scope = angular.element(document.getElementById("divBlogs")).scope();
    return scope;
}

function showBlogs(summaryMessage) {
    $("#placeHolderBlogs").html("");
    $("#divBlogs").show();
    if (summaryMessage != "") {
        showMessage("divSummaryMessageBlogs", summaryMessage, "success");
    }
    //refresh grid
    getBlogsController().loadBlogs(getBlogsController().blogCategory.BlogCategoryId);

}

function getBlogLink(blogId, blogTitle) {
    return "<a  href='/Content/Blog/Blog/" + blogId + "'>" + blogTitle + "</a>";
}
//BlogsController for Angular
ecollabroApp.controller('blogsController', ['$scope', '$compile', 'blogService', function ($scope, $compile, blogService) {
    $scope.blogCategory = {};
    $scope.blogCategoryId = 0;

    //Method initialize
    $scope.initialize = function (blogCategoryId, canAdd, canEdit, canDelete) {
        $scope.canAdd = canAdd;
        $scope.canEdit = canEdit;
        $scope.canDelete = canDelete;
        $scope.blogCategoryId = blogCategoryId;
        $scope.Fields = [{ mDataProp: "BlogTitle" }];
        if ($scope.canEdit || $scope.canDelete || $scope.canAdd)
            $scope.Fields = [{ mDataProp: "BlogId" }, { mDataProp: "BlogTitle" }, { mDataProp: "IsActive" }, { mDataProp: "ApprovalStatus" }, { mDataProp: "Action", bSortable: false }];
        $scope.loadBlogs();
    };



    // Method loadBlogs
    $scope.loadBlogs = function () {
        if ($scope.oTable)
            $('#tblBlogs').dataTable().fnDestroy();

        $scope.oTable = $("#tblBlogs").dataTable(
            {
                "processing": true,
                "serverSide": false,
                aoColumns: $scope.Fields,
                "ajax": $.fn.dataTable.setData = function (request, drawCallback, settings) {
                    blogService.getBlogs($scope.blogCategoryId, request).then(function (resp) {
                        if (resp.businessException == null) {
                            $scope.blogCategory = resp.result.data;
                            // switch as per data table structure for server paging
                            resp.result.data = resp.result.data.Blogs;
                            angular.forEach(resp.result.data, function (value, key) {
                                var id = value.BlogId;
                                value.BlogId = getBlogLink(id, id);
                                value.BlogTitle = getBlogLink(id, value.BlogTitle);
                                value.IsActive = checkActiveDisplay(value.IsActive);
                                value.Action = $scope.getTableLink(id);
                            })
                            drawCallback(resp.result);
                        }
                        else {
                            showMessage("divSummaryMessageBlogs", resp.businessException.ExceptionMessage, "danger");
                        }
                    });

                }

            });
        bindEnterEventOnTable("tblBlogs", $scope.oTable);
    };



    //Method openBlog
    $scope.openBlog = function (blogId, blogCategoryId) {
        blogService.getBlogView(blogCategoryId, blogId).then(function (resp) {
            if (resp.businessException == null) {
                $("#placeHolderBlogs").html(resp.result);
                $("#divBlogs").hide();
                $compile($("#placeHolderBlogs").contents())($scope);
            }
            else {
                showMessage("divSummaryMessageBlogs", resp.businessException.ExceptionMessage, "danger");
            }
        });
    };


    //Method deleteBlog
    $scope.deleteBlog = function (blogId) {
        bootbox.confirm("Are you sure to delete selected Blog?", function (result) {
            if (result) {
                blogService.deleteBlog(blogId).then(function (resp) {
                    if (resp.businessException == null) {
                        showBlogs(resp.result);
                    }
                    else {
                        showMessage("divSummaryMessageBlogs", resp.businessException.ExceptionMessage, "danger");
                    }
                });
            }
        });
    };

    //Method blogCategories
    $scope.blogCategories = function () {
        location.href = "/Content/Blog/blogCategories";
    }

    //Method getTableLink
    $scope.getTableLink = function (blogId) {
        var tableLinks = "";
        if ($scope.canEdit)
            tableLinks = "<a class='ajaxLink' href='javascript:getBlogsController().openBlog(" + blogId + "," + $scope.blogCategoryId + ")'>Edit</a> ";
        if ($scope.canDelete) {
            if (tableLinks != "")
                tableLinks += " | ";
            tableLinks += "<a href='javascript:getBlogsController().deleteBlog(" + blogId + ");'>Delete</a> ";
        }

        return tableLinks;

    };

}]);