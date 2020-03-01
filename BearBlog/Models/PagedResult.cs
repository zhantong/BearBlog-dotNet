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
        public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize)
        {
            var result = new PagedResult<T>
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
                .ToList();
            return result;
        }
    }
}