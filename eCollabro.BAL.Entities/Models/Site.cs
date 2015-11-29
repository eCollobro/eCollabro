namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Site")]
    public partial class Site
    {
        public Site()
        {
            Announcements = new HashSet<Announcement>();
            BlogCategories = new HashSet<BlogCategory>();
            ContentPageCategories = new HashSet<ContentPageCategory>();
            DocumentLibraries = new HashSet<DocumentLibrary>();
            ImageGalleries = new HashSet<ImageGallery>();
            Navigations = new HashSet<Navigation>();
            Roles = new HashSet<Role>();
            SiteConfigurations = new HashSet<SiteConfiguration>();
            SiteContentSettings = new HashSet<SiteContentSetting>();
            SiteFeatures = new HashSet<SiteFeature>();
            UserRoles = new HashSet<UserRole>();
            UserTasks = new HashSet<UserTask>();
        }

        public int SiteId { get; set; }

        [Required]
        [StringLength(10)]
        public string SiteCode { get; set; }

        [StringLength(50)]
        public string SiteName { get; set; }

        [StringLength(255)]
        public string SiteDesc { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifiedById { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<Announcement> Announcements { get; set; }

        public virtual ICollection<BlogCategory> BlogCategories { get; set; }

        public virtual ICollection<ContentPageCategory> ContentPageCategories { get; set; }

        public virtual ICollection<DocumentLibrary> DocumentLibraries { get; set; }

        public virtual ICollection<ImageGallery> ImageGalleries { get; set; }

        public virtual ICollection<Navigation> Navigations { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public virtual ICollection<SiteConfiguration> SiteConfigurations { get; set; }

        public virtual ICollection<SiteContentSetting> SiteContentSettings { get; set; }

        public virtual ICollection<SiteFeature> SiteFeatures { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public virtual ICollection<UserTask> UserTasks { get; set; }
    }
}
