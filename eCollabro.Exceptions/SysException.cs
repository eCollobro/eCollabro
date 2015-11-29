// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;

#endregion

namespace eCollabro.Exceptions
{
    /// <summary>
    /// SysException : custom exceptions 
    /// </summary>
    public class SysException : Exception
    {
        #region Constructor

        /// <summary>
        /// AppException
        /// </summary>
        public SysException()
        {
        }

        /// <summary>
        /// AppException
        /// </summary>
        /// <param name="message"></param>
        public SysException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// AppException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public SysException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion
    }
}
