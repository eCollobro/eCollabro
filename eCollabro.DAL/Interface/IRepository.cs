// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.BAL.Entities.Models;
using eCollabro.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#endregion 
namespace eCollabro.DAL.Interface
{
    /// <summary>
    /// IRepository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IRepository<TEntity> where TEntity : class
    {
        Guid InstanceId { get; }
        TEntity Find(List<PermissionEnum> permissions, params object[] keyValues);
        TEntity Find(params object[] keyValues);
        IQueryable<TEntity> SqlQuery(string query, params object[] parameters);
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void InsertGraph(TEntity entity);
        void InsertGraphRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);
        bool IsEntityAttachedToDB(TEntity entity);
        void Dettach(TEntity entity);
        IRepositoryQuery<TEntity> Query();
        IRepositoryQuery<TEntity> Query(List<PermissionEnum> permissions);
    }

}