#region References

#endregion

namespace eCollabro.Common
{
    /// <summary>
    /// WorkflowConstants
    /// </summary>
    public static class WorkflowConstants
    {
        public static string ApprovalWaitingStatus = "Approval Waiting";
        public static string ApprovedStatus = "Approved";
        public static string RejectedStatus = "Rejected";
    }

    /// <summary>
    /// CoreValidationMessagesConstants
    /// </summary>
    public class CoreValidationMessagesConstants
    {
        public const string eCollabroAlreadySetup = "eCollabroAlreadySetup";
        public const string FeatureNotAssignedToSite = "FeatureNotAssignedToSite";
        public const string InvalidArguments = "InvalidArguments";
        public const string InvalidPasswordResetToken = "InvalidPasswordResetToken";
        public const string InvalidUserCredentials = "InvalidUserCredentials";
        public const string RecordNotExistOrInactive = "RecordNotExistOrInactive";
        public const string RecordNotFound = "RecordNotFound";
        public const string SiteRegistrationNotAllowed = "SiteRegistrationNotAllowed";
        public const string SystemDefinedValue = "SystemDefinedValue";
        public const string UnAuthorized = "UnAuthorized";
        public const string UserAccountLocked = "UserAccountLocked";
        public const string UserAlreadyExist = "UserAlreadyExist";
        public const string AtleastOneSiteCollectionAdminRequired = "AtleastOneSiteCollectionAdminRequired";
        public const string UserNotAssignedToSite = "UserNotAssignedToSite";
        public const string InvalidVerificationToken = "InvalidVerificationToken";
        public const string AccountAlreadyVerified = "AccountAlreadyVerified";
        public const string AccountNotConfirmed = "AccountNotConfirmed";
        public const string AccountNotApproved = "AccountNotApproved";

    }

    /// <summary>
    /// StoreValidationMessagesConstants
    /// </summary>
    public class StoreValidationMessagesConstants
    {
        public const string OrderCartEmpty = "OrderCartEmpty";
    }

    public static class CoreMessageConstants
    {
        public const string GenericResponseError = "GenericResponseError";
    }

    public static class SystemConstant
    {
        public static int SiteAdminRoleId = 1;
    }

    public static class EntityConstants
    {
        public const string Feature = "Feature";
        public const string Content = "Content";
        public const string Role = "Role";
        public const string Site = "Site";
        public const string Module = "Module";
        public const string Navigation = "Navigation";
        public const string ContentPage = "ContentPage";
    }

    public static class SystemConstants
    {
        public const int SiteAdminRoleId = 1;

    }

    public static class ExceptionConstants
    {
        public const string NotLoggedIn = "NotLoggedIn";
    }

    public static class FieldNameConstants
    {
        public const string Common_IsActive="Common_IsActive";
        public const string Common_IsAnomynousAccess="Common_IsAnomynousAccess";
        public const string Common_IsCommentsAllowed = "Common_IsCommentsAllowed";
        public const string Common_IsLikeAllowed="Common_IsLikeAllowed";
        public const string Common_IsRatingAllowed="Common_IsRatingAllowed";
        public const string Common_IsVotingAllowed = "Common_IsVotingAllowed";
    }
}
