// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References
using eCollabro.Utilities;
using System;
using System.Linq;
using eCollabro.BAL.Entities.Models;
using System.Net.Mail;
using System.Net;
#endregion

namespace eCollabro.BAL
{
    /// <summary>
    /// CommonManager
    /// </summary>
    public class CommonManager : BaseManager
    {
        #region Data Members

        private static readonly object _syncRoot = new object();

        #endregion

        #region Methods

        /// <summary>
        /// GetNextCode
        /// </summary>
        /// <returns></returns>
        public String GetNextCode(string EntityName)
        {
            string returnCode = string.Empty;
            try
            {
                CodeFormat codeFormat = eCollabroDbContext.Repository<CodeFormat>().Query().Get().Where(cf => cf.EntityName.Equals(EntityName)).FirstOrDefault();
                lock (codeFormat)
                {
                    codeFormat.CurrentSeed += 1;
                    eCollabroDbContext.Save();

                    int genCodeLength = (codeFormat.Prefix.Trim() + codeFormat.Seprator.Trim() + codeFormat.CurrentSeed.ToString() + codeFormat.Seprator.Trim() + codeFormat.Suffix.Trim()).Length;
                    int shortLength = codeFormat.Codelength.Value - genCodeLength;
                    string zero = string.Empty;
                    for (int i = 0; i < shortLength; i++)
                    {
                        zero = zero + "0";
                    }


                    returnCode = (!String.IsNullOrEmpty(codeFormat.Suffix)) ? (codeFormat.Prefix + codeFormat.Seprator + zero + codeFormat.CurrentSeed.ToString() + codeFormat.Seprator + codeFormat.Suffix) : (codeFormat.Prefix + codeFormat.Seprator + zero + codeFormat.CurrentSeed.ToString());
                }
            }
            catch (Exception ex)
            {
                if (!HandleError(ex))
                    throw;
            }
            return returnCode;
        }

        /// <summary>
        /// SetUserContext
        /// </summary>
        /// <param name="userContext"></param>
        public void SetUserContext(UserContext userContext,RequestContextParameter requestContextParameter)
        {
                if (userContext.LanguageId == 0)
                    userContext.LanguageId = eCollabroDbContext.Repository<lkpLanguage>().Query().Get().Where(op=>op.LanguageCode.Equals(userContext.Language)).FirstOrDefault().LanguageId;
                if (userContext.UserName != string.Empty)
                    userContext.UserId = eCollabroDbContext.Repository<UserMembership>().Query().Get().Where(op=>op.UserName.Equals(userContext.UserName)).FirstOrDefault().UserId;
                if (RequestContext.Current.Get<UserContext>("UserContext") == null)
                    RequestContext.Current.Add<UserContext>("UserContext",userContext);
                if (RequestContext.Current.Get<RequestContextParameter>("RequestParameter") == null)
                    RequestContext.Current.Add("RequestParameter", requestContextParameter);
        }

        /// <summary>
        /// SendEmail
        /// </summary>
        /// <param name="mailMessage"></param>
        public void SendEmail(MailMessage mailMessage)
        {
            try
            {
                SetupManager setupManager = new SetupManager();
                EmailConfiguration emailConfiguration = setupManager.GetEmailConfiguration();

                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = emailConfiguration.RequireCredentials;

                if (emailConfiguration.RequireCredentials)
                {
                    smtp.Credentials = new NetworkCredential(emailConfiguration.Username, emailConfiguration.Password);
                }

                smtp.Host = emailConfiguration.HostName;

                smtp.Port = emailConfiguration.PortNumber;

                smtp.EnableSsl = emailConfiguration.EnableSSL;

                mailMessage.From = new MailAddress(emailConfiguration.DefaultSenderEmail);
                Email email = new Email(smtp);
                email.SendEmail(mailMessage);
            }
            catch(Exception ex)
            {
                log.Error(ex.Message,ex);
            }
        }

        #endregion

    }
}
