
namespace eCollabro.Common
{
    public enum PermissionEnum
    {
        ViewAnomynousContent = 0,
        ViewContent = 1,
        ViewUnapprovedContent = 2,
        ViewInactiveContent = 3,
        AddContent = 4,
        EditContent = 5,
        DeleteContent = 6,
        ApproveContent = 7
    }

    public enum FeatureEnum
    {
        Role = 1,
        User = 2,
        Navigation = 3,
        SiteConfiguration = 4,
        ContentPage = 5,
        Blog = 6,
        DocumentLibrary = 7,
        ImageGallery = 8,
        Announcement = 9,
        Ecommerce=10
    }

    public enum NavigationTypeEnum
    {
        None = 1,
        Content = 2,
        Feature = 3,
        Link = 4
    }
    public enum FeatureSettingEnum
    {
        ApprovalRequired = 1
    }

    public enum ContextEnum
    {
        None = 0,
        User = 1,
        ContentPage = 2,
        Blog = 3,
        Document = 4,
        Image = 5,
        Announcement = 6,
        Product=7
    }
}
