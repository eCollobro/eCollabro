namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SiteConfiguration")]
    public partial class SiteConfiguration
    {
        public int SiteConfigurationId { get; set; }

        public int SiteId { get; set; }

        public bool AllowRegistration { get; set; }

        public int? RegistrationDefaultRoleId { get; set; }

        public bool AccountRequireEmailVerification { get; set; }

        public int? HomePageContentPageId { get; set; }

        public int ModifiedById { get; set; }

        public DateTime ModifiedOn { get; set; }

        public virtual Role Role { get; set; }

        public virtual Site Site { get; set; }
    }
}
