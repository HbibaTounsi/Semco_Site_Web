using SiteWeb.Application.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteWeb.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IReadOnlyList<CategoryDto>> GetAllActiveAsync();
        Task<IReadOnlyList<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(int id);
        Task<CategoryDto?> GetBySlugAsync(string slug);
        Task<CategoryDto> CreateAsync(CategoryCreateDto dto);
        Task<bool> UpdateAsync(CategoryUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> SlugExistsAsync(string slug, int? currentId = null);
    }
}
