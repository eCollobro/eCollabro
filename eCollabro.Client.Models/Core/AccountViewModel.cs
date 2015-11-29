// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System.ComponentModel.DataAnnotations;

#endregion

namespace eCollabro.Client.Models.Core
{
    /// <summary>
    /// ChangePasswordModel
    /// </summary>
    public class ChangePasswordModel
    {
            public string UserName { get; set; }
        
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Current password")]
            public string OldPassword { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// LoginModel
    /// </summary>
    public class LoginModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

    }

    /// <summary>
    /// PasswordResetModel
    /// </summary>
    public class UserTokenVerificationModel
    {

        public string Username { get; set; }

        public string Token { get; set; }
    }


    /// <summary>
    /// RegisterModel
    /// </summary>
    public class RegisterModel
    {
        [Required, Display(Name = "Username"), StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password"), StringLength(20)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Email"), StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
    }

}
