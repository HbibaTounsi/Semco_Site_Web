using Microsoft.EntityFrameworkCore;
using SiteWeb.Core.Entities;
using SiteWeb.Core.Interfaces;
using SiteWeb.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteWeb.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Category?> GetBySlugAsync(string slug)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Slug == slug);
        }

        public async Task<Category?> GetWithProductsAsync(int id)
        {
            return await _dbSet
                .Include(c => c.Products.Where(p => p.IsActive))
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> GetWithProductsBySlugAsync(string slug)
        {
            return await _dbSet
                .Include(c => c.Products.Where(p => p.IsActive))
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Slug == slug);
        }

        public async Task<IReadOnlyList<Category>> GetAllActiveWithProductCountAsync()
        {
            return await _dbSet
                .Where(c => c.IsActive)
                .OrderBy(c => c.DisplayOrder)
                .ThenBy(c => c.Name)
                .Include(c => c.Products)
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task<IReadOnlyList<Category>> GetAllAsync()
        {
            return await _dbSet
                .OrderBy(c => c.DisplayOrder)
                .ThenBy(c => c.Name)
                .Include(c => c.Products)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
