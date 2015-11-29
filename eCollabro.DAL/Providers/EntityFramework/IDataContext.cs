#region

using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace eCollabro.DAL.Providers.EntityFramework
{
    public interface IDataContext : IDisposable
    {
        Guid InstanceId { get; }
        DbSet<T> Set<T>() where T : class;
        int SaveChanges();
    }
}