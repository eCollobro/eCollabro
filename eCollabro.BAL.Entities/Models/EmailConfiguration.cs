namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmailConfiguration")]
    public partial class EmailConfiguration
    {
        public int EmailConfigurationId { get; set; }

        [Required]
        [StringLength(100)]
        public string DefaultSenderEmail { get; set; }

        [Required]
        [StringLength(50)]
        public string HostName { get; set; }

        public int PortNumber { get; set; }

        public bool EnableSSL { get; set; }

        public bool RequireCredentials { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(50)]
        public string Password { get; set; }
    }
}
