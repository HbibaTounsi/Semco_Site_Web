using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteWeb.Application.DTOs.Product
{
    public class ProductListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string? Dimensions { get; set; }
        public string? Color { get; set; }
        public bool IsFeatured { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategorySlug { get; set; } = string.Empty;
    }
}
