using Dale.Utils;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace Dale.Repository
{
    public static class IQueryableExtensions
    {
        static public IConfiguration Configuration { set; get; }

        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query,
                Dictionary<string, Expression<Func<T, object>>> columnsMap, FilterPaginate filter)
        {
            if (string.IsNullOrWhiteSpace(filter.SortBy) || !columnsMap.ContainsKey(filter.SortBy))
            {
                var orderBy = columnsMap.Keys.First();
                return query.OrderByDescending(columnsMap[orderBy]);
            }

            return (filter.IsSortAscending) ?
                    query.OrderBy(columnsMap[filter.SortBy]) :
                    query.OrderByDescending(columnsMap[filter.SortBy]);
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, FilterPaginate filter)
        {
            if (filter.Page <= 0)
            {
                filter.Page = 1;
            }

            if (filter.PageSize <= 0)
            {
                filter.PageSize = int.Parse(Configuration.GetSection("Filter:PageSize").Value);
            }

            if (query.Count() <= filter.PageSize)
            {
                filter.Page = 1;
            }

            return query.Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize);
        }

        // var result = await userManager.Users.SelectWithMethodAsync(async s => await GetUserVM(s));
        public static async Task<IEnumerable<TResult>> SelectWithMethodAsync<TSource, TResult>(
                this IEnumerable<TSource> source, Func<TSource, Task<TResult>> method)
        {
            return await Task.WhenAll(source.Select(async s => await method(s)));
        }
    }
}
