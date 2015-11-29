// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System.Linq;
using eCollabro.BAL.Entities.Models;
using System.Data.Entity;
using eCollabro.DAL.Interface;

#endregion

namespace eCollabro.DAL
{
    #region Extended Repository Extension for Common Repository

    /// <summary>
    /// ExtendedRepository
    /// </summary>
    public partial class ExtendedRepository : IExtendedRepository
    {
        private ICommonRepository _commonRepository;

        public ICommonRepository CommonRepository
        {
            get
            {
                if (_commonRepository == null)
                {
                    _commonRepository = new CommonRepository(this._dbContext);
                }
                return _commonRepository;
            }
        }
    }

    #endregion 

    /// <summary>
    /// CommonRepository
    /// </summary>
    public class CommonRepository : ICommonRepository
    {
        private eCollabroDbModel _dbContext = null;
        public CommonRepository(DbContext dbContext)
        {
            _dbContext = dbContext as eCollabroDbModel;
        }

        #region Methods

        /// <summary>
        /// GetCodeFormats
        /// </summary>
        /// <returns></returns>
        public IQueryable<CodeFormat> GetCodeFormats()
        {
            return _dbContext.CodeFormats;
        }


        /// <summary>
        /// GetLanguageId
        /// </summary>
        /// <param name="Language"></param>
        /// <returns></returns>
        public int GetLanguageId(string languageCode)
        {
            int langId = 0;
            if (languageCode != "" || languageCode != null)
            {
                langId = _dbContext.lkpLanguages.Where(a => a.LanguageCode == languageCode).FirstOrDefault().LanguageId;
            }
            return langId;
        }

        /// <summary>
        /// GetUserId
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int GetUserId(string userName)
        {
            int userId = 0;
            if (userName != "" || userName != null)
            {
                userId = _dbContext.UserMemberships.Where(a => a.UserName == userName).FirstOrDefault().UserId;
            }
            return userId;
        }

        #endregion
    }
}
