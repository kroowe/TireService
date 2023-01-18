using Microsoft.EntityFrameworkCore;
using TireService.Infrastructure;
using TireService.Infrastructure.Entities.Settings;

namespace TireService.Core.Domain.Services;

public class CategoryService : ItemBasicService<Category>
{
    public CategoryService(PostgresContext dbContext, CancellationTokenSource cancellationTokenSource) : base(dbContext, cancellationTokenSource)
    {
    }

    public async Task<IReadOnlyCollection<Category>> GetAllCategoriesByParentCategory(LTree parentCategoryPath)
    {
        return await _dbSet.Where(x => x.CategoryPath.IsDescendantOf(parentCategoryPath)).Include(x => x.CatalogItems)
            .ToListAsync(_token);
    }
}