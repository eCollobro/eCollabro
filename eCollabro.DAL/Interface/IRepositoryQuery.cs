// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.BAL.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#endregion

namespace eCollabro.DAL.Interface
{
    /// <summary>
    /// IRepositoryQuery
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepositoryQuery<TEntity> where TEntity : class
    {
        RepositoryQuery<TEntity> Filter(Expression<Func<TEntity, bool>> filter);
        RepositoryQuery<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        RepositoryQuery<TEntity> Include(Expression<Func<TEntity, object>> expression);
        IEnumerable<TEntity> GetPage(int page, int pageSize, out int totalCount);
        IQueryable<TEntity> Get();
        IQueryable<TEntity> SqlQuery(string query, params object[] parameters);
    }
}