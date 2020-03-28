using System;
using System.Collections.Generic;
using System.Linq;

namespace BearBlog.Models
{
    public class PagedResult<T>
    {
        public PageContext PageContext { get; set; }

        public IList<T> Items { get; set; }
    }

    public static class PagedResultExtension
    {
        public static PagedResult<TResult> GetPaged<T, TResult>(this IQueryable<T> query, int page, int pageSize, Func<T, TResult> selectFunc)
        {
            var result = new PagedResult<TResult>
            {
                PageContext = new PageContext
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    PageCount = (int) Math.Ceiling((double) query.Count() / pageSize)
                }
            };
            var skip = (page - 1) * pageSize;
            result.Items = query.Skip(skip)
                .Take(pageSize)
                .AsEnumerable()
                .Select(selectFunc)
                .ToList();
            return result;
        }
    }
}