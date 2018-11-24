using Common;
using System.Collections.Generic;
using System.Linq;

namespace WebApiServer.Helpers
{
    public static class Paging
    {
        public static IEnumerable<T> GetPaged<T>(IEnumerable<T> items, FilterCriteria criteria)
            where T: IName
        {
            if (criteria != null && criteria.ItemsPerPage > 0)
            {
                if (!string.IsNullOrEmpty(criteria.Name))
                {
                    items = items
                        .Where(cr => cr.Name.Contains(criteria.Name,System.StringComparison.InvariantCultureIgnoreCase));
                }

                items = items
                    .Skip(criteria.Page * criteria.ItemsPerPage)
                    .Take(criteria.ItemsPerPage);
            }

            return items;
        }
    }
}
