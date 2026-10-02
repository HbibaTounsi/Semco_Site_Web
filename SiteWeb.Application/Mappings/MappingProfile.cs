using SiteWeb.Application.DTOs.Category;
using SiteWeb.Application.DTOs.Contact;
using SiteWeb.Application.DTOs.Product;
using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using SiteWeb.Core.Entities;

namespace SiteWeb.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category Mappings
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.ProductCount, opt => opt.MapFrom(src => src.Products != null ? src.Products.Count(p => p.IsActive) : 0));
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();

            // Product Mappings
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty))
                .ForMember(dest => dest.CategorySlug, opt => opt.MapFrom(src => src.Category != null ? src.Category.Slug : string.Empty));

            CreateMap<Product, ProductListDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty))
                .ForMember(dest => dest.CategorySlug, opt => opt.MapFrom(src => src.Category != null ? src.Category.Slug : string.Empty));

            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();

            // Contact Mappings
            CreateMap<ContactFormDto, ContactMessage>()
                .ForMember(dest => dest.SentAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.IsRead, opt => opt.MapFrom(_ => false));

            CreateMap<ContactMessage, ContactMessageDto>();
        }
    }

}
