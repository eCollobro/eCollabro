// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.Service.DataContracts;
using System;
using eCollabro.Service.ServiceContracts;

#endregion

namespace eCollabro.Service
{
    /// <summary>
    /// SetupService
    /// </summary>
    public class LoggerService : BaseService, ILoggerService
    {
        #region Methods

        /// <summary>
        /// LogError
        /// </summary>
        /// <param name="logException"></param>
        /// <returns></returns>
        public ServiceResponse LogError(string logException)
        {
            ServiceResponse logExceptionResponse = new ServiceResponse();
            try
            {
                log.Error(logException);
            }
            catch (Exception ex)
            {
                HandleError(ex, logExceptionResponse); // Can't Log exception
            }

            return logExceptionResponse;

        }

        #endregion
    }
}
