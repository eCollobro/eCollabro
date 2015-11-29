// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;
using System.Linq;
using System.Linq.Expressions;

#endregion 
namespace eCollabro.DAL
{
    /// <summary>
    /// IEnumerableExtensions
    /// </summary>
    public static class IEnumerableExtensions
    {
        private static IOrderedQueryable<T> OrderingHelper<T>(IQueryable<T> source, string propertyName, bool descending, bool anotherLevel)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), string.Empty); // I don't care about some naming
            MemberExpression property = Expression.PropertyOrField(param, propertyName);
            LambdaExpression sort = Expression.Lambda(property, param);

            MethodCallExpression call = Expression.Call(
                typeof(Queryable),
                (!anotherLevel ? "OrderBy" : "ThenBy") + (descending ? "Descending" : string.Empty),
                new[] { typeof(T), property.Type },
                source.Expression,
                Expression.Quote(sort));

            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        }


        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        {
            return OrderingHelper(source, propertyName, false, false);
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            return OrderingHelper(source, propertyName, true, false);
        }

        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string propertyName)
        {
            return OrderingHelper(source, propertyName, false, true);
        }

        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string propertyName)
        {
            return OrderingHelper(source, propertyName, true, true);
        }

        public static IQueryable<T> AddFilter<T, V>(this IQueryable<T> queryable, string propertyName, V propertyValue)
        {
            ParameterExpression pe = Expression.Parameter(typeof(T), "p");
            IQueryable<T> x = queryable.Where<T>(Expression.Lambda<Func<T, bool>>(
                Expression.Equal(Expression.Property(pe, typeof(T).GetProperty(propertyName)),
                    Expression.Constant(propertyValue, typeof(V)), false, typeof(T).GetMethod("op_Equality")), new ParameterExpression[] { pe })
                );

            return (x);
        }

    }
}
