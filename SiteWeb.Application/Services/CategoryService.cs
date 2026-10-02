using AutoMapper;
using SiteWeb.Application.Common;
using SiteWeb.Application.DTOs.Category;
using SiteWeb.Application.Interfaces;
using SiteWeb.Core.Entities;
using SiteWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteWeb.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CategoryDto>> GetAllActiveAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllActiveWithProductCountAsync();
            return _mapper.Map<IReadOnlyList<CategoryDto>>(categories);
        }

        public async Task<IReadOnlyList<CategoryDto>> GetAllAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            return _mapper.Map<IReadOnlyList<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetWithProductsAsync(id);
            return category == null ? null : _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto?> GetBySlugAsync(string slug)
        {
            var category = await _unitOfWork.Categories.GetWithProductsBySlugAsync(slug);
            return category == null ? null : _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateAsync(CategoryCreateDto dto)
        {
            var category = _mapper.Map<Category>(dto);

            // Generate slug
            string baseSlug = SlugHelper.GenerateSlug(dto.Name);
            string slug = baseSlug;
            int counter = 1;
            while (await _unitOfWork.Categories.ExistsAsync(c => c.Slug == slug))
            {
                slug = $"{baseSlug}-{counter++}";
            }
            category.Slug = slug;
            category.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> UpdateAsync(CategoryUpdateDto dto)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(dto.Id);
            if (category == null) return false;

            // If name changed, update slug
            if (!string.Equals(category.Name, dto.Name, StringComparison.OrdinalIgnoreCase))
            {
                string baseSlug = SlugHelper.GenerateSlug(dto.Name);
                string slug = baseSlug;
                int counter = 1;
                while (await _unitOfWork.Categories.ExistsAsync(c => c.Slug == slug && c.Id != dto.Id))
                {
                    slug = $"{baseSlug}-{counter++}";
                }
                category.Slug = slug;
            }

            category.Name = dto.Name;
            category.Description = dto.Description;
            if (!string.IsNullOrEmpty(dto.ImageUrl))
            {
                category.ImageUrl = dto.ImageUrl;
            }
            category.DisplayOrder = dto.DisplayOrder;
            category.IsActive = dto.IsActive;
            category.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetWithProductsAsync(id);
            if (category == null) return false;

            // Prevent deletion if category still has products
            if (category.Products != null && category.Products.Any(p => p.IsActive))
            {
                return false;
            }

            await _unitOfWork.Categories.DeleteAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SlugExistsAsync(string slug, int? currentId = null)
        {
            if (currentId.HasValue)
            {
                return await _unitOfWork.Categories.ExistsAsync(c => c.Slug == slug && c.Id != currentId.Value);
            }
            return await _unitOfWork.Categories.ExistsAsync(c => c.Slug == slug);
        }
    }
}
