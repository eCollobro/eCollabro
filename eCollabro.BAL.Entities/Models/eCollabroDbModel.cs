namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class eCollabroDbModel : DbContext
    {
        public eCollabroDbModel()
            : base("name=eCollabroDbModel")
        {
        }

        public virtual DbSet<Announcement> Announcements { get; set; }
        public virtual DbSet<ApprovalQueue> ApprovalQueues { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<BlogCategory> BlogCategories { get; set; }
        public virtual DbSet<CodeFormat> CodeFormats { get; set; }
        public virtual DbSet<ContentComment> ContentComments { get; set; }
        public virtual DbSet<ContentLikeDislike> ContentLikeDislikes { get; set; }
        public virtual DbSet<ContentPage> ContentPages { get; set; }
        public virtual DbSet<ContentPageCategory> ContentPageCategories { get; set; }
        public virtual DbSet<ContentPermission> ContentPermissions { get; set; }
        public virtual DbSet<ContentRating> ContentRatings { get; set; }
        public virtual DbSet<ContentSetting> ContentSettings { get; set; }
        public virtual DbSet<ContentVote> ContentVotes { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<DocumentLibrary> DocumentLibraries { get; set; }
        public virtual DbSet<EmailConfiguration> EmailConfigurations { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<FeatureContentSetting> FeatureContentSettings { get; set; }
        public virtual DbSet<FeaturePermission> FeaturePermissions { get; set; }
        public virtual DbSet<FileObject> FileObjects { get; set; }
        public virtual DbSet<ImageGallery> ImageGalleries { get; set; }
        public virtual DbSet<ImageObject> ImageObjects { get; set; }
        public virtual DbSet<lkpContext> lkpContexts { get; set; }
        public virtual DbSet<lkpLanguage> lkpLanguages { get; set; }
        public virtual DbSet<lkpNavigationType> lkpNavigationTypes { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Navigation> Navigations { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleFeature> RoleFeatures { get; set; }
        public virtual DbSet<RoleFeaturePermission> RoleFeaturePermissions { get; set; }
        public virtual DbSet<Site> Sites { get; set; }
        public virtual DbSet<SiteCollectionAdmin> SiteCollectionAdmins { get; set; }
        public virtual DbSet<SiteConfiguration> SiteConfigurations { get; set; }
        public virtual DbSet<SiteContentSetting> SiteContentSettings { get; set; }
        public virtual DbSet<SiteFeature> SiteFeatures { get; set; }
        public virtual DbSet<SiteImage> SiteImages { get; set; }
        public virtual DbSet<UserMembership> UserMemberships { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserTask> UserTasks { get; set; }
        public virtual DbSet<WorkflowComment> WorkflowComments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .Property(e => e.BlogContent)
                .IsUnicode(false);

            modelBuilder.Entity<Blog>()
                .Property(e => e.ApprovalStatus)
                .IsUnicode(false);

            modelBuilder.Entity<BlogCategory>()
                .HasMany(e => e.Blogs)
                .WithRequired(e => e.BlogCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContentComment>()
                .Property(e => e.ApprovalStatus)
                .IsUnicode(false);

            modelBuilder.Entity<ContentPage>()
                .Property(e => e.ApprovalStatus)
                .IsUnicode(false);

            modelBuilder.Entity<ContentPageCategory>()
                .HasMany(e => e.ContentPages)
                .WithRequired(e => e.ContentPageCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContentPermission>()
                .HasMany(e => e.FeaturePermissions)
                .WithRequired(e => e.ContentPermission)
                .HasForeignKey(e => e.PermissionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContentSetting>()
                .HasMany(e => e.FeatureContentSettings)
                .WithRequired(e => e.ContentSetting)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContentSetting>()
                .HasMany(e => e.SiteContentSettings)
                .WithRequired(e => e.ContentSetting)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Document>()
                .Property(e => e.ApprovalStatus)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentLibrary>()
                .HasMany(e => e.Documents)
                .WithRequired(e => e.DocumentLibrary)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmailConfiguration>()
                .Property(e => e.DefaultSenderEmail)
                .IsUnicode(false);

            modelBuilder.Entity<EmailConfiguration>()
                .Property(e => e.HostName)
                .IsUnicode(false);

            modelBuilder.Entity<EmailConfiguration>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<EmailConfiguration>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Feature>()
                .Property(e => e.FeatureCode)
                .IsUnicode(false);

            modelBuilder.Entity<Feature>()
                .HasMany(e => e.FeatureContentSettings)
                .WithRequired(e => e.Feature)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Feature>()
                .HasMany(e => e.FeaturePermissions)
                .WithRequired(e => e.Feature)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Feature>()
                .HasMany(e => e.RoleFeatures)
                .WithRequired(e => e.Feature)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Feature>()
                .HasMany(e => e.SiteContentSettings)
                .WithRequired(e => e.Feature)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Feature>()
                .HasMany(e => e.SiteFeatures)
                .WithRequired(e => e.Feature)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FileObject>()
                .HasMany(e => e.Documents)
                .WithOptional(e => e.FileObject)
                .HasForeignKey(e => e.FileId);

            modelBuilder.Entity<ImageGallery>()
                .HasMany(e => e.SiteImages)
                .WithRequired(e => e.ImageGallery)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<lkpContext>()
                .Property(e => e.Context)
                .IsUnicode(false);

            modelBuilder.Entity<lkpContext>()
                .HasMany(e => e.ApprovalQueues)
                .WithRequired(e => e.lkpContext)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<lkpContext>()
                .HasMany(e => e.ContentLikeDislikes)
                .WithRequired(e => e.lkpContext)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<lkpContext>()
                .HasMany(e => e.ContentRatings)
                .WithRequired(e => e.lkpContext)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<lkpContext>()
                .HasMany(e => e.ContentVotes)
                .WithRequired(e => e.lkpContext)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<lkpContext>()
                .HasMany(e => e.FileObjects)
                .WithRequired(e => e.lkpContext)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<lkpContext>()
                .HasMany(e => e.ImageObjects)
                .WithRequired(e => e.lkpContext)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<lkpContext>()
                .HasMany(e => e.UserTasks)
                .WithRequired(e => e.lkpContext)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<lkpContext>()
                .HasMany(e => e.WorkflowComments)
                .WithRequired(e => e.lkpContext)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<lkpLanguage>()
                .Property(e => e.LanguageCode)
                .IsUnicode(false);

            modelBuilder.Entity<lkpNavigationType>()
                .Property(e => e.NavigationTypeCode)
                .IsUnicode(false);

            modelBuilder.Entity<lkpNavigationType>()
                .HasMany(e => e.Navigations)
                .WithRequired(e => e.lkpNavigationType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Module>()
                .Property(e => e.ModuleCode)
                .IsUnicode(false);

            modelBuilder.Entity<Module>()
                .HasMany(e => e.Features)
                .WithRequired(e => e.Module)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Navigation>()
                .Property(e => e.NavigationCode)
                .IsUnicode(false);

            modelBuilder.Entity<Navigation>()
                .HasMany(e => e.Navigation1)
                .WithOptional(e => e.Navigation2)
                .HasForeignKey(e => e.NavigationParentId);

            modelBuilder.Entity<Role>()
                .Property(e => e.RoleCode)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.RoleFeatures)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.SiteConfigurations)
                .WithOptional(e => e.Role)
                .HasForeignKey(e => e.RegistrationDefaultRoleId);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.UserRoles)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoleFeature>()
                .HasMany(e => e.RoleFeaturePermissions)
                .WithRequired(e => e.RoleFeature)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Site>()
                .Property(e => e.SiteCode)
                .IsUnicode(false);

            modelBuilder.Entity<Site>()
                .HasMany(e => e.Announcements)
                .WithRequired(e => e.Site)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Site>()
                .HasMany(e => e.BlogCategories)
                .WithRequired(e => e.Site)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Site>()
                .HasMany(e => e.ContentPageCategories)
                .WithRequired(e => e.Site)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Site>()
                .HasMany(e => e.DocumentLibraries)
                .WithRequired(e => e.Site)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Site>()
                .HasMany(e => e.ImageGalleries)
                .WithRequired(e => e.Site)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Site>()
                .HasMany(e => e.Navigations)
                .WithRequired(e => e.Site)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Site>()
                .HasMany(e => e.Roles)
                .WithRequired(e => e.Site)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Site>()
                .HasMany(e => e.SiteConfigurations)
                .WithRequired(e => e.Site)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Site>()
                .HasMany(e => e.SiteContentSettings)
                .WithRequired(e => e.Site)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Site>()
                .HasMany(e => e.SiteFeatures)
                .WithRequired(e => e.Site)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Site>()
                .HasMany(e => e.UserRoles)
                .WithRequired(e => e.Site)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Site>()
                .HasMany(e => e.UserTasks)
                .WithRequired(e => e.Site)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SiteImage>()
                .Property(e => e.ApprovalStatus)
                .IsUnicode(false);

            modelBuilder.Entity<UserMembership>()
                .HasMany(e => e.SiteCollectionAdmins)
                .WithRequired(e => e.UserMembership)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserMembership>()
                .HasMany(e => e.UserTasks)
                .WithOptional(e => e.UserMembership)
                .HasForeignKey(e => e.AssignedUserId);

            modelBuilder.Entity<UserMembership>()
                .HasMany(e => e.UserRoles)
                .WithRequired(e => e.UserMembership)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserMembership>()
                .HasMany(e => e.UserTasks1)
                .WithOptional(e => e.UserMembership1)
                .HasForeignKey(e => e.AssignedByUserId);

            modelBuilder.Entity<UserTask>()
                .Property(e => e.TaskStatus)
                .IsUnicode(false);

            modelBuilder.Entity<UserTask>()
                .Property(e => e.TaskType)
                .IsUnicode(false);

            modelBuilder.Entity<UserTask>()
                .Property(e => e.ApprovalStatus)
                .IsUnicode(false);
        }
    }
}
