namespace eCollabro.BAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserMembership")]
    public partial class UserMembership
    {
        public UserMembership()
        {
            SiteCollectionAdmins = new HashSet<SiteCollectionAdmin>();
            UserTasks = new HashSet<UserTask>();
            UserRoles = new HashSet<UserRole>();
            UserTasks1 = new HashSet<UserTask>();
        }

        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(128)]
        public string ConfirmationToken { get; set; }

        public bool IsConfirmed { get; set; }

        public bool IsApproved { get; set; }

        public DateTime? LastPasswordFailureDate { get; set; }

        public int PasswordFailuresSinceLastSuccess { get; set; }

        [Required]
        [StringLength(128)]
        public string Password { get; set; }

        public DateTime? PasswordChangedDate { get; set; }

        [Required]
        [StringLength(128)]
        public string PasswordSalt { get; set; }

        [StringLength(128)]
        public string PasswordVerificationToken { get; set; }

        public DateTime? PasswordVerificationTokenExpirationDate { get; set; }

        public bool IsLocked { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifiedById { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<SiteCollectionAdmin> SiteCollectionAdmins { get; set; }

        public virtual ICollection<UserTask> UserTasks { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public virtual ICollection<UserTask> UserTasks1 { get; set; }
    }
}
