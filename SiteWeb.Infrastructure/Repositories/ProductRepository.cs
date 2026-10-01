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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Product?> GetBySlugAsync(string slug)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Slug == slug);
        }

        public async Task<Product?> GetByReferenceAsync(string reference)
        {
            return await _dbSet
                .Include(p => p.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Reference == reference);
        }

        public async Task<Product?> GetWithCategoryByIdAsync(int id)
        {
            return await _dbSet
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product?> GetWithCategoryBySlugAsync(string slug)
        {
            return await _dbSet
                .Include(p => p.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Slug == slug);
        }

        public async Task<IReadOnlyList<Product>> GetByCategoryIdAsync(int categoryId)
        {
            return await _dbSet
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId && p.IsActive)
                .OrderByDescending(p => p.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Product>> GetByCategorySlugAsync(string categorySlug)
        {
            return await _dbSet
                .Include(p => p.Category)
                .Where(p => p.Category.Slug == categorySlug && p.IsActive)
                .OrderByDescending(p => p.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Product>> GetFeaturedProductsAsync(int count = 6)
        {
            return await _dbSet
                .Include(p => p.Category)
                .Where(p => p.IsFeatured && p.IsActive)
                .OrderByDescending(p => p.CreatedAt)
                .Take(count)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Product>> SearchAsync(string? searchTerm, int? categoryId = null, bool activeOnly = true)
        {
            var query = _dbSet.Include(p => p.Category).AsQueryable();

            if (activeOnly)
            {
                query = query.Where(p => p.IsActive);
            }

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string term = searchTerm.Trim().ToLower();
                query = query.Where(p =>
                    p.Name.ToLower().Contains(term) ||
                    p.Reference.ToLower().Contains(term) ||
                    p.ShortDescription.ToLower().Contains(term) ||
                    (p.Specifications != null && p.Specifications.ToLower().Contains(term)) ||
                    (p.Material != null && p.Material.ToLower().Contains(term)));
            }

            return await query.OrderByDescending(p => p.CreatedAt).AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<Product>> GetRelatedProductsAsync(int productId, int categoryId, int count = 4)
        {
            return await _dbSet
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId && p.Id != productId && p.IsActive)
                .OrderByDescending(p => p.IsFeatured)
                .ThenByDescending(p => p.CreatedAt)
                .Take(count)
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return await _dbSet
                .Include(p => p.Category)
                .OrderByDescending(p => p.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
        }
    }

    
}
