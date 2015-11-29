// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;
using System.Data.Entity.Validation;
using System.Text;

#endregion

namespace eCollabro.Logger
{
    /// <summary>
    /// ExceptionLogger
    /// </summary>
    public class ExceptionLogger
    {
        #region Methods

        /// <summary>
        /// LogInfo
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="module"></param>
        public static void LogInfo(string logDetails)
        {
           Log log = Log.GetInstance();
           if (log.DebugEnabled)
           {
               log.WriteDebugLog("Debug : " + logDetails);
           }
         }

        /// <summary>
        /// LogError
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="module"></param>
        public static void LogError(Exception ex)
        {
            Log log = Log.GetInstance();
            string error = GetErrorMessage(ex);
            if (log.DebugEnabled)
            {
                LogInfo("Error : " + error);
                log.WriteExceptionLog("Error : " + ex.Message + " (Debug log Enabled, check debug log file for exception details.) ");
            }
            else
            {
                log.WriteExceptionLog("Error : " + error);
            }

        }

        /// <summary>
        /// GetErrorMessage
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private static string GetErrorMessage(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Exception Type: {0} : Message {1} ", ex.GetType().FullName, ex.Message);
            sb.AppendFormat(" : Source: {0}", ex.Source);
            sb.AppendFormat(" : Stacktrace: {0}", ex.StackTrace);
            if (ex.InnerException != null)
            {
                sb.AppendFormat(" : Inner Exception: Type: {0} : Message {1} ", ex.InnerException.GetType().FullName, ex.InnerException.Message);
            }

            DbEntityValidationException entityValidationEx = ex as DbEntityValidationException;

            if (entityValidationEx != null)
            {
                foreach (var validationErrors in entityValidationEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        sb.AppendFormat(" : Entity Exception: Property: {0} : Message {1} ", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

            }
            return sb.ToString();
        }

        #endregion 
    }
}