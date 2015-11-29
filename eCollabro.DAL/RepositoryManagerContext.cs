#region References
using eCollabro.BAL.Entities;
using eCollabro.BAL.Entities.Models;
using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using System.Web;
#endregion

namespace eCollabro.DAL
{
    /// <summary>
    /// RepositoryManagerContext
    /// </summary>
    public class RepositoryManagerContext : IExtension<OperationContext>,IDisposable
    {

        #region Data Member
        
        private DbContext _dbContext;
        private Hashtable _repositories;
        private UserContext _userContext;
        private bool _disposed;

        // to be used for single threaded application - Windows Application
        private static RepositoryManagerContext _repositoryManagerContext;

        #endregion 

        #region Methods

        /// <summary>
        /// DataBaseContext
        /// </summary>
        internal DbContext DataBaseContext 
        { 
            get { 
            
                if(_dbContext==null)
                {
                    _dbContext = new eCollabroEntities();
                }
                return _dbContext;
            } 
        
        }

        /// <summary>
        /// UserContextDetails
        /// </summary>
        public UserContext UserContextDetails 
        { 
            get { return _userContext; }
            set { _userContext = value; } 
        }


        /// <summary>
        /// GetInstance
        /// </summary>
        /// <returns></returns>
        public static RepositoryManagerContext Current
        {
            get
            {
                RepositoryManagerContext repositoryManagerContext = null;
                if (OperationContext.Current != null) // WCF call
                {
                    repositoryManagerContext = OperationContext.Current.Extensions.Find<RepositoryManagerContext>();
                    if (repositoryManagerContext == null)
                    {
                        repositoryManagerContext = new RepositoryManagerContext();
                        OperationContext.Current.Extensions.Add(repositoryManagerContext);
                    }
                }
                else if (HttpContext.Current != null) // Web Call
                {
                    repositoryManagerContext = HttpContext.Current.Items["RepositoryManagerContext"] as RepositoryManagerContext;
                    if (repositoryManagerContext == null)
                    {
                        repositoryManagerContext = new RepositoryManagerContext();
                        HttpContext.Current.Items.Add("RepositoryManagerContext", repositoryManagerContext);
                    }
                }
                else // Single thread Application Call
                {
                    if (_repositoryManagerContext == null)
                    {
                        _repositoryManagerContext = new RepositoryManagerContext();
                    }
                    repositoryManagerContext = _repositoryManagerContext;
                }
                return repositoryManagerContext;
            }
        }

        #endregion

        #region DisposeDbContext

        /// <summary>
        /// DisposeDbContext
        /// </summary>
        public void DisposeDbContext()
        {
            _dbContext.Dispose();
            _dbContext = null;
        }

        #endregion

        #region Base Methods

        public void Attach(OperationContext owner)
        {
            // do nothing
        }

        public void Detach(OperationContext owner)
        {
            // do nothing
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
                if(_dbContext!=null)
                 _dbContext.Dispose();
            }
            _disposed = true;
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
            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity))));

            return (IRepository<TEntity>)_repositories[type];
        }

        #endregion 
    }
}
