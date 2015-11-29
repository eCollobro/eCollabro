// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System.Collections.Generic;
using eCollabro.BAL.Entities.Models;
using System.Linq;

#endregion 

namespace eCollabro.DAL.Interface
{
    /// <summary>
    /// ICommonRepository
    /// </summary>
    public interface ICommonRepository
    {
        /// <summary>
        /// GetCodeFormats
        /// </summary>
        /// <returns></returns>
        IQueryable<CodeFormat> GetCodeFormats();

        /// <summary>
        /// GetLanguageId
        /// </summary>
        /// <param name="languageCode"></param>
        /// <returns></returns>
        int GetLanguageId(string languageCode);
        
        /// <summary>
        /// GetUserId
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        int GetUserId(string username);
    }
}
