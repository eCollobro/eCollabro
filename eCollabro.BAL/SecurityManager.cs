// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;
using System.Collections.Generic;
using System.Linq;
using eCollabro.Utilities;
using eCollabro.BAL.Entities.Models;
using eCollabro.Exceptions;
using System.Data;
using eCollabro.Common;
using System.Net.Mail;
using System.Configuration;

#endregion

namespace eCollabro.BAL
{
    /// <summary>
    /// SecurityManager 
    /// </summary>
    public class SecurityManager : BaseManager
    {

        #region Data Members

        private CommonManager _commonManager = null;

        #endregion

        #region Constructor

        /// <summary>
        /// AdminManager
        /// </summary>
        public SecurityManager()
        {
            _commonManager = new CommonManager();
        }

        #endregion

        #region Methods

        #region Roles


        /// <summary>
        /// GetRole
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public Role GetRole(int roleId)
        {
            List<PermissionEnum> userPermissions = GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.Role);
            Role role = eCollabroDbContext.Repository<Role>().Find(userPermissions,roleId);
            return GetContentResponse<Role>(role,userPermissions);
        }

        ///<Summary>
        ///Get All Roles
        ///</Summary>
        ///<returns></returns>
        public List<Role> GetRoles()
        {
            List<PermissionEnum> userPermissions = GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.Role);
            List<Role> roles = eCollabroDbContext.Repository<Role>().Query(userPermissions).Filter(flt => flt.SiteId.Equals(UserContextDetails.SiteId)).Get().ToList();
            return roles;
        }

        /// <summary>
        /// Save Role
        /// </summary>
        /// <param name="role"></param>
        public void SaveRole(Role role)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.Role);
            if ((role.RoleId.Equals(0) && !userPermissions.Contains(PermissionEnum.AddContent)) || (!role.RoleId.Equals(0) && !userPermissions.Contains(PermissionEnum.EditContent)))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            if (role.RoleId.Equals(0))
            {
                role.RoleCode = _commonManager.GetNextCode(EntityConstants.Role);
                role.SiteId = UserContextDetails.SiteId;
                role.CreatedById = UserContextDetails.UserId;
                role.CreatedOn = DateTime.UtcNow;
                eCollabroDbContext.Repository<Role>().Insert(role);
                eCollabroDbContext.Save();
            }
            else
            {
                Role oldRole = eCollabroDbContext.Repository<Role>().Query().Filter(flt => flt.RoleId.Equals(role.RoleId)).Get().FirstOrDefault();
                if (oldRole != null)
                {
                    if (oldRole.IsSystem)
                        throw new BusinessException(string.Format(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.SystemDefinedValue), "Role"), CoreValidationMessagesConstants.SystemDefinedValue.ToString());
                    oldRole.RoleName = role.RoleName;
                    oldRole.RoleDescription = role.RoleDescription;
                    oldRole.ModifiedById = UserContextDetails.UserId;
                    oldRole.ModifiedOn = DateTime.UtcNow;
                    oldRole.IsActive = role.IsActive;
                    eCollabroDbContext.Save();
                }
                else
                {
                    throw new DBConcurrencyException();
                }
            }

        }


        /// <summary>
        /// DeleteRole
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public void DeleteRole(int roleId)
        {

            #region Check Permission

            List<PermissionEnum> userPermissions = GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.Role);
            if (!userPermissions.Contains(PermissionEnum.DeleteContent))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            if (!string.IsNullOrEmpty(UserContextDetails.UserName))
            {
                Role oldlRole = eCollabroDbContext.Repository<Role>().Query().Filter(flt => flt.RoleId.Equals(roleId)).Get().FirstOrDefault();
                if (oldlRole != null)
                {
                    if (oldlRole.IsSystem)
                        throw new BusinessException(string.Format(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.SystemDefinedValue), "Role"), CoreValidationMessagesConstants.SystemDefinedValue);
                    oldlRole.IsDeleted = true;
                    oldlRole.ModifiedById = UserContextDetails.UserId;
                    oldlRole.ModifiedOn = DateTime.UtcNow;
                    eCollabroDbContext.Save();
                }
                else
                {
                    throw new DBConcurrencyException();
                }
            }
            else
            {
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);
            }
        }


        #endregion

        #region Role Features

        /// <summary>
        /// GetRoleFeatures
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<FeatureResult> GetRoleFeatures(int roleId)
        {
            List<FeatureResult> siteFeatures = eCollabroDbContext.ExtendedRepository().SecurityRepository.GetSiteAssignedFeatures(UserContextDetails.SiteId);

            List<RoleFeature> roleFeatures = eCollabroDbContext.Repository<RoleFeature>().Query().Include(inc => inc.RoleFeaturePermissions).Filter(qry => qry.RoleId.Equals(roleId)).Get().ToList();
            List<FeaturePermission> featuresPermissions = eCollabroDbContext.Repository<FeaturePermission>().Query().Include(inc => inc.ContentPermission).Get().ToList();
            foreach (FeatureResult siteFeature in siteFeatures)
            {

                RoleFeature roleFeature = roleFeatures.Where(qry => qry.FeatureId.Equals(siteFeature.FeatureId)).FirstOrDefault();
                if (roleFeature != null)
                {
                    siteFeature.IsAssigned = true;
                }
                else
                {
                    siteFeature.IsAssigned = false;
                }
                siteFeature.RoleFeaturePermissions = new List<FeaturePermissionResult>();
                List<FeaturePermission> featureAllowedPermissions = featuresPermissions.Where(qry => qry.FeatureId.Equals(siteFeature.FeatureId)).ToList();
                foreach (FeaturePermission featurePermission in featureAllowedPermissions)
                {
                    bool isAssigned = false;
                    if (roleFeature != null && roleFeature.RoleFeaturePermissions.Any(rec => rec.PermissionId.Equals(featurePermission.PermissionId)))
                        isAssigned = true;
                    siteFeature.RoleFeaturePermissions.Add(new FeaturePermissionResult { FeatureId = siteFeature.FeatureId, ContentPermissionId = featurePermission.PermissionId, ContentPermissionName = featurePermission.ContentPermission.ContentPermissionName, IsAssigned = isAssigned });
                }

            }
            return siteFeatures;
        }

        /// <summary>
        /// SaveRoleFeatures
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="lstFeatures"></param>
        public void SaveRoleFeatures(int roleId, List<FeatureResult> lstFeatures)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.Role);
            if (!userPermissions.Contains(PermissionEnum.EditContent))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            if (!string.IsNullOrEmpty(UserContextDetails.UserName))
            {
                UpdateRoleFeatures(roleId, lstFeatures);
            }
            else
            {
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);
            }
        }

        /// <summary>
        /// UpdateRoleFeatures
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="lstFeatures"></param>
        private void UpdateRoleFeatures(int roleId, List<FeatureResult> lstFeatures)
        {
            List<RoleFeature> lstRoleFeatures = eCollabroDbContext.Repository<RoleFeature>().Query().Include(inc => inc.RoleFeaturePermissions).Filter(qry => qry.RoleId.Equals(roleId)).Get().ToList();
            foreach (FeatureResult siteFeature in lstFeatures)
            {
                RoleFeature oldRoleFeature = lstRoleFeatures.Where(ee => ee.FeatureId.Equals(siteFeature.FeatureId)).FirstOrDefault();
                if (oldRoleFeature == null) //New
                {
                    RoleFeature roleFeature = new RoleFeature();
                    roleFeature.FeatureId = siteFeature.FeatureId;
                    roleFeature.RoleId = roleId;
                    roleFeature.CreatedById = UserContextDetails.UserId;
                    roleFeature.CreatedOn = DateTime.UtcNow;
                    foreach (FeaturePermissionResult permission in siteFeature.RoleFeaturePermissions)
                    {
                        roleFeature.RoleFeaturePermissions.Add(new RoleFeaturePermission { PermissionId = permission.ContentPermissionId, CreatedBy = UserContextDetails.UserId, CreatedOn = DateTime.UtcNow });
                    }
                    eCollabroDbContext.Repository<RoleFeature>().Insert(roleFeature);

                }
                else
                {

                    foreach (FeaturePermissionResult featurePermissionResult in siteFeature.RoleFeaturePermissions)
                    {
                        RoleFeaturePermission permission = oldRoleFeature.RoleFeaturePermissions.Where(qry => qry.PermissionId.Equals(featurePermissionResult.ContentPermissionId)).FirstOrDefault();
                        if (permission == null) // New Permission
                        {
                            oldRoleFeature.RoleFeaturePermissions.Add(new RoleFeaturePermission { PermissionId = featurePermissionResult.ContentPermissionId, CreatedBy = UserContextDetails.UserId, CreatedOn = DateTime.UtcNow });
                        }
                    }
                    // Remove unmapped 
                    foreach (RoleFeaturePermission featurePermission in oldRoleFeature.RoleFeaturePermissions.ToList())
                    {
                        FeaturePermissionResult permission = siteFeature.RoleFeaturePermissions.Where(qry => qry.ContentPermissionId.Equals(featurePermission.PermissionId)).FirstOrDefault();
                        if (permission == null) // New Permission
                        {
                            eCollabroDbContext.Repository<RoleFeaturePermission>().Delete(featurePermission);
                        }
                    }

                }

            }
            List<int> lstSiteFeatureIds = lstFeatures.Select(slt => slt.FeatureId).ToList();
            foreach (RoleFeature rolefeature in lstRoleFeatures)
            {
                if (!lstSiteFeatureIds.Contains(rolefeature.FeatureId))
                {
                    RoleFeature roleFeature = lstRoleFeatures.Where(qry => qry.FeatureId.Equals(rolefeature.FeatureId)).FirstOrDefault();
                    foreach (RoleFeaturePermission roleFeaturePermission in roleFeature.RoleFeaturePermissions.ToList())
                    {
                        eCollabroDbContext.Repository<RoleFeaturePermission>().Delete(roleFeaturePermission);
                    }
                    eCollabroDbContext.Repository<RoleFeature>().Delete(rolefeature);
                }
            }

            eCollabroDbContext.Save();
        }


        #endregion

        #region Account

        /// <summary>
        /// AuthenticateUser
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void AuthenticateUser(string username, string password)
        {
            bool status = false;
            UserMembership userMemberShip = eCollabroDbContext.Repository<UserMembership>().Query().Filter(qry => qry.UserName.Equals(username)).Get().FirstOrDefault();
            if (userMemberShip != null)
            {
                if (userMemberShip.IsLocked)
                {
                    throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UserAccountLocked), CoreValidationMessagesConstants.UserAccountLocked);
                }
                else if(!userMemberShip.IsConfirmed)
                {
                    throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.AccountNotConfirmed), CoreValidationMessagesConstants.AccountNotConfirmed);
                }
                else if(!userMemberShip.IsApproved || !userMemberShip.IsActive)
                {
                    throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.AccountNotApproved), CoreValidationMessagesConstants.AccountNotApproved);
                }
                else
                {
                    if (DataEncryption.ValidateHashedText(DataEncryption.Decrypt(password), userMemberShip.PasswordSalt, userMemberShip.Password))
                    {
                        status = true;
                        userMemberShip.PasswordFailuresSinceLastSuccess = 0;
                    }
                    else
                    {
                        userMemberShip.LastPasswordFailureDate = DateTime.UtcNow;
                        userMemberShip.PasswordFailuresSinceLastSuccess++;
                        if (userMemberShip.PasswordFailuresSinceLastSuccess > 5)
                        {
                            userMemberShip.IsLocked = true;
                        }
                    }
                    eCollabroDbContext.Save();
                }
            }
            if (!status)
            {
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.InvalidUserCredentials), CoreValidationMessagesConstants.InvalidUserCredentials);
            }
        }


        /// <summary>
        /// CreateAccount
        /// </summary>
        /// <param name="userMembership"></param>
        public void CreateAccount(UserMembership userMembership)
        {
            SiteConfiguration siteConfiguration = eCollabroDbContext.Repository<SiteConfiguration>().Query().Filter(qry => qry.SiteId.Equals(UserContextDetails.SiteId)).Get().FirstOrDefault();
            if (siteConfiguration == null || !siteConfiguration.AllowRegistration)
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.SiteRegistrationNotAllowed), CoreValidationMessagesConstants.SiteRegistrationNotAllowed);

            UserMembership existingUser = eCollabroDbContext.Repository<UserMembership>().Query().Filter(qry => qry.UserName.Equals(userMembership.UserName) || qry.Email.Equals(userMembership.Email)).Get().FirstOrDefault();
            if (existingUser != null) //user by user name or email already exist 
            {
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UserAlreadyExist), CoreValidationMessagesConstants.UserAlreadyExist);
            }
            else
            {
                KeyValuePair<string, string> pass = DataEncryption.CreateHashedText(DataEncryption.Decrypt(userMembership.Password), true);

                List<SiteContentSettingResult> siteContentSettingResults = GetSiteFeatureSettings(FeatureEnum.User);
                bool approvalRequired = siteContentSettingResults.Where(qry => qry.ContentSettingId.Equals((int)FeatureSettingEnum.ApprovalRequired)).FirstOrDefault().IsAssigned;

                UserRole userRole = new UserRole();
                userRole.SiteId = UserContextDetails.SiteId;
                userRole.RoleId = siteConfiguration.RegistrationDefaultRoleId.Value;
                userRole.CreatedOn = DateTime.UtcNow;
                userMembership.CreatedOn = DateTime.UtcNow;
                userMembership.UserRoles.Add(userRole);
                userMembership.Password = pass.Key;
                userMembership.PasswordSalt = pass.Value;
                if(siteConfiguration.AccountRequireEmailVerification)
                {
                    userMembership.IsConfirmed = false;
                    userMembership.ConfirmationToken=RandomGenerator.RandomString(10);
                }
                else
                {
                    userMembership.IsConfirmed=true;
                }
                userMembership.IsApproved=!approvalRequired;
                userMembership.IsActive = true;
                eCollabroDbContext.Repository<UserMembership>().Insert(userMembership);

                eCollabroDbContext.Save();
                
                if (siteConfiguration.AccountRequireEmailVerification)// send verification email
                {
                    var verificationLink = "<a href='/account/verifyaccount/?uname=" + userMembership.UserName + "&token=" + userMembership.ConfirmationToken + "'>Verify Account</a>";
                    string subject = "Verify Account";
                    string body = string.Format("Dear {0} <br/> Click link below to Verify your account.<br/>{1}", userMembership.UserName, verificationLink);
                    MailMessage message = new MailMessage("willbeoverwritten@xxx.com", userMembership.Email, subject, body);
                    _commonManager.SendEmail(message);
                }
                 
            }
        }

        /// <summary>
        /// GeneratePasswordResetToken
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public UserMembership ResetPassword(string username)
        {
            UserMembership userMembership = eCollabroDbContext.Repository<UserMembership>().Query().Filter(qry => qry.UserName.Equals(username)).Get().FirstOrDefault();
            if (userMembership == null)
            {
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.InvalidUserCredentials), CoreValidationMessagesConstants.InvalidUserCredentials);
            }
            else
            {
                string token = Guid.NewGuid().ToString();
                userMembership.PasswordVerificationToken = token;
                userMembership.PasswordVerificationTokenExpirationDate = DateTime.Now.AddDays(1).ToUniversalTime();
                eCollabroDbContext.Save();

                //Create email
                var resetLink = "<a href='" + ConfigurationManager.AppSettings["webRoot"].ToString()  +"account/resetpassword/?uname=" + userMembership.UserName + "&token=" + token + "'>Reset Password</a>";
                string subject = "Password Reset";
                string body = "Click Here to Reset Password" + resetLink;
                MailMessage message = new MailMessage("willbeoverwritten@xxx.com", userMembership.Email, subject, body);
                _commonManager.SendEmail(message);
            }
            return userMembership;
        }

        /// <summary>
        /// VerifyAccount
        /// </summary>
        /// <param name="username"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public void VerifyAccount(string username, string token)
        {
            UserMembership userMembership = eCollabroDbContext.Repository<UserMembership>().Query().Filter(qry => qry.UserName.Equals(username)).Get().FirstOrDefault();
            if (userMembership == null)
            {
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.InvalidUserCredentials), CoreValidationMessagesConstants.InvalidUserCredentials);
            }
            else if(userMembership.IsConfirmed)
            {
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.AccountAlreadyVerified), CoreValidationMessagesConstants.AccountAlreadyVerified);
            }
            else if (string.IsNullOrEmpty(userMembership.ConfirmationToken) || userMembership.ConfirmationToken!=token)
            {
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.InvalidVerificationToken), CoreValidationMessagesConstants.InvalidVerificationToken);
            }
            else
            {
                userMembership.IsConfirmed = true;
                userMembership.ModifiedOn = DateTime.UtcNow;
                eCollabroDbContext.Save();
            }
        }
        /// <summary>
        /// ResetPassword
        /// </summary>
        /// <param name="username"></param>
        /// <param name="token"></param>
        public string ResetPassword(string username, string token)
        {
            string encryptedPassword = null;
            UserMembership userMembership = eCollabroDbContext.Repository<UserMembership>().Query().Filter(qry => qry.UserName.Equals(username)).Get().FirstOrDefault();
            if (userMembership == null)
            {
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.InvalidUserCredentials), CoreValidationMessagesConstants.InvalidUserCredentials);
            }
            else if ((string.IsNullOrEmpty(userMembership.PasswordVerificationToken) || userMembership.PasswordVerificationTokenExpirationDate < DateTime.UtcNow) || userMembership.PasswordVerificationToken!=token)
            {
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.InvalidPasswordResetToken), CoreValidationMessagesConstants.InvalidPasswordResetToken);
            }
            else
            {
                encryptedPassword = ResetUserPassword(userMembership);
            }
            return encryptedPassword;
        }

        /// <summary>
        /// ResetUserPassword
        /// </summary>
        /// <param name="userMembership"></param>
        /// <returns></returns>
        private string ResetUserPassword(UserMembership userMembership)
        {
            string password = RandomGenerator.RandomString(8);
            KeyValuePair<string, string> pass = DataEncryption.CreateHashedText(password, true);
            userMembership.Password = pass.Key;
            userMembership.PasswordSalt = pass.Value;
            userMembership.PasswordVerificationToken = string.Empty;
            userMembership.IsLocked = false;
            userMembership.PasswordFailuresSinceLastSuccess = 0;
            eCollabroDbContext.Save();

            //Create email
            string subject = "Password has been reset";
            string body = string.Format("Your password has been reset to {0}", password);
            MailMessage message = new MailMessage("willbeoverwritten@xxx.com", userMembership.Email, subject, body);
            _commonManager.SendEmail(message);
            return DataEncryption.Encrypt(password);
        }


        /// <summary>
        /// ChangePassword
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        public void ChangePassword(string userName, string oldPassword, string newPassword)
        {
            #region Check Permission

            if (!userName.Equals(UserContextDetails.UserName)) // authorized user changing user's password 
            {
                List<PermissionEnum> userPermissions = GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.User);
                if (!userPermissions.Contains(PermissionEnum.EditContent))
                    throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);
            }

            #endregion

            UserMembership userMembership = eCollabroDbContext.Repository<UserMembership>().Query().Filter(qry => qry.UserName.Equals(userName)).Get().FirstOrDefault();
            if (userMembership != null)
            {
                if (userName.Equals(UserContextDetails.UserName)) //user changing his password
                {
                    //check old password
                    if (!DataEncryption.ValidateHashedText(DataEncryption.Decrypt(oldPassword), userMembership.PasswordSalt, userMembership.Password))
                    {
                        throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.InvalidUserCredentials), CoreValidationMessagesConstants.InvalidUserCredentials);
                    }
                }
                    
                KeyValuePair<string, string> pass = DataEncryption.CreateHashedText(DataEncryption.Decrypt(newPassword), true);
                userMembership.Password = pass.Key;
                userMembership.PasswordSalt = pass.Value;
                userMembership.PasswordVerificationToken = string.Empty;
                userMembership.IsLocked = false;
                userMembership.PasswordFailuresSinceLastSuccess = 0;
                eCollabroDbContext.Save();
            }
            else
            {
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.InvalidUserCredentials), CoreValidationMessagesConstants.InvalidUserCredentials);
            }
        }

        #endregion

        #region User

        ///<Summary>
        ///Get Users
        ///</Summary>
        ///<returns></returns>
        public List<UserMembership> GetUsers()
        {
            // user ids on site
            List<int> userIds = eCollabroDbContext.Repository<UserRole>().Query().Filter(qry => qry.SiteId.Equals(UserContextDetails.SiteId)).Get().Select(slt => slt.UserId).ToList();
            List<UserMembership> users = GetUsers(userIds);
            return users;
        }

        /// <summary>
        /// GetUsers
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public List<UserMembership> GetUsers(List<int> userIds)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.User);

            #endregion

            return eCollabroDbContext.Repository<UserMembership>().Query(userPermissions).Filter(qry => userIds.Contains(qry.UserId)).Get().ToList();
        }


        /// <summary>
        /// SaveUser
        /// </summary>
        /// <param name="userMember"></param>
        /// <param name="userProfile"></param>
        /// <param name="userRole"></param>
        public void SaveUser(UserMembership userMember)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.User);
            if ((userMember.UserId.Equals(0) && !userPermissions.Contains(PermissionEnum.AddContent)) || (!userMember.UserId.Equals(0) && !userPermissions.Contains(PermissionEnum.EditContent)))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            if (userMember.UserId.Equals(0)) //Add New User
            {
                #region Add User

                SiteContentSettingResult featureSetting = GetSiteFeatureSettings(FeatureEnum.User).Where(qry=>qry.ContentSettingId.Equals((int)FeatureSettingEnum.ApprovalRequired)).FirstOrDefault();
                if (featureSetting != null && featureSetting.IsAssigned)
                    userMember.IsApproved = false;
                else
                    userMember.IsApproved = true;
                UserUnicity(userMember.UserName,userMember.Email,0);
                string password = RandomGenerator.RandomString(8);
                KeyValuePair<string, string> hashedPassword = DataEncryption.CreateHashedText(password, true);
                userMember.Password = hashedPassword.Key;
                userMember.PasswordSalt = hashedPassword.Value;
                userMember.CreatedById = UserContextDetails.UserId;
                userMember.CreatedOn = DateTime.UtcNow;
                userMember.IsConfirmed = true;
                foreach (UserRole userRole in userMember.UserRoles)
                {
                    userRole.CreatedById = UserContextDetails.UserId;
                    userRole.CreatedOn = DateTime.UtcNow;
                    userRole.SiteId = UserContextDetails.SiteId;
                }
                eCollabroDbContext.Repository<UserMembership>().Insert(userMember);

                eCollabroDbContext.Save();

                //Create email
                string subject = "User Account Created";
                string body = "Your account has been created. Please see your credentials to login. " + Environment.NewLine + " Username " + userMember.UserName + Environment.NewLine + " Password " + password;
                MailMessage message = new MailMessage("willbeoverwritten@xxx.com", userMember.Email, subject, body);
                _commonManager.SendEmail(message);
                #endregion
            }
            else //update user
            {
                #region UpdateUser
                UserMembership oldUserDetails = eCollabroDbContext.Repository<UserMembership>().Query().Include(inc=>inc.UserRoles).Filter(qry => qry.UserName.Equals(userMember.UserName)).Get().FirstOrDefault();
                if (oldUserDetails != null)
                {
                    List<UserRole> existingRoles = oldUserDetails.UserRoles.Where(qry => qry.SiteId.Equals(UserContextDetails.SiteId)).ToList();
                    UserUnicity(userMember.UserName, userMember.Email, userMember.UserId);
                    // Confirm/Approve/Locked to be managed through individual methods
                    oldUserDetails.ModifiedById = UserContextDetails.UserId;
                    oldUserDetails.ModifiedOn = DateTime.UtcNow;
                    oldUserDetails.IsActive = userMember.IsActive;
                    oldUserDetails.Email = userMember.Email;
                    // Add New Roles
                    foreach (UserRole userRole in userMember.UserRoles)
                    {
                        if (!existingRoles.Any(qry=>qry.RoleId.Equals(userRole.RoleId)))
                        {
                            oldUserDetails.UserRoles.Add(new UserRole { CreatedById = UserContextDetails.UserId, CreatedOn = DateTime.UtcNow, RoleId = userRole.RoleId, UserId = oldUserDetails.UserId, SiteId = UserContextDetails.SiteId });
                        }
                    }
                    // Remove Not selected Roles
                    foreach (UserRole userRole in existingRoles)
                    {
                        if (!userMember.UserRoles.Any(qry => qry.RoleId.Equals(userRole.RoleId)))
                        {
                            eCollabroDbContext.Repository<UserRole>().Delete(userRole);
                        }
                    }
                    eCollabroDbContext.Save();
                }
                else
                {
                    throw new DBConcurrencyException();
                }

                #endregion
            }
        }

        /// <summary>
        /// GetUser
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserMembership GetUser(int userId)
        {
            UserMembership user = eCollabroDbContext.Repository<UserMembership>().Query().Include(inc => inc.UserRoles.Select(slt => slt.Role)).Filter(qry => qry.UserId.Equals(userId)).Get().FirstOrDefault();
            if (user == null)
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.RecordNotFound), CoreValidationMessagesConstants.RecordNotFound);
            else
                return user;
        }

        /// <summary>
        /// FindUser
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserMembership FindUser(int userId)
        {
            UserMembership user = eCollabroDbContext.Repository<UserMembership>().Find(userId);
            if (user == null)
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.InvalidUserCredentials), CoreValidationMessagesConstants.InvalidUserCredentials);
            else
                return user;
        }

        /// <summary>
        /// FindUser
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public UserMembership FindUser(string userName)
        {
            UserMembership user = eCollabroDbContext.Repository<UserMembership>().Query().Filter(qry => qry.UserName.Equals(userName)).Get().FirstOrDefault();
            if (user == null)
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.InvalidUserCredentials), CoreValidationMessagesConstants.InvalidUserCredentials);
            else
                return user;
        }

        private bool CheckUser(string userName,string email,int userId)
        {
            UserMembership user = eCollabroDbContext.Repository<UserMembership>().Query().Filter(qry => (qry.UserName.Equals(userName) || qry.Email.Equals(email)) && qry.UserId!=userId).Get().FirstOrDefault();
            if (user == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// UnlockUser
        /// </summary>
        /// <param name="userId"></param>
        public void UnlockUser(int userId)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.User);
            if (!userPermissions.Contains(PermissionEnum.EditContent))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            UserMembership user = FindUser(userId);
            user.IsLocked = false;
            user.ModifiedById = UserContextDetails.UserId;
            user.ModifiedOn = DateTime.UtcNow;
            eCollabroDbContext.Save();
        }

        /// <summary>
        /// ConfirmUser
        /// </summary>
        /// <param name="userId"></param>
        public void ConfirmUser(int userId)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.User);
            if (!userPermissions.Contains(PermissionEnum.EditContent))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            UserMembership user = FindUser(userId);
            user.IsConfirmed = true;
            user.ModifiedById = UserContextDetails.UserId;
            user.ModifiedOn = DateTime.UtcNow;
            eCollabroDbContext.Save();
        }


        /// <summary>
        /// ApproveUser
        /// </summary>
        /// <param name="userId"></param>
        public void ApproveUser(int userId)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.User);
            if (!userPermissions.Contains(PermissionEnum.ApproveContent))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            UserMembership user = FindUser(userId);
            user.IsApproved = true;
            user.ModifiedById = UserContextDetails.UserId;
            user.ModifiedOn = DateTime.UtcNow;
            eCollabroDbContext.Save();
        }


        /// <summary>
        /// Reset user password
        /// </summary>
        /// <param name="userId"></param>
        public void ResetPassword(int userId)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.User);
            if (!userPermissions.Contains(PermissionEnum.EditContent))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            UserMembership user = FindUser(userId);
            ResetUserPassword(user);
        }

        /// <summary>
        /// DeleteUser
        /// </summary>
        /// <param name="userId"></param>
        public void DeleteUser(int userId)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.User);
            if (!userPermissions.Contains(PermissionEnum.DeleteContent))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            UserMembership user = eCollabroDbContext.Repository<UserMembership>().Query().Include(inc => inc.UserRoles).Filter(qry => qry.UserId.Equals(userId)).Get().FirstOrDefault();
            if (user != null)
            {   // check site admin or site collection admin cannot be deleted - to be implemented

                if (!user.UserRoles.Any(qry => qry.SiteId.Equals(UserContextDetails.SiteId))) //user exist for other site , delete roles from site
                {
                    foreach (UserRole userRole in user.UserRoles)
                    {
                        if (userRole.SiteId.Equals(UserContextDetails.SiteId))
                        {
                            eCollabroDbContext.Repository<UserRole>().Delete(userRole);
                        }
                    }
                }
                else
                {
                    user.IsDeleted = true;
                }
                user.ModifiedById = UserContextDetails.UserId;
                user.ModifiedOn = DateTime.UtcNow;
                eCollabroDbContext.Save();
            }
            else
            {
                throw new DBConcurrencyException();
            }
        }

        /// <summary>
        /// UserNameUnicity
        /// </summary>
        /// <param name="username"></param>
        private void UserUnicity(string username,string email, int userId)
        {
            if (CheckUser(username,email,userId))
            {
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UserAlreadyExist), CoreValidationMessagesConstants.UserAlreadyExist);
            }
        }

        /// <summary>
        /// GetUserFeaturePermission  
        /// </summary>
        /// <returns></returns>
        public List<PermissionEnum> GetUserFeaturePermissions(int userId, FeatureEnum featureId)
        {
            List<PermissionEnum> userPermissions = new List<PermissionEnum>();
            SiteFeature feature = eCollabroDbContext.Repository<SiteFeature>().Query().Filter(qry => qry.SiteId.Equals(UserContextDetails.SiteId) && qry.FeatureId.Equals((int)featureId)).Get().FirstOrDefault();
            if (feature != null) // else feature not assigned to Site 
            {
                #region Feature Assigned to Site

                // Anomynous Content Access
                userPermissions.Add(PermissionEnum.ViewAnomynousContent);
  

                if (UserContextDetails.ImpersonateViewRights)
                {
                    userPermissions.Add(PermissionEnum.ViewContent);
                }

                if (UserContextDetails.UserId == 0)
                    return userPermissions;

                // check Site Collection Admin Or Site Admin
                if (CheckSiteCollectionAdmin(UserContextDetails.UserId) || CheckSiteAdmin(UserContextDetails.UserId, UserContextDetails.SiteId))
                {
                    foreach (PermissionEnum val in Enum.GetValues(typeof(PermissionEnum)))
                    {
                        if (!userPermissions.Contains(val))
                        {
                            userPermissions.Add(val);
                        }
                    }// Add All // no need to check further permission
                }
                else
                {

                    List<UserRole> roles = eCollabroDbContext.Repository<UserRole>().Query().Filter(qry => qry.SiteId.Equals(UserContextDetails.SiteId) && qry.UserId.Equals(userId)).Get().ToList();
                    // get all role features for user roles on site
                    List<int> userRoleIds = roles.Select(slc => slc.RoleId).ToList();
                    List<RoleFeature> roleFeatures = eCollabroDbContext.Repository<RoleFeature>().Query().Include(inc => inc.RoleFeaturePermissions).Filter(qry => qry.FeatureId.Equals((int)featureId) && userRoleIds.Contains(qry.RoleId)).Get().ToList();
                    foreach (RoleFeature roleFeature in roleFeatures)
                    {
                        foreach (RoleFeaturePermission roleFeaturePermission in roleFeature.RoleFeaturePermissions)
                        {
                            if (!userPermissions.Contains((PermissionEnum)roleFeaturePermission.PermissionId))
                            {
                                userPermissions.Add((PermissionEnum)roleFeaturePermission.PermissionId);
                            }
                        }
                    }
                }

                #endregion
            }
            else
            {
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.FeatureNotAssignedToSite), CoreValidationMessagesConstants.FeatureNotAssignedToSite);
            }
            return userPermissions;
        }

        #endregion

        #region Navigation

        /// <summary>
        /// GetNavigations
        /// </summary>
        /// <returns></returns>
        public List<NavigationResult> GetNavigations()
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.Navigation);
            if (!(userPermissions.Contains(PermissionEnum.ViewContent)))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            List<NavigationResult> navigation_Results = null;
            if (!string.IsNullOrEmpty(UserContextDetails.UserName))
            {
                int userId = UserContextDetails.UserId;
                navigation_Results = eCollabroDbContext.ExtendedRepository().SecurityRepository.GetNavigations(UserContextDetails.SiteId);
            }
            else
            {
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);
            }
            List<NavigationResult> inOrderResult = new List<NavigationResult>();
            foreach (NavigationResult navigation in navigation_Results.Where(qry => !qry.NavigationParentId.HasValue).ToList())
            {
                inOrderResult.Add(navigation);
                AddChildNavigations(inOrderResult, navigation_Results, navigation.NavigationId);
            }
            return inOrderResult;
        }

        /// <summary>
        /// AddChildNavigations
        /// </summary>
        /// <param name="inOrderResult"></param>
        /// <param name="actualResult"></param>
        /// <param name="currentNavigationId"></param>
        private void AddChildNavigations(List<NavigationResult> inOrderResult, List<NavigationResult> actualResult, int currentNavigationId)
        {
            foreach (NavigationResult navigation in actualResult.Where(qry => qry.NavigationParentId.HasValue && qry.NavigationParentId.Value.Equals(currentNavigationId)).ToList())
            {
                inOrderResult.Add(navigation);
                AddChildNavigations(inOrderResult, actualResult, navigation.NavigationId);
            }
        }

        /// <summary>
        /// GetNavigation
        /// </summary>
        /// <param name="navigationId"></param>
        /// <returns></returns>
        public NavigationResult GetNavigation(int navigationId)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.Navigation);
            if (!userPermissions.Contains(PermissionEnum.ViewContent))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            NavigationResult navigation_Result = eCollabroDbContext.ExtendedRepository().SecurityRepository.GetNavigationDetails(navigationId);
            return GetContentResponse<NavigationResult>( navigation_Result,userPermissions);

        }


        /// <summary>
        /// SaveNavigation
        /// </summary>
        /// <param name="navigation"></param>
        public void SaveNavigation(Navigation navigation)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.Navigation);
            if ((navigation.NavigationId.Equals(0) && !userPermissions.Contains(PermissionEnum.AddContent)) || (!navigation.NavigationId.Equals(0) && !userPermissions.Contains(PermissionEnum.EditContent)))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            if (navigation.NavigationId.Equals(0)) // Adding Navigation
            {
                navigation.NavigationCode = _commonManager.GetNextCode(EntityConstants.Navigation);
                navigation.SiteId = UserContextDetails.SiteId;
                navigation.CreatedById = UserContextDetails.UserId;
                navigation.CreatedOn = DateTime.UtcNow;
                eCollabroDbContext.Repository<Navigation>().Insert(navigation);
                eCollabroDbContext.Save();
            }
            else  // Updating Navigation
            {
                Navigation oldNavigation = eCollabroDbContext.Repository<Navigation>().Find(navigation.NavigationId);
                if (oldNavigation != null)
                {

                    oldNavigation.NavigationText = navigation.NavigationText;
                    oldNavigation.AdditionalHtml = navigation.AdditionalHtml;
                    oldNavigation.SiteId = UserContextDetails.SiteId;
                    oldNavigation.NavigationParentId = navigation.NavigationParentId;
                    oldNavigation.NavigationTypeId = navigation.NavigationTypeId;
                    oldNavigation.FeatureId = navigation.FeatureId;
                    oldNavigation.ContentPageId = navigation.ContentPageId;
                    oldNavigation.Link = navigation.Link;
                    oldNavigation.DisplayOrder = navigation.DisplayOrder;
                    oldNavigation.IsAnomynousAccess = navigation.IsAnomynousAccess;
                    oldNavigation.IsActive = navigation.IsActive;
                    oldNavigation.ModifiedOn = DateTime.UtcNow;
                    oldNavigation.ModifiedById = UserContextDetails.UserId;
                    eCollabroDbContext.Save();

                }
                else
                {
                    throw new DBConcurrencyException();
                }
            }
        }


        /// <summary>
        /// DeleteNavigation
        /// </summary>
        /// <param name="navigationId"></param>
        public void DeleteNavigation(int navigationId)
        {
            #region Check Permission

            List<PermissionEnum> userPermissions = GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.Navigation);
            if (!userPermissions.Contains(PermissionEnum.DeleteContent))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            Navigation oldNavigation = eCollabroDbContext.Repository<Navigation>().Find(navigationId);
            if (oldNavigation != null)
            {
                oldNavigation.IsDeleted = true;
                oldNavigation.ModifiedOn = DateTime.UtcNow;
                oldNavigation.ModifiedById = UserContextDetails.UserId;
                eCollabroDbContext.Save();
            }
            else
            {
                throw new DBConcurrencyException();
            }
        }

        #endregion

        #region Site Configuration

        /// <summary>
        /// GetSiteConfiguration
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public SiteConfiguration GetSiteConfiguration()
        {

            SiteConfiguration siteConfiguration = eCollabroDbContext.Repository<SiteConfiguration>().Query().Filter(qry => qry.SiteId.Equals(UserContextDetails.SiteId)).Get().FirstOrDefault();
            if (siteConfiguration == null)
            {
                siteConfiguration = new SiteConfiguration();
                siteConfiguration.SiteId = UserContextDetails.SiteId;
            }
            return siteConfiguration;
        }

        /// <summary>
        /// SaveSiteConfiguration
        /// </summary>
        /// <param name="siteConfiguration"></param>
        public void SaveSiteConfiguration(SiteConfiguration siteConfiguration)
        {
            #region Check Permission

            // Check Permission 
            List<PermissionEnum> userPermissions = GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.SiteConfiguration);
            if (!userPermissions.Contains(PermissionEnum.EditContent))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            if (eCollabroDbContext.Repository<SiteConfiguration>().IsEntityAttachedToDB(siteConfiguration))
            {
                eCollabroDbContext.Save(); // Service Call after get;
            }
            else
            {
                SiteConfiguration siteConfigurationDB = eCollabroDbContext.Repository<SiteConfiguration>().Query().Filter(qry => qry.SiteId.Equals(UserContextDetails.SiteId)).Get().FirstOrDefault();
                if (siteConfigurationDB == null)
                {
                    siteConfigurationDB = siteConfiguration;
                    eCollabroDbContext.Repository<SiteConfiguration>().Insert(siteConfigurationDB);
                }
                siteConfigurationDB.ModifiedById = UserContextDetails.UserId;
                siteConfigurationDB.ModifiedOn = DateTime.UtcNow;
                siteConfigurationDB.AllowRegistration = siteConfiguration.AllowRegistration;
                siteConfigurationDB.RegistrationDefaultRoleId = siteConfiguration.RegistrationDefaultRoleId;
                siteConfigurationDB.AccountRequireEmailVerification = siteConfiguration.AccountRequireEmailVerification;
                eCollabroDbContext.Save();
            }
        }

        /// <summary>
        /// GetSiteFeaturesSettings
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public List<FeatureResult> GetSiteFeaturesSettings(int siteId)
        {
            List<FeatureResult> siteFeatures = eCollabroDbContext.ExtendedRepository().SecurityRepository.GetSiteAssignedFeatures(siteId);
            List<SiteContentSettingResult> featuresContentSettings = eCollabroDbContext.ExtendedRepository().SecurityRepository.GetFeaturesContentSettings(UserContextDetails.SiteId).ToList();
            foreach (FeatureResult featureResult in siteFeatures)
            {
                featureResult.SiteContentSettings = featuresContentSettings.Where(qry => qry.FeatureId.Equals(featureResult.FeatureId)).ToList();
            }

            return siteFeatures;
        }

        /// <summary>
        /// GetSiteFeatureSettings
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public List<SiteContentSettingResult> GetSiteFeatureSettings(FeatureEnum feature)
        {
            int featureId = Convert.ToInt32(feature);
            List<SiteContentSettingResult> featuresContentSettings = eCollabroDbContext.ExtendedRepository().SecurityRepository.GetFeaturesContentSettings(UserContextDetails.SiteId).Where(qry => qry.FeatureId.Equals(featureId)).ToList();
            return featuresContentSettings;
        }

        /// <summary>
        /// SaveSiteFeaturesSettings
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="featureResults"></param>
        public void SaveSiteFeaturesSettings(int siteId, List<FeatureResult> featureResults)
        {
            #region Check Permission

            // Check Permission 
            List<PermissionEnum> userPermissions = GetUserFeaturePermissions(UserContextDetails.UserId, FeatureEnum.SiteConfiguration);
            if (!userPermissions.Contains(PermissionEnum.EditContent))
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);

            #endregion

            // existing all feature content settings for Site 
            List<SiteContentSettingResult> featuresContentSettings = eCollabroDbContext.ExtendedRepository().SecurityRepository.GetFeaturesContentSettings(UserContextDetails.SiteId).ToList();

            foreach (SiteContentSettingResult result in featuresContentSettings)
            {
                // New setting 
                bool isAssigned = false;
                FeatureResult feature = featureResults.Where(qry => qry.FeatureId.Equals(result.FeatureId)).FirstOrDefault();
                if (feature != null && feature.SiteContentSettings.Any(qry => qry.ContentSettingId.Equals(result.ContentSettingId) && qry.IsAssigned))
                    isAssigned = true;

                if (!result.IsAssigned && isAssigned) // Was not assigned but assigned 
                {
                    eCollabroDbContext.Repository<SiteContentSetting>().Insert(new SiteContentSetting { SiteId = UserContextDetails.SiteId, FeatureId = feature.FeatureId, CreatedById = UserContextDetails.UserId, CreatedOn = DateTime.UtcNow, ContentSettingId = result.ContentSettingId });
                }
                else if (result.IsAssigned && !isAssigned) // Was assigned but not assigned 
                {
                    SiteContentSetting siteContentSetting = eCollabroDbContext.Repository<SiteContentSetting>().Query().Filter(qry => qry.SiteId.Equals(UserContextDetails.SiteId) && qry.ContentSettingId.Equals(result.ContentSettingId) && qry.FeatureId.Equals(feature.FeatureId)).Get().FirstOrDefault();
                    eCollabroDbContext.Repository<SiteContentSetting>().Delete(siteContentSetting);
                }
            }

            eCollabroDbContext.Save();
        }

        #endregion

        #region Sites

        /// <summary>
        /// GetSites
        /// </summary>
        /// <returns></returns>
        public List<Site> GetSites()
        {
            List<Site> sites = null;
            if (CheckSiteCollectionAdmin(UserContextDetails.UserId))
            {
                sites = eCollabroDbContext.Repository<Site>().Query().Get().ToList();
            }
            else
            {
                List<PermissionEnum> userPermissions = new List<PermissionEnum>();
                userPermissions.Add(PermissionEnum.ViewContent);
                sites = eCollabroDbContext.Repository<Site>().Query(userPermissions).Get().ToList();
            }
            return sites;
        }


        /// <summary>
        /// GetSite
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public Site GetSite(int siteId)
        {
            Site site = null;
            if (CheckSiteCollectionAdmin(UserContextDetails.UserId))
            {
                site = eCollabroDbContext.Repository<Site>().Find(siteId);
            }
            else
            {
                List<PermissionEnum> userPermissions = new List<PermissionEnum>();
                userPermissions.Add(PermissionEnum.ViewContent);
                site = eCollabroDbContext.Repository<Site>().Find(siteId);
            }
            return GetContentResponse<Site>(site);
        }

        /// <summary>
        /// SaveSite
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        public void SaveSite(Site site)
        {
            if (!CheckSiteCollectionAdmin(UserContextDetails.UserId))
            {
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);
            }

            if (site.SiteId.Equals(0)) // Adding New Site
            {
                site.SiteCode = _commonManager.GetNextCode(EntityConstants.Site);
                site.CreatedById = UserContextDetails.UserId;
                site.CreatedOn = DateTime.UtcNow;
                eCollabroDbContext.Repository<Site>().Insert(site);
                eCollabroDbContext.Save();
            }
            else  // Updating Site
            {
                if (site.SiteId.Equals(1) && site.IsActive.Equals(false))
                {
                    throw new BusinessException(string.Format(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.SystemDefinedValue),"[Active Field]"), CoreValidationMessagesConstants.SystemDefinedValue);
                }
                Site oldSite = eCollabroDbContext.Repository<Site>().Find(site.SiteId);
                if (oldSite != null)
                {
                    oldSite.SiteName = site.SiteName;
                    oldSite.SiteDesc = site.SiteDesc;
                    oldSite.IsActive = site.IsActive;
                    oldSite.ModifiedById = UserContextDetails.UserId;
                    oldSite.ModifiedOn = DateTime.UtcNow;
                    eCollabroDbContext.Save();
                }
                else
                {
                    throw new DBConcurrencyException();
                }

            }
        }

        /// <summary>
        /// DeleteSite
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public void DeleteSite(int siteId)
        {
            if (!CheckSiteCollectionAdmin(UserContextDetails.UserId))
            {
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);
            }
            if (siteId.Equals(1))
            {
                throw new BusinessException(string.Format(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.SystemDefinedValue),"[Site]"), CoreValidationMessagesConstants.SystemDefinedValue);
            }
            Site oldSite = eCollabroDbContext.Repository<Site>().Find(siteId);
            if (oldSite != null)
            {
                oldSite.IsDeleted = true;
                oldSite.ModifiedById = UserContextDetails.UserId;
                oldSite.ModifiedOn = DateTime.UtcNow;
                eCollabroDbContext.Save();
            }
            else
            {
                throw new DBConcurrencyException();
            }
        }

        /// <summary>
        /// Copy Existing Site
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        public void CopySite(int siteId)
        {
            if (!CheckSiteCollectionAdmin(UserContextDetails.UserId))
            {
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);
            }

            Site site = eCollabroDbContext.Repository<Site>().Query().Include(inc => inc.Navigations).Include(inc => inc.Roles).Filter(qry => qry.SiteId.Equals(siteId)).Get().FirstOrDefault();
            if (site != null)
            {
                #region Site Copy

                Site newSite = new Site();
                newSite.SiteName = site.SiteName + "-Copy";
                newSite.SiteDesc = site.SiteDesc;
                newSite.IsActive = true;
                newSite.SiteCode = _commonManager.GetNextCode(EntityConstants.Site);
                newSite.CreatedById = UserContextDetails.UserId;
                newSite.CreatedOn = DateTime.UtcNow;

                #endregion

                #region Feature Copy

                foreach (SiteFeature siteFeature in site.SiteFeatures)
                {
                    SiteFeature newsiteFeature = new SiteFeature();
                    newsiteFeature.FeatureId = siteFeature.FeatureId;
                    newsiteFeature.CreatedById = UserContextDetails.UserId;
                    newsiteFeature.CreatedOn = DateTime.UtcNow;
                    newSite.SiteFeatures.Add(newsiteFeature);
                }

                #endregion

                #region Navigation Copy

                foreach (Navigation navigation in site.Navigations)
                {
                    Navigation newNavigation = new Navigation();
                    newNavigation.NavigationCode = _commonManager.GetNextCode(EntityConstants.Navigation);
                    newNavigation.NavigationParentId = navigation.NavigationParentId;
                    newNavigation.ContentPageId = navigation.ContentPageId;
                    newNavigation.DisplayOrder = navigation.DisplayOrder;
                    newNavigation.IsAnomynousAccess = navigation.IsAnomynousAccess;
                    newNavigation.FeatureId = navigation.FeatureId;
                    newNavigation.NavigationText = navigation.NavigationText;
                    newNavigation.Link = navigation.Link;
                    newNavigation.IsActive = true;
                    newNavigation.IsDeleted = false;
                    newNavigation.NavigationTypeId = navigation.NavigationTypeId;
                    newNavigation.CreatedById = UserContextDetails.UserId;
                    newNavigation.CreatedOn = DateTime.UtcNow;
                    newSite.Navigations.Add(newNavigation);
                }

                #endregion

                #region Role Copy

                foreach (Role role in site.Roles)
                {
                    Role newRole = new Role();
                    newRole.RoleCode = _commonManager.GetNextCode(EntityConstants.Role);
                    newRole.RoleName = role.RoleName;
                    newRole.RoleDescription = role.RoleDescription;
                    newRole.IsSystem = role.IsSystem;
                    newRole.IsActive = true;
                    newRole.IsDeleted = false;
                    newRole.CreatedById = UserContextDetails.UserId;
                    newRole.CreatedOn = DateTime.UtcNow;
                    newSite.Roles.Add(newRole);
                }

                #endregion

                eCollabroDbContext.Repository<Site>().Insert(newSite);
                eCollabroDbContext.Save();

                #region Change Navigation Ids

                List<Navigation> fxNavition = site.Navigations.ToList();
                Dictionary<int, int?> dict = new Dictionary<int, int?>();
                for (int i = 0; i < fxNavition.Count; i++)
                {
                    if (fxNavition[i].NavigationParentId.HasValue)
                    {
                        int j = 0;
                        for (; j < fxNavition.Count; j++)
                        {
                            if (fxNavition[i].NavigationParentId.Value.Equals(fxNavition[j].NavigationId))
                            {
                                dict.Add(i, j);
                                break;
                            }
                        }

                    }
                    else
                    {
                        dict.Add(i, null);
                    }
                }

                for (int k = 0; k < dict.Count; k++)
                {
                    int? m;
                    dict.TryGetValue(k, out m);
                    if (m == null)
                    {
                        newSite.Navigations.ToList()[k].NavigationParentId = null;
                    }
                    else
                    {
                        newSite.Navigations.ToList()[k].NavigationParentId = newSite.Navigations.ToList()[m.Value].NavigationId;
                    }
                }


                eCollabroDbContext.Save();

                #endregion

            }
            else
            {
                throw new DBConcurrencyException();
            }
        }


        #endregion

        #region Site Features

        /// <summary>
        /// GetSiteFeatures
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public List<FeatureResult> GetSiteFeatures(int siteId)
        {
            List<FeatureResult> siteFeatures = null;
            if (!siteId.Equals(0))
            {
                siteFeatures = eCollabroDbContext.ExtendedRepository().SecurityRepository.GetSiteFeatures(siteId);
            }
            else
            {
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UserAlreadyExist), CoreValidationMessagesConstants.UserAlreadyExist);
            }
            return siteFeatures;
        }

        /// <summary>
        /// SaveSiteFeatures
        /// </summary>
        /// <param name="lstFeatures"></param>
        /// <param name="siteId"></param>
        public void SaveSiteFeatures(List<int> lstFeatures, int siteId, bool isCreateNavigationChecked)
        {
            if (!CheckSiteCollectionAdmin(UserContextDetails.UserId))
            {
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);
            }

            SaveSiteFeatures(lstFeatures, siteId);

            if (isCreateNavigationChecked)
            {
                List<FeatureResult> siteFeatures = eCollabroDbContext.ExtendedRepository().SecurityRepository.GetSiteFeatures(siteId).Where(fx => fx.IsAssigned.HasValue && fx.IsAssigned.Value && fx.IsNavigationLink).ToList();

                List<Navigation> navigationsLst = new List<Navigation>();

                var navLst = eCollabroDbContext.ExtendedRepository().SecurityRepository.GetNavigations(UserContextDetails.SiteId);


                foreach (FeatureResult feature in siteFeatures)
                {
                    if (!navLst.Where(c => c.FeatureId.Equals(feature.FeatureId)).Any())
                    {

                        var module = navLst.Where(fx => fx.NavigationText.Equals(feature.ModuleName)).FirstOrDefault();
                        if (module == null)
                        {
                            // Add top menu (Module Name)
                            Navigation navigation = new Navigation();

                            navigation.NavigationCode = _commonManager.GetNextCode(EntityConstants.Navigation);
                            navigation.NavigationText = feature.ModuleName;
                            navigation.SiteId = siteId;
                            navigation.NavigationTypeId = (int)NavigationTypeEnum.None; // None for top Menu
                            navigation.FeatureId = feature.FeatureId;
                            navigation.IsAnomynousAccess = false; // Default no anonymous access
                            navigation.IsActive = true;
                            navigation.CreatedOn = DateTime.UtcNow;
                            navigation.CreatedById = UserContextDetails.UserId;

                            eCollabroDbContext.Repository<Navigation>().Insert(navigation);
                            eCollabroDbContext.Save();
                            navigationsLst.Add(navigation);
                            navLst.Add(new NavigationResult { NavigationId = navigation.NavigationId, NavigationText = navigation.NavigationText }); // added to search in loop
                        }
                        else
                        {
                            //Add Module Features (Sub Menu)
                            Navigation navigationc = new Navigation
                            {
                                NavigationCode = _commonManager.GetNextCode(EntityConstants.Navigation),
                                NavigationText = feature.FeatureName,
                                SiteId = siteId,
                                NavigationTypeId = (int)NavigationTypeEnum.Feature,
                                FeatureId = feature.FeatureId,
                                IsAnomynousAccess = false, // Need to change
                                NavigationParentId = module.NavigationId,
                                IsActive = true,
                                CreatedOn = DateTime.UtcNow,
                                CreatedById = UserContextDetails.UserId
                            };
                            navigationsLst.Add(navigationc);
                        }
                    }
                }
                List<Navigation> notSavedList = navigationsLst.Where(fx => fx.NavigationId.Equals(0)).ToList();

                notSavedList.ForEach(fx => eCollabroDbContext.Repository<Navigation>().Insert(fx));
                eCollabroDbContext.Save();
            }
        }

        /// <summary>
        /// SaveSiteFeatures
        /// </summary>
        /// <param name="SiteFeatures"></param>
        private void SaveSiteFeatures(List<int> lstFeatures, int siteId)
        {

            List<SiteFeature> siteFeatures = eCollabroDbContext.Repository<SiteFeature>().Query().Filter(qry => qry.SiteId.Equals(siteId)).Get().ToList();
            foreach (int featureId in lstFeatures)
            {
                SiteFeature oldSiteFeature = siteFeatures.Where(ee => ee.FeatureId.Equals(featureId)).FirstOrDefault();
                if (oldSiteFeature == null) //New
                {
                    SiteFeature siteFeature = new SiteFeature();
                    siteFeature.FeatureId = featureId;
                    siteFeature.SiteId = siteId;
                    siteFeature.CreatedById = UserContextDetails.UserId;
                    siteFeature.CreatedOn = DateTime.UtcNow;
                    eCollabroDbContext.Repository<SiteFeature>().Insert(siteFeature);
                }
            }


            foreach (SiteFeature siteFeature in siteFeatures)
            {
                if (!lstFeatures.Contains(siteFeature.FeatureId))
                {
                    eCollabroDbContext.Repository<SiteFeature>().Delete(siteFeature);
                }
            }
            if (lstFeatures.Count == 0) // unselect the existing feature
            {
                foreach (SiteFeature siteFeature in siteFeatures)
                {
                    eCollabroDbContext.Repository<SiteFeature>().Delete(siteFeature);
                }
            }
            eCollabroDbContext.Save();
        }

        #endregion

        #region NavigationFeatures

        /// <summary>
        /// GetNavigationFeatures
        /// </summary>
        /// <returns></returns>
        public List<NavigationResult> GetUserNavigations()
        {
            List<NavigationResult> userNavigations = eCollabroDbContext.ExtendedRepository().SecurityRepository.GetUserNavigations(UserContextDetails.UserId, UserContextDetails.SiteId);
            SiteCollectionAdmin siteCollectionAdmin = eCollabroDbContext.Repository<SiteCollectionAdmin>().Query().Filter(qry => qry.UserId.Equals(UserContextDetails.UserId)).Get().FirstOrDefault();
            if (siteCollectionAdmin != null)//current user is Site Collection Admin
            {
                userNavigations.Add(new NavigationResult { NavigationText = "Portal Setup", Link = "#", NavigationTypeId = 1, NavigationId = 1000 });
                userNavigations.Add(new NavigationResult { NavigationText = "Manage Sites", Link = "/security/sites", NavigationTypeId = 4, NavigationId = 1001, NavigationParentId = 1000 });
                userNavigations.Add(new NavigationResult { NavigationText = "Site Collection Admin", Link = "/setup/sitecollectionadmin", NavigationTypeId = 4, NavigationId = 1001, NavigationParentId = 1000 });
                userNavigations.Add(new NavigationResult { NavigationText = "Email Configuration", Link = "/setup/emailconfiguration", NavigationTypeId = 4, NavigationId = 1002, NavigationParentId = 1000 });

            }
            return userNavigations;
        }



        #endregion

        #region Module & Feature


        /// <summary>
        /// GetModules
        /// </summary>
        /// <returns></returns>
        public List<Module> GetModules()
        {
            List<Module> modules = eCollabroDbContext.Repository<Module>().Query().Get().ToList();
            return modules;
        }

        /// <summary>
        /// GetModules
        /// </summary>
        /// <returns></returns>
        public Module GetModule(int moduleId)
        {
            Module modules = eCollabroDbContext.Repository<Module>().Find(moduleId);
            return modules;
        }

        /// <summary>
        /// GetFeatures
        /// </summary>
        /// <returns></returns>
        public List<Feature> GetFeatures(int moduleId)
        {
            List<Feature> features = eCollabroDbContext.Repository<Feature>().Query().Filter(qry => qry.ModuleId.Equals(moduleId)).Get().ToList();
            return features;
        }

        /// <summary>
        /// SaveModule
        /// </summary>
        /// <param name="module"></param>
        public void SaveModule(Module module)
        {
            if (module.ModuleId.Equals(0))
            {
                module.ModuleCode = _commonManager.GetNextCode(EntityConstants.Module);
                module.CreatedById = UserContextDetails.UserId;
                module.CreatedOn = DateTime.UtcNow;
                eCollabroDbContext.Repository<Module>().Insert(module);
                eCollabroDbContext.Save();
            }
            else
            {
                Module oldModule = eCollabroDbContext.Repository<Module>().Find(module.ModuleId);
                if (oldModule != null)
                {
                    oldModule.ModuleCode = module.ModuleCode;
                    oldModule.ModuleName = module.ModuleName;
                    oldModule.ModuleDescription = module.ModuleDescription;
                    oldModule.ModifiedById = UserContextDetails.UserId;
                    oldModule.ModifiedOn = DateTime.UtcNow;
                    eCollabroDbContext.Save();
                }
                else
                {
                    throw new DBConcurrencyException();
                }
            }

        }

        /// <summary>
        /// DeleteModule
        /// </summary>
        /// <param name="moduleId"></param>
        public void DeleteModule(int moduleId)
        {
            if (CheckSiteCollectionAdmin(UserContextDetails.UserId))
            {
                Module oldModule = eCollabroDbContext.Repository<Module>().Find(moduleId);
                if (oldModule != null)
                {
                    oldModule.ModifiedOn = DateTime.UtcNow;
                    oldModule.ModifiedById = UserContextDetails.UserId;
                    eCollabroDbContext.Save();
                }
            }
            else
            {
                throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.UnAuthorized), CoreValidationMessagesConstants.UnAuthorized);
            }
        }

        /// <summary>
        /// GetFeatures
        /// </summary>
        /// <returns></returns>
        public List<Feature> GetAllFeatures()
        {
            List<Feature> features = eCollabroDbContext.Repository<Feature>().Query().Get().ToList();
            return features;
        }

        /// <summary>
        /// SaveFeature
        /// </summary>
        /// <param name="feature"></param>
        public void SaveFeature(Feature feature)
        {
            if (feature.FeatureId.Equals(0))
            {
                feature.FeatureCode = _commonManager.GetNextCode(EntityConstants.Feature);
                feature.CreatedById = UserContextDetails.UserId;
                feature.CreatedOn = DateTime.UtcNow;
                eCollabroDbContext.Repository<Feature>().Insert(feature);
                eCollabroDbContext.Save();
            }
            else
            {
                Feature oldFeature = eCollabroDbContext.Repository<Feature>().Find(feature.FeatureId);
                if (oldFeature != null)
                {
                    oldFeature.FeatureName = feature.FeatureName;
                    oldFeature.IsNavigationLink = feature.IsNavigationLink;
                    oldFeature.Link = feature.Link;
                    oldFeature.ModifiedById = UserContextDetails.UserId;
                    oldFeature.ModifiedOn = DateTime.UtcNow;
                    eCollabroDbContext.Save();
                }
                else
                {
                    throw new DBConcurrencyException();
                }
            }

        }

        /// <summary>
        /// DeleteFeature
        /// </summary>
        /// <param name="featureId"></param>
        public void DeleteFeature(int featureId)
        {
            Feature oldFeature = eCollabroDbContext.Repository<Feature>().Find(featureId);
            if (oldFeature != null)
            {
                oldFeature.ModifiedById = UserContextDetails.UserId;
                oldFeature.ModifiedOn = DateTime.UtcNow;
                oldFeature.IsDeleted = true;
                eCollabroDbContext.Save();
            }
            else
            {
                throw new DBConcurrencyException();
            }
        }

        #endregion

        #region Check User Permission

        /// <summary>
        /// CheckSiteCollectionAdmin
        /// </summary>
        /// <returns></returns>
        public bool CheckSiteCollectionAdmin(int userId)
        {
            if (userId.Equals(0))
                return false;
            SiteCollectionAdmin siteCollectionAdmin = eCollabroDbContext.Repository<SiteCollectionAdmin>().Query().Filter(qry => qry.UserId.Equals(userId)).Get().FirstOrDefault();
            if (siteCollectionAdmin == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// CheckSiteAdmin
        /// </summary>
        /// <returns></returns>
        public bool CheckSiteAdmin(int userId, int siteId)
        {
            if (userId.Equals(0))
                return false;
            UserRole userRole = eCollabroDbContext.Repository<UserRole>().Query().Filter(qry => qry.UserId.Equals(userId) && qry.SiteId.Equals(siteId) && qry.RoleId.Equals((int)SystemConstant.SiteAdminRoleId)).Get().FirstOrDefault();
            if (userRole == null)
                return false;
            else
                return true;
        }

        #endregion

        #endregion

    }
}
