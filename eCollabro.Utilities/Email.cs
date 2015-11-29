// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System.Net.Mail;

#endregion

namespace eCollabro.Utilities
{
    /// <summary>
    /// Email
    /// </summary>
    public class Email
    {
        
        private SmtpClient _smtpClient = null;

        /// <summary>
        /// Email
        /// </summary>
        /// <param name="smtpClient"></param>
        public Email(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        /// <summary>
        /// SendEmail
        /// </summary>
        /// <param name="message"></param>
        public void SendEmail(MailMessage message)
        {
            message.IsBodyHtml = true;
            _smtpClient.Send(message);
        }

        /// <summary>
        /// SendEmail
        /// </summary>
        /// <param name="from"></param>
        /// <param name="recepients"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public void SendEmail(string from, string recepients, string subject, string body)
        {
            _smtpClient.Send(from,recepients,subject,body); 
        }
    }
}
