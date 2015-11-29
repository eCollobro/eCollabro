#region

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace eCollabro.DAL.Providers.EntityFramework
{
    public class DataContext : DbContext, IDataContext
    {
        private readonly Guid _instanceId;

        public DataContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            _instanceId = Guid.NewGuid();
            Configuration.LazyLoadingEnabled = false;
        }

        public Guid InstanceId
        {
            get { return _instanceId; }
        }

        public new DbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public override int SaveChanges()
        {
            var changes = base.SaveChanges();
            return changes;
        }
    }
}