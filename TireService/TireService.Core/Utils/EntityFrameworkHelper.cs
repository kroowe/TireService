using Microsoft.EntityFrameworkCore;

namespace TireService.Core.Utils
{
    public static class EntityFrameworkHelper
    {
        public static IQueryable<TItem> SetEfIncludeString<TItem>(IQueryable<TItem> queryable, string include) where TItem : class
        {
            if (!string.IsNullOrWhiteSpace(include))
            {
                foreach (var token in GetEfIncludeFromRow(include)) queryable = queryable.Include(token);
            }

            return queryable;
        }

        public static IReadOnlyCollection<string> GetEfIncludeFromRow(string include)
        {
            if (string.IsNullOrWhiteSpace(include)) return Array.Empty<string>();
            include = include.Replace(" ", "");
            return include.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
