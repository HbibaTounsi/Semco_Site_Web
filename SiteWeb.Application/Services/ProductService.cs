using AutoMapper;
using SiteWeb.Application.Common;
using SiteWeb.Application.DTOs.Product;
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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ProductListDto>> GetAllAsync(bool activeOnly = false)
        {
            IReadOnlyList<Product> products;
            if (activeOnly)
            {
                products = await _unitOfWork.Products.FindAsync(p => p.IsActive);
            }
            else
            {
                products = await _unitOfWork.Products.GetAllAsync();
            }

            return _mapper.Map<IReadOnlyList<ProductListDto>>(products);
        }

        public async Task<IReadOnlyList<ProductListDto>> GetFeaturedAsync(int count = 6)
        {
            var products = await _unitOfWork.Products.GetFeaturedProductsAsync(count);
            return _mapper.Map<IReadOnlyList<ProductListDto>>(products);
        }

        public async Task<IReadOnlyList<ProductListDto>> GetByCategorySlugAsync(string categorySlug)
        {
            var products = await _unitOfWork.Products.GetByCategorySlugAsync(categorySlug);
            return _mapper.Map<IReadOnlyList<ProductListDto>>(products);
        }

        public async Task<IReadOnlyList<ProductListDto>> GetByCategoryIdAsync(int categoryId)
        {
            var products = await _unitOfWork.Products.GetByCategoryIdAsync(categoryId);
            return _mapper.Map<IReadOnlyList<ProductListDto>>(products);
        }

        public async Task<IReadOnlyList<ProductListDto>> SearchAsync(ProductFilterDto filter)
        {
            var products = await _unitOfWork.Products.SearchAsync(
                filter.SearchTerm,
                filter.CategoryId,
                activeOnly: true);

            // Sort
            IEnumerable<Product> query = products;
            query = filter.SortBy switch
            {
                "name_desc" => query.OrderByDescending(p => p.Name),
                "date_desc" => query.OrderByDescending(p => p.CreatedAt),
                _ => query.OrderBy(p => p.Name)
            };

            return _mapper.Map<IReadOnlyList<ProductListDto>>(query.ToList());
        }

        public async Task<IReadOnlyList<ProductListDto>> GetRelatedAsync(int productId, int categoryId, int count = 4)
        {
            var products = await _unitOfWork.Products.GetRelatedProductsAsync(productId, categoryId, count);
            return _mapper.Map<IReadOnlyList<ProductListDto>>(products);
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetWithCategoryByIdAsync(id);
            return product == null ? null : _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto?> GetBySlugAsync(string slug)
        {
            var product = await _unitOfWork.Products.GetWithCategoryBySlugAsync(slug);
            return product == null ? null : _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto?> GetByReferenceAsync(string reference)
        {
            var product = await _unitOfWork.Products.GetByReferenceAsync(reference);
            return product == null ? null : _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateAsync(ProductCreateDto dto)
        {
            var product = _mapper.Map<Product>(dto);

            // Generate Slug
            string baseSlug = SlugHelper.GenerateSlug(dto.Name);
            string slug = baseSlug;
            int counter = 1;
            while (await _unitOfWork.Products.ExistsAsync(p => p.Slug == slug))
            {
                slug = $"{baseSlug}-{counter++}";
            }
            product.Slug = slug;
            product.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            var created = await _unitOfWork.Products.GetWithCategoryByIdAsync(product.Id);
            return _mapper.Map<ProductDto>(created ?? product);
        }

        public async Task<bool> UpdateAsync(ProductUpdateDto dto)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(dto.Id);
            if (product == null) return false;

            // If name changed, update slug
            if (!string.Equals(product.Name, dto.Name, StringComparison.OrdinalIgnoreCase))
            {
                string baseSlug = SlugHelper.GenerateSlug(dto.Name);
                string slug = baseSlug;
                int counter = 1;
                while (await _unitOfWork.Products.ExistsAsync(p => p.Slug == slug && p.Id != dto.Id))
                {
                    slug = $"{baseSlug}-{counter++}";
                }
                product.Slug = slug;
            }

            product.Name = dto.Name;
            product.Reference = dto.Reference;
            product.ShortDescription = dto.ShortDescription;
            product.Description = dto.Description;
            if (!string.IsNullOrEmpty(dto.ImageUrl))
            {
                product.ImageUrl = dto.ImageUrl;
            }
            product.Dimensions = dto.Dimensions;
            product.Color = dto.Color;
            product.Material = dto.Material;
            product.Specifications = dto.Specifications;
            product.IsFeatured = dto.IsFeatured;
            product.IsActive = dto.IsActive;
            product.CategoryId = dto.CategoryId;
            product.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null) return false;

            await _unitOfWork.Products.DeleteAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _unitOfWork.Products.CountAsync(p => p.IsActive);
        }

        public async Task<bool> ReferenceExistsAsync(string reference, int? currentId = null)
        {
            if (currentId.HasValue)
            {
                return await _unitOfWork.Products.ExistsAsync(p => p.Reference == reference && p.Id != currentId.Value);
            }
            return await _unitOfWork.Products.ExistsAsync(p => p.Reference == reference);
        }
    }

}
