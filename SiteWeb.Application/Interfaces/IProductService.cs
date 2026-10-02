using SiteWeb.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteWeb.Application.Interfaces
{
    public interface IProductService
    {
        Task<IReadOnlyList<ProductListDto>> GetAllAsync(bool activeOnly = false);
        Task<IReadOnlyList<ProductListDto>> GetFeaturedAsync(int count = 6);
        Task<IReadOnlyList<ProductListDto>> GetByCategorySlugAsync(string categorySlug);
        Task<IReadOnlyList<ProductListDto>> GetByCategoryIdAsync(int categoryId);
        Task<IReadOnlyList<ProductListDto>> SearchAsync(ProductFilterDto filter);
        Task<IReadOnlyList<ProductListDto>> GetRelatedAsync(int productId, int categoryId, int count = 4);
        Task<ProductDto?> GetByIdAsync(int id);
        Task<ProductDto?> GetBySlugAsync(string slug);
        Task<ProductDto?> GetByReferenceAsync(string reference);
        Task<ProductDto> CreateAsync(ProductCreateDto dto);
        Task<bool> UpdateAsync(ProductUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<int> GetTotalCountAsync();
        Task<bool> ReferenceExistsAsync(string reference, int? currentId = null);
    }

}
