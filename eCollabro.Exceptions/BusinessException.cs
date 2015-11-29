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
    /// BusinessException : custom exceptions 
    /// </summary>
    public class BusinessException : Exception
    {
        #region Property

        public string Code{get;set;}

        public bool Overidable { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// AppException
        /// </summary>
        public BusinessException()
        {
        }


        /// <summary>
        /// AppException
        /// </summary>
        /// <param name="message"></param>
        public BusinessException(string message, string code,bool overidable=false)
            : base(message)
        {
            this.Code = code;
            this.Overidable = overidable;
        }

        /// <summary>
        /// AppException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public BusinessException(string message, System.Exception innerException, string code)
            : base(message, innerException)
        {
            this.Code = code;
        }

        #endregion
    }
}
