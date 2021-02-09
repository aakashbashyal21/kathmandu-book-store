using BookStore.Application.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Common.Mappings
{
    public static class MappingExtensions
    {
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
        {
            return PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);
        }



        public static PaginatedList<TDestination> PageList<TDestination>(this IEnumerable<TDestination> queryable, int pageNumber, int pageSize)
        {
            return PaginatedList<TDestination>.Create(queryable, pageNumber, pageSize);
        }

    }
}
