// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References
using eCollabro.DAL;
using System.Collections.Generic;
using eCollabro.BAL.Entities.Models;
using eCollabro.BAL;
using System.Linq;
#endregion

namespace eCollabro.BAL
{
    /// <summary>
    /// 
    /// </summary>
    public class LookupManager:BaseManager
    {
        #region Methods 

        /// <summary>
        /// GetLanguages
        /// </summary>
        /// <returns></returns>
        public List<lkpLanguage> GetLanguages()
        {
            return eCollabroDbContext.Repository<lkpLanguage>().Query().Get().ToList();
        }
        
        /// <summary>
        /// Get Navigation Types
        /// </summary>
        /// <returns></returns>
        public List<lkpNavigationType> GetNavigationTypes()
        {
            return eCollabroDbContext.Repository<lkpNavigationType>().Query().Get().ToList();
        }


        #endregion 
    }
}
