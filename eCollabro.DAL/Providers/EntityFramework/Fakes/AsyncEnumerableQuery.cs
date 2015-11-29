using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace eCollabro.DAL.Providers.EntityFramework.Fakes
{
    public class AsyncEnumerableQuery<T> : EnumerableQuery<T>
    {
        public AsyncEnumerableQuery(IEnumerable<T> enumerable)
            : base(enumerable)
        {
        }

        public AsyncEnumerableQuery(Expression expression)
            : base(expression)
        {
        }

    }
}