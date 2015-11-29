// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.DAL.Interface;
using System.Data.Entity;

#endregion

namespace eCollabro.DAL
{
    /// <summary>
    /// ExtendedRepository
    /// </summary>
    public partial class ExtendedRepository:IExtendedRepository
    {
        private readonly DbContext _dbContext;
        public ExtendedRepository(DbContext dbContext)
        {
            _dbContext=dbContext;
        }
    }
}
