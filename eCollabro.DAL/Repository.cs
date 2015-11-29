// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using eCollabro.BAL.Entities.Models;
using eCollabro.Common;
using System.Reflection;
using eCollabro.DAL.Interface;

#endregion

namespace eCollabro.DAL
{
    /// <summary>
    /// Repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly Guid _instanceId;
        private readonly DbContext _dbContext;
        private readonly List<PermissionEnum> _permissions;
        private bool _checkPermissions = false;
        public Repository(DbContext dbContext)
        {
            _dbContext=dbContext;
            _dbSet = dbContext.Set<TEntity>();
            _instanceId = Guid.NewGuid();
            _permissions = new List<PermissionEnum>();
        }

        public Guid InstanceId
        {
            get { return _instanceId; }
        }

        public void Dettach(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
        }

        public virtual TEntity Find(List<PermissionEnum> permissions, params object[] keyValues)
        {
            _permissions.Clear();
            permissions.ForEach(per => _permissions.Add(per));
            TEntity content=_dbSet.Find(keyValues);
            if (content == null)
                return null;

            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;

            PropertyInfo isActiveProperty = typeof(TEntity).GetProperty("IsActive", flags);

            PropertyInfo approvalStatus = typeof(TEntity).GetProperty("ApprovalStatus", flags);

            PropertyInfo isAnomynousAccess = typeof(TEntity).GetProperty("IsAnomynousAccess", flags);

            PropertyInfo isDeleted = typeof(TEntity).GetProperty("IsDeleted", flags);
            
            // ignore soft deleted records
            if(isDeleted!= null && (bool)isDeleted.GetValue(content))
                return null;

            //ignore inactive records as per permission
            if (!_permissions.Contains(PermissionEnum.ViewInactiveContent) &&  isActiveProperty!= null && !(bool)isActiveProperty.GetValue(content))
                return null;
            //ignore unapproved records as per permission
            if (!_permissions.Contains(PermissionEnum.ViewUnapprovedContent) && approvalStatus != null && !((string)approvalStatus.GetValue(content)).Equals(WorkflowConstants.ApprovedStatus))
                return null;
            //ignore records not having anomynous access
            if ((!_permissions.Contains(PermissionEnum.ViewContent) && _permissions.Contains(PermissionEnum.ViewAnomynousContent)) && isAnomynousAccess != null && !((bool)isAnomynousAccess.GetValue(content)).Equals(true))
                return null;
            
            return content;
        }
         

        public virtual TEntity Find(params object[] keyValues)
        {
            _checkPermissions = false;
            return _dbSet.Find(keyValues);
        }

        public virtual IQueryable<TEntity> SqlQuery(string query, params object[] parameters)
        {
            return _dbSet.SqlQuery(query, parameters).AsQueryable();
        }

        public virtual void Insert(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
        }


        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                Insert(entity);
        }

        public virtual void InsertGraph(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void InsertGraphRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
           
            _dbSet.Attach(entity);
        }

        public virtual void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public virtual IRepositoryQuery<TEntity> Query()
        {
            var repositoryGetFluentHelper = new RepositoryQuery<TEntity>(this);
            _checkPermissions = false;
            return repositoryGetFluentHelper;
        }

        public virtual IRepositoryQuery<TEntity> Query(List<PermissionEnum> permissions)
        {
            _permissions.Clear();
            _checkPermissions = true;
            permissions.ForEach(per=>_permissions.Add(per));
            var repositoryGetFluentHelper = new RepositoryQuery<TEntity>(this);
            return repositoryGetFluentHelper;
        }

        internal IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includeProperties = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includeProperties != null)
                includeProperties.ForEach(i => query = query.Include(i));
            if (filter != null)
                query = query.Where(filter);

            query = ApplyPermission(query);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);

            return query;
        }

        private IQueryable<TEntity> ApplyPermission(IQueryable<TEntity> contentQuery)
        {
            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            // remove deleted records
            if (typeof(TEntity).GetProperty("IsDeleted", flags) != null)
            {
                contentQuery = contentQuery.AddFilter("IsDeleted",false);
            }
            if (_checkPermissions)
            {
                //remove inactive records
                if (!_permissions.Contains(PermissionEnum.ViewInactiveContent) && typeof(TEntity).GetProperty("IsActive", flags) != null)
                    contentQuery = contentQuery.AddFilter("IsActive", true);
                //ignore unapproved records as per permission
                if (!_permissions.Contains(PermissionEnum.ViewUnapprovedContent) && typeof(TEntity).GetProperty("ApprovalStatus", flags) != null)
                    contentQuery = contentQuery.AddFilter("ApprovalStatus",WorkflowConstants.ApprovedStatus);

                //ignore not allowed anomynous records 
                if ((!_permissions.Contains(PermissionEnum.ViewContent) && _permissions.Contains(PermissionEnum.ViewAnomynousContent)) && typeof(TEntity).GetProperty("IsAnomynousAccess", flags) != null)
                    contentQuery = contentQuery.AddFilter("IsAnomynousAccess",true);
            
            }
            return contentQuery;

        }

        /// <summary>
        /// IsEntityAttachedToDB
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsEntityAttachedToDB(TEntity entity)
        {
            if (_dbContext.Entry(entity).State==EntityState.Detached)
                return false;
            else
                return true;
        }
        
    }
}
