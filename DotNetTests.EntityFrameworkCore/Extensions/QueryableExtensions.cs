using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.EntityFrameworkCore.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TSource> IncludeIf<TSource>([NotNull] this IQueryable<TSource> query, bool condition,
            Func<IQueryable<TSource>, IQueryable<TSource>> invoker) where TSource : class => condition ? invoker.Invoke(query) : query;

        public static IQueryable<TSource> WhereIf<TSource>([NotNull] this IQueryable<TSource> query, bool condition,
            Expression<Func<TSource, bool>> whereExpression) where TSource : class =>
            condition ? query.Where(whereExpression) : query;

        public static IQueryable<TSource> OrderIf<TSource>([NotNull] this IQueryable<TSource> query, bool condition,
            Expression<Func<TSource, DateTime>> orderExpression) where TSource : class =>
            condition ? query.OrderBy(orderExpression) : query;

        public static IQueryable<TSource> OrderDescendingIf<TSource>([NotNull] this IQueryable<TSource> query, bool condition,
            Expression<Func<TSource, DateTime>> orderExpression) where TSource : class =>
            condition ? query.OrderByDescending(orderExpression) : query;

        public static IQueryable<TSource> OrderIf<TSource, TKey>([NotNull] this IQueryable<TSource> query, bool condition,
            Expression<Func<TSource, TKey>> orderExpression) where TSource : class =>
            condition ? query.OrderBy(orderExpression) : query;

        public static IQueryable<TSource> OrderDescendingIf<TSource, TKey>([NotNull] this IQueryable<TSource> query, bool condition,
            Expression<Func<TSource, TKey>> orderExpression) where TSource : class =>
            condition ? query.OrderByDescending(orderExpression) : query;

        public static IQueryable<TSource> SkipIf<TSource>([NotNull] this IQueryable<TSource> query, bool condition,
            int skip) where TSource : class => condition ? query.Skip(skip) : query;

        public static IQueryable<TSource> TakeIf<TSource>([NotNull] this IQueryable<TSource> query, bool condition,
            int skip) where TSource : class => condition ? query.Take(skip) : query;
    }
}
