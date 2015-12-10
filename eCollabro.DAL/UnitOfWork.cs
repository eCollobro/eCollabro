// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;
using System.Collections;
using System.Data.Entity;
using eCollabro.BAL.Entities.Models;
using eCollabro.DAL.Interface;
using System.Data.SqlClient;
using System.Collections.Generic;

#endregion

namespace eCollabro.DAL
{
    /// <summary>
    /// UnitOfWork
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Fields

        private readonly DbContext _context;
        private readonly Guid _instanceId;
        private bool _disposed;
        private Hashtable _repositories;

        #endregion Private Fields

        #region Constuctor/Dispose

        public UnitOfWork(DbConnectionParameter dbConnectionParameter)
        {
            if (dbConnectionParameter == DbConnectionParameter.eCollabro)
                _context = new eCollabroDbModel();
            //else if (dbConnectionParameter == DbConnectionParameter.ADP)
            //    _context = new ADPModel();
            _context.Configuration.LazyLoadingEnabled = false;
            _instanceId = Guid.NewGuid();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        #endregion Constuctor/Dispose

        public Guid InstanceId
        {
            get { return _instanceId; }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// ExecuteScripts
        /// </summary>
        /// <param name="scripts"></param>
        public void ExecuteScripts(List<string> scripts)
        {

            using (SqlConnection connection = new SqlConnection(_context.Database.Connection.ConnectionString))
            {
                SqlCommand cm = new SqlCommand();
                cm.Connection = connection;
                connection.Open();
                foreach (string script in scripts)
                {
                    cm.CommandText = script;
                    cm.ExecuteNonQuery();
                }
                connection.Close();

            }
        }
        public IExtendedRepository ExtendedRepository()
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(IExtendedRepository).Name;

            if (_repositories.ContainsKey(type))
            {
                return (IExtendedRepository)_repositories[type];
            }
            _repositories.Add(type, new ExtendedRepository(_context));
            return (IExtendedRepository)_repositories[type];
        }
        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type))
            {
                return (IRepository<TEntity>)_repositories[type];
            }
            var repositoryType = typeof(Repository<>);
            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), this._context));

            return (IRepository<TEntity>)_repositories[type];
        }
    }
}
