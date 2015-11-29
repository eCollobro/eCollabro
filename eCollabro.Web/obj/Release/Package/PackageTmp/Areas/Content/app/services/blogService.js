//blog service
ecollabroApp.service('blogService',['serviceHandler', function (serviceHandler) {

 
    this.getBlogCategories = function () {
        return serviceHandler.executeGetService("/BlogApi/GetBlogCategories/" + headerController().currentSiteId);
    }

    this.getBlogCategory = function (categoryId) {
        return serviceHandler.executeGetService("/BlogApi/GetBlogCategory/" + headerController().currentSiteId + "/" + categoryId);
    }

    this.deleteBlogCategory = function (categoryId) {
        return serviceHandler.executeGetService("/BlogApi/DeleteBlogCategory/" + headerController().currentSiteId + "/" + categoryId);
    }

    this.saveBlogCategory = function (blogCategory) {
        return serviceHandler.executePostService("/BlogApi/SaveBlogCategory/" + headerController().currentSiteId, blogCategory);
    }

    this.getRecentBlogs = function () {
        return serviceHandler.executeGetService("/BlogApi/GetRecentBlogs/" + headerController().currentSiteId);
    }

    this.getApprovedBlogs = function (categoryId) {
        return serviceHandler.executeGetService("/BlogApi/GetApprovedBlogs/" + headerController().currentSiteId + "/" + categoryId);
    }

    this.getBlogs = function (categoryId,request) {
        return serviceHandler.executeGetService("/BlogApi/GetBlogs/" + headerController().currentSiteId + "/" + categoryId,request);
    }

    this.getBlog = function (blogId) {
        return serviceHandler.executeGetService("/BlogApi/GetBlog/" + headerController().currentSiteId + "/" + blogId);
    }

    this.getActiveBlog = function (blogId) {
        return serviceHandler.executeGetService("/BlogApi/GetActiveBlog/" + headerController().currentSiteId + "/" + blogId);
    }

    this.deleteBlog = function (blogId) {
        return serviceHandler.executeGetService("/BlogApi/DeleteBlog/" + headerController().currentSiteId + "/" + blogId);
    }

    this.saveBlog = function (blog) {
        return serviceHandler.executePostService("/BlogApi/SaveBlog/" + headerController().currentSiteId, blog);
    }

    // controllers 
    this.getBlogCategoryView = function (blogCategoryId) {
        return serviceHandler.executeGetService("/Content/Blog/BlogCategory/"+ blogCategoryId);
    }

    this.getBlogView = function (blogCategoryId,blogId) {
        return serviceHandler.executeGetService("/Content/Blog/ManageBlog/" + blogId + "?catId=" + blogCategoryId);
    }
}]);