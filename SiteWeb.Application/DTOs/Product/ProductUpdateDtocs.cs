using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteWeb.Application.DTOs.Product
{
    public class ProductUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string? Dimensions { get; set; }
        public string? Color { get; set; }
        public string? Material { get; set; }
        public string? Specifications { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
    }

}
