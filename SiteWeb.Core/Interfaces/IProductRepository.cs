using SiteWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteWeb.Core.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product?> GetBySlugAsync(string slug);
        Task<Product?> GetByReferenceAsync(string reference);
        Task<Product?> GetWithCategoryByIdAsync(int id);
        Task<Product?> GetWithCategoryBySlugAsync(string slug);
        Task<IReadOnlyList<Product>> GetByCategoryIdAsync(int categoryId);
        Task<IReadOnlyList<Product>> GetByCategorySlugAsync(string categorySlug);
        Task<IReadOnlyList<Product>> GetFeaturedProductsAsync(int count = 6);
        Task<IReadOnlyList<Product>> SearchAsync(string? searchTerm, int? categoryId = null, bool activeOnly = true);
        Task<IReadOnlyList<Product>> GetRelatedProductsAsync(int productId, int categoryId, int count = 4);
    }
}
